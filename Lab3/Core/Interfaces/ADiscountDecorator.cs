namespace Lab3.Core.Interfaces;

public abstract class ADiscountDecorator : IDiscount
{
    private readonly IDiscount _discount;

    public ADiscountDecorator(IDiscount discount)
    {
        _discount = discount;
    }
    
    public virtual decimal CalculateBenefit(decimal price) => _discount.CalculateBenefit(price);
}