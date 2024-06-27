using FluentValidation;
using FluentValidation.AspNetCore;
using IceStormy.Template.Common.Constants;
using IceStormy.Template.Common.Extensions;
using IceStormy.Template.Core.Extensions;
using IceStormy.Template.Core.Profiles;
using IceStormy.Template.Data.Extensions;
using IceStormy.Template.Data.Helpers;
using IceStormy.Template.Extensions;
using IceStormy.Template.Validation;
using Microsoft.AspNetCore.Diagnostics;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddSerilog();
builder.Configuration.AddEnvironmentVariables();
builder.Services
    .ConfigureOptions(builder.Configuration)
    .AddDbContext(builder.Configuration, enableSensitiveData: builder.Environment.IsDevelopment())
    .AddServices()
    .AddRepositories()
    .AddRouting(c => c.LowercaseUrls = true)
    .AddAutoMapper(x => x.AddMaps(typeof(AbstractProfile).Assembly))
    .AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<TemplateValidator>()
    ;

builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.MaxDepth = 64;
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    });

builder.Services.AddSwagger()
    .AddCorsWithDefaultPolicy()
    ;

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c => { c.SerializeAsV2 = true; })
        .UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{CommonConstants.ApiName} API V1");
            c.RoutePrefix = string.Empty;
            c.DocumentTitle = $"{CommonConstants.ApiName} Documentation";
            c.DocExpansion(DocExpansion.None);
        });
}

app
    .UseStatusCodePages()
    .UseCors("AllowAllOrigins")
    .UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization()
    .UseExceptionHandler(applicationBuilder =>
    {
        applicationBuilder.Run(context => HandleError(
            context: context,
            logger: app.Services.GetRequiredService<ILogger<ExceptionHandlerMiddleware>>(),
            env: app.Environment));
    });

app.MapControllers();

MigrationTool.Execute(app.Services);

app.Run();

Task HandleError(HttpContext context, ILogger<ExceptionHandlerMiddleware> logger, IHostEnvironment env)
{
    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

    var exception = exceptionHandlerPathFeature?.Error;

    object logMessage = new
    {
        Message = AbstractConstants.InternalError,
        Path = context.Request.Path.ToString(),
        context.Request.Method
    };

    logger.LogError(exception, "{@Response}", logMessage);

    var response = exception.ToProblemDetails(env);
    context.Response.StatusCode = response.Status ?? 500;

    return context.Response.WriteAsJsonAsync(response);
}