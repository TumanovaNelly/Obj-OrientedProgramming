using Lab2.Core.Enums;
using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class Player(string name, int maxHealth, int baseDamage, 
    IInventory inventory, IEquipmentManager equipmentManager) : IPlayer
{
    public string Name { get; } = name;
    public IScale Health { get; private set; } = new Scale(maxHealth, maxHealth);
    public int BaseDamage { get; private set; } = baseDamage;
    public bool IsAlive => !Health.IsOver;

    public IEquipmentManager EquipmentManager { get; } = equipmentManager;
    public IInventory Inventory { get; } = inventory;

    public bool TryPickUpItem(IItem item) => IsAlive && Inventory.TryAdd(item);

    public bool TryDropItem(Guid itemId, out IItem? item)
    {
        if (!IsAlive) return Inventory.TryRemove(itemId, out item);
        item = null;
        return false;
    }

    public void InteractWithItem(Guid itemId)
    {
        if (!IsAlive) 
            return;
        var item = Inventory.GetItem(itemId);
        item?.UseStrategy.Use(this, item);
    }

    public bool TryUnEquipItemToInventory(EquipmentSlot slot)
    {
        if (!IsAlive) 
            return false;
        var unequippedItem = EquipmentManager.Unequip(slot);
        if (unequippedItem is null)
            return false;

        if (Inventory.TryAdd(unequippedItem)) 
            return true;

        EquipmentManager.Equip(slot, unequippedItem);
        return false;
    }

    public bool TryEquipItemFromInventory(Guid itemId, EquipmentSlot targetSlot)
    {
        if (!IsAlive)
            return false;
        if (!Inventory.TryRemove(itemId, out var item) || item is not IEquippableItem equippedItem)
            return false;

        var oldItem = EquipmentManager.Equip(targetSlot, equippedItem);

        if (oldItem is null || Inventory.TryAdd(oldItem)) 
            return true;
        
        EquipmentManager.Equip(targetSlot, oldItem);
        Inventory.TryAdd(item);
        return false;
    }

    public void TakeDamage(int damage)
    {
        var totalDefense = EquipmentManager.Equipped.Values.OfType<IDefenseProvider>()
            .Sum(x => x.DefenseValue);

        int actualDamage = Math.Max(0, damage - totalDefense);
        Health.Decrement(actualDamage);
    }

    public void Attack(IPlayer target)
    {
        if (!IsAlive)
            return;
        int damage = EquipmentManager.Equipped.Values.OfType<IDamageProvider>()
            .Sum(x => x.DamageValue) + BaseDamage;

        target.TakeDamage(damage);
    }
}
