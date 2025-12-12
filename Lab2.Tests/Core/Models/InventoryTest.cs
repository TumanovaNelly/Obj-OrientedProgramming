namespace Lab2.Tests.Core.Models;

public class InventoryTests
{
    [Fact]
    public void TryAdd_EmptyInventory_ReturnsTrueAndAddsItem()
    {
        // Arrange
        var inventory = TestDataFactory.CreateInventory(capacity: 5);
        var item = TestDataFactory.CreateSword();

        // Act
        var result = inventory.TryAdd(item);

        // Assert
        Assert.True(result);
        
        Assert.True(inventory.TryGetItem(item.Id, out var retrievedItem));
        Assert.Equal(item, retrievedItem);
    }

    [Fact]
    public void TryAdd_FullInventory_ReturnsFalse()
    {
        // Arrange
        var inventory = TestDataFactory.CreateInventory(capacity: 1);
        var item1 = TestDataFactory.CreateSword("Sword 1");
        var item2 = TestDataFactory.CreateSword("Sword 2");

        inventory.TryAdd(item1);

        // Act
        var result = inventory.TryAdd(item2);

        // Assert
        Assert.False(result);
        
        Assert.False(inventory.TryGetItem(item2.Id, out _));
    }

    [Fact]
    public void TryAdd_DuplicateItem_ReturnsFalse()
    {
        // Arrange
        var inventory = TestDataFactory.CreateInventory(capacity: 10);
        var item = TestDataFactory.CreateSword();

        inventory.TryAdd(item);

        // Act
        var result = inventory.TryAdd(item);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TryRemove_ExistingItem_RemovesAndReturnsTrue()
    {
        // Arrange
        var inventory = TestDataFactory.CreateInventory(capacity: 5);
        var item = TestDataFactory.CreateSword();
        inventory.TryAdd(item);

        // Act
        var result = inventory.TryRemove(item.Id, out var removedItem);

        // Assert
        Assert.True(result);
        Assert.Equal(item, removedItem);
        
        Assert.False(inventory.TryGetItem(item.Id, out _));
    }

    [Fact]
    public void TryRemove_NonExistingItem_ReturnsFalse()
    {
        // Arrange
        var inventory = TestDataFactory.CreateInventory(capacity: 5);
        var randomId = Guid.NewGuid();

        // Act
        var result = inventory.TryRemove(randomId, out var removedItem);

        // Assert
        Assert.False(result);
        Assert.Null(removedItem);
    }
}