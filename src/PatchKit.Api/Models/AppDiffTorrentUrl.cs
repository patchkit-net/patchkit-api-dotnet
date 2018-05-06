using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models
{
    public struct AppDiffTorrentUrl
    {
        [JsonConstructor]
        public AppDiffTorrentUrl(string url)
        {
            Url = url;
        }
        
        /// <summary>
        /// Url to diff torrent file.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; }
    }
}