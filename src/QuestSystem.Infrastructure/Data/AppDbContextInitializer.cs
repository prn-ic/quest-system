using Microsoft.EntityFrameworkCore;
using QuestSystem.Core.Quests;

namespace QuestSystem.Infrastructure.Data;

public class AppDbContextInitializer
{
    private readonly AppDbContext _context;

    public AppDbContextInitializer(AppDbContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (!await _context.Quests.AnyAsync())
        {
            await _context.Quests.AddRangeAsync(
                [
                    GenerateQuest(
                        "Повышение статуса жизни",
                        "повышаем",
                        10,
                        10,
                        0,
                        "Убить монстра",
                        "Покакить в унитаз",
                        10
                    ),
                    GenerateQuest(
                        "Повышение статуса жизни2",
                        "повышаем",
                        20,
                        20,
                        1,
                        "Убить монстра",
                        "Пописить в унитаз",
                        20
                    ),
                    GenerateQuest(
                        "Повышение статуса жизни3",
                        "повышаем",
                        30,
                        30,
                        2,
                        "Убить монстра",
                        "Пописить и покакить в унитаз",
                        10
                    ),
                ]
            );

            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    private Quest GenerateQuest(
        string questName,
        string questDescription,
        int exp,
        int currency,
        int minimumLevel,
        string conditionName,
        string conditionDescription,
        int conditionAmount
    )
    {
        QuestCondition condition = new(conditionName, conditionDescription, conditionAmount);
        QuestReward reward = new(exp, currency);
        QuestRequirement requirement = new(minimumLevel);
        Quest quest = new(questName, questDescription, new() { condition }, reward, requirement);

        return quest;
    }
}
