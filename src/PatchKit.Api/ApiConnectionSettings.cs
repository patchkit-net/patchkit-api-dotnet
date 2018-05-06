using PatchKit.Core;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api
{
    /// <summary>
    /// <see cref="BaseApiConnection" /> settings.
    /// </summary>
    public struct ApiConnectionSettings : IValidatable
    {
        public ApiConnectionSettings(ApiConnectionServer mainServer, ImmutableArray<ApiConnectionServer> cacheServers)
        {
            MainServer = mainServer;
            CacheServers = cacheServers;
        }

        /// <summary>
        /// Main API server.
        /// </summary>
        public ApiConnectionServer MainServer { get; }

        /// <summary>
        /// Cache API servers. Priority of servers is based on the array order.
        /// </summary>
        public ImmutableArray<ApiConnectionServer> CacheServers { get; }

        public string ValidationError
        {
            get
            {
                if (!MainServer.IsValid())
                {
                    return MainServer.GetFieldValidationError(nameof(MainServer));
                }

                if (!CacheServers.IsValid())
                {
                    return CacheServers.GetFieldValidationError(nameof(CacheServers));
                }

                return null;
            }
        }

        public static readonly ApiConnectionSettings DefaultApi =
            new ApiConnectionSettings(
                new ApiConnectionServer("api2.patchkit.net", 443, true),
                new[]
                {
                    new ApiConnectionServer("api-cache.patchkit.net", 443, true)
                }.ToImmutableArray());

        public static readonly ApiConnectionSettings DefaultKeysApi =
            new ApiConnectionSettings(
                new ApiConnectionServer("keys2.patchkit.net", 443, true),
                ImmutableArray<ApiConnectionServer>.Empty);
    }
}