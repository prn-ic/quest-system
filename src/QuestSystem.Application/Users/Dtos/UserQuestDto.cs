using QuestSystem.Application.Quests.Dtos;
using QuestSystem.Core.Quests;

namespace QuestSystem.Application.Users.Dtos;

public class UserQuestDto
{
    public required QuestDto Quest { get; set; }
    public QuestStatus Status { get; set; } = QuestStatus.Accepted;
    public List<QuestConditionProgressDto> ConditionProgresses { get; set; } = new();    
}