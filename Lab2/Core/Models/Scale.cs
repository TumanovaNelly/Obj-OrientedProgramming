using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class Scale(int maxValue, int currentValue) : IScale
{
    public int MaxValue { get; private set; } = maxValue;
    public int CurrentValue { get; private set; } = currentValue;
    
    public void Increment(int amount)
        => CurrentValue = int.Min(MaxValue, CurrentValue + amount);

    public void Decrement(int amount)
        => CurrentValue = int.Max(0, CurrentValue - amount);

    public void IncreaseScale(int amount)
        => MaxValue += amount;
}