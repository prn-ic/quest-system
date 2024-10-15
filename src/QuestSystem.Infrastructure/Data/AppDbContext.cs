using System.Reflection;
using Microsoft.EntityFrameworkCore;
using QuestSystem.Application.Common.Contracts;
using QuestSystem.Core.Quests;
using QuestSystem.Core.Users;

namespace QuestSystem.Infrastructure.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public virtual DbSet<Quest> Quests => Set<Quest>();

    public virtual DbSet<User> Users => Set<User>();

    public virtual DbSet<UserQuest> UserQuests => Set<UserQuest>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder
            .Entity<User>()
            .HasData([new("Oleg", 0) { Id = Guid.Parse("a8587ff3-432c-4d91-920e-d1d50c07558e") }]);
    }
}
