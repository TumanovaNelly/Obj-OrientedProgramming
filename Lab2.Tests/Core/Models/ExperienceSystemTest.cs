using Lab2.Core.Models;

namespace Lab2.Tests.Core.Models;

public class ExperienceSystemTest
{
    // Arrange
    private readonly ExperienceSystem _experienceSystem = new(new Scale(10, 0));

    [Fact]
    public void Initialization_DefaultState_IsCorrect()
    {
        // Assert
        Assert.Equal(1, _experienceSystem.Level);
        Assert.Equal(0, _experienceSystem.CurrentExperience);
        Assert.Equal(10, _experienceSystem.ToNextLevelExperience);
    }

    [Fact]
    public void AddExperience_BelowThreshold_DoesNotLevelUp()
    {
        // Arrange
        bool leveledUp = false;
        _experienceSystem.OnLevelUp += () => leveledUp = true;

        // Act
        _experienceSystem.AddExperience(5);

        // Assert
        Assert.Equal(1, _experienceSystem.Level);
        Assert.Equal(5, _experienceSystem.CurrentExperience);
        Assert.Equal(10, _experienceSystem.ToNextLevelExperience);
        Assert.False(leveledUp);
    }

    [Fact]
    public void AddExperience_ExactThreshold_LevelsUpAndResetsExp()
    {
        // Arrange
        int levelUpCount = 0;
        _experienceSystem.OnLevelUp += () => levelUpCount++;

        // Act
        _experienceSystem.AddExperience(10);

        // Assert
        Assert.Equal(2, _experienceSystem.Level);
        Assert.Equal(0, _experienceSystem.CurrentExperience); 
        Assert.Equal(11, _experienceSystem.ToNextLevelExperience); 
        Assert.Equal(1, levelUpCount);
    }

    [Fact]
    public void AddExperience_Overflow_LevelsUpAndCarriesOverExp()
    {
        // Arrange
        int levelUpCount = 0;
        _experienceSystem.OnLevelUp += () => levelUpCount++;
        
        // Act
        _experienceSystem.AddExperience(12);

        // Assert
        Assert.Equal(2, _experienceSystem.Level);
        Assert.Equal(2, _experienceSystem.CurrentExperience);
        Assert.Equal(11, _experienceSystem.ToNextLevelExperience); 
        Assert.Equal(1, levelUpCount);
    }

    [Fact]
    public void AddExperience_MultipleLevelUps_CalculatesCorrectly()
    {
        // Arrange
        int levelUps = 0;
        _experienceSystem.OnLevelUp += () => levelUps++;
        
        // Act
        _experienceSystem.AddExperience(22);

        // Assert
        Assert.Equal(3, _experienceSystem.Level);
        Assert.Equal(1, _experienceSystem.CurrentExperience);
        Assert.Equal(12, _experienceSystem.ToNextLevelExperience); 
        Assert.Equal(2, levelUps); 

    }

    [Fact]
    public void AddExperience_Cumulative_WorksCorrectly()
    {
        // Act
        _experienceSystem.AddExperience(9); // Стало 90/100
        _experienceSystem.AddExperience(2); // Стало 110 total -> -100 -> 10 остаток

        // Assert
        Assert.Equal(2, _experienceSystem.Level);
        Assert.Equal(1, _experienceSystem.CurrentExperience);
    }
}