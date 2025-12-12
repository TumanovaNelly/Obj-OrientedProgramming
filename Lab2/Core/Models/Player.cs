using Lab2.Core.Enums;
using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class Player : ICharacter
{
    public bool IsAlive => !_healthScale.IsEmpty;
    public int CurrentHealth => _healthScale.CurrentValue;
    public int MaxHealth => _healthScale.MaxValue;
    
    public int BaseDamage { get; private set; }
    
    public int Level => _experienceSystem.Level;
    public int CurrentXp => _experienceSystem.CurrentExperience;
    public int ToNextLevelXp => _experienceSystem.ToNextLevelExperience;

    private readonly IWeaponManager _weaponManager;
    private readonly IProtectionManager _protectionManager;
    private readonly IInventory _inventory;
    
    private readonly IScale _healthScale;
    private readonly IExperienceSystem _experienceSystem;

    public Player(
        int baseDamage,
        IWeaponManager weaponManager, IProtectionManager protectionManager, IInventory inventory,
        IScale healthScale,
        IExperienceSystem experienceSystem
    )
    {
        BaseDamage = baseDamage;
        _weaponManager = weaponManager;
        _protectionManager = protectionManager;
        _inventory = inventory;
        _healthScale = healthScale;
        _experienceSystem = experienceSystem;
        _experienceSystem.OnLevelUp += Upgrade;
    }
    
    
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

        AddXp(10);
        targetCharacter.TakeDamage(BaseDamage + _weaponManager.TotalDamage);
    }
    
    public void TakeDamage(int damage)
    {
        if (!IsAlive)
            return;
        
        var totalDamage = int.Max(0, damage - _protectionManager.TotalProtection);
        _healthScale.Decrement(totalDamage);
    }

    public void Heal(int heal)
    {
        if (!IsAlive)
            return;
        
        _healthScale.Increment(heal);
    }
    
    public bool TryTakeWeapon(IWeapon weapon)
    {
        _weaponManager.TakeWeapon(weapon, out var oldWeapon);
        if (oldWeapon is null || _inventory.TryAdd(oldWeapon))
            return true;
        
        _weaponManager.TakeWeapon(oldWeapon, out _);
        return false;
    }
    
    public bool TryLayDownWeapon()
    {
        _weaponManager.LayDownWeapon(out var oldWeapon);
        if (oldWeapon is null || _inventory.TryAdd(oldWeapon))
            return true;
        
        _weaponManager.TakeWeapon(oldWeapon, out _);
        return false;
    }

    public bool TryEquipProtection(IProtection protection)
    {
        _protectionManager.EquipProtection(protection, out var oldProtection);
        if (oldProtection is null || _inventory.TryAdd(oldProtection))
            return true;
        
        _protectionManager.EquipProtection(oldProtection, out _);
        return false;
    }

    public bool TryUnEquipProtection(ProtectionSlot slot)
    {
        _protectionManager.UnEquipProtection(slot, out var oldProtection);
        if (oldProtection is null || _inventory.TryAdd(oldProtection))
            return true;
        
        _protectionManager.EquipProtection(oldProtection, out _);
        return false;
    }
    
    private void AddXp(int xp) 
        => _experienceSystem.AddExperience(xp);

    private void Upgrade()
    {
        BaseDamage += BaseDamage / 10;
        _healthScale.IncreaseScale(_healthScale.MaxValue / 10);
    }
}
