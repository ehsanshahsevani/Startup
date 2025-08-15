using System.Reflection;
using AutoMapper;
using DomainSeedworks.Log;
using Microsoft.AspNetCore.Http;
using SampleResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using PersistenceSeedworks;
using PersistenceSeedworks.LogManager;
using Utilities;
using ViewModels.Shared;

namespace ControllerSeedworks;

[ApiController]
[Route(template: "[controller]")]
public abstract class BaseApiController : ControllerBase
{
    public IMapper Mapper { get; }
    public HttpClient HttpClient { get; }
    public IConfiguration Configuration { get; }
    public IHttpContextAccessor HttpContextAccessor { get; }
    public  IUnitOfWork UnitOfWork { get; }
    public ILogDetailManager LogDetailManager { get; }
    public ILogServerManager LogServerManager { get; }

    public BaseApiController(
        IMapper mapper,
        HttpClient httpClient,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor,
        IUnitOfWork unitOfWork,
        ILogDetailManager logDetailManager, ILogServerManager logServerManager)
    {
        Mapper = mapper;
        HttpClient = httpClient;
        Configuration = configuration;
        HttpContextAccessor = httpContextAccessor;
        UnitOfWork = unitOfWork;
        LogDetailManager = logDetailManager;
        LogServerManager = logServerManager;
    }
    
    // Overload
    // **************************************************
    [NonAction]
    protected IActionResult FluentResult<TResult>(FluentResults.Result<TResult> result)
    {
        var res =
            result.ConvertToSampleResult();
        
        if (res.IsSuccess)
        {
            return Ok(res);
        }
        else
        {
            return BadRequest(res);
        }
    }
    // **************************************************

    // **************************************************
    [NonAction]
    protected IActionResult FluentResult(FluentResults.Result result)
    {
        var res =
            result.ConvertToSampleResult();

        if (res.IsSuccess)
        {
            return Ok(res);
        }
        else
        {
            return BadRequest(res);
        }
    }
    // **************************************************
    
    // **************************************************
    [NonAction]
    public async Task SaveAsync()
    {
        // var token = GetTokenDetail();

        var date = DateTime.Now;

        var detailsLog = new LogDetail()
        {
            // UserId = token.UserId,
            // Token_JWT = token.Token,

            RemoteIP = HttpContext.Connection.RemoteIpAddress?.ToString(),

            PortIP = HttpContext.Connection.RemotePort.ToString(),
			
            HttpReferrer = HttpContext.Request.Headers["Referer"].ToString(),

            IsDeleted = false,

            RequestPath = HttpContext.Request.Path,

            Description = $"{date.ToShamsi(1)} - {date.ToString("HH:mm:ss")} - {date.ToShamsi()}",
        };
        
        await LogDetailManager.CreateAsync(detailsLog);
        
        await UnitOfWork.SaveAsync();
    }
    // **************************************************
    
    public static class ActionScanner
    {
        public static List<ActionViewModel> ScanActions(Assembly assembly, string serverKey)
        {
            var actions = new List<ActionViewModel>();

            var controllerTypes = assembly.GetTypes()
                .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var controller in controllerTypes)
            {
                var controllerName = controller.Name.Replace("Controller", "");

                var methods = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

                foreach (var method in methods)
                {
                    var httpVerbAttr = method.GetCustomAttributes()
                        .FirstOrDefault(a => a is HttpGetAttribute || a is HttpPostAttribute || a is HttpPutAttribute || a is HttpDeleteAttribute);

                    var verb = httpVerbAttr?.GetType().Name.Replace("Attribute", "") ?? "Unknown";
                    string? routeTemplate = (httpVerbAttr as HttpMethodAttribute)?.Template;

                    actions.Add(new ActionViewModel
                    {
                        ActionType = verb,
                        Name = method.Name,
                        
                        ServerKey = serverKey,
                        
                        SubSystemName = controllerName,
                        ControllerName = controllerName,
                        
                        Template = $"/{routeTemplate}" ?? "/"
                    });
                }
            }

            return actions;
        }
    }
}