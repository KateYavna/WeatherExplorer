using WeatherData.Entities;
using WeatherServices.Dto;

namespace WeatherServices.Services.Interfaces
{
    public interface IOpenWeatherService
    {
        Task<IEnumerable<LocationDto>> GetLocations(LocationSearchDto locationSearchDTO);
        Task<LocationDto> GetLocationByZipCode(SearchLocationByZipCode searchLocationByZipCode);
        Task<WeatherDto> GetWeather(LocationDto location);
        Task<WeatherDto> RefreshWeatherDataAsync();
    }
}