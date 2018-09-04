using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models.Keys
{
    public struct LicenseKey
    {
        [JsonConstructor]
        public LicenseKey(string key, string appSecret, string keySecret, int collectionId, int registrations, bool blocked)
        {
            Key = key;
            AppSecret = appSecret;
            KeySecret = keySecret;
            CollectionId = collectionId;
            Registrations = registrations;
            Blocked = blocked;
        }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("app_secret")]
        public string AppSecret { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("key_secret")]
        public string KeySecret { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("collection_id")]
        public int CollectionId { get; }
        
        /// <summary>
        /// Number of key registrations. This is a request wihout a app_secret.
        /// </summary>
        [JsonProperty("registrations")]
        public int Registrations { get; }
        
        /// <summary>
        /// If set to true, this key is blocked for further use.
        /// </summary>
        [JsonProperty("blocked")]
        public bool Blocked { get; }
    }
}