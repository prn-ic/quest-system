using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestSystem.Core.Quests;

namespace QuestSystem.Infrastructure.Configurations;

public class QuestRequirementConfiguration : IEntityTypeConfiguration<QuestRequirement>
{
    public void Configure(EntityTypeBuilder<QuestRequirement> builder)
    {
        builder.Property(x => x.MinimumLevel).IsRequired();
    }
}