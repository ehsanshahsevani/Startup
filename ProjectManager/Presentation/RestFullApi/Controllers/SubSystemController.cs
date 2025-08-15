using Domain;
using AutoMapper;
using Enums.SharedService;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using RestFullApi.Infrastructure;
using Microsoft.AspNetCore.Identity;
using PersistenceSeedworks.LogManager;
using ViewModels.ProjectManager;
using IUnitOfWork = Persistence.IUnitOfWork;

namespace RestFullApi.Controllers;

/// <summary>
/// مدیریت زیر سیستم ها
/// </summary>
public class SubSystemController : BaseControllerIdentity
{
	#region DI Settings & Constructor

	public SubSystemController(IMapper mapper,
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
	
	#region GET : check-server
	/// <summary>
	/// check server id and domain name from other servers
	/// </summary>
	/// <param name="serverId">server id (server key)</param>
	/// <param name="domainName">example: (nameof(Category), nameof(Product))</param>
	/// <param name="cancellationToken"></param>
	/// <returns>result -> boolean</returns>
	[HttpGet("check-server")]
	public async Task<IActionResult> CheckServerAsync(
		[FromQuery] string serverId, [FromQuery] string domainName,
		CancellationToken cancellationToken = default)
	{
		var result = new Result();

		var checkServer = await UnitOfWork
			.SubSystemRepository.CheckSubSystemAndServerIdAsync(domainName, serverId, cancellationToken);

		if (checkServer == false)
		{
			var errorMessage = string.Format(
				Resources.Messages.NotFoundError, Resources.DataDictionary.ServerOrSubSystem);
			
			result.WithError(errorMessage);
		}
		
		return FluentResult(result);
	}

	#endregion

	#region POST : /add-range/{serverId]
	/// <summary>
	/// save all sub systems and register server.
	/// </summary>
	/// <param name="subSystems">domain names</param>
	/// <param name="projectType">enum project type</param>
	/// <param name="serverId">server key in base domain self project</param>
	/// <returns>result</returns>
	[HttpPost(template: "add-range/{serverId}")]
	public async Task<IActionResult> AddAsync
		([FromBody] List<SubSystemResponseViewModel> subSystems, [FromQuery] ProjectType projectType, string serverId)
	{
		var result = new Result();

		var serverEntity =
			await UnitOfWork.ServerRepository.GetByServerIdAndProjectTypeAsync(serverId, projectType);

		if (serverEntity is not null)
		{
			if (serverId == serverEntity.ServerKey)
			{
				// this server is Exist
			}
			else
			{
				result.WithError(Resources.Messages.ServerIsErlyExistButThisServerKeyIsChanged);
			}
		}
		else
		{
			var entity = new Server(projectType, serverId);

			await UnitOfWork.ServerRepository.AddAsync(entity);

			await UnitOfWork.SaveAsync();

			var successMessage =
				string.Format(Resources.Messages.CreateSuccessMessage, Resources.DataDictionary.Server);

			result.WithSuccess(successMessage);
		}

		if (result.IsSuccess == true)
		{
			var entities = Mapper.Map<List<SubSystem>>(subSystems);

			foreach (SubSystem entity in entities)
			{
				var viewModel =
					subSystems.First(x => x.NameEN.Equals(entity.NameEN));

				entity.ServerId = serverId;

				entity.SetId(viewModel.Id);
			}

			var addResult =
				await UnitOfWork.SubSystemRepository.AddRangeAsync(entities, serverId);

			if (addResult.IsSuccess == true)
			{
				await UnitOfWork.SaveAsync();

				var successMessage =
					string.Format(Resources.Messages.ServerIsCheck, serverId);

				result.WithSuccess(successMessage);
			}
			else
			{
				result.WithErrors(addResult.Errors);
			}
		}

		return FluentResult(result);
	}

	#endregion
}