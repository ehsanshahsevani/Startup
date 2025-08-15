using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface ISubSystemLocalRepository : IRepository<SubSystemLocal>
{
	Task AddByNamesAsync(List<string> domains, CancellationToken cancellationToken = default);
}