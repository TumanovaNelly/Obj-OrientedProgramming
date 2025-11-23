using Lab2.Core.Enums;
using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class Protection(string name, int weight, int defenseValue, EquipmentSlot equipSlot) :
    Item(name, weight), IDefenseProvider, IEquippableItem
{
    public EquipmentSlot EquipSlot { get; } = equipSlot;
    public int DefenseValue { get; } = defenseValue;
}
