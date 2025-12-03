using Lab3.Core.Interfaces;

namespace Lab3.Core.Models;

public class BasicPriceCalculator(IDiscountsCalculator discountsCalculator, ITaxesCalculator taxesCalculator)
    : IPriceCalculator
{
    public OrderPrice CalculatePrice(ShoppingCart cart, IReceiptMethod receiptMethod,
        IEnumerable<IDiscount> discounts, IEnumerable<ITax> taxes)
    {
        var itemsPrice = cart.ItemsPrice;
        var deliveryCost = receiptMethod.CalculatePrice(cart.ItemsWeight, cart.ItemsPrice);
        var withDeliveryPrice = itemsPrice + deliveryCost;
        var discountsAmount = discountsCalculator.CalculateTotalDiscountAmount(withDeliveryPrice, discounts);
        var withDiscountAmount = withDeliveryPrice - discountsAmount;
        var taxAmount = taxesCalculator.CalculateTotalTaxAmount(withDiscountAmount, taxes);

        return new OrderPrice
        {
            ItemsPrice = itemsPrice,
            DeliveryCost = deliveryCost,
            DiscountAmount = discountsAmount,
            TaxAmount = taxAmount
        };
    }
}