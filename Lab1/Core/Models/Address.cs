using Lab1.Core.Interfaces;

namespace Lab1.Core.Models;

public class Address(string street, int houseNumber, int classNumber) : IPlace
{
    public string Info => $"ул. {street}, д. {houseNumber}, ауд. {classNumber}";
}