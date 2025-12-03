using Lab3.Core.Interfaces;

namespace Lab3.Core.Models;

public class Delivery(string address) : IReceiptMethod
{
    public string Address { get; } = address;
    public decimal CalculatePrice(ShoppingCart cart) => cart.ItemsPrice < 1000 ? cart.ItemsWeight * 0.05m : 0; 
}


public class PuckUp(string address) : IReceiptMethod
{
    public string Address { get; } = address;
    public decimal CalculatePrice(ShoppingCart cart) => 0;
}