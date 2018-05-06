using PatchKit.Api.Models.Keys;
using PatchKit.Core.Collections.Immutable;
using PatchKit.Core;

namespace PatchKit.Api
{
    public partial interface IKeysApiConnection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"> (required)</param>
        /// <param name="timeout">Request timeout. If <c>null</c> then timeout is disabled</param>
        Job GetJobInfo(string guid, Timeout? timeout);
        
        /// <summary>
        /// Gets keys based on collection id, size and offset.
        /// </summary>
        /// <param name="collectionId"> (required)</param>
        /// <param name="token">Authentication token (required)</param>
        /// <param name="size"> (required)</param>
        /// <param name="offset"> (required)</param>
        /// <param name="timeout">Request timeout. If <c>null</c> then timeout is disabled</param>
        LicenseKeysCollection GetCollectionKeys(int collectionId, string token, int size, int offset, Timeout? timeout);
        
        /// <summary>
        /// Gets key info. Required providing an app secret. Will find only key that matches given app_secret. This request registers itself as key usage until valid key_secret is providen with this request.
        /// </summary>
        /// <param name="key"> (required)</param>
        /// <param name="appSecret"> (required)</param>
        /// <param name="keySecret">If provided and valid, will only do a blocked check. (optional)</param>
        /// <param name="timeout">Request timeout. If <c>null</c> then timeout is disabled</param>
        LicenseKey GetKeyInfo(string key, string appSecret, string keySecret, Timeout? timeout);
    }
}