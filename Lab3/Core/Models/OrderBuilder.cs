using Lab3.Core.Interfaces;

namespace Lab3.Core.Models;

public class OrderBuilder(ShoppingCart cart)
{
    private IReceiptMethod? _receiptMethod;
    private readonly List<IDiscount> _discounts = [];
    private readonly List<ITax> _taxes = [];

    public OrderBuilder SetReceiptMethod(IReceiptMethod receiptMethod)
    {
        _receiptMethod = receiptMethod;
        return this;
    }

    public OrderBuilder AddDiscount(IDiscount discount)
    {
        _discounts.Add(discount);
        return this;
    }

    public OrderBuilder AddTax(ITax tax)
    {
        _taxes.Add(tax);
        return this;
    }

    public Order Build(IPriceCalculator priceCalculator)
    {
        if (_receiptMethod is null)
            throw new InvalidOperationException("Receipt method is not selected");

        var totalPrice = priceCalculator.CalculatePrice(cart, _receiptMethod, _discounts, _taxes);

        return new Order(cart.Items, totalPrice);
    }
}