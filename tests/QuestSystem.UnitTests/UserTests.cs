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
    public void AcceptQuest_QuestAlreadyFinished_ThrowsCannotAddQuestToUserException()
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
        Assert.Throws<CannotAddQuestToUserException>(func);
    }
}
