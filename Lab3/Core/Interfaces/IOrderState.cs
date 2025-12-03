using Lab3.Core.Enums;
using Lab3.Core.Models;

namespace Lab3.Core.Interfaces;

public interface IOrderState
{
    public OrderStatus StatusName { get; }
    public DateTime SetAt { get; }
    public void ProceedToNext(Order order);
    public void Cancel(Order order);
}