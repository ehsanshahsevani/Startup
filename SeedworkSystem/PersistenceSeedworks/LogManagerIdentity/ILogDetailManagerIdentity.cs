using DomainSeedworks.Log;

namespace PersistenceSeedworks.LogManagerIdentity;

public interface ILogDetailManagerIdentity
{
    Task CreateAsync(LogDetail logDetail);
}