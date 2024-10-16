using QuestSystem.Application.Quests.Dtos;

namespace QuestSystem.Application.Quests.Commands.UpdateQuestConditionProgress;

public class UpdateQuestConditionProgressCommand : IRequest<QuestConditionProgressDto>
{
    public Guid UserId { get; set; }
    public Guid QuestId { get; set; }
    public int ConditionId { get; set; }
    public int Progress { get; set; }
}

public class UpdateQuestConditionProgressCommandHandler
    : IRequestHandler<UpdateQuestConditionProgressCommand, QuestConditionProgressDto>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateQuestConditionProgressCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QuestConditionProgressDto> Handle(
        UpdateQuestConditionProgressCommand request,
        CancellationToken cancellationToken
    )
    {
        var userQuest =
            await _context
                .Users.Where(x => x.Id == request.UserId)
                .SelectMany(x => x.UserQuests)
                .Include(x => x.Status)
                .Include(x => x.ConditionProgresses)
                .ThenInclude(x => x.Condition)
                .Include(x => x.Quest)
                .ThenInclude(x => x.Conditions)
                .Include(x => x.Quest)
                .ThenInclude(x => x.Reward)
                .Include(x => x.Quest)
                .ThenInclude(x => x.Requirement)
                .Include(x => x.Status)
                .Where(x => x.Quest.Id == request.QuestId)
                .FirstOrDefaultAsync(cancellationToken)
            ?? throw new InvalidDataException(
                "Не найден квест пользователя с идентификатором пользователя" + request.UserId
            );

        userQuest.UpdateConditionProgress(request.ConditionId, request.Progress);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<QuestConditionProgressDto>(
            userQuest.ConditionProgresses.First(x => x.Id == request.ConditionId)
        );
    }
}
