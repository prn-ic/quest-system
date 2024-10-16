using QuestSystem.Core.Users;

namespace QuestSystem.Application.Users.Extensions;

public static class UserQuestExtension
{
    public static IQueryable<UserQuest> IncludeAll(this IQueryable<UserQuest> userQuests)
    {
        return userQuests
            .Include(x => x.Status)
            .Include(x => x.ConditionProgresses)
            .ThenInclude(x => x.Condition)
            .Include(x => x.Quest)
            .ThenInclude(x => x.Conditions)
            .Include(x => x.Quest)
            .ThenInclude(x => x.Reward)
            .Include(x => x.Quest)
            .ThenInclude(x => x.Requirement)
            .Include(x => x.Status);
    }
}
