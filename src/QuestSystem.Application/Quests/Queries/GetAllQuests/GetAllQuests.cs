using QuestSystem.Application.Quests.Dtos;

namespace QuestSystem.Application.Quests.Queries.GetAllQuests;

public class GetAllQuestsQuery : IRequest<IEnumerable<QuestDto>>
{
    public Guid UserId { get; set; }
}

public class GetAllQuestsQueryHandler : IRequestHandler<GetAllQuestsQuery, IEnumerable<QuestDto>>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetAllQuestsQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<QuestDto>> Handle(
        GetAllQuestsQuery request,
        CancellationToken cancellationToken
    )
    {
        var user =
            await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId)
            ?? throw new InvalidDataException(
                "Не найден пользователь с идентификатором " + request.UserId
            );
        var quests = await _context
            .Quests.Include(x => x.Conditions)
            .Include(x => x.Reward)
            .Include(x => x.Requirement)
            .ToListAsync(cancellationToken);

        quests.Except(user.UserQuests.Select(x => x.Quest).ToList());

        return _mapper.Map<IEnumerable<QuestDto>>(quests);
    }
}
