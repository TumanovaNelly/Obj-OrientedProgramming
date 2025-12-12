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
        IItem item5 = new Potion("Зелье от всех проблем", 100);
        IItem item6 = new Food("Пельмени", 1000);
        
        Player player1 = new PlayerBuilder()
            .SetHealthScale(new Scale(1000, 1000))
            .SetWeaponManager(new WeaponManager())
            .SetProtectionManager(new ProtectionManager())
            .Build();
        
        PlayerInGame playerInGame1 = new PlayerInGame(player1, new Inventory(new Scale(5, 0)));
        
        Player player2 = new PlayerBuilder()
            .SetHealthScale(new Scale(1000, 1000))
            .SetWeaponManager(new WeaponManager())
            .SetProtectionManager(new ProtectionManager())
            .Build();
        
        PlayerInGame playerInGame2 = new PlayerInGame(player2, new Inventory(new Scale(5, 0)));

        playerInGame1.TryPickUpItem(item1);
        playerInGame1.TryPickUpItem(item2);
        playerInGame1.TryPickUpItem(item3);
        playerInGame1.TryPickUpItem(item4);
        playerInGame1.TryPickUpItem(item5);
        
        playerInGame2.TryPickUpItem(item1);
        playerInGame2.TryPickUpItem(item2);
        playerInGame2.TryPickUpItem(item3);
        playerInGame2.TryPickUpItem(item4);
        playerInGame2.TryPickUpItem(item6);
        
        playerInGame1.TryDropItem(item1.Id, out _);
        playerInGame1.TryDropItem(item3.Id, out _);

        playerInGame1.TryUseItem(item4.Id);
        playerInGame1.TryUseItem(item3.Id);
    }
}