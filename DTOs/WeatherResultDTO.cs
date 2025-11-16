namespace WeatherBE.DTOs
{
    public class WeatherResultDTO
    {
        public string CityName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Temperature { get; set; }
    }
}
