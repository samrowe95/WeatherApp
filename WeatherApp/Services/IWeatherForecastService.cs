namespace WeatherApp.Services
{
    public interface IWeatherForecastService<T> where T : class
    {
        Task<T> GetWeatherForecastAsync(string? location, int? days);
    }
}
