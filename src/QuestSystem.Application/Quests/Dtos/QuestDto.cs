namespace QuestSystem.Application.Quests.Dtos;

public class QuestDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public List<QuestConditionDto> Conditions { get; set; } = new();
    public required QuestRewardDto Reward { get; set; }
    public required QuestRequirementDto Requirements { get; set; }
}
