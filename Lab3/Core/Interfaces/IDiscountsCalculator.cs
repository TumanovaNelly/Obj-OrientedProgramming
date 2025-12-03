namespace Lab3.Core.Interfaces;

public interface IDiscountsCalculator
{
    public decimal CalculateTotalDiscountAmount(decimal price, IEnumerable<IDiscount> discounts);
}