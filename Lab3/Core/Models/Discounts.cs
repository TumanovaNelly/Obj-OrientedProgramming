using Lab3.Core.Interfaces;

namespace Lab3.Core.Models;

public class ScoresDiscount(decimal scoresAmount) : IDiscount
{
    public decimal CalculateBenefit(decimal price) 
        => scoresAmount;
}

public class PercentDiscount : IDiscount
{
    private readonly decimal _percent;
    
    public PercentDiscount(int percent)
    {
        if (percent is < 0 or > 100)
            throw new ArgumentException("Percent cannot be less than zero or more than 100");
        
        _percent = percent;
    }
    
    public decimal CalculateBenefit(decimal price) 
        => price * _percent / 100;
}


