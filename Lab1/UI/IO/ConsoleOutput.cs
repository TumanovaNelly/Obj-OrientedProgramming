using Lab1.Helpers;

namespace Lab1.UI.IO;

public static class ConsoleOutput
{
    public static void ShowMenu(string titleInfo, IEnumerable<Command> commands)
    {
        string title = $"~~~~~ {titleInfo} ~~~~~";
        DisplayMessage(title, ConsoleColor.Cyan);

        foreach (var command in commands)
        {
            DisplayMessage($"[{command}] ", ConsoleColor.Cyan, false);
            Console.WriteLine(command.GetDescription());
        }
        
        DisplayMessage(new string('~', title.Length), ConsoleColor.Cyan);
        Console.WriteLine();
    }

    public static void DisplayInfo(string titleInfo, IEnumerable<object> info)
    {
        string title = $"~~~~~ {titleInfo} ~~~~~";
        DisplayMessage(title, ConsoleColor.Cyan);
        
        foreach (var line in info) 
            Console.WriteLine(line);
        
        DisplayMessage(new string('~', title.Length), ConsoleColor.Cyan);
        Console.WriteLine();
    }
    
    public static void DisplayInfo(string titleInfo, object info)
    {
        string title = $"~~~~~ {titleInfo} ~~~~~";
        DisplayMessage(title, ConsoleColor.Cyan);
        
        Console.WriteLine(info);
    }

    public static void DisplayError(string message) => DisplayMessage(message, ConsoleColor.Magenta);

    public static void DisplayRequestMessage(string message) => DisplayMessage(message, ConsoleColor.DarkBlue, false);
    
    private static void DisplayMessage(string message, ConsoleColor color, bool toNewLine = true)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        if (toNewLine) 
            Console.WriteLine();
        Console.ResetColor();
    }
}