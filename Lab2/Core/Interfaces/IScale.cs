namespace Lab2.Core.Interfaces;

public interface IScale
{
    public int MaxValue { get; }
    public int CurrentValue { get; }

    public bool IsEmpty => CurrentValue == 0;
    public bool IsFull => CurrentValue == MaxValue;
    
    public void Increment(int amount);
    public void Decrement(int amount);
    
    public void IncreaseScale(int amount);
}