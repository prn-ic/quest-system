using QuestSystem.WebApi;
using QuestSystem.WebApi.Middlewares;

public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddEnvironmentVariables();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers().ConfigureControllers();

        builder.Services.AddApplicationLayer();
        builder.Services.AddInfrastructureLayer(builder.Configuration);

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();
        await app.Services.InitializeDatabaseAsync();

        app.UseHttpsRedirection();
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.MapControllers();

        app.Run();
    }
}
