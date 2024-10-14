using QuestSystem.Core.Common;
using QuestSystem.Core.Exceptions;

namespace QuestSystem.Core.Quests;

public class QuestRequirement : BaseEntity<int>
{
    public int MinimumLevel { get; private set; }
    public List<Quest> PreviousQuests { get; private set; }

    public QuestRequirement(int minimumLevel, List<Quest> prevQuests)
    {
        GuardException.ValidateIntValueOnNegative(minimumLevel);
        MinimumLevel = minimumLevel;
        PreviousQuests = prevQuests;
    }

    public void UpdateQuests(List<Quest> prevQuests) => PreviousQuests = prevQuests;
}
