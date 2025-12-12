using Lab2.Core.Interfaces;
using Lab2.Core.Models;
using Moq;

namespace Lab2.Tests.Core.Models;

public class InventoryTests
{
    private readonly Inventory _inventory;
    private readonly Scale _scale;

    public InventoryTests()
    {
        _scale = new Scale(10, 0);
        _inventory = new Inventory(_scale);
    }

    private static IItem CreateMockItem(Guid? id = null)
    {
        var mock = new Mock<IItem>();
        mock.Setup(x => x.Id).Returns(id ?? Guid.NewGuid());
        return mock.Object;
    }

    [Fact]
    public void TryAdd_WhenSpaceAvailable_AddsItemAndReturnsTrue()
    {
        // Arrange
        var item = CreateMockItem();

        // Act
        var result = _inventory.TryAdd(item);

        // Assert
        Assert.True(result);
        
        Assert.True(_inventory.TryGetItem(item.Id, out var storedItem));
        Assert.Same(item, storedItem);
        Assert.Equal(1, _scale.CurrentValue);
    }

    [Fact]
    public void TryAdd_WhenInventoryIsFull_ReturnsFalse()
    {
        // Arrange
        var fullScale = new Scale(1, 1);
        var fullInventory = new Inventory(fullScale);
        var item = CreateMockItem();

        // Act
        var result = fullInventory.TryAdd(item);

        // Assert
        Assert.False(result);
        Assert.False(fullInventory.TryGetItem(item.Id, out _));
        Assert.Equal(1, fullScale.CurrentValue); 
    }

    [Fact]
    public void TryAdd_DuplicateItem_ReturnsFalse()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var item1 = CreateMockItem(itemId);
        var item2 = CreateMockItem(itemId); 

        _inventory.TryAdd(item1);

        // Act
        var result = _inventory.TryAdd(item2);

        // Assert
        Assert.False(result);
        Assert.Equal(1, _scale.CurrentValue); 
    }

    [Fact]
    public void TryRemove_ExistingItem_RemovesItAndDecrementsScale()
    {
        // Arrange
        var item = CreateMockItem();
        _inventory.TryAdd(item); 

        // Act
        var result = _inventory.TryRemove(item.Id, out var removedItem);

        // Assert
        Assert.True(result);
        Assert.Same(item, removedItem);
        Assert.False(_inventory.TryGetItem(item.Id, out _));
        Assert.Equal(0, _scale.CurrentValue); 
    }

    [Fact]
    public void TryRemove_NonExistingItem_ReturnsFalse()
    {
        // Act
        var result = _inventory.TryRemove(Guid.NewGuid(), out var removedItem);

        // Assert
        Assert.False(result);
        Assert.Null(removedItem);
        Assert.Equal(0, _scale.CurrentValue);
    }

    [Fact]
    public void TryGetItem_ExistingItem_ReturnsTrueAndItem()
    {
        // Arrange
        var item = CreateMockItem();
        _inventory.TryAdd(item);

        // Act
        var result = _inventory.TryGetItem(item.Id, out var retrievedItem);

        // Assert
        Assert.True(result);
        Assert.Same(item, retrievedItem);
    }

    [Fact]
    public void TryGetItem_NonExistingItem_ReturnsFalse()
    {
        // Act
        var result = _inventory.TryGetItem(Guid.NewGuid(), out var item);

        // Assert
        Assert.False(result);
        Assert.Null(item);
    }
}