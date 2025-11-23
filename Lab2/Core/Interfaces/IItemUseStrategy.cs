namespace Lab2.Core.Interfaces;

public interface IItemUseStrategy
{
    public void Use(IPlayer player, IItem item);
}