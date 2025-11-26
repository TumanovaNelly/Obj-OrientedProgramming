using Lab2.Core.Enums;
using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class Player : IPlayer
{
    public string Name { get; }
    public int CurrentHealth => _health.CurrentValue;
    public int MaxHealth => _health.MaxValue;
    public bool IsAlive => !_health.IsOver;
    public int CurrentLevel => _experience.Level;
    public int CurrentXp => _experience.CurrentXp;
    public int XpToNextLevel => _experience.XpToNextLevel;

    private int _baseDamage;
    private readonly Scale _health;
    private readonly ExperienceSystem _experience = new();
    private readonly IInventory _inventory;
    private readonly IEquipmentManager _equipmentManager;

    public Player(string name, int maxHealth, int baseDamage, IInventory inventory, IEquipmentManager equipmentManager)
    {
        Name = name;
        
        _baseDamage = baseDamage;
        _health= new Scale(maxHealth, maxHealth);
        _experience.OnLevelUp += Upgrade;
        _inventory = inventory;
        _equipmentManager = equipmentManager;
    }
    
    public bool TryPickUpItem(IItem item) => IsAlive && _inventory.TryAdd(item);

    public bool TryDropItem(Guid itemId, out IItem? item)
    {
        if (IsAlive) 
            return _inventory.TryRemove(itemId, out item);
        item = null;
        return false;
    }

    public bool TryInteractWithItem(Guid itemId)
    {
        if (!IsAlive || !_inventory.TryGetItem(itemId, out var item)) 
            return false;

        if (item is null) 
            throw new KeyNotFoundException();
        
        item.UseStrategy.Use(this, item);
        return true;
    }

    public bool TryEquipItemFromInventory(Guid itemId)
    {
        if (!IsAlive || !_inventory.TryRemove(itemId, out var item) || item is null)
            return false;
        
        if (item is not IEquippableItem equippedItem)
        {
            _inventory.TryAdd(item);
            return false;
        }

        var oldItem = _equipmentManager.Equip(equippedItem.EquipSlot, equippedItem);

        if (oldItem is null || _inventory.TryAdd(oldItem)) 
            return true;
        
        _equipmentManager.Equip(oldItem.EquipSlot, oldItem);
        _inventory.TryAdd(item);
        return false;
    }
    
    public bool TryUnEquipItemToInventory(EquipmentSlot slot)
    {
        if (!IsAlive) 
            return false;
        var unequippedItem = _equipmentManager.UnEquip(slot);
        if (unequippedItem is null)
            return false;

        if (_inventory.TryAdd(unequippedItem)) 
            return true;

        _equipmentManager.Equip(slot, unequippedItem);
        return false;
    }

    public void TakeDamage(int damage)
    {
        var totalDefense = _equipmentManager.Equipped.Values.OfType<IDefenseProvider>()
            .Sum(x => x.DefenseValue);

        int actualDamage = Math.Max(0, damage - totalDefense);
        _health.Decrement(actualDamage);
    }

    public void Heal(int healAmount) => _health.Increment(healAmount);

    public void Attack(IPlayer target)
    {
        if (!IsAlive)
            return;
        int damage = _equipmentManager.Equipped.Values.OfType<IDamageProvider>()
            .Sum(x => x.DamageValue) + _baseDamage;

        target.TakeDamage(damage);
    }

    public int AddXp(int xp)
    {
        _experience.AddXp(xp);
        return _experience.Level;
    }

    private void Upgrade(int newLevel)
    {
        _baseDamage += 10;
        _health.IncreaseMaxValue(10);
    }
}
