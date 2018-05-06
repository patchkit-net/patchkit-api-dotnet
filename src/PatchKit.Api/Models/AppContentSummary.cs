using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models
{
    public struct AppContentSummary
    {
        [JsonConstructor]
        public AppContentSummary(string version, long size, long uncompressedSize, string encryptionMethod, string compressionMethod, ImmutableArray<AppContentSummaryFile> files, string hashCode, Chunks chunks)
        {
            Version = version;
            Size = size;
            UncompressedSize = uncompressedSize;
            EncryptionMethod = encryptionMethod;
            CompressionMethod = compressionMethod;
            Files = files;
            HashCode = hashCode;
            Chunks = chunks;
        }
        
        /// <summary>
        /// Version string. Format: MAJOR.MINOR. Present in >= 2.4
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; }
        
        /// <summary>
        /// Content size.
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
        /// List of content files.
        /// </summary>
        [JsonProperty("files"), JsonConverter(typeof(ImmutableArrayJsonConverter<AppContentSummaryFile>))]
        public ImmutableArray<AppContentSummaryFile> Files { get; }
        
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