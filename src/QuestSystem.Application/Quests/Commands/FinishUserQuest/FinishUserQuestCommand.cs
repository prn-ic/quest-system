using QuestSystem.Application.Users.Dtos;

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
                .Users.Include(x => x.UserQuests)
                .ThenInclude(x => x.Status)
                .Include(x => x.UserQuests)
                .ThenInclude(x => x.ConditionProgresses)
                .ThenInclude(x => x.Condition)
                .Include(x => x.UserQuests)
                .ThenInclude(x => x.Quest)
                .ThenInclude(x => x.Conditions)
                .Include(x => x.UserQuests)
                .ThenInclude(x => x.Quest)
                .ThenInclude(x => x.Reward)
                .Include(x => x.UserQuests)
                .ThenInclude(x => x.Quest)
                .ThenInclude(x => x.Requirement)
                .Include(x => x.UserQuests)
                .ThenInclude(x => x.Status)
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
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserQuestDto>(userQuest);
    }
}
