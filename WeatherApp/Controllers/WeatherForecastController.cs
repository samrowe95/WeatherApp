using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels;

namespace WeatherApp.Controllers
{
    public class WeatherForecastController : Controller
    {
        private readonly IWeatherForecastService<WeatherForecastVM> _weatherApiService;
        const string SessionKeyList = "_Forecasts";
        public string? SessionInfo_SessionTime { get; private set; }

        public WeatherForecastController(IWeatherForecastService<WeatherForecastVM> weatherApiService)
        {
            _weatherApiService = weatherApiService;
        }

        public IActionResult Index()
        {
            var sessionService = new ForecastSessionService(HttpContext.Session, SessionKeyList);
            return View("Index", sessionService.GetForecasts());
        }

        [HttpGet]
        public async Task<IActionResult> GetWeatherForecast(string location, int days)
        {
            var forecast = await _weatherApiService.GetWeatherForecastAsync(location, days);
            var sessionService = new ForecastSessionService(HttpContext.Session, SessionKeyList);
            sessionService.AddForecast(forecast);

            return PartialView("WeatherForecast", sessionService.GetForecasts());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
