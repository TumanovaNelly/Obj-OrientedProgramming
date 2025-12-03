using Lab3.Core.Interfaces;
using Lab3.Helpers;

namespace Lab3.Core.Models;

public class Order(IEnumerable<Item> items, OrderPrice price)
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime CreatedAt { get; } = DateTime.Now;
    public string? Status => _state.ToString();
    
    private IOrderState _state = new AwaitingPaymentState();
    private readonly List<Item> _items = items.ToList();
    
    public void Proceed() => 
        _state.ProceedToNext(this);
    
    public void Cancel() => 
        _state.Cancel(this);

    public string GetCheque()
    {
        List<string> lines = [];
        lines.Add(Output.WordInMiddle(" ЧЕК "));
        lines.AddRange(_items.Select(item => item.ToString()));
        lines.Add(Output.Fill());
        lines.Add(price.ToString());
        lines.Add(Output.Fill('='));
        return string.Join(Environment.NewLine, lines);
    }
    
    internal void ChangeState(IOrderState newState) => 
        _state = newState;
}