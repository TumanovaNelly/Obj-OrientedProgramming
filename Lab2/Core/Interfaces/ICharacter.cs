using Lab2.Core.Models;

namespace Lab2.Core.Interfaces;

public interface ICharacter
{
    public void Attack(PlayerInGame targetPlayer);
    public void TakeDamage(int damage);
    public void TakeWeapon(IWeapon weapon, out IItem? oldItem);
    public void EquipProtection(IProtection protection, out IItem? oldItem);
    public void Heal(int heal);

}