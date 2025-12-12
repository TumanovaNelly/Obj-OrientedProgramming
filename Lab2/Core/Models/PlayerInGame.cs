using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class PlayerInGame(Player player, IInventory inventory)
{
    public bool TryPickUpItem(IItem item) => player.IsAlive && inventory.TryAdd(item);

    public bool TryDropItem(Guid itemId, out IItem? item)
    {
        item = null;
        return player.IsAlive && inventory.TryRemove(itemId, out item);
    }
    
    public bool TryUseItem(Guid itemId)
    {
        if (!player.IsAlive || 
            !inventory.TryGetItem(itemId, out IItem? item) || item is null ||
            !item.UseByPlayer(player, out IItem? droppedItem)) 
            return false;
        
        inventory.TryRemove(itemId, out _);
        
        if (droppedItem is not null) 
            inventory.TryAdd(droppedItem);
        return true;
    }

    public void Attack(PlayerInGame targetPlayer)
    {
        if (!player.IsAlive)
            return;

        player.Attack(targetPlayer);
    }
    
    public void TakeDamage(int damage)
    {
        if (!player.IsAlive)
            return;
        
        player.TakeDamage(damage);
    }
}
