using Lab2.Core.Interfaces;
using Lab2.Core.Models;
using Moq;

namespace Lab2.Tests.Core.Models;

public class WeaponManagerTests
{
    private readonly WeaponManager _manager = new();

    private static IWeapon CreateMockWeapon(int damage)
    {
        var mock = new Mock<IWeapon>();
        mock.Setup(w => w.Damage).Returns(damage);
        return mock.Object;
    }

    [Fact]
    public void TotalDamage_NoWeaponEquipped_ReturnsZero()
    {
        Assert.Equal(0, _manager.TotalDamage);
    }

    [Fact]
    public void TakeWeapon_EmptySlot_UpdatesTotalDamageAndReturnsNullOldWeapon()
    {
        // Arrange
        var weapon = CreateMockWeapon(damage: 15);

        // Act
        _manager.TakeWeapon(weapon, out var oldWeapon);

        // Assert
        Assert.Equal(15, _manager.TotalDamage);
        Assert.Null(oldWeapon);
    }

    [Fact]
    public void TakeWeapon_OccupiedSlot_SwapsWeaponsAndReturnsOldWeapon()
    {
        // Arrange
        var sword = CreateMockWeapon(damage: 10);
        var axe = CreateMockWeapon(damage: 25);

        _manager.TakeWeapon(sword, out _);

        // Act
        _manager.TakeWeapon(axe, out var oldWeapon);

        // Assert
        Assert.Equal(25, _manager.TotalDamage); 
        Assert.Same(sword, oldWeapon);
    }

    [Fact]
    public void LayDownWeapon_OccupiedSlot_ResetsDamageAndReturnsWeapon()
    {
        // Arrange
        var weapon = CreateMockWeapon(damage: 50);
        _manager.TakeWeapon(weapon, out _);

        // Act
        _manager.LayDownWeapon(out var oldWeapon);

        // Assert
        Assert.Equal(0, _manager.TotalDamage);
        Assert.Same(weapon, oldWeapon);
    }

    [Fact]
    public void LayDownWeapon_EmptySlot_ReturnsNullAndDamageRemainsZero()
    {
        // Act
        _manager.LayDownWeapon(out var oldWeapon);

        // Assert
        Assert.Equal(0, _manager.TotalDamage);
        Assert.Null(oldWeapon);
    }
}