namespace QuestSystem.Core.Quests;

public class QuestRequirement : BaseEntity<int>
{
    public int MinimumLevel { get; private set; }

#pragma warning disable CS8618
    protected QuestRequirement() { }
#pragma warning restore CS8618
    public QuestRequirement(int minimumLevel)
    {
        GuardException.ValidateIntValueOnNegative(minimumLevel);
        MinimumLevel = minimumLevel;
    }
}
