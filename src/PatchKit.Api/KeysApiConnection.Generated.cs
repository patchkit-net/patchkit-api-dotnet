using PatchKit.Api.Models.Keys;
using PatchKit.Core.Collections.Immutable;
using PatchKit.Core;
using PatchKit.Core.Cancellation;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace PatchKit.Api
{
    public partial class KeysApiConnection
    {
        public Job GetJobInfo(string guid, Timeout? timeout, CancellationToken cancellationToken)
        {
            if (guid == null)
            {
                throw new ArgumentNullException(nameof(guid));
            }
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));
            string path = "v2/jobs/{guid}";
            string query = string.Empty;
            SetPathParam(ref path, "guid", guid.ToString());
            var response = _baseApiConnection.SendRequest(new ApiGetRequest(path, query), timeout, cancellationToken);
            return JsonConvert.DeserializeObject<Job>(response.Body);
        }
        
        public LicenseKeysCollection GetCollectionKeys(int collectionId, string token, int size, int offset, Timeout? timeout, CancellationToken cancellationToken)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));
            string path = "v2/collections/{collection_id}/keys";
            string query = string.Empty;
            SetPathParam(ref path, "collection_id", collectionId.ToString());
            //TODO: Unsupported parameter kind 'header'
            SetQueryParam(ref query, "size", size.ToString());
            SetQueryParam(ref query, "offset", offset.ToString());
            var response = _baseApiConnection.SendRequest(new ApiGetRequest(path, query), timeout, cancellationToken);
            return JsonConvert.DeserializeObject<LicenseKeysCollection>(response.Body);
        }
        
        public LicenseKey GetKeyInfo(string key, string appSecret, string keySecret, Timeout? timeout, CancellationToken cancellationToken)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (appSecret == null)
            {
                throw new ArgumentNullException(nameof(appSecret));
            }
            timeout?.ThrowArgumentExceptionIfNotValid(nameof(timeout));
            string path = "v2/keys/{key}";
            string query = string.Empty;
            SetPathParam(ref path, "key", key.ToString());
            SetQueryParam(ref query, "app_secret", appSecret.ToString());
            if (keySecret != null)
            {
                SetQueryParam(ref query, "key_secret", keySecret.ToString());
            }
            var response = _baseApiConnection.SendRequest(new ApiGetRequest(path, query), timeout, cancellationToken);
            return JsonConvert.DeserializeObject<LicenseKey>(response.Body);
        }
    }
}