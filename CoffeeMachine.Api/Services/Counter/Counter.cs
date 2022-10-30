namespace CoffeeMachine.Api.Services.Counter;

public class Counter : ICounter
{
    private int _count = 0;    

    public int GetCount()
    {
        return _count;
    }

    public void CountNumber()
    {
        _count++;
    }

    public void Reset(){
        _count = 0;
    }

}