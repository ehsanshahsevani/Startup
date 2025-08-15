using AutoMapper;
using Utilities;
using SampleResult;
using DomainSeedworks.Log;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PersistenceSeedworks;
using PersistenceSeedworks.LogManager;

using IConfiguration =
    Microsoft.Extensions.Configuration.IConfiguration;

namespace ControllerSeedworks;

[ApiController]
[Route(template: "[controller]")]
public abstract class BaseApiIdentityController<TUser, TRole> : ControllerBase
    where TUser : IdentityUser<string> where TRole : IdentityRole<string>
{
    public IMapper Mapper { get; }
    private IUnitOfWork UnitOfWork { get; }
    public HttpClient HttpClient { get; }
    public IConfiguration Configuration { get; }
    public IHttpContextAccessor HttpContextAccessor { get; }
    public UserManager<TUser> UserManager { get; }
    public RoleManager<TRole> RoleManager { get; }
    public SignInManager<TUser> SignInManager { get; }
    public ILogDetailManager LogDetailManager { get; }
    public ILogServerManager LogServerManager { get; }

    public BaseApiIdentityController(
        IMapper mapper,
        
        IUnitOfWork unitOfWork,
        
        HttpClient httpClient,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor,
        
        UserManager<TUser> userManager,
        RoleManager<TRole> roleManager,
        SignInManager<TUser> signInManager,
        ILogDetailManager logDetailManager,
        ILogServerManager logServerManager)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;
        HttpClient = httpClient;
        Configuration = configuration;
        HttpContextAccessor = httpContextAccessor;
        UserManager = userManager;
        RoleManager = roleManager;
        SignInManager = signInManager;
        LogDetailManager = logDetailManager;
        LogServerManager = logServerManager;
    }
    
    // overload
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

    // **************************************************
    // protected TResult GetTokenDetail<TResult>()
    // {
    //     string key = Configuration
    //         .GetSection("JwtSettings").GetValue<string>("SecretKey");
    //
    //     var tokenDetails =
    //         JwtUtility.GetTokenDetail(HttpContext, key);
    //
    //     return tokenDetails;
    // }
    // **************************************************
}