using Lab1.Core.Interfaces;
using Lab1.Core.Models;
using Moq;

namespace Lab1.Tests.Core.Models;

public static class CourseTest
{
    public class Constructor
    {
        [Fact]
        public void Should_InitializeWithCorrectTitleAndNewGuid()
        {
            // Arrange
            const string expectedTitle = "Domain-Driven Design Fundamentals";

            // Act
            var course = new Course(expectedTitle);

            // Assert
            Assert.Equal(expectedTitle, course.Title);
            Assert.NotEqual(Guid.Empty, course.Id);
        }

        [Fact]
        public void Should_StartWithNoResponsiblePersonAndEmptyCollections()
        {
            // Arrange & Act
            var course = new Course("Test Course");

            // Assert
            Assert.Null(course.ResponsiblePerson);
            Assert.Empty(course.EnrolledPersons);
            Assert.Empty(course.Formats);
        }
    }

    public class AssignResponsiblePerson
    {
        // Arrange
        private readonly Course _course = new("Test Course");
        private readonly Person _person = new("John", "Doe");
        
        [Fact]
        public void Should_SetTheProvidedPersonAsResponsible()
        {
            // Act
            _course.AssignResponsiblePerson(_person);

            // Assert
            Assert.NotNull(_course.ResponsiblePerson);
            Assert.Same(_person, _course.ResponsiblePerson); 
        }

        [Fact]
        public void Should_ReplaceExistingResponsiblePerson_WhenCalledAgain()
        {
            // Arrange
            _course.AssignResponsiblePerson(_person);
            var newTeacher = new Person("Peter", "Jones");
            
            // Act
            _course.AssignResponsiblePerson(newTeacher);

            // Assert
            Assert.Same(newTeacher, _course.ResponsiblePerson);
        }
    }

    public class AddEnrolledPerson
    {
        // Arrange
        private readonly Course _course = new("Test Course");
        private readonly Person _person = new("John", "Doe");
        
        [Fact]
        public void Should_AddPersonToEnrolledList_WhenListIsEmpty()
        {
            // Act
            _course.AddEnrolledPerson(_person);

            // Assert
            Assert.Single(_course.EnrolledPersons);
            Assert.Contains(_person, _course.EnrolledPersons);
        }

        [Fact]
        public void Should_NotAddSamePerson_WhenPersonIsAlreadyEnrolled()
        {
            _course.AddEnrolledPerson(_person);

            // Act
            _course.AddEnrolledPerson(_person);

            // Assert
            Assert.Single(_course.EnrolledPersons);
        }

        [Fact]
        public void Should_AddMultipleDifferentPersons()
        {
            // Arrange
            var student2 = new Person("Bob", "Builder");

            // Act
            _course.AddEnrolledPerson(_person);
            _course.AddEnrolledPerson(student2);

            // Assert
            Assert.Equal(2, _course.EnrolledPersons.Count);
            Assert.Contains(_person, _course.EnrolledPersons);
            Assert.Contains(student2, _course.EnrolledPersons);
        }
    }

    public class AddFormat
    {
        // Arrange
        private readonly Course _course = new("Advanced Formats");
        private readonly ICourseFormat _formatObject = new Mock<ICourseFormat>().Object;
        
        [Fact]
        public void Should_AddFormatToTheList()
        {
            // Act
            _course.AddFormat(_formatObject);

            // Assert
            Assert.Single(_course.Formats);
            Assert.Contains(_formatObject, _course.Formats);
        }
        
        [Fact]
        public void Should_AddSameFormatMultipleTimes_WhenCalledRepeatedly()
        {
            // Act
            _course.AddFormat(_formatObject);
            _course.AddFormat(_formatObject);
            
            // Assert
            Assert.Single(_course.Formats);
            Assert.Contains(_formatObject, _course.Formats);
        }
    }
}