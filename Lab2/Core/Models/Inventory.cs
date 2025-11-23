using System.Collections.ObjectModel;
using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class Inventory(int maxCapacity) : IInventory
{
    public IScale Capacity { get; set; } = new Scale(maxCapacity, 0);
    
    public IReadOnlyDictionary<Guid, IItem> Items => 
        new ReadOnlyDictionary<Guid, IItem>(_items);
    
    private readonly Dictionary<Guid, IItem> _items = new();

    public bool TryAdd(IItem item)
    {
        if (_items.ContainsKey(item.Id) || Capacity.CurrentValue + item.Weight > Capacity.MaxValue)
            return false;
        
        _items[item.Id] = item;
        Capacity.Increment(item.Weight);
        return true;
    }

    public bool TryRemove(Guid id, out IItem? item)
    {
        if (!_items.Remove(id, out item))
            return false;
        
        Capacity.Decrement(item.Weight);
        return true;
    }

    public IItem? GetItem(Guid id) => _items.TryGetValue(id, out var item) ? (Item?)item : null;
}