using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class PlayerBuilder
{
    private IWeaponManager? _weaponManager;
    private IProtectionManager? _protectionManager;
    private IInventory? _inventory;
    private IScale? _healthScale;
    private IExperienceSystem? _experienceSystem; 
    private int? _baseDamage;

    public PlayerBuilder SetWeaponManager(IWeaponManager weaponManager)
    {
        _weaponManager = weaponManager;
        return this;
    }
    
    public PlayerBuilder SetProtectionManager(IProtectionManager protectionManager)
    {
        _protectionManager = protectionManager;
        return this;
    }
    
    public PlayerBuilder SetInventory(IInventory inventory)
    {
        _inventory = inventory;
        return this;
    }
    
    public PlayerBuilder SetHealthScale(IScale healthScale)
    {
        _healthScale = healthScale;
        return this;
    }
    
    public PlayerBuilder SetExperienceSystem(IExperienceSystem experienceSystem)
    {
        _experienceSystem = experienceSystem;
        return this;
    }
    
    public PlayerBuilder SetBaseDamage(int baseDamage)
    {
        if (_baseDamage < 0)
            throw new ArgumentException("Base damage cannot be negative");
        
        _baseDamage = baseDamage;
        return this;
    }

    public Player Build()
    {
        if (_weaponManager is null)
            throw new InvalidOperationException("Weapon manager is not set");
        
        if (_protectionManager is null)
            throw new InvalidOperationException("Protection manager is not set");
        
        if (_inventory is null)
            throw new InvalidOperationException("Inventory is not set");
        
        if (_healthScale is null)
            throw new InvalidOperationException("Health scale is not set");
        
        if (_experienceSystem is null)
            throw new InvalidOperationException("Experience system is not set");
        
        if (_baseDamage is null)
            throw new InvalidOperationException("Base damage is not set");
        
        return new Player((int)_baseDamage, 
            _weaponManager, _protectionManager, _inventory, 
            _healthScale, _experienceSystem);
    }
}