using Lab3.Core.Models;

namespace Lab3.Tests.Core.Models;

public class ShoppingCartTest
{
    private readonly ShoppingCart _cart = new();
    
    [Fact]
    public void AddItem_NewItem_ShouldAddToList()
    {
        // Arrange
        var product = new Product("Apple", 100m, 10);
        var item = new Item(product, 1);

        // Act
        _cart.AddItem(item);

        // Assert
        Assert.Single(_cart.Items);
        Assert.Equal(product.Id, _cart.Items.First().ProductId);
        Assert.Equal(1, _cart.Items.First().Count);
    }

    [Fact]
    public void AddItem_ExistingItem_ShouldIncrementCountCorrectly()
    {
        // Arrange
        var product = new Product("Banana", 50m, 5);
        _cart.AddItem(new Item(product, 2));

        // Act
        _cart.AddItem(new Item(product, 3));

        // Assert
        Assert.Single(_cart.Items); 
        Assert.Equal(5, _cart.Items.First().Count); // 2 + 3
        Assert.Equal(250m, _cart.ItemsPrice);
    }

    [Fact]
    public void AddItem_ItemWithZeroCount_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var product = new Product("Zero", 10m, 1);
        var item = new Item(product, 0); 

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _cart.AddItem(item));
    }

    [Fact]
    public void RemoveItem_CountGreaterThanOne_ShouldDecrementButStayInList()
    {
        // Arrange
        var product = new Product("Milk", 80m, 1000);
        _cart.AddItem(new Item(product, 2));

        // Act
        _cart.RemoveItem(product.Id);

        // Assert
        Assert.Single(_cart.Items); // Все еще в списке
        Assert.Equal(1, _cart.Items.First().Count); // Количество стало 1
    }

    [Fact]
    public void RemoveItem_CountHitsZero_ShouldBeRemovedFromListImmediately()
    {
        // Arrange
        var product = new Product("Bread", 40m, 300);
        _cart.AddItem(new Item(product, 1));

        // Act
        _cart.RemoveItem(product.Id);

        // Assert
        Assert.Empty(_cart.Items); 
    }

    [Fact]
    public void RemoveItem_UnknownId_ShouldThrowKeyNotFoundException()
    {
        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _cart.RemoveItem(Guid.NewGuid()));
    }

    [Fact]
    public void ItemsPriceAndWeight_ShouldSumCorrectly()
    {
        // Arrange
        var p1 = new Product("A", 100m, 10); 
        var p2 = new Product("B", 200m, 50); 

        _cart.AddItem(new Item(p1, 2)); 
        _cart.AddItem(new Item(p2, 1)); 
        
        // Act & Assert
        Assert.Equal(400m, _cart.ItemsPrice);
        Assert.Equal(70, _cart.ItemsWeight);
    }
    
    [Fact]
    public void CreateOrder_ShouldReturnBuilderLinkedToCart()
    {
        // Act
        var builder = _cart.CreateOrder();

        // Assert
        Assert.NotNull(builder);
    }
}