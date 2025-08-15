using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DatabaseContext : DatabaseContextSeedworks.BaseDbContext<DatabaseContext>
{
	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
	{
	}

	public DbSet<Attachment> Attachments { get; set; }
	public DbSet<Status> Status { get; set; }
	public DbSet<Ticket> Tickets { get; set; }
	public DbSet<TicketSubject> TicketSubjects { get; set; }
	public DbSet<TicketMessage> TicketMessages { get; set; }
	public DbSet<SubSystemLocal> SubSystemLocals { get; set; }
	public DbSet<AttachmentSubject> AttachmentSubjects { get; set; }
	
	public DbSet<PageSetting> PageSettings { get; set; }

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

		modelBuilder.Entity<SubSystemLocal>()
			.HasIndex(x => x.NameEN)
			.IsUnique(unique: true);
		
		modelBuilder.Entity<Attachment>()
			.HasOne(p => p.SubSystemLocal)
			.WithMany(p => p.Attachments)
			.HasForeignKey(m => m.SubSystemLocalId)
			.OnDelete(DeleteBehavior.NoAction);

		modelBuilder.Entity<Attachment>()
			.HasOne(p => p.AttachmentSubject)
			.WithMany(p => p.Attachments)
			.HasForeignKey(m => m.AttachmentSubjectId)
			.OnDelete(DeleteBehavior.NoAction);
		
		modelBuilder.Entity<TicketSubject>()
			.HasMany(c => c.Tickets)
			.WithOne(c => c.TicketSubject)
			.HasForeignKey(c => c.TicketSubjectId)
			.OnDelete(DeleteBehavior.NoAction);
		
		modelBuilder.Entity<Ticket>()
			.HasOne(c => c.Status)
			.WithMany(c => c.Tickets)
			.HasForeignKey(c => c.StatusId)
			.OnDelete(DeleteBehavior.NoAction);

		modelBuilder.Entity<Ticket>()
			.HasMany(c => c.TicketMessages)
			.WithOne(c => c.Ticket)
			.HasForeignKey(c => c.TicketId)
			.OnDelete(DeleteBehavior.NoAction);
		
		base.OnModelCreating(modelBuilder);
	}
}