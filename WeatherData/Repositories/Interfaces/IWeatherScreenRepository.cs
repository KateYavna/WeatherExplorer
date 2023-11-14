using WeatherData.Entities;

namespace WeatherData.Repositories.Interfaces
{
    public interface IWeatherScreenRepository: IRepository<WeatherScreen>
    {
        Task<WeatherScreen> GetLastAddedAsync();
    }
}