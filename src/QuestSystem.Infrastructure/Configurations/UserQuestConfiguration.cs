using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestSystem.Core.Users;

namespace QuestSystem.Infrastructure.Configurations;

public class UserQuestConfiguration : IEntityTypeConfiguration<UserQuest>
{
    public void Configure(EntityTypeBuilder<UserQuest> builder)
    {
        builder.OwnsOne(x => x.Status).Property(x => x.Status).HasColumnName("status");
    }
}
