using QuestSystem.Core.Quests;
using QuestSystem.Core.Users;

namespace QuestSystem.Application.Common.Contracts;

public interface IAppDbContext
{
    DbSet<Quest> Quests { get; }
    DbSet<User> Users { get; }
    DbSet<UserQuest> UserQuests { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
