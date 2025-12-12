namespace Lab2.Core.Interfaces;

public interface IExperienceSystem
{
    public int Level { get; }
    public int CurrentExperience { get; }
    public int ToNextLevelExperience { get; }
    
    public event Action? OnLevelUp;
    public void AddExperience(int amount);
}