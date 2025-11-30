using Lab3.Core.Enums;
using Lab3.Core.Interfaces;

namespace Lab3.Core.Models;

public class Order(DateTime date)
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public bool IsGiftWrapped { get; init; } = false;
    public bool IsExpressDelivery { get; init; } = false;
    public OrderStatus Status => _state.StatusName;
    public IReadOnlyCollection<Item> Items => _items.AsReadOnly();
    
    private readonly List<Item> _items = [];
    private IOrderState _state = new CreatedState();

    public void AddItem(Item item)
    {
        if (_state is not CreatedState)
            throw new InvalidOperationException("Invalid state of order");
        
        _items.Add(item);
    }

    public void Proceed()
    {
        _state.ProceedToNext(this);
    }

    public void Cancel()
    {
        _state.Cancel(this);
    }

    public decimal GetTotalCost(IPricingStrategy pricingStrategy)
    {
        return _items.Count == 0 ? 0 : pricingStrategy.Calculate(this);
    }
    
    public decimal SubTotal => _items.Sum(x => x.Price * x.Count);

    internal void ChangeState(IOrderState newState)
    {
        _state = newState;
    }
}