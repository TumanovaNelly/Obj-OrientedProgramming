using Lab3.Core.Enums;
using Lab3.Core.Interfaces;

namespace Lab3.Core.Models;

public class CreatedState : IOrderState
{
    public OrderStatus StatusName => OrderStatus.Created;

    public void ProceedToNext(Order order)
    {
        if (order.Items.Count == 0) 
            throw new ArgumentException("Order is empty");
        
        Console.WriteLine("Заказ передан на кухню.");
        order.ChangeState(new CookingState());
    }

    public void Cancel(Order order)
    {
        if (order.Items.Count == 0) 
            throw new ArgumentException("Order is empty");
        
        Console.WriteLine("Заказ отменен клиентом.");
        order.ChangeState(new CancelledState());
    }
}

public class CookingState : IOrderState
{
    public OrderStatus StatusName => OrderStatus.Cooking;

    public void ProceedToNext(Order order)
    {
        Console.WriteLine("Еда готова. Передаем курьеру.");
        order.ChangeState(new DeliveryState());
    }

    public void Cancel(Order order)
    {
        throw new InvalidOperationException("Cannot cancel order, it is already cooking");
    }
}

public class DeliveryState : IOrderState
{
    public OrderStatus StatusName => OrderStatus.Delivering;

    public void ProceedToNext(Order order)
    {
        Console.WriteLine("Курьер вручил заказ.");
        order.ChangeState(new CompletedState());
    }

    public void Cancel(Order order)
    {
        throw new InvalidOperationException("Cannot cancel order, it's already delivering");
    }
}

public class CompletedState : IOrderState
{
    public OrderStatus StatusName => OrderStatus.Completed;
    public void ProceedToNext(Order order) { }
    public void Cancel(Order order) { }
}

public class CancelledState : IOrderState
{
    public OrderStatus StatusName => OrderStatus.Cancelled;
    public void ProceedToNext(Order order) { }
    public void Cancel(Order order) { }
}