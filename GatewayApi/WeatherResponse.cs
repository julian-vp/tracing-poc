using WeatherForcast;

namespace GatewayApi
{
    public class WeatherResponse
    {
        public string Location { get; set; }

        public List<WeatherForecast> WeatherForecast { get; set; }
    }
}
