using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models
{
    public struct AppVersionId
    {
        [JsonConstructor]
        public AppVersionId(int id)
        {
            Id = id;
        }
        
        /// <summary>
        /// Version id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; }
    }
}