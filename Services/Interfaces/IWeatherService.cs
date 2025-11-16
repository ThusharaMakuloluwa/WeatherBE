using WeatherBE.DTOs;

namespace WeatherBE.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherResultDTO?> GetWeatherAsync(int cityId);
    }
}
