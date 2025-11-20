namespace Lab1.App.DTOs;

public record PersonDetailedDto(PersonDto GeneralInfo, List<CourseDto>? EnrolledCourses, List<CourseDto>? TaughtCourses);