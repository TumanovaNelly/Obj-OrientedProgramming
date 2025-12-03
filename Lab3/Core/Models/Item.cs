using System.Globalization;
using Lab3.Helpers;

namespace Lab3.Core.Models;

public class Item
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; } 
    public int UnitWeight { get; }
    public decimal UnitPrice
    {
        get => _unitPrice;
        set
        {
            if (value < 0)
                throw new ArgumentException("Price cannot be negative");
            _unitPrice = value;
        }
    }
    
    public int Count
    {
        get => _count;
        set
        {
            if (value < 0)
                throw new ArgumentException("Count cannot be negative");
            _count = value;
        }
    }
    public decimal TotalPrice => UnitPrice * Count;
    public int TotalWeight => UnitWeight * Count;

    private decimal _unitPrice;
    private int _count;

    public Item(string name, int unitWeight, decimal unitPrice, int count)
    {
        Name = name;
        UnitWeight = unitWeight;
        UnitPrice = unitPrice;
        Count = count;
    }

    public override string ToString()
        => Output.MiddleFill($"{Name} (x{Count})", UnitPrice.ToString(CultureInfo.InvariantCulture));
}