namespace Lab1.App.DTOs;

public record CourseDto(Guid Id, string Title, PersonDto? ResponsiblePerson = null)
{
    public override string ToString() =>
        $"{Id} \"{Title}\" ({(ResponsiblePerson is null ? "Преподаватель не назначен" : ResponsiblePerson)})";
};