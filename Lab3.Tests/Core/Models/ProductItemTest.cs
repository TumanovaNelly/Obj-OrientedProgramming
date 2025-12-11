using Lab3.Core.Models;

namespace Lab3.Tests.Core.Models;

public class ProductItemTest
{
    [Fact]
    public void Product_Constructor_SetsPropertiesCorrectly()
    {
        const string name = "Test Product";
        const decimal price = 150.5m;
        const int weight = 500;

        var product = new Product(name, price, weight);

        Assert.Equal(name, product.Name);
        Assert.Equal(price, product.Price);
        Assert.Equal(weight, product.Weight);
        Assert.NotEqual(Guid.Empty, product.Id);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Product_NegativePrice_ThrowsArgumentException(decimal invalidPrice)
    {
        Assert.Throws<ArgumentException>(() => new Product("Name", invalidPrice, 10));
    }

    [Fact]
    public void Product_NegativeWeight_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Product("Name", 100m, -5));
    }

    [Fact]
    public void Product_SetNegativePrice_ThrowsArgumentException()
    {
        var product = new Product("Name", 100m, 10);
        Assert.Throws<ArgumentException>(() => product.Price = -50m);
    }

    [Fact]
    public void Item_CalculatesTotalPriceAndWeight_Correctly()
    {
        const decimal price = 100m;
        const int weight = 20;
        const int count = 5;
        var product = new Product("Item", price, weight);
            
        // Act
        var item = new Item(product, count);

        // Assert
        Assert.Equal(500m, item.TotalPrice);
        Assert.Equal(100, item.TotalWeight);
    }

    [Fact]
    public void Item_SetNegativeCount_ThrowsArgumentException()
    {
        var product = new Product("Item", 100m, 10);
        var item = new Item(product, 1);

        Assert.Throws<ArgumentException>(() => item.Count = -1);
    }
}