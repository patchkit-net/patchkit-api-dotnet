using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models
{
    public struct Upload
    {
        [JsonConstructor]
        public Upload(int id)
        {
            Id = id;
        }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; }
    }
}