using Lab2.Core.Interfaces;
using Lab2.Core.Models;

namespace Lab2.Tests.Core.Models;

public class ScaleTest
{
    [Fact]
    public void Increment_WithinBounds_IncreasesValue()
    {
        // Arrange
        IScale scale = new Scale(maxValue: 100, currentValue: 50);

        // Act
        scale.Increment(10);

        // Assert
        Assert.Equal(60, scale.CurrentValue);
    }

    [Fact]
    public void Increment_ExceedsMax_ClampsToMax()
    {
        // Arrange
        IScale scale = new Scale(maxValue: 100, currentValue: 95);

        // Act
        scale.Increment(10); 

        // Assert
        Assert.Equal(100, scale.CurrentValue);
        Assert.True(scale.IsFull);
    }

    [Fact]
    public void Decrement_BelowZero_ClampsToZero()
    {
        // Arrange
        IScale scale = new Scale(maxValue: 100, currentValue: 5);

        // Act
        scale.Decrement(10); 

        // Assert
        Assert.Equal(0, scale.CurrentValue);
        Assert.True(scale.IsEmpty);
    }

    [Fact]
    public void IncreaseScale_UpdatesMaxValue()
    {
        // Arrange
        IScale scale = new Scale(maxValue: 10, currentValue: 10);

        // Act
        scale.IncreaseScale(5);

        // Assert
        Assert.Equal(15, scale.MaxValue);
        Assert.False(scale.IsFull);
    }
}