using Lab1.Core.Interfaces;

namespace Lab1.Core.Models;

public class Link(string link) : IPlace
{
    public string Info => $"Онлайн занятие: {link}";
}