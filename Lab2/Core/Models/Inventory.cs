using System.Collections.ObjectModel;
using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class Inventory(int maxCapacity) : IInventory
{
    public IReadOnlyDictionary<Guid, IItem> Items => 
        new ReadOnlyDictionary<Guid, IItem>(_items);
    
    
    private readonly Dictionary<Guid, IItem> _items = new();
    private readonly Scale _capacity = new(maxCapacity, 0);
    

    public bool TryAdd(IItem item)
    {
        if (_items.ContainsKey(item.Id) || _capacity.CurrentValue + item.Weight > _capacity.MaxValue)
            return false;
        
        _items[item.Id] = item;
        _capacity.Increment(item.Weight);
        return true;
    }

    public bool TryRemove(Guid id, out IItem? item)
    {
        if (!_items.Remove(id, out item))
            return false;
        
        _capacity.Decrement(item.Weight);
        return true;
    }

    public bool TryGetItem(Guid id, out IItem? item) => _items.TryGetValue(id, out item);
}