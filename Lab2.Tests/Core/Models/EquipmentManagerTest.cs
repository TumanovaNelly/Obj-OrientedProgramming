using Lab2.Core.Enums;
using Lab2.Core.Interfaces;
using Lab2.Core.Models;
using Moq;

namespace Lab2.Tests.Core.Models;

public class EquipmentManagerTest
{
    // Arrange 
    private readonly EquipmentManager _manager = new();
    private readonly Mock<IEquippableItem> _mockItem = new();

    [Fact]
    public void Constructor_ShouldInitializeAllSlotsWithNull()
    {
        // Act
        var equipped = _manager.Equipped;

        // Assert
        Assert.NotEmpty(equipped);

        var expectedCount = Enum.GetValues<EquipmentSlot>().Length;
        Assert.Equal(expectedCount, equipped.Count);

        foreach (var slot in Enum.GetValues<EquipmentSlot>())
        {
            Assert.True(equipped.ContainsKey(slot));
            Assert.Null(equipped[slot]);
        }
    }

    [Fact]
    public void Equip_IntoEmptySlot_ShouldSetItemAndReturnNull()
    {
        // Arrange
        const EquipmentSlot slot = EquipmentSlot.MainHand; 

        // Act
        var result = _manager.Equip(slot, _mockItem.Object);

        // Assert
        Assert.Null(result); 
        Assert.Same(_mockItem.Object, _manager.Get(slot));
        Assert.Same(_mockItem.Object, _manager.Equipped[slot]); 
    }

    [Fact]
    public void Equip_IntoOccupiedSlot_ShouldReplaceItemAndReturnOldOne()
    {
        // Arrange
        const EquipmentSlot slot = EquipmentSlot.Head;
        var oldItemMock = new Mock<IEquippableItem>();
        var newItemMock = new Mock<IEquippableItem>();

        _manager.Equip(slot, oldItemMock.Object); 

        // Act
        var result = _manager.Equip(slot, newItemMock.Object);

        // Assert
        Assert.Same(oldItemMock.Object, result); 
        Assert.Same(newItemMock.Object, _manager.Get(slot)); 
    }

    [Fact]
    public void UnEquip_OccupiedSlot_ShouldRemoveItemAndReturnIt()
    {
        // Arrange
        const EquipmentSlot slot = EquipmentSlot.Body;
        _manager.Equip(slot, _mockItem.Object);

        // Act
        var result = _manager.UnEquip(slot);

        // Assert
        Assert.Same(_mockItem.Object, result); 
        Assert.Null(_manager.Get(slot));
    }

    [Fact]
    public void UnEquip_EmptySlot_ShouldReturnNull()
    {
        // Arrange
        const EquipmentSlot slot = EquipmentSlot.OffHand; 

        // Act
        var result = _manager.UnEquip(slot);

        // Assert
        Assert.Null(result); 
        Assert.Null(_manager.Get(slot)); 
    }

    [Fact]
    public void Get_ShouldReturnCorrectItemOrNull()
    {
        // Arrange
        _manager.Equip(EquipmentSlot.MainHand, _mockItem.Object);

        // Act & Assert
        Assert.Same(_mockItem.Object, _manager.Get(EquipmentSlot.MainHand));
        Assert.Null(_manager.Get(EquipmentSlot.Head));
    }

    [Fact]
    public void Equipped_Dictionary_ShouldBeReadOnly()
    {
        var equipped = _manager.Equipped;

        Assert.Throws<NotSupportedException>(() =>
        {
            var collection = (ICollection<KeyValuePair<EquipmentSlot, IEquippableItem?>>)equipped;
            collection.Add(new KeyValuePair<EquipmentSlot, IEquippableItem?>(EquipmentSlot.Head, null));
        });
    }
}
