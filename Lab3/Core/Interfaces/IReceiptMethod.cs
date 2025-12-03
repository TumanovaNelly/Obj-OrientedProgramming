using Lab3.Core.Models;

namespace Lab3.Core.Interfaces;

public interface IReceiptMethod
{
    public string Address { get; }
    public decimal CalculatePrice(decimal weight, decimal price);
}