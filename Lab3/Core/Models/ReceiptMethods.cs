using Lab3.Core.Interfaces;

namespace Lab3.Core.Models;

public class Delivery(string address) : IReceiptMethod
{
    public string Address { get; } = address;
    public decimal CalculatePrice(decimal weight, decimal price) => price < 1000 ? weight * 0.05m : 0; 
}


public class PuckUp(string address) : IReceiptMethod
{
    public string Address { get; } = address;
    public decimal CalculatePrice(decimal weight, decimal price) => 0;
}