namespace RandomAPI.Models
{
    public class ForecastResult
    {
        public long ElapsedTime { get; }
        public ForecastResponse Forecast { get; }

        public ForecastResult(ForecastResponse forecast, long elapsedTime)
        {
            Forecast = forecast;
            ElapsedTime = elapsedTime;
        }
    }
}
