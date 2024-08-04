using Newtonsoft.Json;

namespace ActualPlaylistBuilder.Models.TrackSearch
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Album
    {
        [JsonProperty("album_type", NullValueHandling = NullValueHandling.Ignore)]
        public string AlbumType;

        [JsonProperty("artists", NullValueHandling = NullValueHandling.Ignore)]
        public List<Artist> Artists;

        [JsonProperty("external_urls", NullValueHandling = NullValueHandling.Ignore)]
        public ExternalUrls ExternalUrls;

        [JsonProperty("href", NullValueHandling = NullValueHandling.Ignore)]
        public string Href;

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id;

        [JsonProperty("images", NullValueHandling = NullValueHandling.Ignore)]
        public List<Image> Images;

        [JsonProperty("is_playable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsPlayable;

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name;

        [JsonProperty("release_date", NullValueHandling = NullValueHandling.Ignore)]
        public string ReleaseDate;

        [JsonProperty("release_date_precision", NullValueHandling = NullValueHandling.Ignore)]
        public string ReleaseDatePrecision;

        [JsonProperty("total_tracks", NullValueHandling = NullValueHandling.Ignore)]
        public int? TotalTracks;

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type;

        [JsonProperty("uri", NullValueHandling = NullValueHandling.Ignore)]
        public string Uri;
    }

    public class Artist
    {
        [JsonProperty("external_urls", NullValueHandling = NullValueHandling.Ignore)]
        public ExternalUrls ExternalUrls;

        [JsonProperty("href", NullValueHandling = NullValueHandling.Ignore)]
        public string Href;

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id;

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name;

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type;

        [JsonProperty("uri", NullValueHandling = NullValueHandling.Ignore)]
        public string Uri;
    }

    public class ExternalIds
    {
        [JsonProperty("isrc", NullValueHandling = NullValueHandling.Ignore)]
        public string Isrc;
    }

    public class ExternalUrls
    {
        [JsonProperty("spotify", NullValueHandling = NullValueHandling.Ignore)]
        public string Spotify;
    }

    public class Image
    {
        [JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
        public int? Height;

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url;

        [JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
        public int? Width;
    }

    public class Item
    {

        

        

        [JsonProperty("href", NullValueHandling = NullValueHandling.Ignore)]
        public string Href { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        

        //[JsonProperty("is_playable", NullValueHandling = NullValueHandling.Ignore)]
        //public bool? IsPlayable;

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("popularity", NullValueHandling = NullValueHandling.Ignore)]
        public int? Popularity { get; set; }

        [JsonProperty("preview_url", NullValueHandling = NullValueHandling.Ignore)]
        public string PreviewUrl { get; set; }

        [JsonProperty("track_number", NullValueHandling = NullValueHandling.Ignore)]
        public int? TrackNumber { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("uri", NullValueHandling = NullValueHandling.Ignore)]
        public string Uri { get; set; }
    }

    public class Root
    {
        [JsonProperty("tracks", NullValueHandling = NullValueHandling.Ignore)]
        public Tracks Tracks {  get; set; }
    }

    public class Tracks
    {
        [JsonProperty("href", NullValueHandling = NullValueHandling.Ignore)]
        public string Href {  get; set; }

        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<Item> Items { get; set; }

        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; set; }

        [JsonProperty("next", NullValueHandling = NullValueHandling.Ignore)]
        public string Next { get; set; }

        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public int? Offset { get; set; }

        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public int? Total { get; set; }
    }


}
