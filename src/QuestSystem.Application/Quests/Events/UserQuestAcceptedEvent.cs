using QuestSystem.Core.Users;

namespace QuestSystem.Application.Quests.Events;

public class UserQuestAcceptedEvent : INotification
{
    public User User { get; set; }

    public UserQuestAcceptedEvent(User user)
    {
        User = user;
    }
}
