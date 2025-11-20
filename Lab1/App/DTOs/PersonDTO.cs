namespace Lab1.App.DTOs;

public record PersonDto(Guid Id, string FirstName, string LastName, bool IsTeacher, bool IsStudent)
{
    public override string ToString()
    {
        List<string> statusList = [];
        if (IsStudent)
            statusList.Add("Студент");
        if (IsTeacher)
            statusList.Add("Преподаватель");
        
        string statuses = string.Join("|", statusList);
        return $"{Id} {statuses} {FirstName} {LastName}";
    } 
}