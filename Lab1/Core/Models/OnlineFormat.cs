using Lab1.Core.Interfaces;

namespace Lab1.Core.Models;

public class OnlineFormat(Address address, ITime time) : ICourseFormat
{
    public IPlace Place { get; } = address;
    public ITime Time { get; } = time;
}