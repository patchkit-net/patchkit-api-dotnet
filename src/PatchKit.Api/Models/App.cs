using Newtonsoft.Json;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api.Models
{
    public struct App
    {
        [JsonConstructor]
        public App(int id, string secret, string platform, string name, string displayName, string author, bool useKeys, string publishMethod, int currentVersion, int lowestVersionWithDiff, string patcherBannerImage, PatcherBannerImageDimensions patcherBannerImageDimensions, string patcherBannerImageUpdatedAt, string patcherDownloadSpeedUnit, bool patcherWhitelabel, string patcherSecret)
        {
            Id = id;
            Secret = secret;
            Platform = platform;
            Name = name;
            DisplayName = displayName;
            Author = author;
            UseKeys = useKeys;
            PublishMethod = publishMethod;
            CurrentVersion = currentVersion;
            LowestVersionWithDiff = lowestVersionWithDiff;
            PatcherBannerImage = patcherBannerImage;
            PatcherBannerImageDimensions = patcherBannerImageDimensions;
            PatcherBannerImageUpdatedAt = patcherBannerImageUpdatedAt;
            PatcherDownloadSpeedUnit = patcherDownloadSpeedUnit;
            PatcherWhitelabel = patcherWhitelabel;
            PatcherSecret = patcherSecret;
        }
        
        /// <summary>
        /// Initial app id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; }
        
        /// <summary>
        /// Application secret
        /// </summary>
        [JsonProperty("secret")]
        public string Secret { get; }
        
        /// <summary>
        /// Application platfrom
        /// </summary>
        [JsonProperty("platform")]
        public string Platform { get; }
        
        /// <summary>
        /// Application name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; }
        
        /// <summary>
        /// Application display name.
        /// </summary>
        [JsonProperty("display_name")]
        public string DisplayName { get; }
        
        /// <summary>
        /// Application author.
        /// </summary>
        [JsonProperty("author")]
        public string Author { get; }
        
        /// <summary>
        /// If set to true, application needs to contact keys server to get valid key_secret for content download.
        /// </summary>
        [JsonProperty("use_keys")]
        public bool UseKeys { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("publish_method")]
        public string PublishMethod { get; }
        
        /// <summary>
        /// Current number of publicly available version (does not count drafts). If 0, no version has been yet published.
        /// </summary>
        [JsonProperty("current_version")]
        public int CurrentVersion { get; }
        
        /// <summary>
        /// The number of lowest version that has a diff file available. For instance if player has version 3 and lowest_version_with_diff=4 then the player can upgrade from version 3 to 4 using diff file. But when lowest_version_with_diff=5 then it's not possible to apply a diff file and player has to re-download the game instead.
        /// </summary>
        [JsonProperty("lowest_version_with_diff")]
        public int LowestVersionWithDiff { get; }
        
        /// <summary>
        /// An https url to image banner that should be displayed inside the patcher. Watch out for patcher_banner_image_dimensions, but for now the size will be fixed at 600x230. If this field is set to null, a default (stored) image should be used. The image will be always in PNG format.
        /// </summary>
        [JsonProperty("patcher_banner_image")]
        public string PatcherBannerImage { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("patcher_banner_image_dimensions")]
        public PatcherBannerImageDimensions PatcherBannerImageDimensions { get; }
        
        /// <summary>
        /// Date and time when patcher banner image has been updated.
        /// </summary>
        [JsonProperty("patcher_banner_image_updated_at")]
        public string PatcherBannerImageUpdatedAt { get; }
        
        /// <summary>
        /// Tells the patcher what format should be used to display download speed unit. human_readable should display kilobytes unless download speed exceeds 1024 kilobytes/s, then megabytes should be displayed.
        /// </summary>
        [JsonProperty("patcher_download_speed_unit")]
        public string PatcherDownloadSpeedUnit { get; }
        
        /// <summary>
        /// If set to true, no PatchKit logo or PatchKit reference should be visible on the patcher.
        /// </summary>
        [JsonProperty("patcher_whitelabel")]
        public bool PatcherWhitelabel { get; }
        
        /// <summary>
        /// The secret of patcher to use.
        /// </summary>
        [JsonProperty("patcher_secret")]
        public string PatcherSecret { get; }
    }
}