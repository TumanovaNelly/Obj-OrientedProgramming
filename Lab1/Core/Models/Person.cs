using Lab1.Core.Interfaces;

namespace Lab1.Core.Models;

public class Person(string firstName, string lastName) : IEntity
{
    public Guid Id { get; } = Guid.NewGuid();
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public bool IsTeacher => _teacherInfo is not null;
    public bool IsStudent => _studentInfo is not null;

    
    private StudentProfile? _studentInfo;
    private TeacherProfile? _teacherInfo;

    
    public void PromoteToTeacher(string department)
    {
        if (!IsTeacher)
            _teacherInfo = new TeacherProfile(department);
    }
    
    public void PromoteToStudent(string group)
    {
        if (!IsStudent)
            _studentInfo = new StudentProfile(group);
    }

    public void AddTaughtCourse(Course course)
    {
        if (!IsTeacher) 
            throw new ArgumentException("Person is not a teacher");
        _teacherInfo!.AddTaughtCourse(course);
    }
    
    public void AddEnrolledCourse(Course course)
    {
        if (!IsStudent)
            throw new ArgumentException("Person is not a student");
        _studentInfo!.AddEnrolledCourse(course);
    }
}