using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ActualPlaylistBuilder.Models
{
    public class SpotifyUser
    {
        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("country")]
        public string Country;

        [JsonProperty("display_name", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("display_name")]
        public string DisplayName;

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("email")]
        public string Email;



        [JsonProperty("href", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("href")]
        public string Href;

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("id")]
        public string Id;



        [JsonProperty("product", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("product")]
        public string Product;

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("type")]
        public string Type;

        [JsonProperty("uri", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("uri")]
        public string Uri;
    }
}
