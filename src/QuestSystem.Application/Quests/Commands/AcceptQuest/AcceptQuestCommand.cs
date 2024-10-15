using QuestSystem.Application.Users.Dtos;

namespace QuestSystem.Application.Quests.Commands.AcceptQuest;

public class AcceptQuestCommand : IRequest<UserQuestDto>
{
    public Guid UserId { get; set; }
    public Guid QuestId { get; set; }
}

public class AcceptQuestCommandHandler : IRequestHandler<AcceptQuestCommand, UserQuestDto>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public AcceptQuestCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserQuestDto> Handle(
        AcceptQuestCommand request,
        CancellationToken cancellationToken
    )
    {
        var quest =
            await _context.Quests.FirstOrDefaultAsync(
                x => x.Id == request.QuestId,
                cancellationToken
            )
            ?? throw new InvalidDataException(
                "Не найден квест с идентификатором " + request.QuestId
            );

        var user =
            await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken)
            ?? throw new InvalidDataException(
                "Не найден пользователь с идентификатором " + request.UserId
            );

        user.AcceptQuest(quest);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserQuestDto>(user.UserQuests.Last());
    }
}
