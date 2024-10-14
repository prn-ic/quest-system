using QuestSystem.Core.Common;
using QuestSystem.Core.Exceptions;

namespace QuestSystem.Core.Quests;

public class QuestConditionProgress : BaseEntity<int>
{
    public QuestCondition Condition { get; private set; }
    public int Progress { get; private set; }

    public QuestConditionProgress(QuestCondition condition, int progress)
    {
        ArgumentNullException.ThrowIfNull(condition);
        GuardException.ValidateIntValueOnNegative(progress);

        Condition = condition;
        Progress = progress;
    }

    public void IncreaseProgress(int progress)
    {
        if (progress < 0 || progress > Condition.Amount)
            throw new CannotIncreaseQuestConditionProgressException();

        Progress = progress;
    }
}
