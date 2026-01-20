using Lab2.Core.Interfaces;
using Lab2.Core.Models;
using Moq;

namespace Lab2.Tests.Core.Models;

public class PlayerTests
{
    private readonly Mock<IWeaponManager> _mockWeaponMgr;
    private readonly Mock<IProtectionManager> _mockProtectionMgr;
    private readonly Mock<IInventory> _mockInventory;
    private readonly Mock<IScale> _mockHealthScale;
    private readonly Mock<IExperienceSystem> _mockXpSystem;
    
    private readonly Player _player;
    private const int InitialBaseDamage = 10;

    public PlayerTests()
    {
        _mockWeaponMgr = new Mock<IWeaponManager>();
        _mockProtectionMgr = new Mock<IProtectionManager>();
        _mockInventory = new Mock<IInventory>();
        _mockHealthScale = new Mock<IScale>();
        _mockXpSystem = new Mock<IExperienceSystem>();

        _mockHealthScale.Setup(s => s.IsEmpty).Returns(false);
        _mockHealthScale.Setup(s => s.CurrentValue).Returns(100);
        _mockHealthScale.Setup(s => s.MaxValue).Returns(100);

        _player = new Player(
            InitialBaseDamage,
            _mockWeaponMgr.Object,
            _mockProtectionMgr.Object,
            _mockInventory.Object,
            _mockHealthScale.Object,
            _mockXpSystem.Object
        );
    }
    
    
    [Fact]
    public void Attack_WhenAlive_DealsDamageAndGainsXp()
    {
        // Arrange
        var targetMock = new Mock<ICharacter>();
        _mockWeaponMgr.Setup(w => w.TotalDamage).Returns(5); 

        // Act
        _player.Attack(targetMock.Object);

        // Assert
        targetMock.Verify(t => t.TakeDamage(15), Times.Once);
        _mockXpSystem.Verify(x => x.AddExperience(10), Times.Once);
    }

    [Fact]
    public void Attack_WhenDead_DoesNothing()
    {
        // Arrange
        _mockHealthScale.Setup(s => s.IsEmpty).Returns(true); 
        var targetMock = new Mock<ICharacter>();

        // Act
        _player.Attack(targetMock.Object);

        // Assert
        targetMock.Verify(t => t.TakeDamage(It.IsAny<int>()), Times.Never);
        _mockXpSystem.Verify(x => x.AddExperience(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public void TakeDamage_CalculatesReductionAndDecrementsHealth()
    {
        // Arrange
        _mockProtectionMgr.Setup(p => p.TotalProtection).Returns(5); 
        const int damage = 20;

        // Act
        _player.TakeDamage(damage);

        // Assert
        _mockHealthScale.Verify(s => s.Decrement(15), Times.Once);
    }

    [Fact]
    public void TakeDamage_HighProtection_DecrementsZero()
    {
        // Arrange
        _mockProtectionMgr.Setup(p => p.TotalProtection).Returns(50); 
        const int damage = 10;

        // Act
        _player.TakeDamage(damage);

        // Assert
        _mockHealthScale.Verify(s => s.Decrement(0), Times.Once);
    }


    [Fact]
    public void TryUseItem_ItemExistsAndConsumed_ReturnsTrue()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var mockItem = new Mock<IItem>();
        
        mockItem.Setup(i => i.UseByPlayer(_player)).Returns(true);

        _mockInventory.Setup(i => i.TryGetItem(itemId, out It.Ref<IItem?>.IsAny))
            .Returns((Guid id, out IItem? item) => { item = mockItem.Object; return true; });
        
        _mockInventory.Setup(i => i.TryRemove(itemId, out It.Ref<IItem?>.IsAny))
            .Returns((Guid id, out IItem? item) => { item = mockItem.Object; return true; });

        // Act
        var result = _player.TryUseItem(itemId);

        // Assert
        Assert.True(result);
        mockItem.Verify(i => i.UseByPlayer(_player), Times.Once);
        _mockInventory.Verify(i => i.TryAdd(It.IsAny<IItem>()), Times.Never);
    }

    [Fact]
    public void TryUseItem_UseReturnsFalse_ReturnsItemToInventory()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var mockItem = new Mock<IItem>();
        
        mockItem.Setup(i => i.UseByPlayer(_player)).Returns(false);

        _mockInventory.Setup(i => i.TryGetItem(itemId, out It.Ref<IItem?>.IsAny))
            .Returns((Guid id, out IItem? item) => { item = mockItem.Object; return true; });
        _mockInventory.Setup(i => i.TryRemove(itemId, out It.Ref<IItem?>.IsAny))
            .Returns((Guid id, out IItem? item) => { item = mockItem.Object; return true; });

        // Act
        var result = _player.TryUseItem(itemId);

        // Assert
        Assert.False(result); 
        _mockInventory.Verify(i => i.TryAdd(mockItem.Object), Times.Once);
    }
    
    [Fact]
    public void TryUseItem_ItemNotFound_ReturnsFalse()
    {
        _mockInventory.Setup(i => i.TryGetItem(It.IsAny<Guid>(), out It.Ref<IItem?>.IsAny))
            .Returns(false);

        var result = _player.TryUseItem(Guid.NewGuid());

        Assert.False(result);
    }

    [Fact]
    public void TryTakeWeapon_EmptyHands_EquipsAndReturnsTrue()
    {
        // Arrange
        var newWeapon = new Mock<IWeapon>().Object;
        
        _mockWeaponMgr.Setup(w => w.TakeWeapon(newWeapon, out It.Ref<IWeapon?>.IsAny))
            .Callback((IWeapon w, out IWeapon? old) => { old = null; });

        // Act
        var result = _player.TryTakeWeapon(newWeapon);

        // Assert
        Assert.True(result);
        _mockInventory.Verify(i => i.TryAdd(It.IsAny<IItem>()), Times.Never); 
    }

    [Fact]
    public void TryTakeWeapon_SwapSuccess_OldWeaponGoesToInventory()
    {
        // Arrange
        var newWeapon = new Mock<IWeapon>().Object;
        var oldWeapon = new Mock<IWeapon>().Object;

        _mockWeaponMgr.Setup(w => w.TakeWeapon(newWeapon, out It.Ref<IWeapon?>.IsAny))
            .Callback((IWeapon w, out IWeapon? old) => { old = oldWeapon; });

        _mockInventory.Setup(i => i.TryAdd(oldWeapon)).Returns(true);

        // Act
        var result = _player.TryTakeWeapon(newWeapon);

        // Assert
        Assert.True(result);
        _mockInventory.Verify(i => i.TryAdd(oldWeapon), Times.Once);
    }

    [Fact]
    public void TryTakeWeapon_InventoryFull_RollbackHappens()
    {
        // Arrange
        var newWeapon = new Mock<IWeapon>().Object;
        var oldWeapon = new Mock<IWeapon>().Object;

        _mockWeaponMgr.Setup(w => w.TakeWeapon(newWeapon, out It.Ref<IWeapon?>.IsAny))
            .Callback((IWeapon w, out IWeapon? old) => { old = oldWeapon; });

        _mockInventory.Setup(i => i.TryAdd(oldWeapon)).Returns(false);

        // Act
        var result = _player.TryTakeWeapon(newWeapon);

        // Assert
        Assert.False(result); 
        _mockWeaponMgr.Verify(w => w.TakeWeapon(oldWeapon, out It.Ref<IWeapon?>.IsAny), Times.Once);
    }
    
    [Fact]
    public void OnLevelUp_IncreasesStats()
    {
        // Act
        _mockXpSystem.Raise(x => x.OnLevelUp += null);

        // Assert
        Assert.Equal(11, _player.BaseDamage);
        
        _mockHealthScale.Verify(s => s.IncreaseScale(10), Times.Once);
    }
}