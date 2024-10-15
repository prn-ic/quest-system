using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestSystem.Core.Quests;

namespace QuestSystem.Infrastructure.Configurations;

public class QuestRewardConfiguration : IEntityTypeConfiguration<QuestReward>
{
    public void Configure(EntityTypeBuilder<QuestReward> builder)
    {
        builder.Property(x => x.Experience).IsRequired();
        builder.Property(x => x.Currency).IsRequired();
    }
}