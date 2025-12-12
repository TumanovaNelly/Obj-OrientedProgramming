using Lab2.Core.Enums;
using Lab2.Core.Models;

namespace Lab2.Core.Interfaces;

public interface IItem : IUsableByPlayer
{
    public Guid Id { get; }
    public string Name { get; }
}

public interface IWeapon : IItem
{
    public int Damage { get; }
}

public interface IProtection : IItem
{
    public ProtectionSlot EquipSlot { get; }
    public int Protect { get; }
}

public interface IHeal : IItem
{
    public int HealAmount { get; }
}

public interface IMaterial : IItem;