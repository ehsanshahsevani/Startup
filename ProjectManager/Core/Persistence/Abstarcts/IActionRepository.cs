using FluentResults;
using PersistenceSeedworks;
using Action = Domain.Action;

namespace Persistence.Abstracts;

public interface IActionRepository : IRepository<Action>
{
	/// <summary>
	/// check action code in the start up other projects!
	/// </summary>
	/// <param name="actionCode">{ActionType}-{ControllerName}-{Template}</param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<bool> FindByActionCodeAsync(string actionCode, CancellationToken cancellationToken = default);

	/// <summary>
	/// check action code in the start up other projects!
	/// </summary>
	/// <param name="actionCodes">in per row data: {ActionType}-{ControllerName}-{Template}</param>
	/// <param name="cancellationToken"></param>
	/// <returns>List Action</returns>
	Task<List<Action>> FindByActionCodesAsync(List<string> actionCodes, CancellationToken cancellationToken = default);

	/// <summary>
	/// Clears the data associated with a specified server key and adds the provided list of actions.
	/// </summary>
	/// <param name="actions">A list of actions to be added after clearing existing data.</param>
	/// <param name="serverKey"></param>
	/// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	Task<Result> ClearAndCreateAsync(List<Action> actions, string serverKey, CancellationToken cancellationToken = default);
}