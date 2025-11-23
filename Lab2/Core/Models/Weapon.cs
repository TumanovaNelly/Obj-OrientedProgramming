using Lab2.Core.Enums;
using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class Weapon(string name, int weight, int damageValue, EquipmentSlot equipSlot) :
    Item(name, weight), IDamageProvider, IEquippableItem
{
    public EquipmentSlot EquipSlot { get; } = equipSlot;
    public int DamageValue { get; } = damageValue;
}