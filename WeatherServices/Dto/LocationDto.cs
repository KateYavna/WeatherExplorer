namespace WeatherServices.Dto
{
    public class LocationDto
    {
        public string name { get; set; }
        public LocalNames local_names { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string zip {  get; set; }
        public string countryFlagImgSrc
        {
            get
            {
                return $"https://flagsapi.com/{country}/shiny/64.png";
            }
        }
    }
}