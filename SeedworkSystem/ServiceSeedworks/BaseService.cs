using System.Net.Http;
using AutoMapper;
using PersistenceSeedworks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PersistenceSeedworks.LogManager;

namespace ServiceSeedworks;

public abstract class BaseService : object
{
    public IMapper Mapper { get; }
    public HttpClient HttpClient { get; }
    public IConfiguration Configuration { get; }
    public IHttpContextAccessor HttpContextAccessor { get; }
    protected IUnitOfWork UnitOfWork { get; }
    protected ILogDetailManager LogDetailManager { get; }
    public ILogServerManager LogServerManager { get; }

    public BaseService(
        IMapper mapper,
        HttpClient httpClient,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor,
        IUnitOfWork unitOfWork,
        ILogDetailManager logDetailManager, ILogServerManager logServerManager) : base()
    {
        Mapper = mapper;
        HttpClient = httpClient;
        Configuration = configuration;
        HttpContextAccessor = httpContextAccessor;
        UnitOfWork = unitOfWork;
        LogDetailManager = logDetailManager;
        LogServerManager = logServerManager;
    }
}