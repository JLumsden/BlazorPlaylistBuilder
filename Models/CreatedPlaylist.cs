using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ActualPlaylistBuilder.Models
{
    public class CreatedPlaylist
    {
        [JsonProperty("collaborative", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("collaborative")]
        public bool Collaborative;

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("description")]
        public string Description;

        [JsonProperty("href", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("href")]
        public string Href;

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("id")]
        public string Id;

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("name")]
        public string Name;

        [JsonProperty("public", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("public")]
        public bool Public;

        [JsonProperty("snapshot_id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("snapshot_id")]
        public string SnapshotId;

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("type")]
        public string Type;

        [JsonProperty("uri", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("uri")]
        public string Uri;
    }
}
