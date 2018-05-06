using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models
{
    public struct AppContentTorrentUrl
    {
        [JsonConstructor]
        public AppContentTorrentUrl(string url)
        {
            Url = url;
        }
        
        /// <summary>
        /// Url to content torrent file.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; }
    }
}