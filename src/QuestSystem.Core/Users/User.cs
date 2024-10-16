namespace QuestSystem.Core.Users;

public class User : BaseEntity<Guid>
{
    public string Name { get; private set; }
    public int Level { get; private set; }
    public List<UserQuest> UserQuests { get; private set; } = new();

#pragma warning disable CS8618
    protected User() { }
#pragma warning restore CS8618
    public User(string name, int level)
    {
        GuardException.ValidateStringValue(name, maxLength: 25);
        GuardException.ValidateUserLevel(level);

        Id = Guid.NewGuid();
        Name = name;
        Level = level;
    }

    public void SetName(string name)
    {
        GuardException.ValidateStringValue(name);
        Name = name;
    }

    public void SetLevel(int level)
    {
        GuardException.ValidateUserLevel(level);
        Level = level;
    }

    // TODO: НЕ ЗАБЫТЬ ПРИДУМАТЬ ЧЕ НИТЬ АТО СОСАЛ
    public void IncreaseLevel(int experiencePoints)
    {
        GuardException.ValidateIntValueOnNegative(experiencePoints);
        Level += experiencePoints;
    }

    public void AcceptQuest(Quest quest)
    {
        var canCreateQuestSpec = new CanCreateQuestToUserSpecification(quest);
        bool existedQuest = UserQuests.Any(x => x.Quest.Id == quest.Id);

        if (!canCreateQuestSpec.IsSatisfiedBy(this))
            throw new CannotAddQuestToUserException();

        if (existedQuest)
            throw new QuestAlreadyTakenToUserException();

        UserQuest userQuest = new(quest);
        UserQuests.Add(userQuest);
    }
}
