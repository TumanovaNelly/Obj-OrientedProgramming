namespace Lab1.App.DTOs;

public record CourseDTO(Guid Id, string Title, Guid? ResponsiblePersonId = null) {}