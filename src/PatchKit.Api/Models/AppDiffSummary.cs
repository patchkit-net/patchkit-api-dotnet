using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models
{
    public struct AppDiffSummary
    {
        [JsonConstructor]
        public AppDiffSummary(string version, long size, long uncompressedSize, string encryptionMethod, string compressionMethod, ImmutableArray<string> addedFiles, ImmutableArray<string> modifiedFiles, ImmutableArray<string> removedFiles, string hashCode, Chunks chunks)
        {
            Version = version;
            Size = size;
            UncompressedSize = uncompressedSize;
            EncryptionMethod = encryptionMethod;
            CompressionMethod = compressionMethod;
            AddedFiles = addedFiles;
            ModifiedFiles = modifiedFiles;
            RemovedFiles = removedFiles;
            HashCode = hashCode;
            Chunks = chunks;
        }
        
        /// <summary>
        /// Version string. Format: MAJOR.MINOR. Present in >= 2.4
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; }
        
        /// <summary>
        /// Diff size.
        /// </summary>
        [JsonProperty("size")]
        public long Size { get; }
        
        /// <summary>
        /// Uncompressed archive size. Present in >= 2.4.
        /// </summary>
        [JsonProperty("uncompressed_size")]
        public long UncompressedSize { get; }
        
        /// <summary>
        /// Encryption method.
        /// </summary>
        [JsonProperty("encryption_method")]
        public string EncryptionMethod { get; }
        
        /// <summary>
        /// Compression method.
        /// </summary>
        [JsonProperty("compression_method")]
        public string CompressionMethod { get; }
        
        /// <summary>
        /// List of added files.
        /// </summary>
        [JsonProperty("added_files"), JsonConverter(typeof(ImmutableArrayJsonConverter<string>))]
        public ImmutableArray<string> AddedFiles { get; }
        
        /// <summary>
        /// List of modified files.
        /// </summary>
        [JsonProperty("modified_files"), JsonConverter(typeof(ImmutableArrayJsonConverter<string>))]
        public ImmutableArray<string> ModifiedFiles { get; }
        
        /// <summary>
        /// List of removed files.
        /// </summary>
        [JsonProperty("removed_files"), JsonConverter(typeof(ImmutableArrayJsonConverter<string>))]
        public ImmutableArray<string> RemovedFiles { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("hash_code")]
        public string HashCode { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("chunks")]
        public Chunks Chunks { get; }
    }
}