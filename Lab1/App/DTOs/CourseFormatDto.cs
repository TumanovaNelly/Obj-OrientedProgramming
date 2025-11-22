namespace Lab1.App.DTOs;

public record CourseFormatDto(string Place, string Time)
{
    public override string ToString() => $"{Place} ({Time})";
}