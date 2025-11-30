namespace Lab3.Core.Models;

public class Item
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; } 
    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
                throw new ArgumentException("Price cannot be negative");
            _price = value;
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

    private decimal _price;
    private int _count;

    public Item(string name, decimal price, int count)
    {
        Name = name;
        Price = price;
        Count = count;
    }
}