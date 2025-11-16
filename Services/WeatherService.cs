using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Text.Json;
using WeatherBE.DTOs;
using WeatherBE.Options;
using WeatherBE.Services.Interfaces;

namespace WeatherBE.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMemoryCache _cache;
        private readonly OpenWeatherOptions _options;

        public WeatherService(IHttpClientFactory clientFactory, IMemoryCache cache, IOptions<OpenWeatherOptions> options) 
        {
            _clientFactory = clientFactory;
            _cache = cache;
            _options = options.Value;
        }

        public async Task<WeatherResultDTO?> GetWeatherAsync(int cityId)
        {
            var cacheKey = $"weather_{cityId}";
            if (_cache.TryGetValue(cacheKey, out WeatherResultDTO cached))
            {
                return cached;
            }

            var client = _clientFactory.CreateClient("OpenWeather");
            var url = $"{_options.BaseUrl}/weather?id={cityId}&appid={_options.ApiKey}&units=metric";

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var result = new WeatherResultDTO
            {
                CityName = root.GetProperty("name").GetString() ?? "",
                Description = root.GetProperty("weather")[0].GetProperty("description").GetString() ?? "",
                Temperature = root.GetProperty("main").GetProperty("temp").GetDouble()
            };

            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }
    }
}
