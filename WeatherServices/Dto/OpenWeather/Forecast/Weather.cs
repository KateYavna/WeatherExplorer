namespace WeatherServices.Dto.OpenWeather.Forecast
{
    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public string iconImgSrc
        {
            get
            {
                return $"http://openweathermap.org/img/wn/{icon}@2x.png";
            }
        }
    }
}