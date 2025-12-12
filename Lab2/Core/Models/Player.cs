using Lab2.Core.Enums;
using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class Player( 
    int baseDamage, 
    IWeaponManager weaponManager, IProtectionManager protectionManager, IInventory inventory,
    IScale healthScale
    ) : ICharacter
{
    public bool IsAlive => !healthScale.IsEmpty;
    
    private readonly IInventory _inventory = inventory;
    public int CurrentHealth => healthScale.CurrentValue;
    public int MaxHealth => healthScale.MaxValue;
    public int BaseDamage { get; } = baseDamage;
    
    public bool TryPickUpItem(IItem item) => IsAlive && _inventory.TryAdd(item);

    public bool TryDropItem(Guid itemId, out IItem? item)
    {
        item = null;
        return IsAlive && _inventory.TryRemove(itemId, out item);
    }
    
    public bool TryUseItem(Guid itemId)
    {
        if (!IsAlive || 
            !_inventory.TryGetItem(itemId, out IItem? item) || item is null) 
            return false;
        
        if (!_inventory.TryRemove(itemId, out var oldItem) || oldItem is null) 
            return false;

        if (!item.UseByPlayer(this))
            _inventory.TryAdd(oldItem);
        return true;
    }
    
    public void Attack(ICharacter targetCharacter)
    {
        if (!IsAlive)
            return;

        targetCharacter.TakeDamage(BaseDamage + weaponManager.TotalDamage);
    }
    
    public void TakeDamage(int damage)
    {
        if (!IsAlive)
            return;
        
        var totalDamage = int.Max(0, damage - protectionManager.TotalProtection);
        healthScale.Decrement(totalDamage);
    }

    public void Heal(int heal)
    {
        if (!IsAlive)
            return;
        
        healthScale.Increment(heal);
    }
    
    public bool TryTakeWeapon(IWeapon weapon)
    {
        weaponManager.TakeWeapon(weapon, out var oldWeapon);
        if (oldWeapon is null || _inventory.TryAdd(oldWeapon))
            return true;
        
        weaponManager.TakeWeapon(oldWeapon, out _);
        return false;
    }
    
    public bool TryLayDownWeapon()
    {
        weaponManager.LayDownWeapon(out var oldWeapon);
        if (oldWeapon is null || _inventory.TryAdd(oldWeapon))
            return true;
        
        weaponManager.TakeWeapon(oldWeapon, out _);
        return false;
    }

    public bool TryEquipProtection(IProtection protection)
    {
        protectionManager.EquipProtection(protection, out var oldProtection);
        if (oldProtection is null || _inventory.TryAdd(oldProtection))
            return true;
        
        protectionManager.EquipProtection(oldProtection, out _);
        return false;
    }

    public bool TryUnEquipProtection(ProtectionSlot slot)
    {
        protectionManager.UnEquipProtection(slot, out var oldProtection);
        if (oldProtection is null || _inventory.TryAdd(oldProtection))
            return true;
        
        protectionManager.EquipProtection(oldProtection, out _);
        return false;
    }
}
