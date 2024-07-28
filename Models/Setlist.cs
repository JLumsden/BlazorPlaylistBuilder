using Newtonsoft.Json;

namespace ActualPlaylistBuilder.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Artist
    {
        [JsonProperty("mbid", NullValueHandling = NullValueHandling.Ignore)]
        public string mbid { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }

        [JsonProperty("sortName", NullValueHandling = NullValueHandling.Ignore)]
        public string sortName { get; set; }

        [JsonProperty("disambiguation", NullValueHandling = NullValueHandling.Ignore)]
        public string disambiguation { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string url { get; set; }
    }

    public class City
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }

        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string state { get; set; }

        [JsonProperty("stateCode", NullValueHandling = NullValueHandling.Ignore)]
        public string stateCode { get; set; }

        [JsonProperty("coords", NullValueHandling = NullValueHandling.Ignore)]
        public Coords coords { get; set; }

        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public Country country { get; set; }
    }

    public class Coords
    {
        [JsonProperty("lat", NullValueHandling = NullValueHandling.Ignore)]
        public double lat { get; set; }

        [JsonProperty("long", NullValueHandling = NullValueHandling.Ignore)]
        public double @long { get; set; }
    }

    public class Country
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string code { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }
    }

    public class Root
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }

        [JsonProperty("versionId", NullValueHandling = NullValueHandling.Ignore)]
        public string versionId { get; set; }

        [JsonProperty("eventDate", NullValueHandling = NullValueHandling.Ignore)]
        public string eventDate { get; set; }

        [JsonProperty("lastUpdated", NullValueHandling = NullValueHandling.Ignore)]
        public string lastUpdated { get; set; }

        [JsonProperty("artist", NullValueHandling = NullValueHandling.Ignore)]
        public Artist artist { get; set; }

        [JsonProperty("venue", NullValueHandling = NullValueHandling.Ignore)]
        public Venue venue { get; set; }

        [JsonProperty("tour", NullValueHandling = NullValueHandling.Ignore)]
        public Tour tour { get; set; }

        [JsonProperty("sets", NullValueHandling = NullValueHandling.Ignore)]
        public Sets sets { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string url { get; set; }
    }

    public class Set
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }

        [JsonProperty("song", NullValueHandling = NullValueHandling.Ignore)]
        public List<Song> song { get; set; }

        [JsonProperty("encore", NullValueHandling = NullValueHandling.Ignore)]
        public int? encore { get; set; }
    }

    public class Sets
    {
        [JsonProperty("set", NullValueHandling = NullValueHandling.Ignore)]
        public List<Set> set { get; set; }
    }

    public class Song
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }
    }

    public class Tour
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }
    }

    public class Venue
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }

        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public City city { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string url { get; set; }
    }


}
