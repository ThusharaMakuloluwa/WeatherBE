# Weather API – ASP.NET Core 8 (Backend)

This is the backend Web API for the Weather App.  
(Frontend → [WeatherUI](https://github.com/ThusharaMakuloluwa/WeatherUI.git))

The API:

- Loads a static list of cities from `Data/cities.json`
- Retrieves live weather information from **OpenWeather API**
- Protects all endpoints using **Auth0 JWT Authentication**
- Serves data to the Weather UI frontend

---

## Tech Stack

- **.NET**: .NET 8
- **Framework**: ASP.NET Core Web API
- **Auth**: Auth0 (JWT Bearer)
- **HTTP Client**: `HttpClientFactory`
- **JSON**: `System.Text.Json`

---

## Project Structure 

```text
WeatherBE/
  Controllers/
    WeatherController.cs
  Models/
    City.cs
    OpenWeatherResponse.cs
  Services/
    CityService.cs
    WeatherService.cs
    Interfaces/
      ICityService.cs
      IWeatherService.cs
  Options/
    OpenWeatherOptions.cs
  DTOs/
    WeatherResultsDTO.cs
  Data/
    cities.json
  appsettings.json
  Program.cs

