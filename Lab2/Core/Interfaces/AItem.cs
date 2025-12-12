using Lab2.Core.Enums;
using Lab2.Core.Models;

namespace Lab2.Core.Interfaces;


public abstract class AItem(string name) : IItem
{
    public abstract bool UseByPlayer(ICharacter character);

    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; } = name;
}


public abstract class AWeapon(string name, int damage) : AItem(name), IWeapon
{
    public override bool UseByPlayer(ICharacter character)
        => character.TryTakeWeapon(this);
    
    public int Damage { get; } = damage;
}


public abstract class AProtection(string name, ProtectionSlot equipSlot, int protect) : AItem(name), IProtection
{
    public override bool UseByPlayer(ICharacter character)
        => character.TryEquipProtection(this);
    
    public ProtectionSlot EquipSlot { get; } = equipSlot;
    public int Protect { get; } = protect;
}


public abstract class AHeal(string name, int healValue) : AItem(name), IHeal
{
    public int HealAmount { get; } = healValue;
    
    public override bool UseByPlayer(ICharacter character)
    {
        character.Heal(HealAmount);
        return true;
    }
}


public abstract class AMaterial(string name, ProtectionSlot equipSlot, int protect) : AItem(name), IMaterial
{
    public override bool UseByPlayer(ICharacter character) => false;
}