using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestSystem.Core.Quests;

namespace QuestSystem.Infrastructure.Configurations;

public class QuestConditionConfiguration : IEntityTypeConfiguration<QuestCondition>
{
    public void Configure(EntityTypeBuilder<QuestCondition> builder)
    {
        builder.Property(x => x.Type).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Aim).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Amount).IsRequired();
    }
}