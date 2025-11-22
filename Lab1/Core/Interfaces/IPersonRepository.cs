using Lab1.Core.Models;

namespace Lab1.Core.Interfaces;

public interface IPersonRepository : IRepository<Person>
{
    public IEnumerable<Person> GetAllStudents();
    public IEnumerable<Person> GetAllTeachers();
}