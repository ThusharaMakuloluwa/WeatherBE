using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Text.Json;
using WeatherBE.DTOs;
using WeatherBE.Models;
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
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            
            var weatherData = JsonSerializer.Deserialize<OpenWeatherResponse>(json, options);
            
            if (weatherData == null) return null;

            var result = new WeatherResultDTO
            {
                CityId = weatherData.Id,
                CityName = weatherData.Name,
                Country = weatherData.Sys.Country,
                DateTime = DateTimeOffset.FromUnixTimeSeconds(weatherData.Dt)
                    .ToOffset(TimeSpan.FromSeconds(weatherData.Timezone))
                    .ToString("yyyy-MM-dd HH:mm:ss"),
                Temperature = weatherData.Main.Temp,
                TempMin = weatherData.Main.TempMin,
                TempMax = weatherData.Main.TempMax,
                Description = weatherData.Weather.FirstOrDefault()?.Description ?? "",
                Pressure = weatherData.Main.Pressure,
                Humidity = weatherData.Main.Humidity,
                Visibility = weatherData.Visibility,
                WindSpeed = weatherData.Wind.Speed,
                WindDegree = weatherData.Wind.Deg,
                Sunrise = DateTimeOffset.FromUnixTimeSeconds(weatherData.Sys.Sunrise)
                    .ToOffset(TimeSpan.FromSeconds(weatherData.Timezone))
                    .ToString("yyyy-MM-dd HH:mm:ss"),
                Sunset = DateTimeOffset.FromUnixTimeSeconds(weatherData.Sys.Sunset)
                    .ToOffset(TimeSpan.FromSeconds(weatherData.Timezone))
                    .ToString("yyyy-MM-dd HH:mm:ss")
            };

            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }
    }
}
