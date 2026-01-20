using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class Sword(string name, int damage) : AWeapon(name, damage);
public class Bow(string name, int damage) : AWeapon(name, damage);
public class Gun(string name, int damage) : AWeapon(name, damage);