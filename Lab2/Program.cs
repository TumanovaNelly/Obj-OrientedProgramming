using Lab2.Core.Enums;
using Lab2.Core.Models;

namespace Lab2;

public static class Program
{
    public static void Main()
    {
        
        Player player = new Player("Bill", 100, 10, 
            new Inventory(100),  new EquipmentManager());
        Item sword = new Weapon("Sword", 100, 20, EquipmentSlot.MainHand);
        Item sword2 = new Weapon("Sword2", 10, 20, EquipmentSlot.MainHand);
        Item shield = new Protection("Shield", 10, 13, EquipmentSlot.OffHand);
        Item potion = new Food("Potion", 1, 10);
        player.TryPickUpItem(sword);
        player.TryEquipItemFromInventory(sword.Id);
        player.TryPickUpItem(shield);
        player.TryPickUpItem(potion);
        player.TryPickUpItem(sword2);
        player.TryEquipItemFromInventory(sword2.Id);
        Console.WriteLine(player);
    }
}