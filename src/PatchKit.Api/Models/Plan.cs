using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models
{
    public struct Plan
    {
        [JsonConstructor]
        public Plan(ImmutableArray<string> capabilities)
        {
            Capabilities = capabilities;
        }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("capabilities"), JsonConverter(typeof(ImmutableArrayJsonConverter<string>))]
        public ImmutableArray<string> Capabilities { get; }
    }
}