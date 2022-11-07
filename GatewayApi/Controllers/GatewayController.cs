using Microsoft.AspNetCore.Mvc;
using WeatherForcast;
using Newtonsoft.Json;

namespace GatewayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GatewayController : ControllerBase
    {
        private readonly ILogger<GatewayController> _logger;

        public GatewayController(ILogger<GatewayController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeather")]
        public async Task<WeatherResponse> Get(int location, int noOfdays)
        {
            var locationsClient = new HttpClient { BaseAddress = new Uri("http://locationsapi:5218") };

            var weatherClient = new HttpClient { BaseAddress = new Uri("http://weathersapi:5290") };

            var locationName = await locationsClient.GetStringAsync($"/Locations?location={location}");

            var weatherResponse = await weatherClient.GetAsync($"/WeatherForecast?noOfdays={noOfdays}");

            var weatherResponseContent = await weatherResponse.Content.ReadAsStringAsync();

            var forcast = JsonConvert.DeserializeObject<List<WeatherForecast>>(weatherResponseContent);

            return new WeatherResponse
            {
                Location = locationName,
                WeatherForecast = forcast ?? new List<WeatherForecast>()

            };
        }
    }
}