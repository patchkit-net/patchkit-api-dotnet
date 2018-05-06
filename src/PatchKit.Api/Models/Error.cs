using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models
{
    public struct Error
    {
        [JsonConstructor]
        public Error(string message, string symbol)
        {
            Message = message;
            Symbol = symbol;
        }
        
        /// <summary>
        /// Human-readable error message
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; }
        
        /// <summary>
        /// Error symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; }
    }
}