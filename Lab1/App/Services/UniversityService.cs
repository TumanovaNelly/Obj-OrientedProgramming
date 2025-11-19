using Lab1.App.DTOs;
using Lab1.Core.Interfaces;
using Lab1.Core.Models;

namespace Lab1.App.Services;

public class UniversityService(ICourseRepository courseRepository, IPersonRepository personRepository)
{
    private readonly ICourseRepository _coursesRepository = courseRepository;
    private readonly IPersonRepository _personRepository = personRepository;

    public CourseDTO CreateCourse(string title)
    {
        var course = new Course(title);
        _coursesRepository.Add(course);
        return new CourseDTO(course.Id, course.Title);
    }

    public PersonDTO CreatePerson(string firstName, string lastName)
    {
        var person = new Person(firstName, lastName);
        _personRepository.Add(person);
        return new PersonDTO(person.Id, person.FirstName, person.LastName);
    }
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

    public IEnumerable<CourseDTO> GetCourses() => _coursesRepository.GetAll()
            .Select(course => new CourseDTO(course.Id, course.Title, course.ResponsiblePerson?.Id));
    
    public IEnumerable<PersonDTO> GetPersons() => _personRepository.GetAll()
        .Select(person => new PersonDTO(person.Id, person.FirstName, person.LastName, person.IsTeacher, person.IsStudent));
    
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
    
    private Person GetPersonById(Guid personId)
    {
        var person = _personRepository.GetById(personId);
        if (person is null)
            throw new KeyNotFoundException($"Person with ID:{personId} not found");
        return person;
    }
    
    private Course GetCourseById(Guid courseId)
    {
        var course = _coursesRepository.GetById(courseId);
        if (course is null)
            throw new KeyNotFoundException($"Course with ID:{courseId} not found");
        return course;
    }
}