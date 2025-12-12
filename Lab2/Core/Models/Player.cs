using Lab2.Core.Enums;
using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class Player( 
    int baseDamage, 
    IWeaponManager weaponManager, IProtectionManager protectionManager,
    IScale healthScale
    ) : ICharacter
{
    public bool IsAlive => !healthScale.IsEmpty;
    public int CurrentHealth => healthScale.CurrentValue;
    public int MaxHealth => healthScale.MaxValue;
    public int BaseDamage { get; } = baseDamage;
    
    public void Attack(PlayerInGame targetPlayer)
    {
        if (!IsAlive)
            return;

        targetPlayer.TakeDamage(BaseDamage + weaponManager.TotalDamage);
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
    
    public void TakeWeapon(IWeapon weapon, out IItem? oldItem)
    {
        weaponManager.TakeWeapon(weapon, out var oldWeapon);
        oldItem = oldWeapon ?? null;
    }
    
    public void LayDownWeapon(out IItem? oldItem)
    {
        weaponManager.LayDownWeapon(out var oldWeapon);
        oldItem = oldWeapon ?? null;
    }

    public void EquipProtection(IProtection protection, out IItem? oldItem)
    {
        protectionManager.EquipProtection(protection, out var oldProtection);
        oldItem = oldProtection ?? null;
    }

    public void UnEquipProtection(ProtectionSlot slot, out IItem? oldItem)
    {
        protectionManager.UnEquipProtection(slot, out var oldProtection);
        oldItem = oldProtection ?? null;
    }
}
