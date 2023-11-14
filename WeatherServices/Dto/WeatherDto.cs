using WeatherServices.Dto.OpenWeather.Current;
using WeatherServices.Dto.OpenWeather.Forecast;

namespace WeatherServices.Dto
{
    public class WeatherDto
    {
        public LocationDto Location { get; set; }

        public CurrentWeatherDto CurrentWeather { get; set; }

        public WeatherForecastDto WeatherForecast { get; set; }       
    }
}