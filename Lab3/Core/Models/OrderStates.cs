using Lab3.Core.Enums;
using Lab3.Core.Interfaces;

namespace Lab3.Core.Models;

public class AwaitingPaymentState : AOrderState
{
    public override OrderStatus StatusName => OrderStatus.AwaitingPayment;
    public override void ProceedToNext(Order order)
    {
        order.ChangeState(new CookingState());
    }

    public override void Cancel(Order order)
    {
        order.ChangeState(new CancelledState());
    }
}

public class CookingState : AOrderState
{
    public override OrderStatus StatusName => OrderStatus.Cooking;

    public override void ProceedToNext(Order order)
    {
        order.ChangeState(new DeliveryState());
    }

    public override void Cancel(Order order)
    {
        throw new InvalidOperationException("Cannot cancel order, it is already cooking");
    }
}

public class DeliveryState : AOrderState
{
    public override OrderStatus StatusName => OrderStatus.Delivering;

    public override void ProceedToNext(Order order)
    {
        order.ChangeState(new CompletedState());
    }

    public override void Cancel(Order order)
    {
        throw new InvalidOperationException("Cannot cancel order, it's already delivering");
    }
}

public class CompletedState : AOrderState
{
    public override OrderStatus StatusName => OrderStatus.Completed;
    public override void ProceedToNext(Order order) { }
    public override void Cancel(Order order) { }
}

public class CancelledState : AOrderState
{
    public override OrderStatus StatusName => OrderStatus.Cancelled;
    public override void ProceedToNext(Order order) { }
    public override void Cancel(Order order) { }
}