namespace Lab1.UI.IO;

public static class ConsoleInput
{
    public static bool TryReadWord(out string word)
    {
        string? input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input))
        {
            word = input;
            return true;
        }
        
        word = string.Empty;
        return false;
    }

    public static string ReadWord(string requestMessage)
    {
        string word;
        do
        {
            ConsoleOutput.DisplayRequestMessage(requestMessage);
        } 
        while (!TryReadWord(out word));

        return word;
    }
    
    public static bool TryReadId(string requestMessage, out Guid id)
    {
        id = Guid.Empty;
        return Guid.TryParse(ReadWord(requestMessage), out id);
    }
}