using Lab2.Core.Enums;
using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class Player(string name, int maxHealth, int baseDamage, 
    IInventory inventory, IEquipmentManager equipmentManager) : IPlayer
{
    public string Name { get; } = name;
    public int BaseDamage { get; private set; } = baseDamage;
    public bool IsAlive => !_health.IsOver;
    
    private readonly Scale _health = new(maxHealth, maxHealth);
    
    public bool TryPickUpItem(IItem item) => IsAlive && inventory.TryAdd(item);

    public bool TryDropItem(Guid itemId, out IItem? item)
    {
        if (!IsAlive) return inventory.TryRemove(itemId, out item);
        item = null;
        return false;
    }

    public bool TryInteractWithItem(Guid itemId)
    {
        if (!IsAlive || !inventory.TryGetItem(itemId, out var item)) 
            return false;
        
        item!.UseStrategy.Use(this, item);
        return true;
    }

    public bool TryEquipItemFromInventory(Guid itemId)
    {
        if (!IsAlive)
            return false;
        if (!inventory.TryRemove(itemId, out var item) || item is not IEquippableItem equippedItem)
            return false;

        var oldItem = equipmentManager.Equip(equippedItem.EquipSlot, equippedItem);

        if (oldItem is null || inventory.TryAdd(oldItem)) 
            return true;
        
        equipmentManager.Equip(oldItem.EquipSlot, oldItem);
        inventory.TryAdd(item);
        return false;
    }
    
    public bool TryUnEquipItemToInventory(EquipmentSlot slot)
    {
        if (!IsAlive) 
            return false;
        var unequippedItem = equipmentManager.UnEquip(slot);
        if (unequippedItem is null)
            return false;

        if (inventory.TryAdd(unequippedItem)) 
            return true;

        equipmentManager.Equip(slot, unequippedItem);
        return false;
    }

    public void TakeDamage(int damage)
    {
        var totalDefense = equipmentManager.Equipped.Values.OfType<IDefenseProvider>()
            .Sum(x => x.DefenseValue);

        int actualDamage = Math.Max(0, damage - totalDefense);
        _health.Decrement(actualDamage);
    }

    public void Heal(int healAmount) => _health.Increment(healAmount);

    public void Attack(IPlayer target)
    {
        if (!IsAlive)
            return;
        int damage = equipmentManager.Equipped.Values.OfType<IDamageProvider>()
            .Sum(x => x.DamageValue) + BaseDamage;

        target.TakeDamage(damage);
    }

    public void Upgrade()
    {
        BaseDamage = (int)(BaseDamage * 1.5);
        _health.IncreaseMaxValue(20);
    }
}
