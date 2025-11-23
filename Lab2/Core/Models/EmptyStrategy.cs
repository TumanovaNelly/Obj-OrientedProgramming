using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class EmptyStrategy : IItemUseStrategy
{
    public void Use(IPlayer player, IItem item) { }
}