using Microsoft.AspNetCore.Mvc;

namespace QuestSystem.WebApi;

public static class WebConfiguration
{
    public static IMvcBuilder ConfigureControllers(this IMvcBuilder builder)
    {
        builder
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

        return builder;
    }
}
