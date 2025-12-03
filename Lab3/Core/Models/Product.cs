namespace Lab3.Core.Models;

public class Product
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; } 
    public int Weight { get; }

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

    private decimal _price;

    public Product(string name, decimal price, int weight)
    {
        Name = name;
        Price = price;
        if (weight < 0)
            throw new ArgumentException("Weight cannot be negative");
        Weight = weight;
    }
}