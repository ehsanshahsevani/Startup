using FluentResults;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;
using Action = Domain.Action;

namespace Persistence.Repositories;

public class ActionRepository : Repository<Action>, IActionRepository
{
	internal ActionRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}

	/// <summary>
	/// check action code in the start up other projects!
	/// </summary>
	/// <param name="actionCode">{ActionType}-{ControllerName}-{Template}</param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public async Task<bool> FindByActionCodeAsync(string actionCode, CancellationToken cancellationToken = default)
	{
		var result =
			await DbSet
				.Where(current => current.IsDeleted == false)
				.Where(current => current.IsActive == true)
				.AnyAsync(current => current.ActionCode == actionCode, cancellationToken);

		return result;
	}

	/// <summary>
	/// check action code in the start up other projects!
	/// </summary>
	/// <param name="actionCodes">in per row data: {ActionType}-{ControllerName}-{Template}-{ServerId}</param>
	/// <param name="cancellationToken"></param>
	/// <returns>Task<List<Action>></returns>
	public async Task<List<Action>> FindByActionCodesAsync(List<string> actionCodes,
		CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			.Where(current => current.IsDeleted == false) // Same as checking IsDeleted == false
			.Where(current => current.IsActive == true)
			.Where(current =>
				actionCodes.Contains(
					current.ActionType + "-" +
					current.ControllerName + "-" +
					current.Template + "-" +
					current.ServerId))
			.ToListAsync(cancellationToken);

		return result;
	}

	/// <summary>
	/// Clears the data associated with a specified server key and adds the provided list of actions.
	/// </summary>
	/// <param name="actions">A list of actions to be added after clearing existing data.</param>
	/// <param name="serverKey"></param>
	/// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task<Result> ClearAndCreateAsync(List<Action> actions, string serverKey,
		CancellationToken cancellationToken = default)
	{
		var result = new FluentResults.Result();

		if (string.IsNullOrEmpty(serverKey) == false)
		{
			await ClearAsync(serverKey, cancellationToken);

			var resultCheckSubSystem =
				await FindSubSystemAndSetInActions(actions, serverKey, cancellationToken);

			result.WithErrors(resultCheckSubSystem.Errors);

			await AddRangeAsync(actions, cancellationToken);
		}
		
		return result;
	}

	private async Task ClearAsync(string serverKey, CancellationToken cancellationToken = default)
	{
		// This will delete all data where the ServerId matches the provided serverKey.
		await DbSet
			.Where(current => current.ServerId == serverKey)
			.ExecuteDeleteAsync(cancellationToken);
	}

	private async Task<Result> FindSubSystemAndSetInActions(
		List<Action> actions, string serverKey, CancellationToken cancellationToken = default)
	{
		var result = new FluentResults.Result();

		ISubSystemRepository subSystemRepository =
			new SubSystemRepository(DatabaseContext);

		foreach (var action in actions)
		{
			action.ServerId = serverKey;
			
			var subSystem =
				await subSystemRepository.FindSubSystemByNameAndServerIdAsync(
					action.ControllerName, serverKey, cancellationToken);

			if (subSystem is not null)
			{
				action.SubSystemId = subSystem.Id;
			}
			else
			{
				action.SubSystemId = null;
			}
		}

		return result;
	}
}