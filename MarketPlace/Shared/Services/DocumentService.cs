using AutoMapper;
using Persistence;
using Microsoft.AspNetCore.Http;
using PersistenceSeedworks.LogManager;
using Microsoft.Extensions.Configuration;

namespace Services;

public class DocumentService : Base.BaseMarketplaceService
{
	public DocumentService(IMapper mapper, HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, ILogDetailManager logDetailManager, ILogServerManager logServerManager) : base(mapper, httpClient, configuration, httpContextAccessor, unitOfWork, logDetailManager, logServerManager)
	{
	}
}