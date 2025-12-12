using Lab2.Core.Enums;
using Lab2.Core.Interfaces;
using Lab2.Core.Models;

namespace Lab2.Tests.Core.Models;

public class ItemTest
{
    [Fact]
    public void Weapon_UseByPlayer_EquipsWeapon()
    {
        // Arrange
        var player = TestDataFactory.CreatePlayer();
        var sword = new Sword("Excalibur", damage: 50);

        // Act
        var result = sword.UseByPlayer(player, out var droppedItem);

        // Assert
        Assert.True(result); 
        Assert.Null(droppedItem);
    }

    [Fact]
    public void Heal_UseByPlayer_RestoresHealth()
    {
        // Arrange
        var player = TestDataFactory.CreatePlayer(maxHealth: 100);
        player.TakeDamage(50); 
        
        var potion = new Potion("Healing Potion", healValue: 20);

        // Act
        var result = potion.UseByPlayer(player, out var droppedItem);

        // Assert
        Assert.True(result);
        Assert.Null(droppedItem);
    }

    [Fact]
    public void Material_UseByPlayer_ReturnsFalse()
    {
        // Arrange
        var player = TestDataFactory.CreatePlayer();
        
        var rock = new TestMaterial("Rock"); 

        // Act
        var result = rock.UseByPlayer(player, out var droppedItem);

        // Assert
        Assert.False(result);
        Assert.Null(droppedItem);
    }

    private class TestMaterial(string name) : AMaterial(name, ProtectionSlot.OffHand, 0);
}