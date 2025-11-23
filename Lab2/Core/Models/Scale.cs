using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class Scale(int maxValue, int currentValue) : IScale
{
    public int MaxValue { get; private set; } = maxValue;
    public int CurrentValue { get; private set; } = currentValue;

    public void Increment(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Value cannot be negative");
        
        CurrentValue += amount;
        if (CurrentValue > MaxValue)
            CurrentValue = MaxValue;
    }

    public void Decrement(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Value cannot be negative");
        
        CurrentValue -= amount;
        if (CurrentValue < 0)
            CurrentValue = 0;
    }

    public void IncreaseMaxValue(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Value cannot be negative");
        
        MaxValue += amount;
    }

    public void DecreaseMaxValue(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Value cannot be negative");
        
        MaxValue -= amount;
        
        if (MaxValue < 0)
            MaxValue = 0;
        
        if (CurrentValue > MaxValue)
            CurrentValue = MaxValue;
    }
}