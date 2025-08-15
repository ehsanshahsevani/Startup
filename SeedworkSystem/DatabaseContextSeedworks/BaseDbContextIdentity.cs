using DomainSeedworks.Log;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DatabaseContextSeedworks;

public abstract class BaseDbContextIdentity<TUser, TRole, TKey> : IdentityDbContext<TUser, TRole, TKey>
    where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
{
    public BaseDbContextIdentity(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<LogDetail> LogDetails { get; set; }
    public DbSet<LogServer> LogServers { get; set; }
}