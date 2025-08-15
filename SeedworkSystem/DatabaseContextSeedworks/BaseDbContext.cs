using DomainSeedworks.Log;
using Microsoft.EntityFrameworkCore;
namespace DatabaseContextSeedworks;

public abstract class BaseDbContext<TDbContext> : DbContext where TDbContext : DbContext
{
#pragma warning disable CS8618
    public BaseDbContext
#pragma warning restore CS8618
        (DbContextOptions<TDbContext> options)
        : base(options)
    {
    }

    public DbSet<LogDetail> LogDetails { get; set; }
    public DbSet<LogServer> LogServers { get; set; }
}