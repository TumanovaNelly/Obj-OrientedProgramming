using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class ConsumeStrategy(int healAmount) : IItemUseStrategy
{
    public void Use(IPlayer player, IItem item)
    {
        player.Health.Increment(healAmount);
        player.TryDropItem(item.Id, out _);
    }
}