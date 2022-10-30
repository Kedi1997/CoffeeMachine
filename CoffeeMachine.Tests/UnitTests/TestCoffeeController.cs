using CoffeeMachine.Api.Controllers;
using CoffeeMachine.Api.DTOs;
using CoffeeMachine.Api.Services.Coffee;
using CoffeeMachine.Api.Services.Counter;
using CoffeeMachine.Api.Services.Date;
using CoffeeMachine.Api.Services.Weather;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CoffeeMachine.Tests.UnitTests;

public class TestCoffeeController
{

    private readonly Mock<ICoffeeService> _mockCoffeeService;
    private readonly Mock<ICounter> _mockCounter;
    private readonly Mock<IDateService> _mockDateService;
    private readonly Mock<IWeatherService> _mockWeatherService;

    public TestCoffeeController()
    {
        _mockCounter = new Mock<ICounter>();
        _mockDateService = new Mock<IDateService>();
        _mockCoffeeService = new Mock<ICoffeeService>();
        _mockWeatherService = new Mock<IWeatherService>();

    }

    [Fact]
    public async void BrewCoffee_OnSuccess_ReturnCoffeeResponse(){

        // Arrange
        var coffeeResponse = new CoffeeResponse{
            Message = "Your piping hot coffee is ready",
            Prepared = new DateTime(2022,10,29,19,0,0).ToString()
        };
        _mockCoffeeService.Setup(x => x.BrewCoffee()).ReturnsAsync(coffeeResponse);
        var coffeeController = new CoffeeController(_mockCoffeeService.Object);

        // Act
        var result = (OkObjectResult) await coffeeController.BrewCoffee();

        // Assert
        result.StatusCode.Should().Be(200);
        result.Value.Should().Be(coffeeResponse);
    }

    [Fact]
    public async void BrewCoffee_OutOfCoffee_Return503ServiceUnavailable(){

        // Arrange
        _mockCounter.Setup(x => x.GetCount()).Returns(5);
        var coffeeController = new CoffeeController(new CoffeeService(_mockCounter.Object,_mockDateService.Object, _mockWeatherService.Object));

        // Act
        var result = (ObjectResult) await coffeeController.BrewCoffee();

        // Assert
        result.StatusCode.Should().Be(503);
        result.Value.Should().Be(string.Empty);
        _mockCounter.Verify(x => x.Reset(), Times.Once());
    }

    [Fact]
    public async void BrewCoffee_OnAprilFirst_Return418ImATeapot(){

        // Arrange
        _mockDateService.Setup(x => x.IsAprilFirst()).Returns(true);
        var coffeeController = new CoffeeController(new CoffeeService(_mockCounter.Object,_mockDateService.Object, _mockWeatherService.Object));

        // Act
        var result = (ObjectResult) await coffeeController.BrewCoffee();

        // Assert
        result.StatusCode.Should().Be(418);
        result.Value.Should().Be(string.Empty);
        _mockDateService.Verify(x => x.IsAprilFirst(), Times.Once());
    }
}