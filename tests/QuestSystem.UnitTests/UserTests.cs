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
        QuestRequirement requirement = new(0, new());
        Quest quest = new("Повышение статуса жизни", "повышаем", new(){condition}, reward, requirement);

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
        QuestRequirement requirement = new(1, new());
        Quest quest = new("Повышение статуса жизни", "повышаем", new(){condition}, reward, requirement);

        // Act
        var func = () => user.AcceptQuest(quest);

        // Assert
        Assert.Throws<CannotAddQuestToUserException>(func);
    }
    [Fact]
    public void AcceptQuest_WithNotEnoughQuest_ThrowsCannotAddQuestToUserException()
    {
        // Arrange
        User user = new("Oleg", 0);
        QuestCondition condition = new("Убить монстра", "Покакить в унитаз", 10);
        QuestReward reward = new(10, 10);
        QuestRequirement requirement = new(1, new());
        Quest quest = new("Повышение статуса жизни", "повышаем", new(){condition}, reward, requirement);
        requirement.PreviousQuests.Add(quest);
        Quest quest2 = new("Повышение статуса жизни", "повышаем", new(){condition}, reward, requirement);

        // Act
        var func = () => user.AcceptQuest(quest2);

        // Assert
        Assert.Throws<CannotAddQuestToUserException>(func);
    }
}
