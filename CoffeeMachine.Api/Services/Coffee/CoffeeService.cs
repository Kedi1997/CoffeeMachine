using CoffeeMachine.Api.DTOs;
using CoffeeMachine.Api.Exceptions;
using CoffeeMachine.Api.Services.Counter;
using CoffeeMachine.Api.Services.Date;
using CoffeeMachine.Api.Services.Weather;

namespace CoffeeMachine.Api.Services.Coffee;

public class CoffeeService : ICoffeeService
{
    private readonly ICounter _counter;
    private readonly IDateService _dateService;
    private readonly IWeatherService _weatherService;

    public CoffeeService(ICounter counter, IDateService dateService,IWeatherService weatherService)
    {
        _counter = counter;
        _dateService = dateService;
        _weatherService = weatherService;
    }

    public async Task<CoffeeResponse> BrewCoffee()
    {
        try{

            if(_dateService.IsAprilFirst()){
                throw new AprilFirstException();
            }

            _counter.CountNumber();

            if(_counter.GetCount() % 5 == 0){
                _counter.Reset();
                throw new OutOfCoffeeException();
            }

            var temp = await _weatherService.GetCurrentWeather();
        
            var response = new CoffeeResponse();
            response.Message = temp >= 30 ? "Your refreshing iced coffee is ready" :  "Your piping hot coffee is ready";
            response.Prepared =_dateService.GetCurrentDate().ToString("yyyy-MM-ddTHH:mm:sszzz");
            return response;
        }catch(Exception ex){
            throw;
        }
    }
}