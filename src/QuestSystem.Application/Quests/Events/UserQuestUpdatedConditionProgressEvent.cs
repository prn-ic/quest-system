using QuestSystem.Core.Users;

namespace QuestSystem.Application.Quests.Events;

public class UserQuestUpdatedConditionProgress : INotification
{
    public UserQuest UserQuest { get; set; }

    public UserQuestUpdatedConditionProgress(UserQuest userQuest)
    {
        UserQuest = userQuest;
    }
}
