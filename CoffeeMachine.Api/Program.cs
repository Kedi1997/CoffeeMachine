using CoffeeMachine.Api.Services.Coffee;
using CoffeeMachine.Api.Services.Counter;
using CoffeeMachine.Api.Services.Date;
using CoffeeMachine.Api.Services.Weather;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddControllers();
    // builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddScoped<ICoffeeService, CoffeeService>();
    builder.Services.AddSingleton<ICounter, Counter>();
    builder.Services.AddSingleton<IDateService, DateService>();
    builder.Services.AddScoped<IWeatherService, WeatherService>();
    builder.Services.AddHttpClient();

}


var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}

app.Run();
