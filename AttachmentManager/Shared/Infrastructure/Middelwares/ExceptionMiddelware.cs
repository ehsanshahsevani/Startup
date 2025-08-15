using System.Net;
using System.Text.Json;
using DomainSeedworks.Log;
// using Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using PersistenceSeedworks.LogManager;
using SampleResult;

namespace Infrastructure.Middelwares;

public static class ExceptionMiddelware
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var result = new FluentResults.Result<string>();

                using var scope = app.Services.CreateScope();

                var serverLogManager =
                    scope.ServiceProvider
                        .GetService(typeof(ILogServerManager)) as ILogServerManager;

                context.Response.StatusCode =
                    (int)HttpStatusCode.InternalServerError;

                context.Response.ContentType = "application/json";

                IExceptionHandlerFeature? contextFeature =
                    context.Features.Get<IExceptionHandlerFeature>();

                string? path = context.Request.Path.Value;
                string methode = context.Request.Method;

                if (contextFeature != null)
                {
                    System.Text.StringBuilder stringBuilder = new();

                    stringBuilder.Append($"{nameof(path)}: {path}");
                    stringBuilder.Append($" - ");
                    stringBuilder.Append($"{nameof(methode)}: {methode}");
                    stringBuilder.Append($" - ");
                    stringBuilder.Append
                        ($"{nameof(contextFeature.Error)}: {contextFeature.Error}");

                    var message = stringBuilder.ToString();

                    dynamic features = context.Features;
                    var featuresVar = context.Features;

                    var serverLog = new LogServer()
                    {
                        IsDeleted = false,
                        Exceptions = contextFeature.Error.ToString(),
                        Message = message,
                        RequestPath = path,
                        Description =
                            $"controller: {contextFeature.RouteValues?["controller"]} - action: {contextFeature.RouteValues?["action"]}",
                        MethodName = contextFeature.RouteValues?["action"]?.ToString(),
                        ClassName = contextFeature.RouteValues?["controller"]?.ToString(),
                        Namespace = contextFeature.Endpoint?.DisplayName,
                        RemoteIP = context.Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                        PortIP = context.Request.HttpContext.Connection.RemotePort.ToString(),
                        HttpReferrer = context.Request.Headers["Referer"].ToString(),
                    };

                    var logId = await serverLogManager!.CreateAsync(serverLog);

                    if (message.Contains("IDX10223: Lifetime validation failed.") == true)
                    {
                        result.WithError(Resources.Messages.TokenLifeTimeError);

                        var sampleResult = result.ConvertToSampleResult();

                        sampleResult.Value = logId;

                        var json = JsonSerializer.Serialize(sampleResult);

                        await context.Response.WriteAsync(json);
                    }
                    else
                    {
                        result.WithError(Resources.Messages.SystemError);

                        var sampleResult = result.ConvertToSampleResult();

                        sampleResult.Value = logId;

                        var json = JsonSerializer.Serialize(sampleResult);

                        await context.Response.WriteAsync(json);
                    }
                }
            });
        });
    }
}