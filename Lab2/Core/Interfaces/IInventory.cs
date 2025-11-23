namespace Lab2.Core.Interfaces;

public interface IInventory
{
    public IScale Capacity { get; set; }
    public IReadOnlyDictionary<Guid, IItem> Items { get; }
    
    public bool TryAdd(IItem item);
    public bool TryRemove(Guid id, out IItem? item);
    public IItem? GetItem(Guid id);
}