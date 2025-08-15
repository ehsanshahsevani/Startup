using DomainSeedworks.Log;

namespace PersistenceSeedworks.LogManager;

public interface ILogDetailManager
{
    Task CreateAsync(LogDetail logDetail);
}