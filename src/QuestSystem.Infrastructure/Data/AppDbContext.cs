using System.Reflection;
using Microsoft.EntityFrameworkCore;
using QuestSystem.Application.Common.Contracts;
using QuestSystem.Core.Quests;
using QuestSystem.Core.Users;

namespace QuestSystem.Infrastructure.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public virtual DbSet<Quest> Quests => Set<Quest>();

    public virtual DbSet<QuestCondition> QuestConditions => Set<QuestCondition>();

    public virtual DbSet<QuestConditionProgress> QuestConditionProgresses =>
        Set<QuestConditionProgress>();

    public virtual DbSet<QuestRequirement> QuestRequirements => Set<QuestRequirement>();

    public virtual DbSet<QuestReward> QuestRewards => Set<QuestReward>();

    public virtual DbSet<User> Users => Set<User>();

    public virtual DbSet<UserQuest> UserQuests => Set<UserQuest>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<User>().HasData([new("Oleg", 0)]);
        builder.Entity<Quest>().HasData([
            GenerateQuest("Повышение статуса жизни", "повышаем", 10, 10, 0, new(), "Убить монстра", "Покакить в унитаз", 10),
            GenerateQuest("Повышение статуса жизни2", "повышаем", 20, 20, 1, new(), "Убить монстра", "Пописить в унитаз", 20),
            GenerateQuest("Повышение статуса жизни3", "повышаем", 30, 30, 2, new(), "Убить монстра", "Пописить и покакить в унитаз", 10),
        ]);
    }

    private Quest GenerateQuest(
        string questName,
        string questDescription,
        int exp,
        int currency,
        int minimumLevel,
        List<Quest> prevQuests,
        string conditionName,
        string conditionDescription,
        int conditionAmount
    )
    {
        QuestCondition condition = new(conditionName, conditionDescription, conditionAmount);
        QuestReward reward = new(exp, currency);
        QuestRequirement requirement = new(minimumLevel, prevQuests);
        return new(questName, questDescription, new() { condition }, reward, requirement);
    }
}
