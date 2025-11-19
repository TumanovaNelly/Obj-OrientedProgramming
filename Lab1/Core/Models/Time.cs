using Lab1.Core.Interfaces;

namespace Lab1.Core.Models;

public class Time(TimeOnly startTime, TimeOnly endTime, IData data) : ITime
{
    public TimeOnly StartTime { get; } = startTime;
    public TimeOnly EndTime { get; } = endTime;
    public IData Data { get; } = data;
}