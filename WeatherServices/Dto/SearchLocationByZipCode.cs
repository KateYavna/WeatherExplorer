using System.ComponentModel.DataAnnotations;

namespace WeatherServices.Dto
{
    public class SearchLocationByZipCode
    {
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Country { get; set; }
    }
}