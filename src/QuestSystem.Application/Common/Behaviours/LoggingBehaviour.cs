using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace QuestSystem.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<IRequest> _logger;

    public LoggingBehaviour(ILogger<IRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogInformation(
                "[UTC: {date}] Request: Handle request {Name} {@Request}",
                DateTime.UtcNow,
                requestName,
                request
            );
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(
                ex,
                $"[UTC: {DateTime.UtcNow}] Request: Exception for Request {requestName} {request}. ExceptionName: {ex.Message}"
            );
            throw;
        }
    }
}
