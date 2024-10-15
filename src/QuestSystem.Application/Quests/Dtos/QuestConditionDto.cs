namespace QuestSystem.Application.Quests.Dtos;

public class QuestConditionDto
{
    public int Id { get; set; }
    public required string Type { get; set; }
    public required string Aim { get; set; }
    public int Amount { get; set; }
}