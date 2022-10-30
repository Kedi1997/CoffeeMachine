using CoffeeMachine.Api.DTOs;
using CoffeeMachine.Api.Exceptions;
using CoffeeMachine.Api.Services.Counter;
using CoffeeMachine.Api.Services.Date;

namespace CoffeeMachine.Api.Services.Coffee;

public class CoffeeService : ICoffeeService
{
    private readonly ICounter _counter;
    private readonly IDateService _dateService;

    public CoffeeService(ICounter counter, IDateService dateService)
    {
        _counter = counter;
        _dateService = dateService;
    }

    public CoffeeResponse BrewCoffee()
    {
        if(_dateService.IsAprilFirst()){
            throw new AprilFirstException();
        }

        _counter.CountNumber();

        if(_counter.GetCount() % 5 == 0){
            _counter.Reset();
            throw new OutOfCoffeeException();
        }

        return new CoffeeResponse{
            Message = "Your piping hot coffee is ready",
            Prepared = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
        };
    }
}