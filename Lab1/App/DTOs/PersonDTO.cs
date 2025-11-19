namespace Lab1.App.DTOs;

public record PersonDTO(Guid Id, string FirstName, string LastName, bool IsTeacher = false, bool IsStudent = false);