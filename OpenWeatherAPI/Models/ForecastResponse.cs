using System.Text.Json.Serialization;

namespace RandomAPI.Models
{
    public class ForecastResponse
    {
        [JsonPropertyName("location")]
        public ForecastLocation Location { get; set; }
        [JsonPropertyName("current")]
        public ForecastCurrent CurrentForecast { get; set; }
    }
}
