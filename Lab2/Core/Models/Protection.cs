using Lab2.Core.Enums;
using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class Protection(string name, int weight, int defenseValue, EquipmentSlot equipSlot) :
    Item(name, weight), IDefenseProvider, IEquippableItem, IUpgradable
{
    public EquipmentSlot EquipSlot { get; } = equipSlot;
    public int DefenseValue { get; private set; } = defenseValue;
    
    public void Upgrade() => DefenseValue = (int)(DefenseValue * 1.15);
}
