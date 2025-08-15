using Domain;
using AutoMapper;
using Persistence;
using ViewModels.Shared;
using Enums.SharedService;
using Action = Domain.Action;
using Microsoft.AspNetCore.Mvc;
using RestFullApi.Infrastructure;
using Microsoft.AspNetCore.Identity;
using PersistenceSeedworks.LogManager;

namespace RestFullApi.Controllers;

public class ActionController : BaseControllerIdentity
{
	public ActionController(IMapper mapper, IUnitOfWork unitOfWork, HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, ILogDetailManager logDetailManager, ILogServerManager logServerManager) : base(mapper, unitOfWork, httpClient, configuration, httpContextAccessor, userManager, roleManager, signInManager, logDetailManager, logServerManager)
	{
	}
	
	#region POST : /add-range/{serverId}
	[HttpPost(template: "add-range/{serverId}")]
	public async Task<IActionResult> AddRangeAsync
		([FromBody] List<ActionViewModel> actions,
			[FromQuery] ProjectType projectType, [FromRoute] string serverId)
	{
		var result = new FluentResults.Result();

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
				return FluentResult(result);
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

		var entities = Mapper.Map<List<Action>>(actions);
			
		var resultClearAndCreate = await UnitOfWork.ActionRepository
			.ClearAndCreateAsync(entities, serverKey: serverId);

		result.WithErrors(resultClearAndCreate.Errors);
		
		if (result.IsSuccess == true)
		{
			await UnitOfWork.SaveAsync();
			
			await UnitOfWork.SubSystemRoleAccessRepository.CheckAllPermissionsAsync();
			
			var successMessage =
				string.Format(Resources.Messages.ServerIsCheck, serverId);
			
			result.WithSuccess(successMessage);
		}

		return FluentResult(result);
	}

	#endregion
}