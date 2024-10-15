using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestSystem.Infrastructure.Data;
using QuestSystem.WebApi.Middlewares;

public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder
            .Services.AddControllers()
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
                    .Json
                    .ReferenceLoopHandling
                    .Ignore
            )
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context
                        .ModelState.Values.SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage);

                    return new BadRequestObjectResult(
                        new { Message = "Ошибки валидации", Errors = errors }
                    );
                };
            });

        builder.Services.AddApplicationLayer();
        builder.Services.AddInfrastructureLayer(builder.Configuration);

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
            var initialiser = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();

            await initialiser.InitializeAsync();
        }

        app.UseHttpsRedirection();
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.MapControllers();

        app.Run();
    }
}
