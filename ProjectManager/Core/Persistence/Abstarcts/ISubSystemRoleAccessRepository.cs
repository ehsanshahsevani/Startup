using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface ISubSystemRoleAccessRepository : IRepository<SubSystemRoleAccess>
{
	/// <summary>
	/// check repeat and deleted data from actions table and delete data from this table! 
	/// </summary>
	/// <param name="cancellationToken"></param>
	Task CheckAllPermissionsAsync(CancellationToken cancellationToken = default);
}