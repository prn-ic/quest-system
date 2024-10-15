using QuestSystem.Core.Exceptions;
using QuestSystem.Core.Quests;
using QuestSystem.Core.Users;

namespace QuestSystem.UnitTests;

public class UserQuestTests
{
    [Fact]
    public void UpdateConditionProgress_WithCorrectUse_ShouldBeOk()
    {
        // Arrange
        User user = new("Oleg", 0);
        QuestCondition condition = new("Убить монстра", "Покакить в унитаз", 10);
        QuestReward reward = new(10, 10);
        QuestRequirement requirement = new(0);
        Quest quest =
            new("Повышение статуса жизни", "повышаем", new() { condition }, reward, requirement);
        user.AcceptQuest(quest);
        UserQuest userQuest = user.UserQuests.First();

        // Act
        userQuest.UpdateConditionProgress(condition.Id, 1);

        // Assert
        Assert.True(true);
    }

    [Fact]
    public void UpdateConditionProgress_WithIncorrectConditionId_ThrowsNotFoundConditionInUserQuestException()
    {
        // Arrange
        User user = new("Oleg", 0);
        QuestCondition condition = new("Убить монстра", "Покакить в унитаз", 10);
        QuestReward reward = new(10, 10);
        QuestRequirement requirement = new(0);
        Quest quest =
            new("Повышение статуса жизни", "повышаем", new() { condition }, reward, requirement);
        user.AcceptQuest(quest);
        UserQuest userQuest = user.UserQuests.First();

        // Act
        var func = () => userQuest.UpdateConditionProgress(int.MaxValue, 1);

        // Assert
        Assert.Throws<NotFoundConditionInUserQuestException>(func);
    }

    [Fact]
    public void Finish_WithCorrectParameters_ShouldBeOk()
    {
        // Arrange
        User user = new("Oleg", 0);
        QuestCondition condition = new("Убить монстра", "Покакить в унитаз", 10);
        QuestReward reward = new(10, 10);
        QuestRequirement requirement = new(0);
        Quest quest =
            new("Повышение статуса жизни", "повышаем", new() { condition }, reward, requirement);
        user.AcceptQuest(quest);
        UserQuest userQuest = user.UserQuests.First();
        userQuest.UpdateConditionProgress(condition.Id, 10);

        // Act
        var func = () => userQuest.Finish(user);

        // Assert
        func();
        Assert.True(true);
    }

    [Fact]
    public void Finish_WithProgressLessThanNeed_ThrowsCannotUpdateConditionProgressException()
    {
        // Arrange
        User user = new("Oleg", 0);
        QuestCondition condition = new("Убить монстра", "Покакить в унитаз", 10);
        QuestReward reward = new(10, 10);
        QuestRequirement requirement = new(0);
        Quest quest =
            new("Повышение статуса жизни", "повышаем", new() { condition }, reward, requirement);
        user.AcceptQuest(quest);
        UserQuest userQuest = user.UserQuests.First();

        // Act
        var func = () => userQuest.Finish(user);

        // Assert
        Assert.Throws<CannotUpdateConditionProgressException>(func);
    }
}
