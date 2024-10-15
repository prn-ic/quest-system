using System.Linq.Expressions;

namespace QuestSystem.Core.Users;

public class CanCreateQuestToUserSpecification : Specification<User>
{
    private Quest _quest;

    public CanCreateQuestToUserSpecification(Quest quest)
    {
        _quest = quest;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return x =>
            x.Level >= _quest.Requirement.MinimumLevel
            && x.UserQuests.Where(x => x.Status.Equals(QuestStatus.Accepted)).Count() < 10;
    }
}
