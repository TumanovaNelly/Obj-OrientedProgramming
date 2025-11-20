using Lab1.App.DTOs;
using Lab1.Core.Interfaces;
using Lab1.Core.Models;

namespace Lab1.App.Services;

public class UniversityService(ICourseRepository courseRepository, IPersonRepository personRepository)
{
    public Guid AddCourse(string title)
    {
        var course = new Course(title);
        courseRepository.Add(course);
        return course.Id;
    }
    
    public void DeleteCourse(Guid courseId) => courseRepository.Remove(courseId);

    public Guid AddPerson(string firstName, string lastName)
    {
        var person = new Person(firstName, lastName);
        personRepository.Add(person);
        return person.Id;
    } 
    
    public void DeletePerson(Guid personId) => personRepository.Remove(personId);
    
    public void PromotePersonToStudent(Guid personId, string group) 
        => GetPersonById(personId).PromoteToStudent(group);
    
    public void PromotePersonToTeacher(Guid personId, string department) 
        => GetPersonById(personId).PromoteToTeacher(department);

    public void AssignTeacherToCourse(Guid courseId, Guid teacherId)
    {
        var course = GetCourseById(courseId);
        var teacher = GetTeacherById(teacherId);
        
        teacher.AddTaughtCourse(course);
        course.AssignResponsiblePerson(teacher);
    }

    public void AssignStudentToCourse(Guid courseId, Guid studentId)
    {
        var course = GetCourseById(courseId);
        var student = GetStudentById(studentId);
        
        student.AddEnrolledCourse(course);
        course.AddEnrolledPerson(student);
    }
    
    public List<PersonDto> GetAllPersonsInfo() => personRepository.GetAll()
        .Select(GetPersonDto).ToList();

    public PersonDetailedDto GetPersonDetailedInfo(Guid personId)
    {
        var person = GetPersonById(personId);
        var generalInfo = GetPersonDto(person);
        var enrolledCoursesInfo = 
            person.IsStudent ? person.EnrolledCourses.Select(GetCourseDto).ToList() : null;
        var taughtCoursesInfo = 
            person.IsTeacher ? person.TaughtCourses.Select(GetCourseDto).ToList() : null;
        return new PersonDetailedDto(generalInfo, enrolledCoursesInfo, taughtCoursesInfo);
    }
    
    public List<CourseDto> GetAllCoursesInfo() => courseRepository.GetAll()
        .Select(GetCourseDto).ToList();

    public CourseDetailedDto GetCourseDetailedInfo(Guid courseId)
    {
        var course = GetCourseById(courseId);
        var generalInfo = GetCourseDto(course);
        var formatsInfo = course.Formats.Select(GetCourseFormatDto).ToList();
        var enrolledPersonsInfo = course.EnrolledPersons.Select(GetPersonDto).ToList();
        return new CourseDetailedDto(generalInfo, formatsInfo, enrolledPersonsInfo);
    }
    private PersonDto GetPersonDto(Person person) =>
        new(person.Id, person.FirstName, person.LastName, person.IsTeacher, person.IsStudent);
    
    private CourseDto GetCourseDto(Course course) => new(course.Id, course.Title, 
        course.ResponsiblePerson is not null ? GetPersonDto(course.ResponsiblePerson) : null );
    
    private CourseFormatDto GetCourseFormatDto(ICourseFormat format) => new(format.Place.Info, format.Time.Info);
    
    private Person GetPersonById(Guid personId)
    {
        var person = personRepository.GetById(personId);
        if (person is null)
            throw new KeyNotFoundException($"Person with ID:{personId} not found");
        return person;
    }
    
    private Person GetTeacherById(Guid teacherId)
    {
        var teacher = GetPersonById(teacherId);
        if (!teacher.IsTeacher)
            throw new KeyNotFoundException($"Teacher with ID:{teacherId} not found");
        return teacher;
    }
    
    private Person GetStudentById(Guid studentId)
    {
        var student = GetPersonById(studentId);
        if (!student.IsStudent)
            throw new KeyNotFoundException($"Student with ID:{studentId} not found");
        return student;
    }
    
    private Course GetCourseById(Guid courseId)
    {
        var course = courseRepository.GetById(courseId);
        if (course is null)
            throw new KeyNotFoundException($"Course with ID:{courseId} not found");
        return course;
    }
}