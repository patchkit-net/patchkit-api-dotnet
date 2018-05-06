using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models
{
    public struct StatsReport
    {
        [JsonConstructor]
        public StatsReport(string eventName, string appSecret, int appVersion, long senderId, string caller, string operatingSystemFamily, string operatingSystemVersion)
        {
            EventName = eventName;
            AppSecret = appSecret;
            AppVersion = appVersion;
            SenderId = senderId;
            Caller = caller;
            OperatingSystemFamily = operatingSystemFamily;
            OperatingSystemVersion = operatingSystemVersion;
        }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("event_name")]
        public string EventName { get; }
        
        /// <summary>
        /// Secret of game application if applicable.
        /// </summary>
        [JsonProperty("app_secret")]
        public string AppSecret { get; }
        
        /// <summary>
        /// Version of game application if applicable.
        /// </summary>
        [JsonProperty("app_version")]
        public int AppVersion { get; }
        
        /// <summary>
        /// Unique client id. Should remain the same for all applications launched on this machine + account. Used to identify the player.
        /// </summary>
        [JsonProperty("sender_id")]
        public long SenderId { get; }
        
        /// <summary>
        /// Caller id the same as for caller GET parameters. More information: http://redmine.patchkit.net/projects/patchkit-documentation/wiki/Web_API_General_Info
        /// </summary>
        [JsonProperty("caller")]
        public string Caller { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("operating_system_family")]
        public string OperatingSystemFamily { get; }
        
        /// <summary>
        /// Operating system version without family name, for instance '10.11' for OSX.
        /// </summary>
        [JsonProperty("operating_system_version")]
        public string OperatingSystemVersion { get; }
    }
}