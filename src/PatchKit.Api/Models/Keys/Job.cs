using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models.Keys
{
    public struct Job
    {
        [JsonConstructor]
        public Job(string guid, bool pending, bool finished)
        {
            Guid = guid;
            Pending = pending;
            Finished = finished;
        }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("guid")]
        public string Guid { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pending")]
        public bool Pending { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("finished")]
        public bool Finished { get; }
    }
}