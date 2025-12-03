using Lab3.Core.Interfaces;

namespace Lab3.Core.Models;

public class BasicTax : ITax
{
    public decimal CalculateAmount(decimal price) => price * 0.2M;
}
