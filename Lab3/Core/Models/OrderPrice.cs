using System.Globalization;
using Lab3.Helpers;

namespace Lab3.Core.Models;

public class OrderPrice
{
    public decimal ItemsPrice { get; init; }
    public decimal DeliveryCost { get; init; }
    public decimal DiscountAmount { get; init; }
    public decimal TaxAmount { get; init; }
    public decimal TotalAmount => 
        ItemsPrice + DeliveryCost - DiscountAmount + TaxAmount;

    public override string ToString()
        => $"""
            {Output.MiddleFill("Стоимость товаров", ItemsPrice.ToString(CultureInfo.InvariantCulture))}
            {Output.MiddleFill("Стоимость доставки", DeliveryCost.ToString(CultureInfo.InvariantCulture))}
            {Output.MiddleFill("Скидка", (-DiscountAmount).ToString(CultureInfo.InvariantCulture))}
            {Output.MiddleFill("Налог", TaxAmount.ToString(CultureInfo.InvariantCulture))}
            {Output.Fill()}
            {Output.MiddleFill("ИТОГОВАЯ СТОИМОСТЬ", TotalAmount.ToString(CultureInfo.InvariantCulture))}
            """;
}