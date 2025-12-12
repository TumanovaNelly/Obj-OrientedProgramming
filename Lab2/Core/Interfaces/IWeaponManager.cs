namespace Lab2.Core.Interfaces;

public interface IWeaponManager 
{
    public int TotalDamage { get; }
    public void TakeWeapon(IWeapon weapon, out IWeapon? oldWeapon);
    public void LayDownWeapon(out IWeapon? oldWeapon);
}