namespace Lab2.Core.Models;

public class ExperienceSystem
{
    public int Level { get; private set; } = 1;
    public int CurrentXp { get; private set; }
    public int XpToNextLevel => Level * 100;

    public event Action<int>? OnLevelUp;

    public void AddXp(int amount)
    {
        CurrentXp += amount;
        
        while (CurrentXp >= XpToNextLevel)
        {
            CurrentXp -= XpToNextLevel;
            OnLevelUp?.Invoke(++Level);
        }
    }
}