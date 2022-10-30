namespace CoffeeMachine.Api.Services.Date;

public class DateService : IDateService
{
    public DateTime GetCurrentDate()
    {
        return DateTime.Now;
    }

    public bool IsAprilFirst()
    {
        var date = DateTime.Now;
        return date.Month == 4 & date.Day == 1;
    }
}