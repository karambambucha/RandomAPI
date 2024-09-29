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

        private async Task<string> GetForecast(string location, string key)
        {
            var pointsRequestQuery = $"https://api.weatherapi.com/v1/current.json?key={key}&q={location}&aqi=yes"; //get the URI
            var result = await _client.GetFromJsonAsync<JsonObject>(pointsRequestQuery);
            return result.ToJsonString();
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ForecastResult> Get([FromQuery] string location, [FromQuery] string key)
        {
            string json;
            var watch = Stopwatch.StartNew();
            var keyName = $"forecast:{location}";
            json = await _redis.StringGetAsync(keyName);
            if (string.IsNullOrEmpty(json))
            {
                json = await GetForecast(location, key);
                var setTask = _redis.StringSetAsync(keyName, json);
                var expireTask = _redis.KeyExpireAsync(keyName, TimeSpan.FromSeconds(3600));
                await Task.WhenAll(setTask, expireTask);
            }

            var forecast = JsonSerializer.Deserialize<ForecastResponse>(json);
            watch.Stop();
            var result = new ForecastResult(forecast, watch.ElapsedMilliseconds);

            return result;
        }
    }

}
