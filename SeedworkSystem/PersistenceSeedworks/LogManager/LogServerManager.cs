using DomainSeedworks.Log;
using Microsoft.EntityFrameworkCore;

namespace PersistenceSeedworks.LogManager;

/// <summary>
/// base log deatil POCO file !
/// </summary>
public class LogServerManager<TDbContext> :
    ILogServerManager where TDbContext : DbContext
{
    public TDbContext DbContext { get; }

    public LogServerManager(TDbContext dbContext) : base()
    {
        DbContext = dbContext;
    }

    public async Task<string> CreateAsync(LogServer logServer)
    {
        await DbContext.AddAsync(logServer);
        await DbContext.SaveChangesAsync();
        
        return logServer.Id;
    }
}