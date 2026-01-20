using Lab2.Core.Models;

namespace Lab2.Core.Interfaces;

public interface ICharacter
{
    public bool IsAlive { get; }
    public int CurrentHealth { get; }
    public int MaxHealth { get; }
    public void Attack(ICharacter targetPlayer);
    public void TakeDamage(int damage);
    public bool TryTakeWeapon(IWeapon weapon);
    public bool TryEquipProtection(IProtection protection);
    public void Heal(int heal);

}