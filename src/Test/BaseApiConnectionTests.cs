using System;
using System.Linq;
using System.Net;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using PatchKit.Api;
using PatchKit.Core.Cancellation;
using PatchKit.Core.Collections.Immutable;
using PatchKit.Logging;
using PatchKit.Network;

namespace Test
{
    [TestFixture]
    public class BaseApiConnectionTests
    {
        private ILogger _logger;
        private IHttpClient _httpClient;
        private IApiCache _apiCache;

        [SetUp]
        public void SetUp()
        {
            _logger = Substitute.For<ILogger>();
            _httpClient = Substitute.For<IHttpClient>();
            _apiCache = Substitute.For<IApiCache>();
        }

        [DatapointSource] private static readonly int[] Ports = {80, 81, 443, 444};

        [Theory]
        public void SendGetRequest_SendsCorrectHttpRequest(int mainServerPort, bool mainServerHttps)
        {
            Assume.That(Ports.Contains(mainServerPort));

            var settings = new ApiConnectionSettings(
                new ApiConnectionServer("main_server", mainServerPort, mainServerHttps),
                ImmutableArray<ApiConnectionServer>.Empty);

            var apiConnection = new BaseApiConnection(settings, _httpClient, _apiCache, _logger);

            var protocol = mainServerHttps ? "https" : "http";

            var request = new HttpGetRequest(new Uri($"{protocol}://main_server:{mainServerPort}/path?query"));

            _httpClient.SendRequest(request, null, CancellationToken.Empty).Returns(new HttpResponse("test", HttpStatusCode.OK));

            apiConnection.SendRequest(new ApiGetRequest("path", "query"), null, CancellationToken.Empty);

            _httpClient.Received(1).SendRequest(request, null, CancellationToken.Empty);
        }

        [Theory]
        public void SendGetRequest_Returns_Valid_Response(HttpStatusCode code)
        {
            Assume.That((int) code >= 200 && (int) code <= 299);

            var settings = new ApiConnectionSettings(
                new ApiConnectionServer("main_server", 80, false),
                ImmutableArray<ApiConnectionServer>.Empty);

            var apiConnection = new BaseApiConnection(settings, _httpClient, _apiCache, _logger);

            _httpClient
                .SendRequest(new HttpGetRequest(new Uri("http://main_server/path?query")), null,
                    CancellationToken.Empty)
                .Returns(new HttpResponse("test", code));

            var apiResponse =
                apiConnection.SendRequest(new ApiGetRequest("path", "query"), null, CancellationToken.Empty);
            apiResponse.Body.Should().Be("test");
        }

        [Theory]
        public void SendGetRequest_If_Main_Server_Connection_Fails_Fallback_To_1st_Cache_Server(
            HttpStatusCode errorCode)
        {
            Assume.That((int) errorCode >= 500);

            var settings = new ApiConnectionSettings(
                new ApiConnectionServer("main_server", 80, false),
                new[]
                {
                    new ApiConnectionServer("cache_server_1", 88, false),
                    new ApiConnectionServer("cache_server_2", 40, false)
                }.ToImmutableArray());

            var apiConnection = new BaseApiConnection(settings, _httpClient, _apiCache, _logger);

            _httpClient
                .SendRequest(new HttpGetRequest(new Uri("http://main_server:80/path?query")), null,
                    CancellationToken.Empty)
                .Returns(new HttpResponse("error", errorCode));

            _httpClient
                .SendRequest(new HttpGetRequest(new Uri("http://cache_server_1:88/path?query")), null,
                    CancellationToken.Empty)
                .Returns(new HttpResponse("success", HttpStatusCode.OK));

            var apiResponse =
                apiConnection.SendRequest(new ApiGetRequest("path", "query"), null, CancellationToken.Empty);
            apiResponse.Body.Should().Be("success");
        }

        [Theory]
        public void SendGetRequest_If_Main_Server_Error_Throw_ApiResponseException(HttpStatusCode errorCode)
        {
            Assume.That((int) errorCode >= 300 && (int) errorCode <= 499);

            var settings = new ApiConnectionSettings(
                new ApiConnectionServer("main_server", 80, false),
                new[]
                {
                    new ApiConnectionServer("cache_server_1", 40, false),
                    new ApiConnectionServer("cache_server_2", 88, false)
                }.ToImmutableArray());

            var apiConnection = new BaseApiConnection(settings, _httpClient, _apiCache, _logger);

            _httpClient
                .SendRequest(new HttpGetRequest(new Uri("http://main_server:80/path?query")), null,
                    CancellationToken.Empty)
                .Returns(new HttpResponse("error", errorCode));

            Action act = () =>
                apiConnection.SendRequest(new ApiGetRequest("path", "query"), null, CancellationToken.Empty);

            act.Should().Throw<ApiResponseException>();
        }

        [Test]
        public void SendGetRequest_If_All_Servers_Connection_Fails_Throw_ApiConnectionException()
        {
            var settings = new ApiConnectionSettings(
                new ApiConnectionServer("main_server", 80, false),
                new[]
                {
                    new ApiConnectionServer("cache_server_1", 40, false),
                    new ApiConnectionServer("cache_server_2", 88, false)
                }.ToImmutableArray());

            var apiConnection = new BaseApiConnection(settings, _httpClient, _apiCache, _logger);

            _httpClient
                .SendRequest(new HttpGetRequest(new Uri("http://main_server:80/path?query")), null,
                    CancellationToken.Empty)
                .Throws(new WebException());

            _httpClient
                .SendRequest(new HttpGetRequest(new Uri("http://cache_server_1:40/path?query")), null,
                    CancellationToken.Empty)
                .Returns(new HttpResponse("failure", HttpStatusCode.BadRequest));

            _httpClient
                .SendRequest(new HttpGetRequest(new Uri("http://cache_server_2:88/path?query")), null,
                    CancellationToken.Empty)
                .Returns(new HttpResponse("failure", HttpStatusCode.NotFound));

            Action act = () =>
                apiConnection.SendRequest(new ApiGetRequest("path", "query"), null, CancellationToken.Empty);

            act.Should().Throw<ApiConnectionException>();
        }

        [Test]
        public void SendGetRequest_If_Main_And_1st_Cache_Servers_Connection_Fails_Fallback_To_2nd_Cache_Server()
        {
            var settings = new ApiConnectionSettings(
                new ApiConnectionServer("main_server", 80, false),
                new[]
                {
                    new ApiConnectionServer("cache_server_1", 40, false),
                    new ApiConnectionServer("cache_server_2", 88, false)
                }.ToImmutableArray());

            var apiConnection = new BaseApiConnection(settings, _httpClient, _apiCache, _logger);

            _httpClient
                .SendRequest(new HttpGetRequest(new Uri("http://main_server:80/path?query")), null,
                    CancellationToken.Empty)
                .Throws(new WebException());

            _httpClient
                .SendRequest(new HttpGetRequest(new Uri("http://cache_server_1:40/path?query")), null,
                    CancellationToken.Empty)
                .Returns(new HttpResponse("failure", HttpStatusCode.BadRequest));

            _httpClient
                .SendRequest(new HttpGetRequest(new Uri("http://cache_server_2:88/path?query")), null,
                    CancellationToken.Empty)
                .Returns(new HttpResponse("success", HttpStatusCode.OK));

            var apiResponse =
                apiConnection.SendRequest(new ApiGetRequest("path", "query"), null, CancellationToken.Empty);
            apiResponse.Body.Should().Be("success");
        }

        [Test]
        public void SendGetRequest_Caches_Success_Response()
        {
            var settings = new ApiConnectionSettings(
                new ApiConnectionServer("main_server", 80, false),
                ImmutableArray<ApiConnectionServer>.Empty);

            var apiConnection = new BaseApiConnection(settings, _httpClient, _apiCache, _logger);

            var httpRequest = new HttpGetRequest(new Uri("http://main_server:80/path?query"));

            _httpClient.SendRequest(httpRequest, null, CancellationToken.Empty)
                .Returns(new HttpResponse("test", HttpStatusCode.OK));

            var apiRequest = new ApiGetRequest("path", "query");

            apiConnection.SendRequest(apiRequest, null, CancellationToken.Empty);

            _apiCache.Received(1).Save(apiRequest, new ApiResponse("test"));
        }

        [Test]
        public void SendGetRequest_Uses_Cached_Response()
        {
            var settings = new ApiConnectionSettings(
                new ApiConnectionServer("main_server", 80, false),
                ImmutableArray<ApiConnectionServer>.Empty);

            var apiConnection = new BaseApiConnection(settings, _httpClient, _apiCache, _logger);

            var apiResponse = new ApiResponse("cached-response");
            var apiRequest = new ApiGetRequest("path", "query");
            _apiCache.Retrieve(apiRequest).Returns(apiResponse);

            apiConnection.SendRequest(apiRequest, null, CancellationToken.Empty).Should().Be(apiResponse);
        }

        [Theory]
        public void SendPostRequest_SendsCorrectHttpRequest(int mainServerPort, bool mainServerHttps)
        {
            Assume.That(Ports.Contains(mainServerPort));

            var settings = new ApiConnectionSettings(
                new ApiConnectionServer("main_server", mainServerPort, mainServerHttps),
                ImmutableArray<ApiConnectionServer>.Empty);

            var apiConnection = new BaseApiConnection(settings, _httpClient, _apiCache, _logger);

            var protocol = mainServerHttps ? "https" : "http";

            var content = new byte[] {1, 2, 3}.ToImmutableArray();
            var contentType = HttpPostRequestContentType.ApplicationXWWWFormUrlEncoded;

            var request = new HttpPostRequest(
                new Uri($"{protocol}://main_server:{mainServerPort}/path?query"), content, contentType);

            _httpClient
                .SendRequest(request, null, CancellationToken.Empty)
                .Returns(new HttpResponse("test", HttpStatusCode.OK));

            apiConnection.SendRequest(new ApiPostRequest("path", "query", content, contentType), null,
                CancellationToken.Empty);

            _httpClient.Received(1).SendRequest(request, null, CancellationToken.Empty);
        }

        [Theory]
        public void SendPostRequest_Returns_Valid_Response(HttpStatusCode code)
        {
            Assume.That((int) code >= 200 && (int) code <= 299);

            var settings = new ApiConnectionSettings(
                new ApiConnectionServer("main_server", 80, false),
                ImmutableArray<ApiConnectionServer>.Empty);

            var apiConnection = new BaseApiConnection(settings, _httpClient, _apiCache, _logger);

            var content = new byte[] {1, 2, 3}.ToImmutableArray();
            var contentType = HttpPostRequestContentType.ApplicationXWWWFormUrlEncoded;

            var request = new HttpPostRequest(
                new Uri("http://main_server:80/path?query"),
                content, contentType);

            _httpClient
                .SendRequest(request, null, CancellationToken.Empty)
                .Returns(new HttpResponse("test", code));

            var apiResponse =
                apiConnection.SendRequest(new ApiPostRequest("path", "query", content, contentType), null,
                    CancellationToken.Empty);
            apiResponse.Body.Should().Be("test");
        }

        [Theory]
        public void SendPostRequest_If_Main_Server_Connection_Fails_Dont_Use_Cache_Servers(
            HttpStatusCode errorCode)
        {
            Assume.That((int) errorCode >= 300);

            var settings = new ApiConnectionSettings(
                new ApiConnectionServer("main_server", 80, false),
                new[]
                {
                    new ApiConnectionServer("cache_server_1", 40, false),
                    new ApiConnectionServer("cache_server_2", 88, false)
                }.ToImmutableArray());

            var apiConnection = new BaseApiConnection(settings, _httpClient, _apiCache, _logger);

            var content = new byte[] {1, 2, 3}.ToImmutableArray();
            var contentType = HttpPostRequestContentType.ApplicationXWWWFormUrlEncoded;

            var mainRequest = new HttpPostRequest(
                new Uri("http://main_server:80/path?query"),
                content, contentType);

            var cache1Request = new HttpPostRequest(
                new Uri("http://cache_server_1:40/path?query"),
                content, contentType);

            var cache2Request = new HttpPostRequest(
                new Uri("http://cache_server_2:88/path?query"),
                content, contentType);

            _httpClient
                .SendRequest(mainRequest, null, CancellationToken.Empty)
                .Returns(new HttpResponse("error", errorCode));

            _httpClient
                .SendRequest(cache1Request, null, CancellationToken.Empty)
                .Returns(new HttpResponse("success", HttpStatusCode.OK));


            _httpClient
                .SendRequest(cache2Request, null, CancellationToken.Empty)
                .Returns(new HttpResponse("success", HttpStatusCode.OK));

            Action act = () =>
                apiConnection.SendRequest(new ApiPostRequest("path", "query", content, contentType), null,
                    CancellationToken.Empty);

            act.Should().Throw<Exception>();
        }
    }
}