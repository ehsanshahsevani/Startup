using DomainSeedworks.Log;

namespace PersistenceSeedworks.LogManager;

public interface ILogServerManager
{
    Task<string> CreateAsync(LogServer logServer);
}