using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuestSystem.Application.Quests.Commands.AcceptQuest;
using QuestSystem.Application.Quests.Commands.FinishUserQuest;
using QuestSystem.Application.Quests.Commands.UpdateQuestConditionProgress;
using QuestSystem.Application.Quests.Queries.GetAllQuests;
using QuestSystem.WebApi.Models;

namespace QuestSystem.WebApi.Controllers;

[ApiController]
[Route("api/quests")]
public class QuestController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllQuestsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPost("accept-quest")]
    public async Task<IActionResult> AcceptQuest(
        AcceptQuestCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("finish")]
    public async Task<IActionResult> FinishQuest(
        FinishUserQuestCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{userQuestId}")]
    public async Task<IActionResult> UpdateUserQuest(
        Guid userQuestId,
        UpdateQuestRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(
            new UpdateQuestConditionProgressCommand()
            {
                UserQuestId = userQuestId,
                ConditionId = request.ConditionId,
                Progress = request.Progress,
            },
            cancellationToken
        );

        return Ok(result);
    }
}
