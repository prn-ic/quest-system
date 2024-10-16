using QuestSystem.Core.Exceptions;
using QuestSystem.Core.Quests;
using QuestSystem.Core.Users;

namespace QuestSystem.UnitTests;

public class UserTests
{
    [Fact]
    public void AcceptQuest_WithValidEnv_ShouldBeOk()
    {
        // Arrange
        User user = new("Oleg", 0);
        QuestCondition condition = new("Убить монстра", "Покакить в унитаз", 10);
        QuestReward reward = new(10, 10);
        QuestRequirement requirement = new(0);
        Quest quest =
            new("Повышение статуса жизни", "повышаем", new() { condition }, reward, requirement);

        // Act
        user.AcceptQuest(quest);

        // Assert
        Assert.Single(user.UserQuests);
    }

    [Fact]
    public void AcceptQuest_WithInvalidLevel_ThrowsCannotAddQuestToUserException()
    {
        // Arrange
        User user = new("Oleg", 0);
        QuestCondition condition = new("Убить монстра", "Покакить в унитаз", 10);
        QuestReward reward = new(10, 10);
        QuestRequirement requirement = new(1);
        Quest quest =
            new("Повышение статуса жизни", "повышаем", new() { condition }, reward, requirement);

        // Act
        var func = () => user.AcceptQuest(quest);

        // Assert
        Assert.Throws<CannotAddQuestToUserException>(func);
    }

    [Fact]
    public void AcceptQuest_QuestAlreadyFinished_ThrowsQuestAlreadyTakenToUserException()
    {
        // Arrange
        User user = new("Oleg", 0);
        QuestCondition condition = new("Убить монстра", "Покакить в унитаз", 10);
        QuestReward reward = new(10, 10);
        QuestRequirement requirement = new(0);
        Quest quest =
            new("Повышение статуса жизни", "повышаем", new() { condition }, reward, requirement);
        user.AcceptQuest(quest);

        // Act
        var func = () => user.AcceptQuest(quest);

        // Assert
        Assert.Throws<QuestAlreadyTakenToUserException>(func);
    }

    [Fact]
    public void IncreaseLevel_WithValidParameters_ShouldBeOk()
    {
        // Arrange
        const int LEVEL_INCREASE_VALUE = 10;
        User user = new("Oleg", 0);

        // Act
        user.IncreaseLevel(LEVEL_INCREASE_VALUE);

        // Assert
        Assert.Equal(LEVEL_INCREASE_VALUE, user.Level);
    }

    [Theory]
    [InlineData(-1)]
    public void IncreaseLevel_WithInvalidParameters_ThrowsArgumentOutOfRangeException(
        int levelIncreaseValue
    )
    {
        // Arrange
        User user = new("Oleg", 0);

        // Act
        var func = () => user.IncreaseLevel(levelIncreaseValue);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(func);
    }
}
