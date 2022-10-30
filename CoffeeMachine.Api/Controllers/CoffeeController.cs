using CoffeeMachine.Api.Exceptions;
using CoffeeMachine.Api.Services.Coffee;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.Api.Controllers;

[ApiController]
public class CoffeeController : ControllerBase{

    private readonly ICoffeeService _coffeeService;

    public CoffeeController(ICoffeeService coffeeService)
    {
        _coffeeService = coffeeService;
    }

    [HttpGet]
    [Route("brew-coffee")]
    public IActionResult BrewCoffee()
    {
        try
        {
            var response = _coffeeService.BrewCoffee();
            return Ok(response);

        }
        catch(OutOfCoffeeException ex)
        {
            // return 503 Service Unavailable with empty response body
            return StatusCode(503,"");
        }
        catch(AprilFirstException ex)
        {
            return StatusCode(418,"");
        }
        
    }

}