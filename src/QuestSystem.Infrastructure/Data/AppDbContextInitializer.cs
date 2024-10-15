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

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
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
                        new(),
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
                        new(),
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
                        new(),
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
        List<Quest> prevQuests,
        string conditionName,
        string conditionDescription,
        int conditionAmount
    )
    {
        QuestCondition condition = new(conditionName, conditionDescription, conditionAmount);
        QuestReward reward = new(exp, currency);
        QuestRequirement requirement = new(minimumLevel, prevQuests);
        Quest quest = new(questName, questDescription, new() { condition }, reward, requirement);

        return quest;
    }
}
