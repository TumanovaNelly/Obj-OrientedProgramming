using Lab1.Core.Interfaces;

namespace Lab1.Core.Models;

public class PersonRepository : ARepository<Person>, IPersonRepository
{
    public IEnumerable<Person> GetAllStudents() => Storage.Values.Where(p => p.IsStudent);
    public IEnumerable<Person> GetAllTeachers() => Storage.Values.Where(p => p.IsTeacher);
}