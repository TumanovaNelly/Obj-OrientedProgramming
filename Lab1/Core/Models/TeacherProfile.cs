namespace Lab1.Core.Models;

public class TeacherProfile(string department)
{
    public string Department { get; } = department;
    public IReadOnlyList<Course> TaughtCourses => _taughtCourses.AsReadOnly();
    
    private readonly List<Course> _taughtCourses = [];
    
    public void AddTaughtCourse(Course course) => _taughtCourses.Add(course);
}