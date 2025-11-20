using System.ComponentModel;

namespace Lab1.UI.IO;

public enum Command
{
    [Description("Добавить курс")]
    AC,  // add course
    [Description("Удалить курс")]
    DC,  // delete course
    [Description("Подробнее о курсе")]
    CI,  // course information
    [Description("Добавить человека")]
    AP,  // add person
    [Description("Удалить человека")]
    DP,  // delete person
    [Description("Назначить преподавателя на курс")]
    AT,  // assign teacher to course
    [Description("Посмотреть информацию о пользователе")]
    PI,  // person information
    [Description("Записать студента на курс")]
    AS,  // assign student to course
    [Description("Выход")]
    EX   // exit
}