using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using Dadata;
using Dadata.Model;
using RandomAPI.Models;

namespace RandomAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DadataController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly IDatabase _redis;

        public DadataController(HttpClient client, IConnectionMultiplexer muxer)
        {
            _client = client;
            _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("dadataCachingApp", "1.0"));
            _redis = muxer.GetDatabase();
        }
        private async Task<string> PostDadata(string BIK)
        {
            var token = "f515d742b50aa9404531d52f68ae9dc4f98ece9f";
            var api = new SuggestClientAsync(token);
            var result = await api.FindBank("044525225");
            return result.ToString();
        }

        [HttpPost(Name = "PostDadata")]
        public async Task<DadataResult> Post([FromQuery] string BIK)
        {
            string json;
            var watch = Stopwatch.StartNew();
            var keyName = $"dadata:{BIK}";
            json = await _redis.StringGetAsync(keyName);
            if (string.IsNullOrEmpty(json))
            {
                json = await PostDadata(BIK);
                var setTask = _redis.StringSetAsync(keyName, json);
                var expireTask = _redis.KeyExpireAsync(keyName, TimeSpan.FromSeconds(3600));
                await Task.WhenAll(setTask, expireTask);
            }

            var dadataResult = JsonSerializer.Deserialize<IEnumerable<SuggestResponse<Bank>>>(json);
            watch.Stop();
            var result = new DadataResult(dadataResult, watch.ElapsedMilliseconds);

            return result;
        }
    }
}
