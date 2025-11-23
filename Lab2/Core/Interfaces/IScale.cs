namespace Lab2.Core.Interfaces;

public interface IScale
{
    public int MaxValue { get; }

    public int CurrentValue { get; }
    
    public bool IsOver => CurrentValue == 0;
    public bool IsComplete => CurrentValue == MaxValue;
    
    public void Increment(int amount);
    public void Decrement(int amount);
    
    public void IncreaseMaxValue(int amount);
    public void DecreaseMaxValue(int amount);
}