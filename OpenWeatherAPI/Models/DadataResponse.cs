using System.Text.Json.Serialization;

namespace RandomAPI.Models
{
    public class DadataResponse
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
