using Moq;
using Lab1.App.Services;
using Lab1.Core.Interfaces;
using Lab1.Core.Models;

namespace Lab1.Tests.App.Services;

public class UniversityServiceTests
{
    // Arrange
    private readonly Mock<ICourseRepository> _mockCourseRepo = new();
    private readonly Mock<IPersonRepository> _mockPersonRepo = new();
    
    private readonly UniversityService _universityService;

    protected UniversityServiceTests()
    {
        _universityService = new UniversityService(_mockCourseRepo.Object, _mockPersonRepo.Object);
    }
    
    public class CreationMethods : UniversityServiceTests
    {
        [Fact]
        public void CreateCourse_Should_AddCourseToRepositoryAndReturnCorrectDto()
        {
            // Arrange
            var courseTitle = "Advanced TDD";
            
            // Act
            var resultDto = _universityService.CreateCourse(courseTitle);
            
            // Assert
            _mockCourseRepo.Verify(repo => repo.Add(It.IsAny<Course>()), Times.Once);
            Assert.NotNull(resultDto);
            Assert.Equal(courseTitle, resultDto.Title);
            Assert.NotEqual(Guid.Empty, resultDto.Id);
        }

        [Fact]
        public void CreatePerson_Should_AddPersonToRepositoryAndReturnCorrectDto()
        {
            // Arrange
            var firstName = "John";
            var lastName = "Doe";

            // Act
            var resultDto = _universityService.CreatePerson(firstName, lastName);

            // Assert
            _mockPersonRepo.Verify(repo => repo.Add(It.IsAny<Person>()), Times.Once);

            Assert.NotNull(resultDto);
            Assert.Equal(firstName, resultDto.FirstName);
            Assert.Equal(lastName, resultDto.LastName);
        }
    }
    
    public class AssignmentMethods : UniversityServiceTests
    {
        [Fact]
        public void AssignTeacherToCourse_Should_Succeed_WhenPersonIsATeacher()
        {
            // Arrange
            var teacher = new Person("Prof.", "Plum");
            teacher.PromoteToTeacher("CS"); // Делаем его преподавателем
            var course = new Course("Algorithms");

            _mockPersonRepo.Setup(repo => repo.GetById(teacher.Id)).Returns(teacher);
            _mockCourseRepo.Setup(repo => repo.GetById(course.Id)).Returns(course);

            // Act
            var exception = Record.Exception(() => _universityService.AssignTeacherToCourse(course.Id, teacher.Id));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void AssignTeacherToCourse_Should_ThrowKeyNotFoundException_WhenPersonIsNotATeacher()
        {
            // Arrange
            var notATeacher = new Person("Peter", "Pan"); 
            var course = new Course("Flying 101");

            _mockPersonRepo.Setup(repo => repo.GetById(notATeacher.Id)).Returns(notATeacher);
            _mockCourseRepo.Setup(repo => repo.GetById(course.Id)).Returns(course);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => _universityService.AssignTeacherToCourse(course.Id, notATeacher.Id));
        }

        [Fact]
        public void AssignStudentToCourse_Should_ThrowKeyNotFoundException_WhenPersonIsNotAStudent()
        {
            // Arrange
            var notAStudent = new Person("Captain", "Hook");
            var course = new Course("Swashbuckling");

            _mockPersonRepo.Setup(repo => repo.GetById(notAStudent.Id)).Returns(notAStudent);
            _mockCourseRepo.Setup(repo => repo.GetById(course.Id)).Returns(course);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => _universityService.AssignStudentToCourse(course.Id, notAStudent.Id));
        }
    }

    public class QueryMethods : UniversityServiceTests
    {
        [Fact]
        public void GetCourses_Should_ReturnMappedDtosFromRepository()
        {
            // Arrange
            var teacher = new Person("Prof.", "Xavier");
            var courses = new List<Course>
            {
                new("Telepathy 101"),
                new("Genetics 202")
            };
            courses[1].AssignResponsiblePerson(teacher);

            _mockCourseRepo.Setup(repo => repo.GetAll()).Returns(courses);

            // Act
            var resultDtos = _universityService.GetCourses().ToList();

            // Assert
            Assert.Equal(2, resultDtos.Count);
            Assert.Equal("Telepathy 101", resultDtos[0].Title);
            Assert.Null(resultDtos[0].ResponsiblePersonId); 
            
            Assert.Equal("Genetics 202", resultDtos[1].Title);
            Assert.Equal(teacher.Id, resultDtos[1].ResponsiblePersonId);
        }
    }
}