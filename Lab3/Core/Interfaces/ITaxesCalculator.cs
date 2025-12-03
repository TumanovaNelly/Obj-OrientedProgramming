namespace Lab3.Core.Interfaces;

public interface ITaxesCalculator
{
    public decimal CalculateTotalTaxAmount(decimal price, IEnumerable<ITax> taxes);
}