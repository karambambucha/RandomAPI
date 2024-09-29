using System.Text.Json.Serialization;

namespace RandomAPI.Models
{
    public class ForecastLocation
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        [JsonPropertyName("tz_id")]
        public string tzId { get; set; }

        [JsonPropertyName("localtime_epoch")]
        public double LocalTimeEpoch { get; set; }

        [JsonPropertyName("localtime")]
        public string LocalTime { get; set; }
    }
}
