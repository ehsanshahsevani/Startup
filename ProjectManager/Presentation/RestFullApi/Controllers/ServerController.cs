using Domain;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using RestFullApi.Infrastructure;
using Microsoft.AspNetCore.Identity;
using PersistenceSeedworks.LogManager;
using IUnitOfWork = Persistence.IUnitOfWork;

namespace RestFullApi.Controllers;

public class ServerController : BaseControllerIdentity
{
	#region DI Settings & Constructor

	public ServerController(IMapper mapper,
		IUnitOfWork unitOfWork, HttpClient httpClient,
		IConfiguration configuration, IHttpContextAccessor httpContextAccessor,
		UserManager<User> userManager, RoleManager<Role> roleManager,
		SignInManager<User> signInManager, ILogDetailManager logDetailManager,
		ILogServerManager logServerManager)
		: base(mapper, unitOfWork, httpClient, configuration, httpContextAccessor,
			userManager, roleManager, signInManager, logDetailManager, logServerManager)
	{
	}

	#endregion

	#region GET : Index

	[HttpGet(template: "check-server")]
	public async Task<IActionResult> CheckServerAsync([FromQuery] string serverKey)
	{
		var result = new Result();

		var server =
			await UnitOfWork.ServerRepository.FindAsync(serverKey);

		if (server is null)
		{
			var errorMessage = string.Format
				(Resources.Messages.NotFoundError, Resources.DataDictionary.Server);
			
			result.WithError(errorMessage);
		}
		
		return FluentResult(result);
	}

	#endregion
}