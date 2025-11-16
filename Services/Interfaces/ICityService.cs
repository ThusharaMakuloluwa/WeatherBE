using WeatherBE.Models;

namespace WeatherBE.Services.Interfaces
{
    public interface ICityService
    {
        IReadOnlyList<City> GetAll();
        City? GetByCode(int code);
    }
}
