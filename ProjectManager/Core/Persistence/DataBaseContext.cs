using Domain;
using DatabaseContextSeedworks;
using Microsoft.EntityFrameworkCore;
using Action = Domain.Action;

namespace Persistence;

public class DatabaseContext : BaseDbContextIdentity<User, Role, string>
{
#pragma warning disable CS8618
    public DatabaseContext
#pragma warning restore CS8618
        (DbContextOptions options)
        : base(options)
    {
    }

    public override DbSet<Role> Roles { get; set; }
    public override DbSet<User> Users { get; set; }
    
    public DbSet<SubSystem> SubSystems { get; set; }
    
    public DbSet<Commuting> Commutings { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    public DbSet<Server> Servers { get; set; }

    public DbSet<Domain.Action> Actions { get; set; }
    public DbSet<Dashboard> Dashboards { get; set; }
    public DbSet<DashboardPageRole> DashboardPageRoles { get; set; }
    public DbSet<ReferalLog> ReferalLogs { get; set; }
    public DbSet<SubSystemRoleAccess> SubSystemRoleAccesses { get; set; }

    public DbSet<UserRelation> UserRelations { get; set; }
    public DbSet<UserRelationTemp> UserRelationTemps { get; set; }

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

        modelBuilder.Entity<Server>()
            .HasIndex(x => x.ServerKey)
            .IsUnique(unique: true);
        
        modelBuilder.Entity<SubSystem>()
            .HasOne(current => current.Server)
            .WithMany(current => current.SubSystems)
            .HasForeignKey(current => current.ServerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<SubSystem>()
            .HasMany(current => current.Actions)
            .WithOne(current => current.SubSystem)
            .HasForeignKey(current => current.SubSystemId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Server>()
            .HasMany(x => x.SubSystemRoleAccesses)
            .WithOne(x => x.Server)
            .HasForeignKey(x => x.ServerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Action>()
            .HasOne(x => x.Server)
            .WithMany(x => x.Actions)
            .HasForeignKey(x => x.ServerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Role>()
            .HasMany(x => x.SubSystemRoleAccesses)
            .WithOne(x => x.Role)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Dashboard>()
            .HasOne(x => x.Server)
            .WithMany(x => x.Dashboards)
            .HasForeignKey(x => x.ServerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Dashboard>()
            .HasMany(x => x.DashboardPageRoles)
            .WithOne(x => x.Dashboard)
            .HasForeignKey(x => x.DashboardId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<ReferalLog>()
            .HasOne(x => x.User)
            .WithMany(x => x.ReferalLogs)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ReferalLog>()
            .HasOne(x => x.ReferalUser)
            .WithMany(x => x.ReferalLogsForOthers)
            .HasForeignKey(x => x.ReferalUserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Commuting>()
            .HasOne(p => p.User)
            .WithMany(p => p.Commutings)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Notification>()
            .HasOne(p => p.SavedByUser)
            .WithMany(p => p.Notifications)
            .HasForeignKey(m => m.SavedByUserId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<UserRelation>()
            .HasOne(x => x.Server)
            .WithMany(x => x.UserRelations)
            .HasForeignKey(x => x.ServerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<UserRelation>()
            .HasOne(x => x.SubSystem)
            .WithMany(x => x.UserRelations)
            .HasForeignKey(x => x.SubSystemId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<UserRelation>()
            .HasOne(x => x.User)
            .WithMany(x => x.UserRelations)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction); 
        
        modelBuilder.Entity<UserRelationTemp>()
            .HasOne(x => x.Server)
            .WithMany(x => x.UserRelationTemps)
            .HasForeignKey(x => x.ServerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<UserRelationTemp>()
            .HasOne(x => x.SubSystem)
            .WithMany(x => x.UserRelationTemps)
            .HasForeignKey(x => x.SubSystemId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<UserRelationTemp>()
            .HasOne(x => x.User)
            .WithMany(x => x.UserRelationTemps)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction); 
        
        base.OnModelCreating(modelBuilder);
    }
}