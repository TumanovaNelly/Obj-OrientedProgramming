using Lab2.Core.Interfaces;
using Lab2.Core.Models;
using Moq;

namespace Lab2.Tests.Core.Models;

public class InventoryTest
{
    private Mock<IItem> CreateMockItem(int weight, string name = "Item")
    {
        var mock = new Mock<IItem>();
        mock.Setup(x => x.Id).Returns(Guid.NewGuid());
        mock.Setup(x => x.Weight).Returns(weight);
        mock.Setup(x => x.Name).Returns(name);
        return mock;
    }

    [Fact]
    public void Constructor_ShouldInitializeEmpty()
    {
        // Arrange
        var inventory = new Inventory(100);

        // Act & Assert
        Assert.Empty(inventory.Items);
    }

    [Fact]
    public void TryAdd_ValidItemWithinCapacity_ShouldReturnTrueAndAddItem()
    {
        // Arrange
        var inventory = new Inventory(10);
        var itemMock = CreateMockItem(5);

        // Act
        bool result = inventory.TryAdd(itemMock.Object);

        // Assert
        Assert.True(result);
        Assert.Single(inventory.Items); 
        Assert.Same(itemMock.Object, inventory.Items[itemMock.Object.Id]);
    }

    [Fact]
    public void TryAdd_ItemExceedingCapacity_ShouldReturnFalseAndNotAdd()
    {
        // Arrange
        var inventory = new Inventory(10);
        var itemMock = CreateMockItem(11);

        // Act
        bool result = inventory.TryAdd(itemMock.Object);

        // Assert
        Assert.False(result);
        Assert.Empty(inventory.Items);
    }

    [Fact]
    public void TryAdd_DuplicateItem_ShouldReturnFalse()
    {
        // Arrange
        var inventory = new Inventory(20);
        var itemMock = CreateMockItem(5);

        inventory.TryAdd(itemMock.Object);

        // Act
        bool result = inventory.TryAdd(itemMock.Object); 

        // Assert
        Assert.False(result);
        Assert.Single(inventory.Items); 
    }

    [Fact]
    public void TryAdd_MultipleItemsCalculatingWeight_ShouldWorkCorrectly()
    {
        // Arrange
        var inventory = new Inventory(10);
        var item1 = CreateMockItem(4);
        var item2 = CreateMockItem(5);
        var item3 = CreateMockItem(2); 

        // Act
        bool res1 = inventory.TryAdd(item1.Object);
        bool res2 = inventory.TryAdd(item2.Object);
        bool res3 = inventory.TryAdd(item3.Object);

        // Assert
        Assert.True(res1);
        Assert.True(res2);
        Assert.False(res3);
        Assert.Equal(2, inventory.Items.Count);
    }

    [Fact]
    public void TryRemove_ExistingItem_ShouldReturnTrueAndFreeUpCapacity()
    {
        // Arrange
        var inventory = new Inventory(10);
        var itemMock = CreateMockItem(10);

        inventory.TryAdd(itemMock.Object);

        Assert.False(inventory.TryAdd(CreateMockItem(1).Object));

        // Act
        bool result = inventory.TryRemove(itemMock.Object.Id, out var removedItem);

        // Assert
        Assert.True(result);
        Assert.Same(itemMock.Object, removedItem);
        Assert.Empty(inventory.Items);

        Assert.True(inventory.TryAdd(CreateMockItem(5).Object));
    }

    [Fact]
    public void TryRemove_NonExistingItem_ShouldReturnFalse()
    {
        // Arrange
        var inventory = new Inventory(10);
        var itemInInventory = CreateMockItem(5);
        inventory.TryAdd(itemInInventory.Object);

        var randomId = Guid.NewGuid();

        // Act
        bool result = inventory.TryRemove(randomId, out var removedItem);

        // Assert
        Assert.False(result);
        Assert.Null(removedItem);
        Assert.Single(inventory.Items); 
    }

    [Fact]
    public void TryGetItem_ExistingId_ShouldReturnItem()
    {
        // Arrange
        var inventory = new Inventory(10);
        var itemMock = CreateMockItem(5);
        inventory.TryAdd(itemMock.Object);

        // Act
        bool result = inventory.TryGetItem(itemMock.Object.Id, out var foundItem);

        // Assert
        Assert.True(result);
        Assert.Same(itemMock.Object, foundItem);
    }

    [Fact]
    public void TryGetItem_NonExistingId_ShouldReturnFalse()
    {
        // Arrange
        var inventory = new Inventory(10);

        // Act
        bool result = inventory.TryGetItem(Guid.NewGuid(), out var foundItem);

        // Assert
        Assert.False(result);
        Assert.Null(foundItem);
    }

    [Fact]
    public void Items_Property_ShouldBeReadOnly()
    {
        // Arrange
        var inventory = new Inventory(10);

        // Act & Assert
        var items = inventory.Items;

        Assert.Throws<NotSupportedException>(() =>
        {
            var dict = (IDictionary<Guid, IItem>)items;
            dict.Add(Guid.NewGuid(), CreateMockItem(1).Object);
        });
    }
}