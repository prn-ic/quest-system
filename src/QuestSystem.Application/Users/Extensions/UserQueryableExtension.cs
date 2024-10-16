using QuestSystem.Core.Users;

namespace QuestSystem.Application.Users.Extensions;

public static class UserQueryableExtension
{
    public static IQueryable<User> IncludeAll(this IQueryable<User> users)
    {
        return users
            .Include(x => x.UserQuests)
            .ThenInclude(x => x.Status)
            .Include(x => x.UserQuests)
            .ThenInclude(x => x.ConditionProgresses)
            .ThenInclude(x => x.Condition)
            .Include(x => x.UserQuests)
            .ThenInclude(x => x.Quest)
            .ThenInclude(x => x.Conditions)
            .Include(x => x.UserQuests)
            .ThenInclude(x => x.Quest)
            .ThenInclude(x => x.Reward)
            .Include(x => x.UserQuests)
            .ThenInclude(x => x.Quest)
            .ThenInclude(x => x.Requirement)
            .Include(x => x.UserQuests)
            .ThenInclude(x => x.Status);
    }
}
