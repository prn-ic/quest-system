namespace QuestSystem.Core.Quests;

public class Quest : BaseEntity<Guid>
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public List<QuestCondition> Conditions { get; private set; } = new();
    public QuestReward Reward { get; private set; }
    public QuestRequirement Requirement { get; private set; }
#pragma warning disable CS8618 
    protected Quest() { }
#pragma warning restore CS8618 
    public Quest(
        string title,
        string description,
        List<QuestCondition> conditions,
        QuestReward reward,
        QuestRequirement requirement
    )
    {
        GuardException.ValidateStringValue(input: title, maxLength: 50);
        GuardException.ValidateStringValue(input: description, maxLength: 250);
        ArgumentNullException.ThrowIfNull(reward);
        ArgumentNullException.ThrowIfNull(requirement);

        Title = title;
        Description = description;
        Conditions = conditions;
        Reward = reward;
        Requirement = requirement;
    }

    public void SetTitle(string title)
    {
        GuardException.ValidateStringValue(input: title, maxLength: 50);
        Title = title;
    }

    public void SetDescription(string description)
    {
        GuardException.ValidateStringValue(input: description, maxLength: 250);
        Description = description;
    }

    public void AddCondition(QuestCondition condition) => Conditions.Add(condition);

    public void RemoveConditionById(int conditionId)
    {
        var condition =
            Conditions.FirstOrDefault(x => x.Id == conditionId)
            ?? throw new CannotRemoveConditionByIdException(conditionId);

        Conditions.Remove(condition);
    }

    public void UpdateReward(QuestReward reward)
    {
        ArgumentNullException.ThrowIfNull(reward);
        Reward = reward;
    }

    public void UpdateRequirement(QuestRequirement requirement)
    {
        ArgumentNullException.ThrowIfNull(requirement);
        Requirement = requirement;
    }
}
