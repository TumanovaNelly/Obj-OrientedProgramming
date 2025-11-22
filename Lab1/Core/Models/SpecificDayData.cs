using Lab1.Core.Interfaces;

namespace Lab1.Core.Models;

public class SpecificDayData(DateOnly date) : IData
{
    public string Info { get; } = date.ToShortDateString();
}