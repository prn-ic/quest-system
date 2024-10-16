using QuestSystem.Core.Users;

namespace QuestSystem.Application.Quests.Events;

public class UserQuestFinishedEvent : INotification
{
    public UserQuest UserQuest { get; set; }

    public UserQuestFinishedEvent(UserQuest userQuest)
    {
        UserQuest = userQuest;
    }
}
