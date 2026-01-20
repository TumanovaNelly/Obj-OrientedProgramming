using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class Inventory(IScale countScale) : IInventory
{
    private readonly Dictionary<Guid, IItem> _items = new();


    public bool TryAdd(IItem item)
    {
        if (_items.ContainsKey(item.Id) || countScale.IsFull)
            return false;
        
        _items[item.Id] = item;
        countScale.Increment(1);
        return true;
    }

    public bool TryRemove(Guid id, out IItem? item)
    {
        if (!_items.Remove(id, out item))
            return false;
        
        countScale.Decrement(1);
        return true;
    }

    public bool TryGetItem(Guid id, out IItem? item) 
        => _items.TryGetValue(id, out item);
}