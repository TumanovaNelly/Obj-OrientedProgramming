using Lab3.Helpers;
using Lab3.Core.Enums;
using Lab3.Core.Models;

namespace Lab3.Core.Interfaces;

public abstract class AOrderState : IOrderState
{
    public abstract OrderStatus StatusName { get; }
    public DateTime SetAt { get; } = DateTime.Now;
    public abstract void ProceedToNext(Order order);
    public abstract void Cancel(Order order);

    public override string ToString() 
        => $"Заказ {StatusName.GetDescription()} {SetAt.ToShortDateString()} в {SetAt.ToShortTimeString()}";
}