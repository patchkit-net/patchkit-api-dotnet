using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models
{
    public struct Changelog
    {
        [JsonConstructor]
        public Changelog(int versionId, string versionLabel, string changes)
        {
            VersionId = versionId;
            VersionLabel = versionLabel;
            Changes = changes;
        }
        
        /// <summary>
        /// Version id.
        /// </summary>
        [JsonProperty("version_id")]
        public int VersionId { get; }
        
        /// <summary>
        /// Human readable label.
        /// </summary>
        [JsonProperty("version_label")]
        public string VersionLabel { get; }
        
        /// <summary>
        /// Changes description.
        /// </summary>
        [JsonProperty("changes")]
        public string Changes { get; }
    }
}