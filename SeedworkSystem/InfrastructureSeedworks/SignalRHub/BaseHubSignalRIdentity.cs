using PersistenceSeedworks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using PersistenceSeedworks.LogManager;

using IConfiguration =
    Microsoft.Extensions.Configuration.IConfiguration;

namespace InfrastructureSeedworks.SignalRHub;

public abstract class BaseHubSignalRIdentity<TUser, TRole> : Hub
    where TUser : IdentityUser where TRole : IdentityRole
{
    public IUnitOfWork UnitOfWork { get; }
    public HttpClient HttpClient { get; }
    public IConfiguration Configuration { get; }
    public IHttpContextAccessor HttpContextAccessor { get; }
    public UserManager<TUser> UserManager { get; }
    public RoleManager<TRole> RoleManager { get; }
    public SignInManager<TUser> SignInManager { get; }
    public ILogDetailManager LogDetailManager { get; }
    public ILogServerManager LogServerManager { get; }

    public BaseHubSignalRIdentity(
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
}