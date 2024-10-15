using Newtonsoft.Json;
using QuestSystem.Application.Common.Exceptions;
using QuestSystem.Core.Common;

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
        catch (DomainException ex)
        {
            ErrorModel model = new() { Message = ex.Message };
            await HandleAsync(context, model);
        }
        catch (ValidationException ex)
        {
            ErrorModel model = new() { Message = ex.Message, Errors = ex.Errors };
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
