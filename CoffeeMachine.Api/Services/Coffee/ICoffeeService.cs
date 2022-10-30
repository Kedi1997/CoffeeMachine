using CoffeeMachine.Api.DTOs;

namespace CoffeeMachine.Api.Services.Coffee;

public interface ICoffeeService
{
    public Task<CoffeeResponse> BrewCoffee();
}