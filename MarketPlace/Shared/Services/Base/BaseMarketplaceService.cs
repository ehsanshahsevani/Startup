using AutoMapper;
using Persistence;
using ServiceSeedworks;
using Microsoft.AspNetCore.Http;
using PersistenceSeedworks.LogManager;
using Microsoft.Extensions.Configuration;

namespace Services.Base;

public class BaseMarketplaceService : BaseService
{
	protected BaseMarketplaceService(IMapper mapper, HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, ILogDetailManager logDetailManager, ILogServerManager logServerManager) : base(mapper, httpClient, configuration, httpContextAccessor, unitOfWork, logDetailManager, logServerManager)
	{
		UnitOfWork = unitOfWork;
	}

	protected new IUnitOfWork UnitOfWork { get; set; }
}