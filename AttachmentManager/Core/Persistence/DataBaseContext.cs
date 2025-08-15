using DatabaseContextSeedworks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DatabaseContext : BaseDbContext<DatabaseContext>
{
#pragma warning disable CS8618
    public DatabaseContext
#pragma warning restore CS8618
        (DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<SubSystemLocal> SystemLocals { get; set; }
    
    #region OnConfiguring

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseLazyLoadingProxies();
    }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Settings relations and domains

        // modelBuilder.ApplyConfigurationsFromAssembly
        //     (assembly: typeof(Configurations.UserEntityTypeConfiguration).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}