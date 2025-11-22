using Lab1.Core.Interfaces;

namespace Lab1.Core.Models;

public enum DayOfWeek
{
    Понедельник,
    Вторник,
    Среда,
    Четверг,
    Пятница,
    Суббота,
    Воскресенье
}

public class DayOfWeekData(DayOfWeek dayOfWeek) : IData
{
    public string Info { get; } = dayOfWeek.ToString();
}