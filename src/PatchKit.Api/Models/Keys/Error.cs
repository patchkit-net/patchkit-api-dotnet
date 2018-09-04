using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models.Keys
{
    public struct Error
    {
        [JsonConstructor]
        public Error(string message)
        {
            Message = message;
        }
        
        /// <summary>
        /// Human-readable error message
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; }
    }
}