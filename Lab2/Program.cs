using Lab2.Core.Enums;
using Lab2.Core.Interfaces;
using Lab2.Core.Models;

namespace Lab2;

public static class Program
{
    public static void Main()
    {
        IItem item1 = new Armor("Броня броненосца", 120);
        IItem item2 = new Pants("Штаны обыкновенные", 1);
        IItem item3 = new Sword("Меч-леденец", 100);
        IItem item4 = new Gun("Просто пушка", 1000);
        IItem item5 = new Food("Пельмени", 1000);
        IItem item6 = new Potion("Варенье из шишек", 10);
        
        Player player = new PlayerBuilder()
            .SetBaseDamage(10)
            .SetHealthScale(new Scale(1000, 1000))
            .SetWeaponManager(new WeaponManager())
            .SetInventory(new Inventory(new Scale(5, 0)))
            .SetProtectionManager(new ProtectionManager())
            .Build();
        

        player.TryPickUpItem(item1);
        player.TryPickUpItem(item2);
        player.TryPickUpItem(item3);
        player.TryPickUpItem(item4);
        player.TryPickUpItem(item5);

        Console.Write(player.TryUseItem(item3.Id));
        player.TryPickUpItem(item6);
        player.TryUseItem(item4.Id);
        player.TryUseItem(item5.Id);
        player.TryUseItem(item1.Id);
        player.TryUseItem(item2.Id);
    }
}