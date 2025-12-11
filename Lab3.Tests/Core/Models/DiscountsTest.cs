using Lab3.Core.Interfaces;
using Lab3.Core.Models;
using Moq;

namespace Lab3.Tests.Core.Models;

public class DiscountsTest
{
    [Theory]
    [InlineData(10, 1000, 100)] // 10% от 1000 = 100
    [InlineData(50, 200, 100)]  // 50% от 200 = 100
    [InlineData(0, 1000, 0)]    // 0% = 0
    [InlineData(100, 500, 500)] // 100% = 500
    public void PercentDiscount_CalculateBenefit_ReturnsCorrectValue(int percent, decimal price, decimal expected)
    {
        var discount = new PercentDiscount(percent);
        var result = discount.CalculateBenefit(price);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void PercentDiscount_InvalidPercent_ThrowsArgumentException(int percent)
    {
        Assert.Throws<ArgumentException>(() => new PercentDiscount(percent));
    }
    
    [Fact]
    public void ScoresDiscount_ReturnsFixedAmount()
    {
        var discount = new ScoresDiscount(500m);
        var result = discount.CalculateBenefit(10000m); 
        Assert.Equal(500m, result);
    }
    
    [Fact]
    public void MaxAmountDecorator_BenefitExceedsLimit_ReturnsLimit()
    {
        // Arrange
        var mockDiscount = new Mock<IDiscount>();
        mockDiscount.Setup(d => d.CalculateBenefit(It.IsAny<decimal>())).Returns(1000m);

        const decimal maxAmount = 300m;
        var decorator = new MaxAmountDiscountDecorator(mockDiscount.Object, maxAmount);

        // Act
        var result = decorator.CalculateBenefit(5000m);

        // Assert
        Assert.Equal(300m, result); 
    }

    [Fact]
    public void MaxAmountDecorator_BenefitWithinLimit_ReturnsBaseBenefit()
    {
        // Arrange
        var mockDiscount = new Mock<IDiscount>();
        mockDiscount.Setup(d => d.CalculateBenefit(It.IsAny<decimal>())).Returns(100m);

        const decimal maxAmount = 300m;
        var decorator = new MaxAmountDiscountDecorator(mockDiscount.Object, maxAmount);

        // Act
        var result = decorator.CalculateBenefit(5000m);

        // Assert
        Assert.Equal(100m, result); 
    }

    [Fact]
    public void MaxAmountDecorator_ZeroOrNegativeLimit_ThrowsException()
    {
        var mockDiscount = new Mock<IDiscount>();
        Assert.Throws<ArgumentException>(() => new MaxAmountDiscountDecorator(mockDiscount.Object, 0));
        Assert.Throws<ArgumentException>(() => new MaxAmountDiscountDecorator(mockDiscount.Object, -10));
    }

    [Fact]
    public void MaxPercentDecorator_BenefitExceedsPercentCap_ReturnsCap()
    {
        // Arrange
        var mockDiscount = new Mock<IDiscount>();
        mockDiscount.Setup(d => d.CalculateBenefit(It.IsAny<decimal>())).Returns(500m);

        const decimal maxPercent = 10m; 
        var decorator = new MaxPercentDiscountDecorator(mockDiscount.Object, maxPercent);

        const decimal itemPrice = 2000m; 

        // Act
        var result = decorator.CalculateBenefit(itemPrice);

        // Assert
        Assert.Equal(200m, result);
    }

    [Fact]
    public void MaxPercentDecorator_InvalidPercent_ThrowsException()
    {
        var mockDiscount = new Mock<IDiscount>();
        Assert.Throws<ArgumentException>(() => new MaxPercentDiscountDecorator(mockDiscount.Object, -1));
        Assert.Throws<ArgumentException>(() => new MaxPercentDiscountDecorator(mockDiscount.Object, 101));
    }
}