namespace Lab3.Core.Interfaces;

public abstract class ADiscountDecorator(IDiscount discount) : IDiscount
{
    public virtual decimal CalculateBenefit(decimal price) => discount.CalculateBenefit(price);
}