using Microsoft.AspNetCore.Mvc;

namespace LocationsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private static readonly Dictionary<int, string> Locations = new()
        {
            { 1, "Southampton"},
            { 2, "Basingstoke" },
            { 3, "Reading" },
            { 4, "London" }
        };

        private readonly ILogger<LocationsController> _logger;

        public LocationsController(ILogger<LocationsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public string Get(int location)
        {
            if (Locations.ContainsKey(location)) { return Locations[location]; }

            return "Location UNKNOWN";
        }
    }
}