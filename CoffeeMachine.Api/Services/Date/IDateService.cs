namespace CoffeeMachine.Api.Services.Date;

public interface IDateService
{
    DateTime GetCurrentDate();
    bool IsAprilFirst();
}