using Lab3.Core.Models;

namespace Lab3;

public static class Program
{
    public static void Main()
    {
        var shoppingCart = new ShoppingCart();
        shoppingCart.AddItem(new Item("Курыца", 600, 599.90M, 2));
        shoppingCart.AddItem(new Item("ВкусНоСок", 500, 159.80M, 1));
        var order = shoppingCart.CreateOrder()
            .SetReceiptMethod(new Delivery("Гражданка, д. 210, лит.Я, кв. 666"))
            .AddDiscount(new ScoresDiscount(10))
            .AddDiscount(new PercentDiscount(5))
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