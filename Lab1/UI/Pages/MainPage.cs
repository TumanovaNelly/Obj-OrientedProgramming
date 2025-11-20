using Lab1.App.Services;
using Lab1.Core.Models;
using Lab1.UI.IO;

namespace Lab1.UI.Pages;

public class MainPage
{
    private readonly Dictionary<Command, Action> _commands = new();
    
    private readonly UniversityService _universityService = new(new CoursesRepository(), new PersonRepository());
    private bool _isRunning;

    public MainPage()
    {
        _commands[Command.AC] = HandleAddCourse;
        _commands[Command.DC] = HandleDeleteCourse;
        _commands[Command.AT] = HandleAssignTeacherToCourse;
        _commands[Command.CI] = HandleShowCourseInfo;
        _commands[Command.AP] = HandleAddPerson;
        _commands[Command.DP] = HandleDeletePerson;
        _commands[Command.AS] = HandleAssignStudentToCourse;
        _commands[Command.PI] = HandleShowPersonInfo;
        _commands[Command.EX] = HandleExit;
    }


    public void Run()
    {
        _isRunning = true;
        ConsoleOutput.ShowMenu("Меню админа", _commands.Keys.ToList());

        while (_isRunning)
            ProcessCommand(_commands);
    }
    
    private static void ProcessCommand(Dictionary<Command, Action> commands)
    {
        string input = ConsoleInput.ReadWord("Введите имя команды: ");
        
        if (Enum.TryParse<Command>(input, true, out var command) 
            && commands.TryGetValue(command, out var action))
            action.Invoke();
        else ConsoleOutput.DisplayError("Unknown command");
    }
    
    private void HandleAddPerson()
    {
        string name = ConsoleInput.ReadWord("Введите имя: ");
        string surname = ConsoleInput.ReadWord("Введите фамилию: ");
        
        var personId = _universityService.AddPerson(name, surname);
        
        ConsoleOutput.DisplayRequestMessage("Введите название группы, если это студент (иначе - Enter): ");
        if (ConsoleInput.TryReadWord(out var group))
            _universityService.PromotePersonToStudent(personId, group);
        
        ConsoleOutput.DisplayRequestMessage("Введите название отдела, если это преподаватель (иначе - Enter): ");
        if (ConsoleInput.TryReadWord(out var department))
            _universityService.PromotePersonToTeacher(personId, department);
        ShowPersonInfo(personId);
    }
    
    private void HandleDeletePerson()
    {
        ShowPersons();
        if (!ConsoleInput.TryReadId("Введите ID человека: ", out var personId))
        {
            ConsoleOutput.DisplayError("Неверный формат ID человека");
            return;
        }

        try
        {
            _universityService.DeletePerson(personId);
        }
        catch (KeyNotFoundException exception)
        {
            ConsoleOutput.DisplayError(exception.Message);
        }
        ShowPersons();
    }

    private void HandleAssignStudentToCourse()
    {
        ShowCourses();
        if (!ConsoleInput.TryReadId("Введите ID курса: ", out var courseId))
        {
            ConsoleOutput.DisplayError("Неверный формат ID курса");
            return;
        }
        
        ShowPersons();
        if (!ConsoleInput.TryReadId("Введите ID студента: ", out var personId))
        {
            ConsoleOutput.DisplayError("Неверный формат ID студента");
            return;
        }

        try
        {
            _universityService.AssignStudentToCourse(courseId, personId);
        }
        catch (KeyNotFoundException exception)
        {
            ConsoleOutput.DisplayError(exception.Message);
        }
        ShowPersonInfo(personId);
        ShowCourseInfo(courseId);
    }
    
    private void HandleShowPersonInfo()
    {
        ShowPersons();
        if (!ConsoleInput.TryReadId("Введите ID человека: ", out var personId))
        {
            ConsoleOutput.DisplayError("Неверный формат ID человека");
            return;
        }
        ShowPersonInfo(personId);
    }

    
    private void HandleAddCourse()
    {
        string title = ConsoleInput.ReadWord("Введите название курса: ");
        _universityService.AddCourse(title);
        ShowCourses();
    }
    
    private void HandleDeleteCourse()
    {
        ShowCourses();
        if (!ConsoleInput.TryReadId("Введите ID курса: ", out var courseId))
        {
            ConsoleOutput.DisplayError("Неверный формат ID курса");
            return;
        }

        try
        {
            _universityService.DeleteCourse(courseId);
        }
        catch (KeyNotFoundException exception)
        {
            ConsoleOutput.DisplayError(exception.Message);
        }
        ShowCourses();
    }
    
    private void HandleAssignTeacherToCourse()
    {
        ShowCourses();
        if (!ConsoleInput.TryReadId("Введите ID курса: ", out var courseId))
        {
            ConsoleOutput.DisplayError("Неверный формат ID курса");
            return;
        }
        
        ShowPersons();
        if (!ConsoleInput.TryReadId("Введите ID преподавателя: ", out var personId))
        {
            ConsoleOutput.DisplayError("Неверный формат ID преподавателя");
            return;
        }

        try
        {
            _universityService.AssignTeacherToCourse(courseId, personId);
        }
        catch (KeyNotFoundException exception)
        {
            ConsoleOutput.DisplayError(exception.Message);
        }
        ShowCourseInfo(courseId);
        ShowPersonInfo(personId);
    }

    private void HandleShowCourseInfo()
    {
        ShowCourses();
        if (!ConsoleInput.TryReadId("Введите ID курса: ", out var courseId))
        {
            ConsoleOutput.DisplayError("Неверный формат ID курса");
            return;
        }
        ShowCourseInfo(courseId);
    }
    
    private void HandleExit() => _isRunning = false;
    
    private void ShowPersons() => ConsoleOutput.DisplayInfo("Люди :)", _universityService.GetAllPersonsInfo());

    private void ShowCourses() => 
        ConsoleOutput.DisplayInfo("Список курсов", _universityService.GetAllCoursesInfo());

    private void ShowPersonInfo(Guid personId)
    {
        var info = _universityService.GetPersonDetailedInfo(personId);
        ConsoleOutput.DisplayInfo("Информация о человеке", info.GeneralInfo);
        if (info.EnrolledCourses is not null)
            ConsoleOutput.DisplayInfo("Записи на курсы", info.EnrolledCourses);
        if (info.TaughtCourses is not null)
            ConsoleOutput.DisplayInfo("Преподаваемые курсы", info.TaughtCourses);
    }
    
    private void ShowCourseInfo(Guid courseId)
    {
        var info = _universityService.GetCourseDetailedInfo(courseId);
        ConsoleOutput.DisplayInfo("Информация о курсе", info.GeneralInfo);
        ConsoleOutput.DisplayInfo("Расписание", info.CourseFormats);
        ConsoleOutput.DisplayInfo("Записанные студенты", info.EnrolledPersons);
    }
}