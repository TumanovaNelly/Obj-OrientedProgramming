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
    
    public class AddMethods : UniversityServiceTests
    {
        [Fact]
        public void AddPerson_Should_AddPersonToRepository()
        {
            // Arrange
            const string firstName = "John";
            const string lastName = "Doe";

            // Act
            _universityService.AddPerson(firstName, lastName);

            // Assert
            _mockPersonRepo.Verify(repo => repo.Add(It.IsAny<Person>()), Times.Once);
        }
        
        [Fact]
        public void AddCourse_Should_AddCourseToRepositoryAndReturnCorrectDto()
        {
            // Arrange
            const string courseTitle = "Advanced TDD";
            
            // Act
            _universityService.AddCourse(courseTitle);
            
            // Assert
            _mockCourseRepo.Verify(repo => repo.Add(It.IsAny<Course>()), Times.Once);
        }
    }
    
    public class AssignmentMethods : UniversityServiceTests
    {
        [Fact]
        public void AssignTeacherToCourse_Should_Succeed_WhenPersonIsATeacher()
        {
            // Arrange
            var teacher = new Person("Prof.", "Plum");
            teacher.PromoteToTeacher("CS"); 
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
            var resultDto = _universityService.GetAllCoursesInfo().ToList();

            // Assert
            Assert.Equal(2, resultDto.Count);
            Assert.Equal("Telepathy 101", resultDto[0].Title);
            Assert.Null(resultDto[0].ResponsiblePerson); 
            
            Assert.Equal("Genetics 202", resultDto[1].Title);
            Assert.NotNull(resultDto[1].ResponsiblePerson);
            Assert.Equal(teacher.Id, resultDto[1].ResponsiblePerson!.Id);
        }
    }
}