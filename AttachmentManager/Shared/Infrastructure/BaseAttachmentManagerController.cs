using AutoMapper;
using ControllerSeedworks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Persistence;
using PersistenceSeedworks.LogManager;

namespace RestFullApi.Infrastructure;

public class BaseAttachmentManagerController : BaseApiController
{
	public BaseAttachmentManagerController(
		IMapper mapper, HttpClient httpClient, IConfiguration configuration,
		IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork,
		ILogDetailManager logDetailManager, ILogServerManager logServerManager)
		: base(mapper, httpClient, configuration, httpContextAccessor, unitOfWork, logDetailManager, logServerManager)
	{
	}
}