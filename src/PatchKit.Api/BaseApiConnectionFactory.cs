using System;
using JetBrains.Annotations;
using PatchKit.Core;
using PatchKit.Logging;
using PatchKit.Network;

namespace PatchKit.Api
{
    public class BaseApiConnectionFactory : IBaseApiConnectionFactory
    {
        [NotNull] private readonly IHttpClient _httpClient;
        [NotNull] private readonly IApiCache _apiCache;
        [NotNull] private readonly ILogger _logger;

        public BaseApiConnectionFactory([NotNull] IHttpClient httpClient,
            [NotNull] IApiCache apiCache,
            [NotNull] ILogger logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiCache = apiCache ?? throw new ArgumentNullException(nameof(apiCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public IBaseApiConnection Create(ApiConnectionSettings settings)
        {
            settings.ThrowArgumentExceptionIfNotValid(nameof(settings));
            return new BaseApiConnection(settings, _httpClient, _apiCache, _logger);
        }
    }
}