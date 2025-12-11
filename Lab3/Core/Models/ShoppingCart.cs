namespace Lab3.Core.Models;

public class ShoppingCart
{
    public IReadOnlyCollection<Item> Items => _items.Values.ToList().AsReadOnly();
    public decimal ItemsPrice => _items.Values.Sum(x => x.TotalPrice);
    public decimal ItemsWeight => _items.Values.Sum(x => x.TotalWeight);
    
    private readonly Dictionary<Guid, Item> _items = new();


    public void AddItem(Item item)
    {
        if (item.Count == 0)
            throw new InvalidOperationException("Cannot add empty item to a shopping cart");
        if (!_items.TryAdd(item.ProductId, item))
            _items[item.ProductId].Count += item.Count;
        else _items[item.ProductId] = item;
    }

    public void RemoveItem(Guid itemId)
    {
        if (!_items.TryGetValue(itemId, out var item))
            throw new KeyNotFoundException("Item not found");
        --item.Count;
        if (item.Count == 0)
            _items.Remove(itemId);
    }

    public OrderBuilder CreateOrder() => new(this);
}