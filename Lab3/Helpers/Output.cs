namespace Lab3.Helpers;

public static class Output
{
    public static string MiddleFill(string leftWord, string rightWord, char fillingChar = '.', int width = 50) 
        => $"{leftWord}{rightWord.PadLeft(width - leftWord.Length, fillingChar)}";

    public static string WordInMiddle(string word, char fillingChar = '=', int width = 50)
        => string.Join(string.Empty,
            new string(fillingChar, (width - word.Length) / 2 + (width - word.Length) % 2),
            word, new string(fillingChar, (width - word.Length) / 2));
    public static string Fill(char fillingChar = '~', int width = 50) => new(fillingChar, width);
}