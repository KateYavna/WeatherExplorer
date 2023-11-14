namespace WeatherData.Entities
{
    public class WeatherScreen: BaseEntity
    {
        public string WeatherDescription { get; set; }
        public double Temperature { get; set;}
        public double TemperatureFeelsLike { get; set;}
        public double TemperatureMax {  get; set;}
        public double TemperatureMin { get; set;}
        public int Humidity { get; set;}
        public double WindSpeed { get; set;}
        public double Latitude {  get; set;}
        public double Longitude { get; set;}
        public string Name { get; set;}
        public string Country { get; set;}
        public DateTime? ScreenTime { get; set;}= DateTime.UtcNow;
    }
}