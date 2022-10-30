namespace CoffeeMachine.Api.Services.Counter;

public interface ICounter{
    int GetCount();
    void CountNumber();
    void Reset();
}