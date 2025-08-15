using PersistenceSeedworks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using PersistenceSeedworks.LogManager;
using Microsoft.Extensions.Configuration;

namespace InfrastructureSeedworks.SignalRHub;

public abstract class BaseHubSignalR : Hub
{
    public HttpClient HttpClient { get; }
    public IConfiguration Configuration { get; }
    public IHttpContextAccessor HttpContextAccessor { get; }
    public IUnitOfWork UnitOfWork { get; }
    public ILogDetailManager LogDetailManager { get; }
    public ILogServerManager LogServerManager { get; }

    public BaseHubSignalR(
        HttpClient httpClient,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor,
        IUnitOfWork unitOfWork,
        ILogDetailManager logDetailManager, ILogServerManager logServerManager)
    {
        HttpClient = httpClient;
        Configuration = configuration;
        HttpContextAccessor = httpContextAccessor;
        UnitOfWork = unitOfWork;
        LogDetailManager = logDetailManager;
        LogServerManager = logServerManager;
    }
}