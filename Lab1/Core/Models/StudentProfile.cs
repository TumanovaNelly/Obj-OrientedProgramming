namespace Lab1.Core.Models;

public class StudentProfile(string group)
{
    public string Group { get; } = group;
    public IReadOnlyList<Course> EnrolledCourses => _enrolledCourses.AsReadOnly();
    
    private readonly List<Course> _enrolledCourses = [];
    
    public void AddEnrolledCourse(Course course) 
    {
        if (!_enrolledCourses.Contains(course))
            _enrolledCourses.Add(course);
    }
}