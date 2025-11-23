using Lab2.Core.Enums;
using Lab2.Core.Models;

namespace Lab2;

public static class Program
{
    public static void Main()
    {
        
        Player player = new Player("Bill", 100, 10, 
            new Inventory(100),  new EquipmentManager());
        Item sword = new Weapon("Sword", 10, 20, EquipmentSlot.MainHand);
        Item shield = new Protection("Shield", 10, 13, EquipmentSlot.OffHand);
        Item potion = new Food("Potion", 1, 10);
        player.TryPickUpItem(sword);
        player.TryPickUpItem(shield);
        player.TryPickUpItem(potion);
    }
}