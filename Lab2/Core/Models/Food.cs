namespace Lab2.Core.Models;

public class Food(string name, int weight, int healAmount) : 
    Item(name, weight, new HealStrategy(healAmount));