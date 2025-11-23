using Lab2.Core.Enums;

namespace Lab2.Core.Interfaces;

public interface IPlayer
{
    public string Name { get; }
    public IScale Health { get; }
    public int BaseDamage { get; }
    public bool IsAlive { get; }
    public bool TryPickUpItem(IItem item);
    public bool TryDropItem(Guid itemId, out IItem? item);
    public void InteractWithItem(Guid itemId);
    public bool TryUnEquipItemToInventory(EquipmentSlot slot);
    public bool TryEquipItemFromInventory(Guid itemId, EquipmentSlot targetSlot);
    public void TakeDamage(int damage);
    public void Attack(IPlayer target);
}
