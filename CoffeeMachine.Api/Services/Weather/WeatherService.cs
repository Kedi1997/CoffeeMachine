
namespace CoffeeMachine.Api.Services.Weather;

public class WeatherService : IWeatherService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public WeatherService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<double> GetCurrentWeather()
    {
        try{
            
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.Timeout = TimeSpan.FromSeconds(5000);

            var response = await httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?lat=-37.81&lon=144.94&appid={_configuration["WeatherAPIKey"]}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var obj =  Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(content);
            var temp = obj?.main?.temp?.Value;

            return KelvinToCelsius(temp);

        }catch(Exception ex){
            return 0;
        }
    }

    public double KelvinToCelsius(double kelvin)
    {
        return (kelvin - 273.15);
    }
}