namespace QuestSystem.Application.Quests.Dtos;

public class QuestConditionProgressDto
{
    public int Id { get; set; }
    public required QuestConditionDto Condition { get; set; }
    public int Progress { get; set; }
}