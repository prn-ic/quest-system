namespace QuestSystem.Application.Quests.Dtos;

public class QuestRewardDto
{
    public int Id { get; set; }
    public int Experience { get; set; }
    public List<string> Items { get; set; } = new();
    public int Currency { get; set; }
}