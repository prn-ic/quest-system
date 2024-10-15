using QuestSystem.Application.Quests.Dtos;
using QuestSystem.Application.Users.Dtos;
using QuestSystem.Core.Quests;
using QuestSystem.Core.Users;

namespace QuestSystem.Application.Common.Mappings;

public class AppProfileMapping : Profile
{
    public AppProfileMapping()
    {
        CreateMap<Quest, QuestDto>().ReverseMap();
        CreateMap<QuestCondition, QuestConditionDto>().ReverseMap();
        CreateMap<QuestConditionProgress, QuestConditionProgressDto>().ReverseMap();
        CreateMap<QuestRequirement, QuestRequirementDto>().ReverseMap();
        CreateMap<QuestReward, QuestRewardDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<UserQuest, UserQuestDto>().ReverseMap();
    }
}
