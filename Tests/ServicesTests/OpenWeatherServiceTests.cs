using Moq.Protected;
using Moq;
using WeatherData.Repositories.Interfaces;
using WeatherServices.Dto;
using WeatherServices.Services;
using System.Text;
using System.Text.Json;
using WeatherServices.Dto.OpenWeather.Current;
using WeatherServices.Dto.OpenWeather.Forecast;
using WeatherData.Entities;

namespace Tests.ServicesTests
{
    [TestFixture]
    public class OpenWeatherServiceTests
    {
        private Mock<IWeatherScreenRepository> _weatherScreenRepositoryMock;
        private Mock<Microsoft.Extensions.Configuration.IConfiguration> _configurationMock;
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private HttpClient _httpClient;
        private OpenWeatherService _openWeatherService;

        [SetUp]
        public void Setup()
        {
            _weatherScreenRepositoryMock = new Mock<IWeatherScreenRepository>();
            _configurationMock = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _openWeatherService = new OpenWeatherService(_configurationMock.Object, _weatherScreenRepositoryMock.Object);
            _openWeatherService.client = _httpClient;
        }

        [Test]
        public async Task GetLocations_ReturnsLocations()
        {
            // Arrange
            var locationSearchDto = new LocationSearchDto { Location = "London" };
            var expectedLocations = new List<LocationDto> { new LocationDto { name = "London" } };

            _configurationMock.Setup(c => c["OpenWeatherAPIKey"]).Returns("ApiKey");
            _httpMessageHandlerMock.SetupResponse(expectedLocations);

            // Act
            var result = await _openWeatherService.GetLocations(locationSearchDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLocations.Count, result.Count());
            foreach (var expectedLocation in expectedLocations)
            {
                Assert.IsTrue(result.Any(r => r.name == expectedLocation.name));
            }
        }

        [Test]
        public async Task GetLocationByZipCode_ReturnsLocation()
        {
            // Arrange
            var searchLocationByZipCode = new SearchLocationByZipCode { ZipCode = "69002", Country = "UA" };
            var expectedLocation = new LocationDto { name = "Zaporizhzhia" };

            _configurationMock.Setup(c => c["OpenWeatherAPIKey"]).Returns("ApiKey");
            _httpMessageHandlerMock.SetupResponse(expectedLocation);

            // Act
            var result = await _openWeatherService.GetLocationByZipCode(searchLocationByZipCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLocation.name, result.name);

        }              
    }

    public static class HttpMessageHandlerExtensions
    {
        public static void SetupResponse<T>(this Mock<HttpMessageHandler> handler, T responseContent)
        {
            var jsonString = JsonSerializer.Serialize(responseContent);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = content,
                });
        }
    }    
}