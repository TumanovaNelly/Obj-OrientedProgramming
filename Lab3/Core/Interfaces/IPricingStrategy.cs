using Lab3.Core.Models;

namespace Lab3.Core.Interfaces;

public interface IPricingStrategy
{
    decimal Calculate(Order order);
}