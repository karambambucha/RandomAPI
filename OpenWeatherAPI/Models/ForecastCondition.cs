using System.Text.Json.Serialization;

namespace RandomAPI.Models
{
    public class ForecastCondition
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("Icon")]
        public string icon { get; set; }
        [JsonPropertyName("code")]
        public double Code { get; set; }
    }
}
