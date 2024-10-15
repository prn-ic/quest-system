namespace QuestSystem.Core.Quests;

public class QuestCondition : BaseEntity<int>
{
    public string Type { get; private set; }
    public string Aim { get; private set; }
    public int Amount { get; private set; }

#pragma warning disable CS8618
    protected QuestCondition() { }
#pragma warning restore CS8618
    public QuestCondition(string type, string aim, int amount)
    {
        GuardException.ValidateStringValue(type, maxLength: 50);
        GuardException.ValidateStringValue(aim, maxLength: 50);
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
