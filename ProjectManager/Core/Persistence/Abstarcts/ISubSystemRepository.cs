using Domain;
using FluentResults;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface ISubSystemRepository:IRepository<SubSystem>
{
	Task<Result> AddRangeAsync(
		IEnumerable<SubSystem?> entities, string serverId, CancellationToken cancellationToken = default);

	Task<bool> CheckSubSystemAndServerIdAsync(
		string domainName, string serverId, CancellationToken cancellationToken = default);

	Task<SubSystem?> FindByNameAsync(string modelSubSystemName, CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds a subsystem by its name and associated server ID in the database.
	/// </summary>
	/// <param name="domainName">The name of the subsystem to search for.</param>
	/// <param name="serverId">The ID of the server associated with the subsystem.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the matching SubSystem entity if found; otherwise, null.</returns>
	Task<SubSystem?> FindSubSystemByNameAndServerIdAsync(
		string domainName, string serverId, CancellationToken cancellationToken = default);
}