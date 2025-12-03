using System.Globalization;
using Lab3.Helpers;

namespace Lab3.Core.Models;

public class Item
{
    public Guid ProductId => _product.Id;
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

    public decimal TotalPrice => _product.Price * Count;
    public int TotalWeight => _product.Weight * Count;

    private readonly Product _product;
    private int _count;

    public Item(Product product, int count)
    {
        _product = product;
        Count = count;
    }

    public override string ToString()
        => Output.MiddleFill($"{_product.Name} (x{Count})", 
            _product.Price.ToString(CultureInfo.InvariantCulture));
}