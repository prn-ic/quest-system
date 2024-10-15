using Newtonsoft.Json;
using QuestSystem.Application.Common.Exceptions;
using QuestSystem.Core.Common;
using QuestSystem.Core.Exceptions;
using QuestSystem.Core.Quests;

namespace QuestSystem.WebApi.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (CannotUpdateConditionProgressException ex)
        {
            var errors = new Dictionary<string, QuestConditionProgress[]>
            {
                { "Uncompleted Conditions", ex.Progresses.ToArray() },
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;

            var result = JsonConvert.SerializeObject(new { Message = ex.Message, Errors = errors });

            await context.Response.WriteAsync(result);
        }
        catch (ValidationException ex)
        {
            ErrorModel model = new() { Message = ex.Message, Errors = ex.Errors };
            await HandleAsync(context, model);
        }
        catch (DomainException ex)
        {
            ErrorModel model = new() { Message = ex.Message };
            await HandleAsync(context, model);
        }
        catch (InvalidDataException ex)
        {
            ErrorModel model = new() { Message = ex.Message };
            await HandleAsync(context, model);
        }
        catch (Exception)
        {
            ErrorModel model = new() { Message = "Internal server Error" };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var result = JsonConvert.SerializeObject(model);

            await context.Response.WriteAsync(result);
        }
    }

    private async Task HandleAsync(HttpContext context, ErrorModel model)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 400;

        var result = JsonConvert.SerializeObject(model);

        await context.Response.WriteAsync(result);
    }

    private class ErrorModel
    {
        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("Errors")]
        public Dictionary<string, string[]> Errors { get; set; } = new();
    }
}
