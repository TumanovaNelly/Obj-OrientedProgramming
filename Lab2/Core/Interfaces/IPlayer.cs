using Lab2.Core.Enums;

namespace Lab2.Core.Interfaces;

public interface IPlayer
{
    public string Name { get; }
    public int CurrentHealth { get; }
    public int MaxHealth { get; }
    public bool IsAlive { get; }
    public int CurrentLevel { get; }
    public int CurrentXp { get; }
    public int XpToNextLevel { get; }
    public bool TryPickUpItem(IItem item);
    public bool TryDropItem(Guid itemId, out IItem? item);
    public bool TryInteractWithItem(Guid itemId);
    public bool TryEquipItemFromInventory(Guid itemId);
    public bool TryUnEquipItemToInventory(EquipmentSlot slot);
    public void Heal(int healAmount);
    public void TakeDamage(int damage);
    public void Attack(IPlayer target);
    public int AddXp(int xp);
}
