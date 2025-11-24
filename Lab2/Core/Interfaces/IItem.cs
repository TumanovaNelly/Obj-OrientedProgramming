namespace Lab2.Core.Interfaces;

public interface IItem
{
    public Guid Id { get; }
    public string Name { get; }
    public int Weight { get; }
    
    IItemUseStrategy UseStrategy { get; }
}