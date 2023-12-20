using WeatherApp.ViewModels;

namespace WeatherApp.Services
{
    public class ForecastSessionService
    {
        private readonly ISession _session;
        private readonly string _sessionkey;

        public ForecastSessionService(ISession session, string sessionkey)
        {
            _session = session;
            _sessionkey = sessionkey;
        }

        public void AddForecast(WeatherForecastVM forecast)
        {
            var forecasts = new List<WeatherForecastVM>();

            if (GetForecasts() != null)
                forecasts.AddRange(GetForecasts());

            var existingForecast = forecasts.FirstOrDefault(n =>
             n.Location.Name == forecast.Location.Name &&
             n.Location.Region == forecast.Location.Region &&
             n.Location.Country == forecast.Location.Country);

            if (existingForecast != null)
            {
                forecasts.Remove(existingForecast);
            }

            forecasts.Insert(0, forecast);


            _session.Set(_sessionkey, forecasts);
        }

        public IEnumerable<WeatherForecastVM> GetForecasts()
        {
            return _session.Get<WeatherForecastVM>(_sessionkey) ?? Enumerable.Empty<WeatherForecastVM>();
        }
    }
}
