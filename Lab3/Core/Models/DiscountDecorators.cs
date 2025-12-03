using Lab3.Core.Interfaces;

namespace Lab3.Core.Models;

public class MaxAmountDiscountDecorator : ADiscountDecorator
{
    private readonly decimal _maxAmount;

    public MaxAmountDiscountDecorator(IDiscount discount, decimal maxAmount) : base(discount)
    {
        if (maxAmount <= 0)
            throw new ArgumentException("Max amount must be positive", nameof(maxAmount));
        
        _maxAmount = maxAmount;
    }
    
    public override decimal CalculateBenefit(decimal price)
    {
        var baseBenefit = base.CalculateBenefit(price);
        return Math.Min(baseBenefit, _maxAmount);
    }
}

public class MaxPercentDiscountDecorator : ADiscountDecorator
{
    private readonly decimal _maxPercent;

    public MaxPercentDiscountDecorator(IDiscount discount, decimal maxPercent) : base(discount)
    {
        if (maxPercent is < 0 or > 100)
            throw new ArgumentException("Percent cannot be less than zero or more than 100");
        
        _maxPercent = maxPercent;
    }
    
    public override decimal CalculateBenefit(decimal price)
    {
        var baseBenefit = base.CalculateBenefit(price);
        return Math.Min(baseBenefit, price * _maxPercent / 100);
    }
}
