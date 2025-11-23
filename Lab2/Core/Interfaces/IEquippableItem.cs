using Lab2.Core.Enums;

namespace Lab2.Core.Interfaces;

public interface IEquippableItem : IItem
{
    public EquipmentSlot EquipSlot { get; }
}