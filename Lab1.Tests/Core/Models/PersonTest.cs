using Lab1.Core.Models;

namespace Lab1.Tests.Core.Models;

public static class PersonTest
{
    public class Constructor
    {
        [Fact]
        public void Should_InitializeWithCorrectNamesAndGuid()
        {
            // Arrange
            const string firstName = "John";
            const string lastName = "Doe";

            // Act
            var person = new Person(firstName, lastName);

            // Assert
            Assert.Equal(firstName, person.FirstName);
            Assert.Equal(lastName, person.LastName);
            Assert.NotEqual(Guid.Empty, person.Id);
        }

        [Fact]
        public void Should_InitiallyBeNeitherStudentNorTeacher()
        {
            // Arrange
            const string firstName = "John";
            const string lastName = "Doe";

            // Act
            var person = new Person(firstName, lastName);

            // Assert
            Assert.False(person.IsStudent);
            Assert.False(person.IsTeacher);
        }
    }

    public class RolePromotion
    {
        // Arrange
        private readonly Person _person = new("Bob", "Builder");
        
        [Fact]
        public void PromoteToStudent_Should_MakePersonAStudent_WhenNotAlreadyOne()
        {
            // Act
            _person.PromoteToStudent("Group-A");

            // Assert
            Assert.True(_person.IsStudent);
            Assert.False(_person.IsTeacher);
        }

        [Fact]
        public void PromoteToTeacher_Should_MakePersonATeacher_WhenNotAlreadyOne()
        {
            // Act
            _person.PromoteToTeacher("Construction Dept.");

            // Assert
            Assert.True(_person.IsTeacher);
            Assert.False(_person.IsStudent);
        }

        [Fact]
        public void PromoteToStudent_Should_DoNothing_WhenAlreadyAStudent()
        {
            // Arrange
            _person.PromoteToStudent("Group-B");

            // Act
            _person.PromoteToStudent("Group-C");

            // Assert
            Assert.True(_person.IsStudent);
        }
    }

    public class CourseInteractions
    {
        // Arrange
        private readonly Person _person = new("Bob", "Builder");
        private readonly Course _course = new("Building");
        
        [Fact]
        public void AddEnrolledCourse_Should_ThrowArgumentException_WhenPersonIsNotAStudent()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _person.AddEnrolledCourse(_course));
        }

        [Fact]
        public void AddTaughtCourse_Should_ThrowArgumentException_WhenPersonIsNotATeacher()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _person.AddTaughtCourse(_course));
        }

        [Fact]
        public void AddEnrolledCourse_Should_Succeed_WhenPersonIsAStudent()
        {
            // Arrange
            _person.PromoteToStudent("Gryffindor");
            
            // Act
            var exception = Record.Exception(() => _person.AddEnrolledCourse(_course));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void AddTaughtCourse_Should_Succeed_WhenPersonIsATeacher()
        {
            // Arrange
            _person.PromoteToTeacher("Potions");

            // Act
            var exception = Record.Exception(() => _person.AddTaughtCourse(_course));

            // Assert
            Assert.Null(exception);
        }
    }
}