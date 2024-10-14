using QuestSystem.Core.Common;
using QuestSystem.Core.Exceptions;

namespace QuestSystem.Core.Quests;

public class QuestCondition : BaseEntity<int>
{
    public string Type { get; private set; }
    public string Aim { get; private set; }
    public int Amount { get; private set; }

    public QuestCondition(string type, string aim, int amount)
    {
        GuardException.ValidateStringValue(type);
        GuardException.ValidateStringValue(aim);
        GuardException.ValidateIntValueOnNegative(amount);

        Type = type;
        Aim = aim;
        Amount = amount;
    }

    public void SetType(string type)
    {
        GuardException.ValidateStringValue(type);
        Type = type;
    }

    public void SetAim(string aim)
    {
        GuardException.ValidateStringValue(aim);
        Aim = aim;
    }

    public void SetAmount(int amount)
    {
        GuardException.ValidateIntValueOnNegative(amount);
        Amount = amount;
    }
}
