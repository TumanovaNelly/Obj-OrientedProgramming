using Lab2.Core.Enums;
using Lab2.Core.Interfaces;
using Lab2.Core.Models;
using Moq;

namespace Lab2.Tests.Core.Models;

public class PlayerTest
{
    private readonly Mock<IInventory> _inventoryMock;
    private readonly Mock<IEquipmentManager> _equipmentMock;
    private readonly Player _player;

    private const string Name = "Hero";
    private const int MaxHealth = 100;
    private const int BaseDamage = 10;

    public PlayerTest()
    {
        _inventoryMock = new Mock<IInventory>();
        _equipmentMock = new Mock<IEquipmentManager>();

        _equipmentMock.Setup(x => x.Equipped)
            .Returns(new Dictionary<EquipmentSlot, IEquippableItem?>());

        _player = new Player(Name, MaxHealth, BaseDamage, 
            _inventoryMock.Object, _equipmentMock.Object);
    }
    
    [Fact]
    public void Constructor_ShouldInitializeCorrectly()
    {
        Assert.Equal(Name, _player.Name);
        Assert.Equal(MaxHealth, _player.CurrentHealth);
        Assert.True(_player.IsAlive);
    }

    [Fact]
    public void TryDropItem_WhenDead_ShouldReturnFalse()
    {
        _player.TakeDamage(MaxHealth + 10);

        var result = _player.TryDropItem(Guid.NewGuid(), out _);

        Assert.False(result);
        _inventoryMock.Verify(x => x.TryRemove(It.IsAny<Guid>(), out It.Ref<IItem?>.IsAny), Times.Never);
    }


    [Fact]
    public void TakeDamage_ShouldConsiderDefense()
    {
        // Arrange: Броня дает 5 защиты
        var armorMock = new Mock<IEquippableItem>();
        var defenseProvider = armorMock.As<IDefenseProvider>();
        defenseProvider.Setup(x => x.DefenseValue).Returns(5);

        var equipped = new Dictionary<EquipmentSlot, IEquippableItem?>
        {
            { EquipmentSlot.Body, armorMock.Object }
        };
        _equipmentMock.Setup(x => x.Equipped).Returns(equipped);

        // Act: Урон 20
        _player.TakeDamage(20);

        // Assert: 100 - (20 - 5) = 85
        Assert.Equal(85, _player.CurrentHealth);
    }

    [Fact]
    public void Attack_ShouldCalculateTotalDamage()
    {
        // Arrange
        var targetMock = new Mock<IPlayer>();

        // Меч дает 15 урона
        var swordMock = new Mock<IEquippableItem>();
        var damageProvider = swordMock.As<IDamageProvider>();
        damageProvider.Setup(x => x.DamageValue).Returns(15);

        var equipped = new Dictionary<EquipmentSlot, IEquippableItem?>
        {
            { EquipmentSlot.MainHand, swordMock.Object }
        };
        _equipmentMock.Setup(x => x.Equipped).Returns(equipped);

        // Act
        _player.Attack(targetMock.Object);

        // Assert: Base(10) + Sword(15) = 25
        targetMock.Verify(x => x.TakeDamage(25), Times.Once);
    }
    
    
    [Fact]
    public void AddXp_ShouldTriggerUpgrade_WhenLevelUp()
    {
        // Act
        _player.AddXp(100);

        // Assert
        Assert.Equal(2, _player.CurrentLevel);
        Assert.Equal(MaxHealth + 10, _player.MaxHealth);
        var targetMock = new Mock<IPlayer>();
        _player.Attack(targetMock.Object);
        targetMock.Verify(x => x.TakeDamage(20), Times.Once);
    }
    
    [Fact]
    public void TryEquip_WhenNotEquippable_ShouldReturnItemToInventory()
    {
        // Arrange
        var itemId = Guid.NewGuid();

        var simpleItemMock = new Mock<IItem>();
        IItem simpleItem = simpleItemMock.Object;

        // Инвентарь отдает предмет
        _inventoryMock.Setup(x => x.TryRemove(itemId, out simpleItem)).Returns(true);

        // Act
        var result = _player.TryEquipItemFromInventory(itemId);

        // Assert
        Assert.False(result);
        _inventoryMock.Verify(x => x.TryAdd(simpleItem), Times.Once);
        _equipmentMock.Verify(x => x.Equip(It.IsAny<EquipmentSlot>(), It.IsAny<IEquippableItem>()), Times.Never);
    }

    [Fact]
    public void TryEquip_WhenInventoryFullForSwap_ShouldRollback()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        const EquipmentSlot slot = EquipmentSlot.Head;

        var newItemMock = new Mock<IEquippableItem>();
        newItemMock.Setup(x => x.EquipSlot).Returns(slot);
        IItem newItemBase = newItemMock.Object;

        var oldItemMock = new Mock<IEquippableItem>();
        oldItemMock.Setup(x => x.EquipSlot).Returns(slot); 
        IItem oldItemBase = oldItemMock.Object;

        _inventoryMock.Setup(x => x.TryRemove(itemId, out newItemBase)).Returns(true);
        _equipmentMock.Setup(x => x.Equip(slot, newItemMock.Object)).Returns(oldItemMock.Object);
        _inventoryMock.Setup(x => x.TryAdd(oldItemBase)).Returns(false);

        // Act
        var result = _player.TryEquipItemFromInventory(itemId);

        // Assert
        Assert.False(result); 

        // ROLLBACK CHECK:
        _equipmentMock.Verify(x => x.Equip(slot, oldItemMock.Object), Times.Once);
        _inventoryMock.Verify(x => x.TryAdd(newItemBase), Times.Once);
    }

    [Fact]
    public void TryInteractWithItem_ShouldUseStrategy()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var itemMock = new Mock<IItem>();
        var strategyMock = new Mock<IItemUseStrategy>();

        itemMock.Setup(x => x.UseStrategy).Returns(strategyMock.Object);
        var itemObj = itemMock.Object;

        _inventoryMock.Setup(x => x.TryGetItem(itemId, out itemObj)).Returns(true);

        // Act
        var result = _player.TryInteractWithItem(itemId);

        // Assert
        Assert.True(result);
        strategyMock.Verify(x => x.Use(_player, itemObj), Times.Once);
    }
}