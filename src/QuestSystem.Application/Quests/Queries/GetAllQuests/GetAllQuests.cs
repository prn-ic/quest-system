using QuestSystem.Application.Quests.Dtos;

namespace QuestSystem.Application.Quests.Queries.GetAllQuests;

public class GetAllQuestsQuery : IRequest<IEnumerable<QuestDto>> { }

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
        var quests = await _context.Quests.ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<QuestDto>>(quests);
    }
}
