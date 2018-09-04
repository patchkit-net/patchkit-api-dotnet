using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models.Keys
{
    public struct LicenseKeysCollectionMetaData
    {
        [JsonConstructor]
        public LicenseKeysCollectionMetaData(int totalCount)
        {
            TotalCount = totalCount;
        }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("total_count")]
        public int TotalCount { get; }
    }
}