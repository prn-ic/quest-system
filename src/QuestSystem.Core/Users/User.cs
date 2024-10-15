namespace QuestSystem.Core.Users;

public class User : BaseEntity<Guid>
{
    public string Name { get; private set; }
    public int Level { get; private set; }
    public List<UserQuest> UserQuests { get; private set; } = new();

    public User(string name, int level)
    {
        GuardException.ValidateStringValue(name);
        GuardException.ValidateUserLevel(level);

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

        if (!canCreateQuestSpec.IsSatisfiedBy(this) || existedQuest)
            throw new CannotAddQuestToUserException();

        UserQuest userQuest = new(quest);
        UserQuests.Add(userQuest);
    }
}
