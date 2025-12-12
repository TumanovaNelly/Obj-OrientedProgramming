using Lab2.Core.Enums;
using Lab2.Core.Interfaces;
using Lab2.Core.Models;
using Moq;

namespace Lab2.Tests.Core.Models;

public class ProtectionManagerTest
{
    private readonly ProtectionManager _manager = new();
    
    private static IProtection CreateMockProtection(ProtectionSlot slot, int protectValue)
    {
        var mock = new Mock<IProtection>();
        mock.Setup(p => p.EquipSlot).Returns(slot);
        mock.Setup(p => p.Protect).Returns(protectValue);
        return mock.Object;
    }

    [Fact]
    public void TotalProtection_InitiallyZero()
    {
        Assert.Equal(0, _manager.TotalProtection);
    }

    [Fact]
    public void EquipProtection_EmptySlot_AddsProtectionAndReturnsNullOldItem()
    {
        // Arrange
        var helmet = CreateMockProtection(ProtectionSlot.Head, 10);

        // Act
        _manager.EquipProtection(helmet, out var oldProtection);

        // Assert
        Assert.Equal(10, _manager.TotalProtection);
        Assert.Null(oldProtection);
    }

    [Fact]
    public void EquipProtection_OccupiedSlot_SwapsItemsAndReturnsOldItem()
    {
        // Arrange
        var weakHelmet = CreateMockProtection(ProtectionSlot.Head, 5);
        var strongHelmet = CreateMockProtection(ProtectionSlot.Head, 20);

        _manager.EquipProtection(weakHelmet, out _);

        // Act
        _manager.EquipProtection(strongHelmet, out var oldProtection);

        // Assert
        Assert.Equal(20, _manager.TotalProtection); 
        Assert.Same(weakHelmet, oldProtection); 
    }

    [Fact]
    public void EquipProtection_DifferentSlots_SumsProtectionCorrectly()
    {
        // Arrange
        var helmet = CreateMockProtection(ProtectionSlot.Head, 10);
        var armor = CreateMockProtection(ProtectionSlot.Body, 50);
        var shield = CreateMockProtection(ProtectionSlot.OffHand, 15);

        // Act
        _manager.EquipProtection(helmet, out _);
        _manager.EquipProtection(armor, out _);
        _manager.EquipProtection(shield, out _);

        // Assert
        Assert.Equal(75, _manager.TotalProtection);
    }

    [Fact]
    public void UnEquipProtection_OccupiedSlot_RemovesProtectionAndReturnsItem()
    {
        // Arrange
        var armor = CreateMockProtection(ProtectionSlot.Body, 100);
        _manager.EquipProtection(armor, out _);

        // Act
        _manager.UnEquipProtection(ProtectionSlot.Body, out var removedItem);

        // Assert
        Assert.Equal(0, _manager.TotalProtection);
        Assert.Same(armor, removedItem);
    }

    [Fact]
    public void UnEquipProtection_EmptySlot_ReturnsNullAndChangesNothing()
    {
        // Act
        _manager.UnEquipProtection(ProtectionSlot.Legs, out var removedItem);

        // Assert
        Assert.Equal(0, _manager.TotalProtection);
        Assert.Null(removedItem);
    }

    [Fact]
    public void UnEquipProtection_OneOfMany_UpdatesTotalCorrectly()
    {
        // Arrange
        var helmet = CreateMockProtection(ProtectionSlot.Head, 10);
        var pants = CreateMockProtection(ProtectionSlot.Legs, 5);
        
        _manager.EquipProtection(helmet, out _);
        _manager.EquipProtection(pants, out _);
        
        // Act
        _manager.UnEquipProtection(ProtectionSlot.Head, out _);

        // Assert
        Assert.Equal(5, _manager.TotalProtection); 
    }
}