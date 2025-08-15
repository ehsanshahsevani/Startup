using DomainSeedworks.Log;

namespace PersistenceSeedworks.LogManagerIdentity;

public interface ILogServerManagerIdentity
{
    Task CreateAsync(LogServer logServer);
}