using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestSystem.Core.Quests;

namespace QuestSystem.Infrastructure.Configurations;

public class QuestConditionProgressConfiguration : IEntityTypeConfiguration<QuestConditionProgress>
{
    public void Configure(EntityTypeBuilder<QuestConditionProgress> builder)
    {
        builder.Property(x => x.Condition).IsRequired();
        builder.Property(x => x.Progress).IsRequired();
    }
}