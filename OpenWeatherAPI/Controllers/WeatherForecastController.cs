using Microsoft.AspNetCore.Mvc;
using RandomAPI.Models;
using StackExchange.Redis;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;


namespace RandomAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly IDatabase _redis;

        public WeatherForecastController(HttpClient client, IConnectionMultiplexer muxer)
        {
            _client = client;
            _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("weatherCachingApp", "1.0"));
            _redis = muxer.GetDatabase();
        }

        private async Task<string> GetForecast(double latitude, double longitude)
        {
            var pointsRequestQuery = $"https://api.weather.gov/points/{latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)},{longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}"; //get the URI
            var result = await _client.GetFromJsonAsync<JsonObject>(pointsRequestQuery);
            var gridX = result["properties"]["gridX"].ToString();
            var gridY = result["properties"]["gridY"].ToString();
            var gridId = result["Properties"]["gridId"].ToString();
            var forecastRequestQuery = $"https://api.weather.gov/gridpoints/{gridId}/{gridX},{gridY}/forecast";
            var forecastResult = await _client.GetFromJsonAsync<JsonObject>(forecastRequestQuery);
            var periodsJson = forecastResult["properties"]["periods"].ToJsonString();
            return periodsJson;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ForecastResult> Get([FromQuery] double latitude, [FromQuery] double longitude)
        {
            string json;
            var watch = Stopwatch.StartNew();
            var keyName = $"forecast:{latitude},{longitude}";
            json = await _redis.StringGetAsync(keyName);
            if (string.IsNullOrEmpty(json))
            {
                json = await GetForecast(latitude, longitude);
                var setTask = _redis.StringSetAsync(keyName, json);
                var expireTask = _redis.KeyExpireAsync(keyName, TimeSpan.FromSeconds(3600));
                await Task.WhenAll(setTask, expireTask);
            }

            var forecast = JsonSerializer.Deserialize<IEnumerable<ForecastResponse>>(json);
            watch.Stop();
            var result = new ForecastResult(forecast, watch.ElapsedMilliseconds);

            return result;
        }
    }

}
