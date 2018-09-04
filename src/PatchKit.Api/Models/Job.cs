using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models
{
    public struct Job
    {
        [JsonConstructor]
        public Job(string jobGuid)
        {
            JobGuid = jobGuid;
        }
        
        /// <summary>
        /// Job GUID to be used with Jobs API.
        /// </summary>
        [JsonProperty("job_guid")]
        public string JobGuid { get; }
    }
}