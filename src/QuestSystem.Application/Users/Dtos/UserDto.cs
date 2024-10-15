namespace QuestSystem.Application.Users.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int Level { get; set; }
    public List<UserQuestDto> UserQuests { get; set; } = new();
}