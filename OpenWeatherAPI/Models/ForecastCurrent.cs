using System.Text.Json.Serialization;

namespace RandomAPI.Models
{
    public class ForecastCurrent
    {
        [JsonPropertyName("last_updated_epoch")]
        public double LastUpdatedEpoch { get; set; }
        [JsonPropertyName("last_updated")]
        public string LastUpdated { get; set; }
        [JsonPropertyName("temp_c")]
        public double TempC { get; set; }
        [JsonPropertyName("temp_f")]
        public double TempF { get; set; }
        [JsonPropertyName("is_day")]
        public double IsDay { get; set; }
        [JsonPropertyName("condition")]
        public ForecastCondition Condition { get; set; }
        [JsonPropertyName("wind_mph")]
        public double WindMph { get; set; }
        [JsonPropertyName("wind_kph")]
        public double WindKph { get; set; }
        [JsonPropertyName("wind_degree")]
        public double WindDegree { get; set; }
        [JsonPropertyName("wind_dir")]
        public string WindDirection { get; set; }
        [JsonPropertyName("pressure_mb")]
        public double PressureMb { get; set; }
        [JsonPropertyName("pressure_in")]
        public double PressureIn { get; set; }
        [JsonPropertyName("precip_mm")]
        public double PrecipMm { get; set; }
        [JsonPropertyName("humidity")]
        public double Humidity { get; set; }
        [JsonPropertyName("cloud")]
        public double Cloud { get; set; }
        [JsonPropertyName("feelslike_c")]
        public double FeelsLikeC { get; set; }
        [JsonPropertyName("feelslike_f")]
        public double FeelsLikeF { get; set; }
        [JsonPropertyName("windchill_c")]
        public double WindChillC { get; set; }
        [JsonPropertyName("windchill_f")]
        public double WindChillF { get; set; }
        [JsonPropertyName("heatindex_c")]
        public double HeatIndexC { get; set; }
        [JsonPropertyName("heatindex_f")]
        public double HeatIndexF { get; set; }
        [JsonPropertyName("dewpoint_c")]
        public double DewPointC { get; set; }
        [JsonPropertyName("dewpoint_f")]
        public double DewPointF { get; set; }
        [JsonPropertyName("vis_km")]
        public double VisKm { get; set; }
        [JsonPropertyName("vis_miles")]
        public double VisMiles { get; set; }
        [JsonPropertyName("uv")]
        public double UV { get; set; }
        [JsonPropertyName("gust_mph")]
        public double GustMph { get; set; }
        [JsonPropertyName("gust_kph")]
        public double GustKph { get; set; }

    }
}
