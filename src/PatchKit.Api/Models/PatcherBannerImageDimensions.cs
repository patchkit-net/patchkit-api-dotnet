using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models
{
    public struct PatcherBannerImageDimensions
    {
        [JsonConstructor]
        public PatcherBannerImageDimensions(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("x")]
        public int X { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("y")]
        public int Y { get; }
    }
}