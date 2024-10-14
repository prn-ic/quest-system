using QuestSystem.Core.Common;
using QuestSystem.Core.Exceptions;

namespace QuestSystem.Core.Quests;

public class QuestReward : BaseEntity<int>
{
    public int Experience { get; private set; }
    public List<string> Items { get; private set; } = new();
    public int Currency { get; private set; }

    public QuestReward(int experience, int currency)
    {
        GuardException.ValidateQuestRewardExperience(experience);
        GuardException.ValidateQuestRewardCurrency(currency);
        Experience = experience;
        Currency = currency;
    }

    public void SetExperience(int experience)
    {
        GuardException.ValidateQuestRewardExperience(experience);
        Experience = experience;
    }

    public void SetCurrency(int currency)
    {
        GuardException.ValidateQuestRewardCurrency(currency);
        Currency = currency;
    }

    public void AddItems(List<string> items) => Items.AddRange(items);

    public void RemoveItem(string item)
    {
        var itemToRemove =
            Items.FirstOrDefault(x => x.Equals(item))
            ?? throw new NotFoundItemInRewardException(item);
        Items.Remove(itemToRemove);
    }
}
