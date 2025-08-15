using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface ISubSystemLocalRepository : IRepository<SubSystemLocal>
{
	Task AddByNamesAsync(List<string> domains, CancellationToken cancellationToken = default);
	
	Task<SubSystemLocal?> FindByNameAsync(string domain, CancellationToken cancellationToken = default);

	Task<string?> FindDescriptionBySubSystemNameAsync(string subSystemName,
		CancellationToken cancellationToken = default);

	Task<SubSystemLocal?> UpdateDescriptionByIdAsync(string id, string description,
		CancellationToken cancellationToken = default);
}