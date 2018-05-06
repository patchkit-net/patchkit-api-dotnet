using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models
{
    public struct AppContentSummaryFile
    {
        [JsonConstructor]
        public AppContentSummaryFile(string path, string hash, long size, string flags)
        {
            Path = path;
            Hash = hash;
            Size = size;
            Flags = flags;
        }
        
        /// <summary>
        /// File path.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; }
        
        /// <summary>
        /// File hash.
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { get; }
        
        /// <summary>
        /// Uncompressed file size in bytes. Present in >= 2.3
        /// </summary>
        [JsonProperty("size")]
        public long Size { get; }
        
        /// <summary>
        /// File flags, present in >= 2.3
        /// </summary>
        [JsonProperty("flags")]
        public string Flags { get; }
    }
}