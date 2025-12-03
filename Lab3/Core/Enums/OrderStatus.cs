using System.ComponentModel;

namespace Lab3.Core.Enums;

public enum OrderStatus
{
    [Description("ожидает оплаты")]
    AwaitingPayment,
    [Description("готовится")]
    Cooking,
    [Description("ожидает получения/доставляется")]
    Delivering,
    [Description("получен")]
    Completed,
    [Description("отменен")]
    Cancelled
}