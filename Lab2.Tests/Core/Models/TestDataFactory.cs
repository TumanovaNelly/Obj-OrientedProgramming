using Lab2.Core.Interfaces;
using Lab2.Core.Models;

namespace Lab2.Tests.Core.Models;

public static class TestDataFactory
{
    public static Sword CreateSword(string name = "Test Sword", int damage = 10)
    {
        return new Sword(name, damage);
    }

    public static Inventory CreateInventory(int capacity)
    {
        var scale = new Scale(capacity, 0); 
        return new Inventory(scale);
    }
    
    public static MockItem CreateMockItem()
    {
        return new MockItem();
    }

    public class MockItem : IItem
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name => "Mock Item";

        public bool UseByPlayer(ICharacter character, out IItem? droppedItem)
        {
            droppedItem = null;
            return true;
        }
    }
    
    public static Player CreatePlayer(int maxHealth = 100)
    {
        var weaponMgr = new WeaponManager();
        var protectMgr = new ProtectionManager();
        var healthScale = new Scale(maxHealth, maxHealth);
            
        return new Player(10, weaponMgr, protectMgr, healthScale);
    }
}