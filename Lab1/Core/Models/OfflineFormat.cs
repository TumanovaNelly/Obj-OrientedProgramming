using Lab1.Core.Interfaces;

namespace Lab1.Core.Models;

public class OfflineFormat(Link link, ITime time) : ICourseFormat
{
    public IPlace Place { get; } = link;
    public ITime Time { get; } = time;
}