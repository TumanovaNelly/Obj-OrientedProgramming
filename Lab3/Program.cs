using Lab3.Core.Models;

namespace Lab3;

public static class Program
{
    public static void Main()
    {
        var chicken = new Product("Курыца", 599.90M, 600);
        var juice = new Product("ВкусНоСок", 159.80M, 500);
        var shoppingCart = new ShoppingCart();
        shoppingCart.AddItem(new Item(chicken, 2));
        shoppingCart.AddItem(new Item(juice, 1));
        var order = shoppingCart.CreateOrder()
            .SetReceiptMethod(new Delivery("Гражданка, д. 210, лит.Я, кв. 666"))
            .AddDiscount(new PercentDiscount(5))
            .AddDiscount(new MaxPercentDiscountDecorator(
                new ScoresDiscount(2000),
                50))
            .AddTax(new BasicTax())
            .Build(new BasicPriceCalculator(new BasicDiscountsCalculator(), new BasicTaxesCalculator()));
        Console.WriteLine(order.Status);
        order.Proceed();
        Console.WriteLine(order.GetCheque());
        Console.WriteLine(order.Status);
        order.Proceed();
        Console.WriteLine(order.Status);
        order.Proceed();
        Console.WriteLine(order.Status);
        order.Proceed();
        Console.WriteLine(order.Status);
    }
}