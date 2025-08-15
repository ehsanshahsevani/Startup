using DomainSeedworks.Log;
using Microsoft.EntityFrameworkCore.Storage;
using PersistenceSeedworks.LogManager;

namespace PersistenceSeedworks;

public interface IUnitOfWork : IDisposable
{
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    void Dispose();
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    Task<SampleResult.Result> SaveAsync();
    // **********
    Task<IDbContextTransaction> BeginTransactionAsync();

    Task MigrateAsync();
    // **********
}