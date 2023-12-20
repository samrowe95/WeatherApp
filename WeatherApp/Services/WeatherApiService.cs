using System.Net.Http.Headers;
using WeatherApp.ViewModels;


namespace WeatherApp.Services
{
    public class WeatherApiService : IWeatherForecastService<WeatherForecastVM>
    {
        private readonly HttpClient _httpClient;

        private const string _apiKey = "6b91c2df16654c22a26153716231712";

        public WeatherApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<WeatherForecastVM> GetWeatherForecastAsync(string? location, int? days)
        {
            string endpoint = "forecast.json?key=" + _apiKey;

            if (location != null)
            {
                endpoint += "&q=" + location;
            }

            if (days != null)
            {
                endpoint += "&days=" + days;
            }

            endpoint += "&aqi=no&alerts=no";

            return await GetAsync<WeatherForecastVM>(endpoint);
        }

        public async Task<T> GetAsync<T>(string query)
        {

            var response = await _httpClient.GetAsync(query);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<T>();
                return content;
            }
            else
                throw new HttpRequestException($"Failed to retrieve data. Status code: {response.StatusCode}");
        }


    }
}
