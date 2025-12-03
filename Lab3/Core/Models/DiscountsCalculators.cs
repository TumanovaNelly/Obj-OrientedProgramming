using Lab3.Core.Interfaces;

namespace Lab3.Core.Models;

public class BasicDiscountsCalculator : IDiscountsCalculator
{
    public decimal CalculateTotalDiscountAmount(decimal price, IEnumerable<IDiscount> discounts)
    {
        decimal totalDiscount = 0M;
        foreach (var discount in discounts)
        {
            totalDiscount += discount.CalculateBenefit(price - totalDiscount);
        }
        return totalDiscount;
    }
}