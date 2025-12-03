using Lab3.Core.Models;

namespace Lab3.Core.Interfaces;

public interface IPriceCalculator
{
    public OrderPrice CalculatePrice(ShoppingCart cart, IReceiptMethod receiptMethod, 
        IEnumerable<IDiscount> discounts, IEnumerable<ITax> taxes);
}