using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models
{
    public struct AppVersion
    {
        [JsonConstructor]
        public AppVersion(int id, string label, string changelog, long publishDate, string contentGuid, string contentMetaGuid, string diffGuid, string diffMetaGuid, bool draft, bool pendingPublish, bool published, float publishProgress, string mainExecutable, ImmutableArray<string> mainExecutableArgs, ImmutableArray<string> ignoredFiles, bool publishWhenProcessed, string processingStartedAt, string processingFinishedAt, bool canBeImported)
        {
            Id = id;
            Label = label;
            Changelog = changelog;
            PublishDate = publishDate;
            ContentGuid = contentGuid;
            ContentMetaGuid = contentMetaGuid;
            DiffGuid = diffGuid;
            DiffMetaGuid = diffMetaGuid;
            Draft = draft;
            PendingPublish = pendingPublish;
            Published = published;
            PublishProgress = publishProgress;
            MainExecutable = mainExecutable;
            MainExecutableArgs = mainExecutableArgs;
            IgnoredFiles = ignoredFiles;
            PublishWhenProcessed = publishWhenProcessed;
            ProcessingStartedAt = processingStartedAt;
            ProcessingFinishedAt = processingFinishedAt;
            CanBeImported = canBeImported;
        }
        
        /// <summary>
        /// Initial version id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; }
        
        /// <summary>
        /// Version label.
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; }
        
        /// <summary>
        /// Description of changes.
        /// </summary>
        [JsonProperty("changelog")]
        public string Changelog { get; }
        
        /// <summary>
        /// Unix timestamp of publish date.
        /// </summary>
        [JsonProperty("publish_date")]
        public long PublishDate { get; }
        
        /// <summary>
        /// Guid of content file.
        /// </summary>
        [JsonProperty("content_guid")]
        public string ContentGuid { get; }
        
        /// <summary>
        /// Guid of content meta file if available.
        /// </summary>
        [JsonProperty("content_meta_guid")]
        public string ContentMetaGuid { get; }
        
        /// <summary>
        /// Guid of diff file or null if there's no diff.
        /// </summary>
        [JsonProperty("diff_guid")]
        public string DiffGuid { get; }
        
        /// <summary>
        /// Guid of diff meta file if available.
        /// </summary>
        [JsonProperty("diff_meta_guid")]
        public string DiffMetaGuid { get; }
        
        /// <summary>
        /// Set to true if this version is a draft version.
        /// </summary>
        [JsonProperty("draft")]
        public bool Draft { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pending_publish")]
        public bool PendingPublish { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("published")]
        public bool Published { get; }
        
        /// <summary>
        /// If pending_publish is true, this number will indicate the publishing progress.
        /// </summary>
        [JsonProperty("publish_progress")]
        public float PublishProgress { get; }
        
        /// <summary>
        /// Main executable relative path without slash at the beginning.
        /// </summary>
        [JsonProperty("main_executable")]
        public string MainExecutable { get; }
        
        /// <summary>
        /// Main executable arguments
        /// </summary>
        [JsonProperty("main_executable_args"), JsonConverter(typeof(ImmutableArrayJsonConverter<string>))]
        public ImmutableArray<string> MainExecutableArgs { get; }
        
        /// <summary>
        /// Relative list of paths that should be ignored for local data consistency.
        /// </summary>
        [JsonProperty("ignored_files"), JsonConverter(typeof(ImmutableArrayJsonConverter<string>))]
        public ImmutableArray<string> IgnoredFiles { get; }
        
        /// <summary>
        /// Set to true if version will be published after successfull processing.
        /// </summary>
        [JsonProperty("publish_when_processed")]
        public bool PublishWhenProcessed { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("processing_started_at")]
        public string ProcessingStartedAt { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("processing_finished_at")]
        public string ProcessingFinishedAt { get; }
        
        /// <summary>
        /// If true then this version can be imported to other application. Visible only for owners.
        /// </summary>
        [JsonProperty("can_be_imported")]
        public bool CanBeImported { get; }
    }
}