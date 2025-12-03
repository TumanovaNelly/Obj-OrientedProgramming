using Lab3.Core.Interfaces;

namespace Lab3.Core.Models;

public class BasicTaxesCalculator : ITaxesCalculator
{
    public decimal CalculateTotalTaxAmount(decimal price, IEnumerable<ITax> taxes) 
        => taxes.Sum(tax => tax.CalculateAmount(price));
}