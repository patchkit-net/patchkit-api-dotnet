using PatchKit.Api.Models;
using PatchKit.Core.Collections.Immutable;
using PatchKit.Core;
using PatchKit.Core.Cancellation;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace PatchKit.Api
{
    public partial class ApiConnection
    {
        public ImmutableArray<App> ListUserApplications(string apiKey, Timeout? timeout, CancellationToken cancellationToken)
        {
            if (apiKey == null)
            {
                throw new ArgumentNullException(nameof(apiKey));
            }
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));
            string path = "1/apps";
            string query = string.Empty;
            SetQueryParam(ref query, "api_key", apiKey.ToString());
            var response = _baseApiConnection.SendRequest(new ApiGetRequest(path, query), timeout, cancellationToken);
            return JsonConvert.DeserializeObject<ImmutableArray<App>>(response.Body, new ImmutableArrayJsonConverter<App>());
        }
        
        public App GetApplicationInfo(string appSecret, Timeout? timeout, CancellationToken cancellationToken)
        {
            if (appSecret == null)
            {
                throw new ArgumentNullException(nameof(appSecret));
            }
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));
            string path = "1/apps/{app_secret}";
            string query = string.Empty;
            SetPathParam(ref path, "app_secret", appSecret.ToString());
            var response = _baseApiConnection.SendRequest(new ApiGetRequest(path, query), timeout, cancellationToken);
            return JsonConvert.DeserializeObject<App>(response.Body);
        }
        
        public ImmutableArray<Changelog> GetAppChangelog(string appSecret, Timeout? timeout, CancellationToken cancellationToken)
        {
            if (appSecret == null)
            {
                throw new ArgumentNullException(nameof(appSecret));
            }
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));
            string path = "1/apps/{app_secret}/changelog";
            string query = string.Empty;
            SetPathParam(ref path, "app_secret", appSecret.ToString());
            var response = _baseApiConnection.SendRequest(new ApiGetRequest(path, query), timeout, cancellationToken);
            return JsonConvert.DeserializeObject<ImmutableArray<Changelog>>(response.Body, new ImmutableArrayJsonConverter<Changelog>());
        }
        
        public ImmutableArray<AppVersion> GetAppVersionList(string appSecret, string apiKey, Timeout? timeout, CancellationToken cancellationToken)
        {
            if (appSecret == null)
            {
                throw new ArgumentNullException(nameof(appSecret));
            }
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));
            string path = "1/apps/{app_secret}/versions";
            string query = string.Empty;
            SetPathParam(ref path, "app_secret", appSecret.ToString());
            if (apiKey != null)
            {
                SetQueryParam(ref query, "api_key", apiKey.ToString());
            }
            var response = _baseApiConnection.SendRequest(new ApiGetRequest(path, query), timeout, cancellationToken);
            return JsonConvert.DeserializeObject<ImmutableArray<AppVersion>>(response.Body, new ImmutableArrayJsonConverter<AppVersion>());
        }
        
        public AppVersion GetAppLatestAppVersion(string appSecret, Timeout? timeout, CancellationToken cancellationToken)
        {
            if (appSecret == null)
            {
                throw new ArgumentNullException(nameof(appSecret));
            }
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));
            string path = "1/apps/{app_secret}/versions/latest";
            string query = string.Empty;
            SetPathParam(ref path, "app_secret", appSecret.ToString());
            var response = _baseApiConnection.SendRequest(new ApiGetRequest(path, query), timeout, cancellationToken);
            return JsonConvert.DeserializeObject<AppVersion>(response.Body);
        }
        
        public AppVersionId GetAppLatestAppVersionId(string appSecret, Timeout? timeout, CancellationToken cancellationToken)
        {
            if (appSecret == null)
            {
                throw new ArgumentNullException(nameof(appSecret));
            }
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));
            string path = "1/apps/{app_secret}/versions/latest/id";
            string query = string.Empty;
            SetPathParam(ref path, "app_secret", appSecret.ToString());
            var response = _baseApiConnection.SendRequest(new ApiGetRequest(path, query), timeout, cancellationToken);
            return JsonConvert.DeserializeObject<AppVersionId>(response.Body);
        }
        
        public AppVersion GetAppVersion(string appSecret, int versionId, string apiKey, Timeout? timeout, CancellationToken cancellationToken)
        {
            if (appSecret == null)
            {
                throw new ArgumentNullException(nameof(appSecret));
            }
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));
            string path = "1/apps/{app_secret}/versions/{version_id}";
            string query = string.Empty;
            SetPathParam(ref path, "app_secret", appSecret.ToString());
            SetPathParam(ref path, "version_id", versionId.ToString());
            if (apiKey != null)
            {
                SetQueryParam(ref query, "api_key", apiKey.ToString());
            }
            var response = _baseApiConnection.SendRequest(new ApiGetRequest(path, query), timeout, cancellationToken);
            return JsonConvert.DeserializeObject<AppVersion>(response.Body);
        }
        
        public AppContentSummary GetAppVersionContentSummary(string appSecret, int versionId, Timeout? timeout, CancellationToken cancellationToken)
        {
            if (appSecret == null)
            {
                throw new ArgumentNullException(nameof(appSecret));
            }
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));
            string path = "1/apps/{app_secret}/versions/{version_id}/content_summary";
            string query = string.Empty;
            SetPathParam(ref path, "app_secret", appSecret.ToString());
            SetPathParam(ref path, "version_id", versionId.ToString());
            var response = _baseApiConnection.SendRequest(new ApiGetRequest(path, query), timeout, cancellationToken);
            return JsonConvert.DeserializeObject<AppContentSummary>(response.Body);
        }
        
        public AppDiffSummary GetAppVersionDiffSummary(string appSecret, int versionId, Timeout? timeout, CancellationToken cancellationToken)
        {
            if (appSecret == null)
            {
                throw new ArgumentNullException(nameof(appSecret));
            }
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));
            string path = "1/apps/{app_secret}/versions/{version_id}/diff_summary";
            string query = string.Empty;
            SetPathParam(ref path, "app_secret", appSecret.ToString());
            SetPathParam(ref path, "version_id", versionId.ToString());
            var response = _baseApiConnection.SendRequest(new ApiGetRequest(path, query), timeout, cancellationToken);
            return JsonConvert.DeserializeObject<AppDiffSummary>(response.Body);
        }
        
        public AppContentTorrentUrl GetAppVersionContentTorrentUrl(string appSecret, int versionId, string keySecret, Timeout? timeout, CancellationToken cancellationToken)
        {
            if (appSecret == null)
            {
                throw new ArgumentNullException(nameof(appSecret));
            }
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));
            string path = "1/apps/{app_secret}/versions/{version_id}/content_torrent_url";
            string query = string.Empty;
            SetPathParam(ref path, "app_secret", appSecret.ToString());
            SetPathParam(ref path, "version_id", versionId.ToString());
            if (keySecret != null)
            {
                SetQueryParam(ref query, "key_secret", keySecret.ToString());
            }
            var response = _baseApiConnection.SendRequest(new ApiGetRequest(path, query), timeout, cancellationToken);
            return JsonConvert.DeserializeObject<AppContentTorrentUrl>(response.Body);
        }
        
        public AppDiffTorrentUrl GetAppVersionDiffTorrentUrl(string appSecret, int versionId, string keySecret, Timeout? timeout, CancellationToken cancellationToken)
        {
            if (appSecret == null)
            {
                throw new ArgumentNullException(nameof(appSecret));
            }
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));
            string path = "1/apps/{app_secret}/versions/{version_id}/diff_torrent_url";
            string query = string.Empty;
            SetPathParam(ref path, "app_secret", appSecret.ToString());
            SetPathParam(ref path, "version_id", versionId.ToString());
            if (keySecret != null)
            {
                SetQueryParam(ref query, "key_secret", keySecret.ToString());
            }
            var response = _baseApiConnection.SendRequest(new ApiGetRequest(path, query), timeout, cancellationToken);
            return JsonConvert.DeserializeObject<AppDiffTorrentUrl>(response.Body);
        }
        
        public ImmutableArray<ResourceUrl> GetAppVersionContentUrls(string appSecret, int versionId, string country, Timeout? timeout, CancellationToken cancellationToken)
        {
            if (appSecret == null)
            {
                throw new ArgumentNullException(nameof(appSecret));
            }
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));
            string path = "1/apps/{app_secret}/versions/{version_id}/content_urls";
            string query = string.Empty;
            SetPathParam(ref path, "app_secret", appSecret.ToString());
            SetPathParam(ref path, "version_id", versionId.ToString());
            if (country != null)
            {
                SetQueryParam(ref query, "country", country.ToString());
            }
            var response = _baseApiConnection.SendRequest(new ApiGetRequest(path, query), timeout, cancellationToken);
            return JsonConvert.DeserializeObject<ImmutableArray<ResourceUrl>>(response.Body, new ImmutableArrayJsonConverter<ResourceUrl>());
        }
        
        public ImmutableArray<ResourceUrl> GetAppVersionDiffUrls(string appSecret, int versionId, string country, Timeout? timeout, CancellationToken cancellationToken)
        {
            if (appSecret == null)
            {
                throw new ArgumentNullException(nameof(appSecret));
            }
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));
            string path = "1/apps/{app_secret}/versions/{version_id}/diff_urls";
            string query = string.Empty;
            SetPathParam(ref path, "app_secret", appSecret.ToString());
            SetPathParam(ref path, "version_id", versionId.ToString());
            if (country != null)
            {
                SetQueryParam(ref query, "country", country.ToString());
            }
            var response = _baseApiConnection.SendRequest(new ApiGetRequest(path, query), timeout, cancellationToken);
            return JsonConvert.DeserializeObject<ImmutableArray<ResourceUrl>>(response.Body, new ImmutableArrayJsonConverter<ResourceUrl>());
        }
        
        public Plan GetPlanInfo(string apiKey, Timeout? timeout, CancellationToken cancellationToken)
        {
            if (apiKey == null)
            {
                throw new ArgumentNullException(nameof(apiKey));
            }
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));
            string path = "1/me/plan";
            string query = string.Empty;
            SetQueryParam(ref query, "api_key", apiKey.ToString());
            var response = _baseApiConnection.SendRequest(new ApiGetRequest(path, query), timeout, cancellationToken);
            return JsonConvert.DeserializeObject<Plan>(response.Body);
        }
    }
}