using QuestSystem.Core.Common;
using QuestSystem.Core.Exceptions;
using QuestSystem.Core.Quests;

namespace QuestSystem.Core.Users;

public class UserQuest : BaseEntity<Guid>
{
    public Quest Quest { get; private set; }
    public QuestStatus Status { get; private set; } = QuestStatus.Accepted;
    public List<QuestConditionProgress> ConditionProgresses { get; private set; } = new();
    public bool GotReward { get; private set; } = false;

    public UserQuest(Quest quest)
    {
        ArgumentNullException.ThrowIfNull(quest);

        Quest = quest;
        InitializeConditionProgresses(quest);
    }

    public void UpdateConditionProgress(int conditionId, int progress)
    {
        var condition =
            ConditionProgresses.FirstOrDefault(x => x.Id == conditionId)
            ?? throw new NotFoundConditionInUserQuestException(conditionId);

        condition.IncreaseProgress(progress);

        if (Status.Equals(QuestStatus.Accepted))
            Status = QuestStatus.InProgress;

        if (CheckIfAllConditionsAreDone())
            Status = QuestStatus.Completed;
    }

    // Конечно, можно искать по другому, но мне впадлу xD
    public bool CheckIfAllConditionsAreDone()
    {
        foreach (var condition in ConditionProgresses)
        {
            if (condition.Condition.Amount != condition.Progress)
                return false;
        }

        return true;
    }

    public void Finish(User user)
    {
        if (Status.Equals(QuestStatus.Finished))
            return;

        if (!CheckIfAllConditionsAreDone())
        {
            var undonedConditions = ConditionProgresses.Where(x =>
                x.Progress != x.Condition.Amount
            );
            throw new CannotUpdateConditionProgressException(undonedConditions.ToList());
        }

        if (!GotReward)
        {
            user.IncreaseLevel(Quest.Reward.Experience);
            // тут может быть логика добавления предметов и всего такого, но в тз ничего не сказано, не буду делать
            GotReward = true;
        }
    }

    private void InitializeConditionProgresses(Quest quest)
    {
        foreach (var condition in quest.Conditions)
        {
            ConditionProgresses.Add(new(condition, 0));
        }
    }
}
