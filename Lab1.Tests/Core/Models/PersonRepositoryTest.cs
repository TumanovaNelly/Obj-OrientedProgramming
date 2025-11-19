using Lab1.Core.Models;

namespace Lab1.Tests.Core.Models;

public static class PersonRepositoryTest
{
    public class BasicCrudOperations
    {
        // Arrange
        private readonly PersonRepository _repository = new();
        private readonly Person _person1 = new("John", "Doe");
        private readonly Person _person2 = new("Jane", "Smith");
        
        [Fact]
        public void Add_And_GetById_Should_StoreAndRetrievePerson()
        {
            // Act
            _repository.Add(_person1);
            var retrievedPerson = _repository.GetById(_person1.Id);

            // Assert
            Assert.NotNull(retrievedPerson);
            Assert.Same(_person1, retrievedPerson);
        }

        [Fact]
        public void GetById_Should_ReturnNull_WhenPersonNotFound()
        {
            // Act
            var retrievedPerson = _repository.GetById(Guid.NewGuid()); 

            // Assert
            Assert.Null(retrievedPerson);
        }
        
        [Fact]
        public void GetAll_Should_ReturnAllAddedPersons()
        {
            // Arrange
            _repository.Add(_person1);
            _repository.Add(_person2);

            // Act
            var allPersons = _repository.GetAll().ToList();

            // Assert
            Assert.Equal(2, allPersons.Count);
            Assert.Contains(_person1, allPersons);
            Assert.Contains(_person2, allPersons);
        }

        [Fact]
        public void Remove_Should_DeletePersonFromStorage()
        {
            // Arrange
            _repository.Add(_person1);

            // Act
            _repository.Remove(_person1.Id);
            var retrievedPerson = _repository.GetById(_person1.Id);

            // Assert
            Assert.Null(retrievedPerson);
        }
    }

    public class RoleBasedQueries
    {
        private readonly PersonRepository _repository;
        private readonly Person _student;
        private readonly Person _teacher;
        private readonly Person _mentor; 
        private readonly Person _plainPerson;

        public RoleBasedQueries()
        {
            // Arrange
            _repository = new PersonRepository();

            _student = new Person("Alice", "Student");
            _student.PromoteToStudent("S123");

            _teacher = new Person("Bob", "Teacher");
            _teacher.PromoteToTeacher("IT Dept.");
            
            _mentor = new Person("Charlie", "Mentor");
            _mentor.PromoteToStudent("S456");
            _mentor.PromoteToTeacher("Science Dept.");

            _plainPerson = new Person("David", "Plain");

            _repository.Add(_student);
            _repository.Add(_teacher);
            _repository.Add(_mentor);
            _repository.Add(_plainPerson);
        }
        
        [Fact]
        public void GetAllStudents_Should_ReturnOnlyPersonsWithStudentRole()
        {
            // Act
            var students = _repository.GetAllStudents().ToList();

            // Assert
            Assert.Equal(2, students.Count);
            Assert.Contains(_student, students);
            Assert.Contains(_mentor, students);
            Assert.DoesNotContain(_teacher, students);
            Assert.DoesNotContain(_plainPerson, students);
        }

        [Fact]
        public void GetAllTeachers_Should_ReturnOnlyPersonsWithTeacherRole()
        {
            // Act
            var teachers = _repository.GetAllTeachers().ToList();

            // Assert
            Assert.Equal(2, teachers.Count);
            Assert.Contains(_teacher, teachers);
            Assert.Contains(_mentor, teachers);
            Assert.DoesNotContain(_student, teachers);
            Assert.DoesNotContain(_plainPerson, teachers);
        }
        
        [Fact]
        public void GetAllStudents_Should_ReturnEmpty_WhenNoStudentsExist()
        {
            // Arrange
            var emptyRepo = new PersonRepository();
            emptyRepo.Add(_teacher);

            // Act
            var students = emptyRepo.GetAllStudents();

            // Assert
            Assert.Empty(students);
        }
        
        [Fact]
        public void GetAllTeachers_Should_ReturnEmpty_WhenNoTeachersExist()
        {
            // Arrange
            var emptyRepo = new PersonRepository();
            emptyRepo.Add(_student);

            // Act
            var teachers = emptyRepo.GetAllTeachers();

            // Assert
            Assert.Empty(teachers);
        }
    }
}