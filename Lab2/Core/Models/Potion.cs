using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class Potion(string name, int healValue) : AHeal(name, healValue);

public class Food(string name, int healValue) : AHeal(name, healValue);

public class Bandage(string name, int healValue) : AHeal(name, healValue);