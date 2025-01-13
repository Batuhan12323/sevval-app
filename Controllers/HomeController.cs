
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherAppWithLocalAPI.Models;

namespace WeatherAppWithLocalAPI.Controllers
{
    public class HomeController : Controller
    {
        private const string LocalWeatherApiUrl = "http://localhost:5000/api/weather/{0}";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetWeather(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                ViewBag.Error = "Lütfen bir şehir adı giriniz.";
                return View("Index");
            }

            using (var client = new HttpClient())
            {
                try
                {
                    var url = string.Format(LocalWeatherApiUrl, city);
                    var response = await client.GetStringAsync(url);
                    var weatherData = JsonConvert.DeserializeObject<WeatherViewModel>(response);
                    return View("WeatherResult", weatherData);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = $"Hava durumu bilgisi alınamadı: {ex.Message}";
                    return View("Index");
                }
            }
        }
    }
}
    