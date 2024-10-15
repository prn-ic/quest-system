using QuestSystem.Application.Quests.Dtos;
using QuestSystem.Core.Quests;

namespace QuestSystem.Application.Common.Mappings;

public class AppProfileMapping : Profile
{
    public AppProfileMapping()
    {
        CreateMap<Quest, QuestDto>().ReverseMap();
        CreateMap<QuestCondition, QuestConditionDto>().ReverseMap();
        CreateMap<QuestConditionProgress, QuestConditionProgressDto>().ReverseMap();
        CreateMap<QuestRequirement, QuestRequirementDto>().ReverseMap();
        CreateMap<QuestReward, QuestRewardDto>();
    }
}
