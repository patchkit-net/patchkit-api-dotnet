using System;
using System.Net;
using JetBrains.Annotations;
using PatchKit.Core;
using PatchKit.Core.Cancellation;
using PatchKit.Core.Collections.Immutable;
using PatchKit.Logging;
using PatchKit.Network;

namespace PatchKit.Api
{
    public class BaseApiConnection : IBaseApiConnection
    {
        private enum ServerType
        {
            MainServer,
            CacheServer
        }

        private readonly ApiConnectionSettings _settings;

        [NotNull] private readonly IHttpClient _httpClient;
        [NotNull] private readonly IApiCache _apiCache;
        [NotNull] private readonly ILogger _logger;

        public BaseApiConnection(ApiConnectionSettings settings,
            [NotNull] IHttpClient httpClient,
            [NotNull] IApiCache apiCache,
            [NotNull] ILogger logger)
        {
            settings.ThrowArgumentExceptionIfNotValid(nameof(settings));

            _settings = settings;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiCache = apiCache ?? throw new ArgumentNullException(nameof(apiCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ApiResponse SendRequest(ApiGetRequest request, Timeout? timeout, CancellationToken cancellationToken)
        {
            request.ThrowArgumentExceptionIfNotValid(nameof(request));
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));

            _logger.LogDebug($"Sending GET request with path: '{request.Path}' and query: '{request.Query}'...");

            var cachedResponse = _apiCache.Retrieve(request);
            if (cachedResponse.HasValue)
            {
                _logger.LogDebug("Response found in cache No need to send request again.");
                _logger.LogTrace($"Cached response body: {cachedResponse.Value.Body}");
                return cachedResponse.Value;
            }

            HttpResponse SendRequestToServer(ApiConnectionServer server)
            {
                var uri = PrepareAddress(server, request.Path, request.Query);
                return _httpClient.SendRequest(new HttpGetRequest(uri),
                    timeout, cancellationToken);
            }

            var response = SendRequest(SendRequestToServer, true);

            _apiCache.Save(request, response);

            return response;
        }

        public ApiResponse SendRequest(ApiPostRequest request, Timeout? timeout, CancellationToken cancellationToken)
        {
            request.ThrowArgumentExceptionIfNotValid(nameof(request));
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));

            _logger.LogDebug(
                $"Sending POST request with path: '{request.Path}', query: '{request.Query}' and content of length '{request.Content.Length}'...");

            HttpResponse SendRequestToServer(ApiConnectionServer server) => _httpClient.SendRequest(
                new HttpPostRequest(PrepareAddress(server, request.Path, request.Query), request.Content,
                    request.ContentType), timeout, cancellationToken);

            return SendRequest(SendRequestToServer, false);
        }

        private ApiResponse SendRequest(ApiConnectionServer server, Func<ApiConnectionServer, HttpResponse> sendRequest,
            ServerType serverType)
        {
            _logger.LogDebug(
                $"Sending request to server ({serverType}): '{server.Host}:{server.Port}' (uses HTTPS: {server.UseHttps})...");

            var httpResponse = sendRequest(server);

            _logger.LogDebug("Received response. Checking whether it is valid...");
            _logger.LogTrace($"Response status code: {httpResponse.StatusCode}");

            if (IsResponseValid(httpResponse, serverType))
            {
                var response = new ApiResponse(httpResponse.Body);

                _logger.LogDebug("Successfully got response.");
                _logger.LogTrace($"Response body: {response.Body}");

                return response;
            }

            _logger.LogWarning("Response is not valid.");

            if (IsResponseUnexpectedError(httpResponse, serverType))
            {
                throw new ApiResponseException((int) httpResponse.StatusCode);
            }

            throw new ApiServerConnectionException(
                $"Server \'{server.Host}\' returned code {(int) httpResponse.StatusCode}");
        }

        private ApiResponse SendRequest(Func<ApiConnectionServer, HttpResponse> sendRequest, bool useCacheServers)
        {
            try
            {
                Exception mainServerException;

                try
                {
                    return SendRequest(_settings.MainServer, sendRequest, ServerType.MainServer);
                }
                catch (WebException e)
                {
                    _logger.LogWarning("Error while connecting to main API server.", e);
                    mainServerException = e;
                }
                catch (ApiServerConnectionException e)
                {
                    _logger.LogWarning("Error while connecting to main API server.", e);
                    mainServerException = e;
                }

                var cacheServersExceptions = new Exception[_settings.CacheServers.Length];

                if (useCacheServers)
                {
                    _logger.LogDebug("Trying to use cache servers...");

                    for (var i = 0; i < _settings.CacheServers.Length; i++)
                    {
                        try
                        {
                            return SendRequest(_settings.CacheServers[i], sendRequest, ServerType.CacheServer);
                        }
                        catch (WebException e)
                        {
                            _logger.LogWarning("Error while connecting to cache API server.", e);
                            cacheServersExceptions[i] = e;
                        }
                        catch (ApiServerConnectionException e)
                        {
                            _logger.LogWarning("Error while connecting to cache API server.", e);
                            cacheServersExceptions[i] = e;
                        }
                    }
                }

                throw new ApiConnectionException(mainServerException,
                    cacheServersExceptions.ToImmutableArray());
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to send request.", e);
                throw;
            }
        }

        private static bool IsResponseValid(HttpResponse httpResponse, ServerType serverType)
        {
            switch (serverType)
            {
                case ServerType.MainServer:
                    return IsStatusCodeOk(httpResponse.StatusCode);
                case ServerType.CacheServer:
                    return httpResponse.StatusCode == HttpStatusCode.OK;
                default:
                    throw new ArgumentOutOfRangeException(nameof(serverType), serverType, null);
            }
        }

        private static bool IsResponseUnexpectedError(HttpResponse httpResponse, ServerType serverType)
        {
            switch (serverType)
            {
                case ServerType.MainServer:
                    return !IsStatusCodeOk(httpResponse.StatusCode) &&
                           !IsStatusCodeServerError(httpResponse.StatusCode);
                case ServerType.CacheServer:
                    return false; // ignore any api cache error
                default:
                    throw new ArgumentOutOfRangeException(nameof(serverType), serverType, null);
            }
        }

        private static HttpAddress PrepareAddress(ApiConnectionServer server, string path, string query)
        {
            return new HttpAddress(new UriBuilder
            {
                Host = server.Host,
                Path = path,
                Query = query,
                Port = server.Port,
                Scheme = server.UseHttps ? Uri.UriSchemeHttps : Uri.UriSchemeHttp
            }.Uri);
        }

        private static bool IsStatusCodeOk(HttpStatusCode statusCode)
        {
            return IsWithin((int) statusCode, 200, 299);
        }

        private static bool IsStatusCodeServerError(HttpStatusCode statusCode)
        {
            return IsWithin((int) statusCode, 500, 599);
        }

        private static bool IsWithin(int value, int min, int max)
        {
            return value >= min && value <= max;
        }
    }
}