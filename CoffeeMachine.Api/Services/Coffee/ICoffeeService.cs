using CoffeeMachine.Api.DTOs;

namespace CoffeeMachine.Api.Services.Coffee;

public interface ICoffeeService
{
    public CoffeeResponse BrewCoffee();
}