using PatchKit.Api.Models;
using PatchKit.Core.Collections.Immutable;
using PatchKit.Core;

namespace PatchKit.Api
{
    public partial interface IApiConnection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiKey">Application owner API key. (required)</param>
        /// <param name="timeout">Request timeout. If <c>null</c> then timeout is disabled</param>
        ImmutableArray<App> (string apiKey, Timeout? timeout);
        
        /// <summary>
        /// Gets detailes app info
        /// </summary>
        /// <param name="appSecret">Secret of an application. (required)</param>
        /// <param name="timeout">Request timeout. If <c>null</c> then timeout is disabled</param>
        App (string appSecret, Timeout? timeout);
        
        /// <summary>
        /// Gets a complete changelog of an application.
        /// </summary>
        /// <param name="appSecret">Secret of an application. (required)</param>
        /// <param name="timeout">Request timeout. If <c>null</c> then timeout is disabled</param>
        ImmutableArray<Changelog> GetAppChangelog(string appSecret, Timeout? timeout);
        
        /// <summary>
        /// Gets the basic information for all published versions. When API Key is provided, draft version information is included if draft version exists.
        /// </summary>
        /// <param name="appSecret">Secret of an application. (required)</param>
        /// <param name="apiKey">Application owner API key. (optional)</param>
        /// <param name="timeout">Request timeout. If <c>null</c> then timeout is disabled</param>
        ImmutableArray<AppVersion> GetAppVersionList(string appSecret, string apiKey, Timeout? timeout);
        
        /// <summary>
        /// Gets latest application version object.
        /// </summary>
        /// <param name="appSecret">Secret of an application. (required)</param>
        /// <param name="timeout">Request timeout. If <c>null</c> then timeout is disabled</param>
        AppVersion GetAppLatestAppVersion(string appSecret, Timeout? timeout);
        
        /// <summary>
        /// Gets latest application version id. Please use /apps/{app_secret} instead to get the latest version.
        /// </summary>
        /// <param name="appSecret">Secret of an application. (required)</param>
        /// <param name="timeout">Request timeout. If <c>null</c> then timeout is disabled</param>
        AppVersionId GetAppLatestAppVersionId(string appSecret, Timeout? timeout);
        
        /// <summary>
        /// Gets selected version object. If API key is provided, can get the information about draft version.
        /// </summary>
        /// <param name="appSecret">Secret of an application. (required)</param>
        /// <param name="versionId">Version id. (required)</param>
        /// <param name="apiKey">Application owner API key. (optional)</param>
        /// <param name="timeout">Request timeout. If <c>null</c> then timeout is disabled</param>
        AppVersion GetAppVersion(string appSecret, int versionId, string apiKey, Timeout? timeout);
        
        /// <summary>
        /// Gets selected version content summary.
        /// </summary>
        /// <param name="appSecret">Secret of an application. (required)</param>
        /// <param name="versionId">Version id. (required)</param>
        /// <param name="timeout">Request timeout. If <c>null</c> then timeout is disabled</param>
        AppContentSummary GetAppVersionContentSummary(string appSecret, int versionId, Timeout? timeout);
        
        /// <summary>
        /// Gets selected version diff summary.
        /// </summary>
        /// <param name="appSecret">Secret of an application. (required)</param>
        /// <param name="versionId">Version id. (required)</param>
        /// <param name="timeout">Request timeout. If <c>null</c> then timeout is disabled</param>
        AppDiffSummary GetAppVersionDiffSummary(string appSecret, int versionId, Timeout? timeout);
        
        /// <summary>
        /// Gets selected application version content torrent url.
        /// </summary>
        /// <param name="appSecret">Secret of an application. (required)</param>
        /// <param name="versionId">Version id. (required)</param>
        /// <param name="keySecret">Key secret provided by key server. This value is optional and is needed only if application is secured by license keys. (optional)</param>
        /// <param name="timeout">Request timeout. If <c>null</c> then timeout is disabled</param>
        AppContentTorrentUrl GetAppVersionContentTorrentUrl(string appSecret, int versionId, string keySecret, Timeout? timeout);
        
        /// <summary>
        /// Gets selected application version diff torrent url.
        /// </summary>
        /// <param name="appSecret">Secret of an application. (required)</param>
        /// <param name="versionId">Version id. (required)</param>
        /// <param name="keySecret">Key secret provided by key server. This value is optional and is needed only if application is secured by license keys. (optional)</param>
        /// <param name="timeout">Request timeout. If <c>null</c> then timeout is disabled</param>
        AppDiffTorrentUrl GetAppVersionDiffTorrentUrl(string appSecret, int versionId, string keySecret, Timeout? timeout);
        
        /// <summary>
        /// Gets selected application version content urls.
        /// </summary>
        /// <param name="appSecret">Secret of an application. (required)</param>
        /// <param name="versionId">Version id. (required)</param>
        /// <param name="country">Country iso code (optional)</param>
        /// <param name="timeout">Request timeout. If <c>null</c> then timeout is disabled</param>
        ImmutableArray<ResourceUrl> GetAppVersionContentUrls(string appSecret, int versionId, string country, Timeout? timeout);
        
        /// <summary>
        /// Gets selected application version diff urls.
        /// </summary>
        /// <param name="appSecret">Secret of an application. (required)</param>
        /// <param name="versionId">Version id. (required)</param>
        /// <param name="country">Country iso code (optional)</param>
        /// <param name="timeout">Request timeout. If <c>null</c> then timeout is disabled</param>
        ImmutableArray<ResourceUrl> GetAppVersionDiffUrls(string appSecret, int versionId, string country, Timeout? timeout);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiKey">Application owner API key. Required when not using a session. (required)</param>
        /// <param name="timeout">Request timeout. If <c>null</c> then timeout is disabled</param>
        Plan GetPlanInfo(string apiKey, Timeout? timeout);
    }
}