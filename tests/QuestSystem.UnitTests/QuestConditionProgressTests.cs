using QuestSystem.Core.Exceptions;
using QuestSystem.Core.Quests;

namespace QuestSystem.UnitTests;

public class QuestConditionProgressTests
{
    [Fact]
    public void IncreaseProgress_WithCorrectValues_ShouldBeOk()
    {
        // Arrange
        const int INCREASE_VALUE = 5;
        QuestCondition condition = new QuestCondition("Убить монстра", "Покакить в унитаз", 10);
        QuestConditionProgress progress = new QuestConditionProgress(condition, 0);

        // Act
        progress.IncreaseProgress(INCREASE_VALUE);

        // Assert
        Assert.Equal(INCREASE_VALUE, progress.Progress);
    }

    [Theory]
    [InlineData(15, 10)]
    [InlineData(-1, 10)]
    public void IncreaseProgress_WithInvalidValues_ThrowsCannotIncreaseQuestConditionProgressException(int increaseValue, int conditionValue)
    {
        // Arrange
        QuestCondition condition = new QuestCondition("Убить монстра", "Покакить в унитаз", conditionValue);
        QuestConditionProgress progress = new QuestConditionProgress(condition, 0);

        // Act
        var func = () => progress.IncreaseProgress(increaseValue);

        // Assert
        Assert.Throws<CannotIncreaseQuestConditionProgressException>(func);
    }
}
