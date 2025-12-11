using Lab3.Core.Models;

namespace Lab3.Tests.Core.Models;

public class ReceiptMethodsTest
{
    [Fact]
    public void Delivery_PriceUnder1000_CalculatesCostBasedOnWeight()
    {
        var delivery = new Delivery("Address");
        const decimal weight = 100m;
        const decimal price = 999m;

        var cost = delivery.CalculatePrice(weight, price);

        Assert.Equal(5m, cost);
    }

    [Fact]
    public void Delivery_Price1000OrMore_IsFree()
    {
        var delivery = new Delivery("Address");
        const decimal weight = 5000m;
        const decimal price = 1000m; 

        var cost = delivery.CalculatePrice(weight, price);

        Assert.Equal(0m, cost);
    }
        
    [Fact]
    public void PickUp_AlwaysFree()
    {
        var pickup = new PuckUp("Address");
        var cost = pickup.CalculatePrice(10000m, 100000m);
        Assert.Equal(0m, cost);
    }
}