using System.ComponentModel.DataAnnotations;

namespace WeatherServices.Dto
{
    public class LocationSearchDto
    {
        [Required]
        public string Location { get; set; }
    }
}