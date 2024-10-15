namespace QuestSystem.Core.Quests;

public class QuestStatus : ValueObject
{
    private string _status;
    public string Status => _status;
    public static QuestStatus Accepted => new("Accepted");
    public static QuestStatus InProgress => new("In Progress...");
    public static QuestStatus Completed => new("Completed");
    public static QuestStatus Finished => new("Finished");

    public QuestStatus(string status)
    {
        _status = status;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Status;
    }
}
