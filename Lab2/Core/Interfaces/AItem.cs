using Lab2.Core.Enums;
using Lab2.Core.Models;

namespace Lab2.Core.Interfaces;


public abstract class AItem(string name) : IItem
{
    public abstract bool UseByPlayer(ICharacter character, out IItem? droppedItem);

    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; } = name;
}


public abstract class AWeapon(string name, int damage) : AItem(name), IWeapon
{
    public override bool UseByPlayer(ICharacter character, out IItem? droppedItem)
    {
        character.TakeWeapon(this, out droppedItem);
        return true;
    }
    public int Damage { get; } = damage;
}


public abstract class AProtection(string name, ProtectionSlot equipSlot, int protect) : AItem(name), IProtection
{
    public override bool UseByPlayer(ICharacter character, out IItem? droppedItem)
    {
        character.EquipProtection(this, out droppedItem);
        return true;
    }
    public ProtectionSlot EquipSlot { get; } = equipSlot;
    public int Protect { get; } = protect;
}


public abstract class AHeal(string name, int healValue) : AItem(name), IHeal
{
    public int HealAmount { get; } = healValue;
    
    public override bool UseByPlayer(ICharacter character, out IItem? droppedItem)
    {
        character.Heal(HealAmount);
        droppedItem = null;
        return true;
    }
}


public abstract class AMaterial(string name, ProtectionSlot equipSlot, int protect) : AItem(name), IMaterial
{
    public override bool UseByPlayer(ICharacter character, out IItem? droppedItem)
    {
        droppedItem = null;
        return false;
    }
}