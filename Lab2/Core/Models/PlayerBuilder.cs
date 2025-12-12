using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class PlayerBuilder
{
    private IWeaponManager? _weaponManager;
    private IProtectionManager? _protectionManager;
    private IScale? _healthScale;
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
    
    public PlayerBuilder SetHealthScale(IScale healthScale)
    {
        _healthScale = healthScale;
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
        
        if (_healthScale is null)
            throw new InvalidOperationException("Health scale is not set");
        
        if (_baseDamage is null)
            throw new InvalidOperationException("Base damage cannot be negative");
        
        return new Player((int)_baseDamage, _weaponManager, _protectionManager, _healthScale);
    }
}