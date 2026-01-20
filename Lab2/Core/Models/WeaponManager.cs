using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class WeaponManager : IWeaponManager
{
    private IWeapon? _slot;
    public int TotalDamage => _slot?.Damage ?? 0;

    public void TakeWeapon(IWeapon weapon, out IWeapon? oldWeapon)
    {
        oldWeapon = _slot;
        _slot = weapon;
    }

    public void LayDownWeapon(out IWeapon? oldWeapon)
    {
        oldWeapon = _slot;
        _slot = null;
    }
}

