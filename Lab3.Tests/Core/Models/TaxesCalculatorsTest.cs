using Lab3.Core.Interfaces;
using Lab3.Core.Models;
using Moq;

namespace Lab3.Tests.Core.Models;

public class TaxesCalculatorsTest
{
    [Fact]
    public void DiscountsCalculator_ApplySequentially_CalculatesCorrectly()
    {
        // Arrange
        var calculator = new BasicDiscountsCalculator();
            
        var d1 = new Mock<IDiscount>();
        d1.Setup(x => x.CalculateBenefit(It.IsAny<decimal>())).Returns(100m);
            
        var d2 = new PercentDiscount(10); 

        var discounts = new List<IDiscount> { d1.Object, d2 };
        const decimal initialPrice = 1000m;

        // Act
        var totalDiscount = calculator.CalculateTotalDiscountAmount(initialPrice, discounts);

        // Assert
        Assert.Equal(190m, totalDiscount);
    }

    [Fact]
    public void PriceCalculator_FullFlow_ReturnsCorrectOrderPrice()
    {
        // Arrange
        var mockDiscountCalc = new Mock<IDiscountsCalculator>();
        mockDiscountCalc.Setup(x => x.CalculateTotalDiscountAmount(It.IsAny<decimal>(), It.IsAny<IEnumerable<IDiscount>>()))
            .Returns(50m); 

        var mockTaxCalc = new Mock<ITaxesCalculator>();
        mockTaxCalc.Setup(x => x.CalculateTotalTaxAmount(It.IsAny<decimal>(), It.IsAny<IEnumerable<ITax>>()))
            .Returns(20m);

        var mockReceipt = new Mock<IReceiptMethod>();
        mockReceipt.Setup(x => x.CalculatePrice(It.IsAny<decimal>(), It.IsAny<decimal>()))
            .Returns(200m); 

        var calc = new BasicPriceCalculator(mockDiscountCalc.Object, mockTaxCalc.Object);

        var cart = new ShoppingCart();
        cart.AddItem(new Item(new Product("A", 1000m, 1), 1));

        // Act
        var result = calc.CalculatePrice(cart, mockReceipt.Object, [], []);

        // Assert
        Assert.Equal(1000m, result.ItemsPrice);
        Assert.Equal(200m, result.DeliveryCost);
        Assert.Equal(50m, result.DiscountAmount);
        Assert.Equal(20m, result.TaxAmount);
        
        Assert.Equal(1170m, result.TotalAmount);
    }
        
    [Fact]
    public void BasicTax_Calculates20Percent()
    {
        var tax = new BasicTax();
        var result = tax.CalculateAmount(100m);
        Assert.Equal(20m, result);
    }
}