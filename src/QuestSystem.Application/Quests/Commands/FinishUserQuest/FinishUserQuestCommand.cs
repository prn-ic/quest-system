using QuestSystem.Application.Quests.Events;
using QuestSystem.Application.Users.Dtos;
using QuestSystem.Application.Users.Extensions;

namespace QuestSystem.Application.Quests.Commands.FinishUserQuest;

public class FinishUserQuestCommand : IRequest<UserQuestDto>
{
    public Guid QuestId { get; set; }
    public Guid UserId { get; set; }
}

public class FinishUserQuestCommandHandler : IRequestHandler<FinishUserQuestCommand, UserQuestDto>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public FinishUserQuestCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserQuestDto> Handle(
        FinishUserQuestCommand request,
        CancellationToken cancellationToken
    )
    {
        var user =
            await _context
                .Users.IncludeAll()
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken)
            ?? throw new InvalidDataException(
                "Не найден пользователь с идентификатором " + request.UserId
            );

        var userQuest =
            user.UserQuests.FirstOrDefault(x => x.Quest.Id == request.QuestId)
            ?? throw new InvalidDataException(
                "Не найден квест пользователя с идентификатором " + request.QuestId
            );

        userQuest.Finish(user);
        userQuest.AddEvent(new UserQuestFinishedEvent(userQuest));
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserQuestDto>(userQuest);
    }
}
