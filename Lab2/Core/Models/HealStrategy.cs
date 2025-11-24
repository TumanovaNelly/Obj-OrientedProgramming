using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class HealStrategy(int healAmount) : IItemUseStrategy
{
    public void Use(IPlayer player, IItem item)
    {
        player.Heal(healAmount);
        player.TryDropItem(item.Id, out _);
    }
}