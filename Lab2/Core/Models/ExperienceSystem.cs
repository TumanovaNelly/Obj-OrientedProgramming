using Lab2.Core.Interfaces;

namespace Lab2.Core.Models;

public class ExperienceSystem(IScale levelScale) : IExperienceSystem
{
    public int Level { get; private set; } = 1;
    public int CurrentExperience => levelScale.CurrentValue;
    public int ToNextLevelExperience => levelScale.MaxValue;

    public event Action? OnLevelUp;
    
    public void AddExperience(int amount)
    {
        var newExperience = levelScale.CurrentValue + amount;
        levelScale.Decrement(levelScale.CurrentValue);
        
        while (newExperience >= levelScale.MaxValue)
        {
            newExperience -= levelScale.MaxValue;
            levelScale.IncreaseScale(levelScale.MaxValue / 10);
            ++Level;
            OnLevelUp?.Invoke();
        }
        
        levelScale.Increment(newExperience);
    }
}