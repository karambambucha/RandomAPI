namespace RandomAPI.Models
{
    public class ForecastResult
    {
        public long ElapsedTime { get; }
        public IEnumerable<ForecastResponse> Forecasts { get; }

        public ForecastResult(IEnumerable<ForecastResponse> forecasts, long elapsedTime)
        {
            Forecasts = forecasts;
            ElapsedTime = elapsedTime;
        }
    }
}
