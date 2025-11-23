using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class Item(string name, int weight, IItemUseStrategy? useStrategy = null) : IItem
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; } = name;
    public int Weight { get; } = weight;
    public IItemUseStrategy UseStrategy { get; } = useStrategy ?? new EmptyStrategy();
}