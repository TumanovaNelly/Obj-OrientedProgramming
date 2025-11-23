using Lab2.Core.Enums;

namespace Lab2.Core.Interfaces;

public interface IEquipmentManager
{
    public IReadOnlyDictionary<EquipmentSlot, IEquippableItem?> Equipped { get; }
    
    public IEquippableItem? Equip(EquipmentSlot slot, IEquippableItem item);

    public IEquippableItem? Unequip(EquipmentSlot slot);

    public IEquippableItem? Get(EquipmentSlot slot);
}