namespace Lab1.App.DTOs;

public record CourseDetailedDto(CourseDto GeneralInfo, List<CourseFormatDto> CourseFormats, List<PersonDto> EnrolledPersons);