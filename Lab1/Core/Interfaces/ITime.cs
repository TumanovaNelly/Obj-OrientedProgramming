namespace Lab1.Core.Interfaces;

public interface ITime
{
    public TimeOnly StartTime { get; }
    
    public TimeOnly EndTime { get; }
    
    public IData Data { get; }

    public string Info => $"{Data.Info} c {StartTime} до {EndTime}";
}