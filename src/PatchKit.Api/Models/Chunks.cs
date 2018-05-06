using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models
{
    public struct Chunks
    {
        [JsonConstructor]
        public Chunks(int size, ImmutableArray<string> hashes)
        {
            Size = size;
            Hashes = hashes;
        }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("hashes"), JsonConverter(typeof(ImmutableArrayJsonConverter<string>))]
        public ImmutableArray<string> Hashes { get; }
    }
}