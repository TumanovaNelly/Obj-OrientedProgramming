using Lab1.Core.Interfaces;

namespace Lab1.Core.Models;

public class Course(string title) : IEntity
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Title { get; } = title;
    public Person? ResponsiblePerson { get; private set; }
    public IReadOnlyList<Person> EnrolledPersons => _enrolledPersons.AsReadOnly();
    public IReadOnlyList<ICourseFormat> Formats => _formats.AsReadOnly();
    
    
    private readonly List<Person> _enrolledPersons = [];
    private readonly List<ICourseFormat> _formats = [];

    public void AssignResponsiblePerson(Person person) => ResponsiblePerson = person;

    public void AddEnrolledPerson(Person person)
    {
        if (!_enrolledPersons.Contains(person)) 
            _enrolledPersons.Add(person);
    }
    
    public void AddFormat(ICourseFormat format) => _formats.Add(format);
}