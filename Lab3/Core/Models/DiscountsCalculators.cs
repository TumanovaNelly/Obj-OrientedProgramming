using Lab3.Core.Interfaces;

namespace Lab3.Core.Models;

public class BasicDiscountsCalculator : IDiscountsCalculator
{
    public decimal CalculateTotalDiscountAmount(decimal price, IEnumerable<IDiscount> discounts) 
        => discounts.Aggregate(0M, (current, discount) 
            => current + discount.CalculateBenefit(price - current));
}