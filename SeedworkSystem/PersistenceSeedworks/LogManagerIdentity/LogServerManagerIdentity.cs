using DatabaseContextSeedworks;
using DomainSeedworks.Log;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersistenceSeedworks.LogManager;

namespace PersistenceSeedworks.LogManagerIdentity;

public class LogServerManagerIdentity<TDbContext, TUser, TRole, TKey> :
    ILogServerManager where TDbContext : BaseDbContextIdentity<TUser, TRole, TKey>
    where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
{
    public TDbContext DbContext { get; }

    public LogServerManagerIdentity(TDbContext dbContext) : base()
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