using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models.Keys
{
    public struct LicenseKeysCollection
    {
        [JsonConstructor]
        public LicenseKeysCollection(LicenseKeysCollectionMetaData metadata, ImmutableArray<LicenseKey> keys)
        {
            Metadata = metadata;
            Keys = keys;
        }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("_metadata")]
        public LicenseKeysCollectionMetaData Metadata { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("keys"), JsonConverter(typeof(ImmutableArrayJsonConverter<LicenseKey>))]
        public ImmutableArray<LicenseKey> Keys { get; }
    }
}