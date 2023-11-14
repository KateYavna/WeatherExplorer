using Microsoft.Extensions.Configuration;
using WeatherData.Repositories.Interfaces;
using WeatherServices.Dto;
using WeatherServices.Dto.OpenWeather.Current;
using WeatherServices.Dto.OpenWeather.Forecast;
using WeatherServices.Services.Interfaces;

namespace WeatherServices.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        public HttpClient client;
        private readonly string apiKey;
        private readonly IWeatherScreenRepository _weatherScreenRepository;
        public OpenWeatherService(IConfiguration configuration, IWeatherScreenRepository weatherScreenRepository)
        {
            client = new();
            apiKey = configuration["OpenWeatherAPIKey"];
            _weatherScreenRepository = weatherScreenRepository;
        }

        public async Task<IEnumerable<LocationDto>> GetLocations(LocationSearchDto locationSearchDto)
        {
            IEnumerable<LocationDto> locations = null;
            string requestUri = GetGeoRequestUri(locationSearchDto);
            HttpResponseMessage response = await client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
                locations = await response.Content.ReadAsAsync<IEnumerable<LocationDto>>();
            return locations;
        }

        public async Task<LocationDto> GetLocationByZipCode(SearchLocationByZipCode searchLocationByZipCode)
        {
            LocationDto location = null;
            string requestUri = GetGeoRequestByZipCodeUri(searchLocationByZipCode);
            HttpResponseMessage response = await client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode) 
                location = await response.Content.ReadAsAsync<LocationDto>();
            return location;
        }
        public async Task<WeatherDto> GetWeather(LocationDto location)
        {
            var currentWeather = await GetCurrentWeather(location);
            var weatherForecast = await GetWeatherForecast(location);

            return new WeatherDto
            {
                Location = location,
                CurrentWeather = currentWeather ?? new CurrentWeatherDto(),
                WeatherForecast = weatherForecast ?? new WeatherForecastDto()
            };
        }
        public async Task<WeatherDto> RefreshWeatherDataAsync()
        {
            var weatherScreen = await _weatherScreenRepository.GetLastAddedAsync();
            var locationDto = new LocationDto()
            {
                name = weatherScreen.Name,
                lon = weatherScreen.Longitude,
                lat = weatherScreen.Latitude,
                country = weatherScreen.Country,
            };
           return await GetWeather(locationDto);
        }
        private async Task<CurrentWeatherDto> GetCurrentWeather(LocationDto location)
        {
            CurrentWeatherDto currentWeather = new();
            string requestUri = GetCurrentWeatherRequestUri(location);
            HttpResponseMessage response = await client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
                currentWeather = await response.Content.ReadAsAsync<CurrentWeatherDto>();
            return currentWeather;
        }

        private async Task<WeatherForecastDto> GetWeatherForecast(LocationDto location)
        {
            WeatherForecastDto weatherForecast = new();
            string requestUri = GetWeatherForecastRequestUri(location);
            HttpResponseMessage response = await client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
                weatherForecast = await response.Content.ReadAsAsync<WeatherForecastDto>();
            return weatherForecast;
        }
        private string GetGeoRequestUri(LocationSearchDto locationSearchDto) =>
           $"https://api.openweathermap.org/geo/1.0/direct?q={locationSearchDto.Location}&limit=5&appid={apiKey}";
        private string GetGeoRequestByZipCodeUri(SearchLocationByZipCode searchLocationByZipCode) =>
            $"http://api.openweathermap.org/geo/1.0/zip?zip={searchLocationByZipCode.ZipCode},{searchLocationByZipCode.Country}&appid={apiKey}";
        private string GetCurrentWeatherRequestUri(LocationDto location) =>
                    $"https://api.openweathermap.org/data/2.5/weather?lat={location.lat}&lon={location.lon}&units=metric&appid={apiKey}";
        private string GetWeatherForecastRequestUri(LocationDto location) =>
                    $"https://api.openweathermap.org/data/2.5/forecast?lat={location.lat}&lon={location.lon}&units=metric&appid={apiKey}";
    }
}