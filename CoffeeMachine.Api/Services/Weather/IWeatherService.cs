namespace CoffeeMachine.Api.Services.Weather;

public interface IWeatherService
{
    public Task<double> GetCurrentWeather();
}