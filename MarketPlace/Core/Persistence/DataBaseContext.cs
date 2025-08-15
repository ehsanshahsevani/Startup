using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DatabaseContext : DatabaseContextSeedworks.BaseDbContext<DatabaseContext>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<AccountCoding> AccountCodings { get; set; }
    public DbSet<AccountCodingSubSystemLocal> AccountCodingSubSystemLocals { get; set; }
    public DbSet<AttachmentSubject> AttachmentSubjects { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<TalaSootBankAccount> BankAccounts { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<ProductBranch>  ProductBranches { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentDetail> DocumentDetails { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<GoldRequest> GoldRequests { get; set; }
    public DbSet<GoldRequestStatus> GoldRequestStatuses { get; set; }
    public DbSet<GoldTreasuryOnline> GoldTreasuryOnlines { get; set; }
    public DbSet<GoldTreasuryReceive> GoldTreasuryReceives { get; set; }
    public DbSet<GoldValue> GoldValues { get; set; }
    public DbSet<IncomeCommissionFee> IncomeCommissionFees { get; set; }
    public DbSet<IncomeGoldPurchaseFee> IncomeGoldPurchaseFees { get; set; }
    public DbSet<IncomeSaleOfGoldFee> IncomeSaleOfGoldFees { get; set; }
    public DbSet<IncomeWalletRechargeFee> IncomeWalletRechargeFees { get; set; }
    public DbSet<IncomeMaintenanceAndInsuranceFee> IncomeMaintenanceAndInsuranceFees { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<PageSetting> PageSettings { get; set; }
    public DbSet<TagPageSetting> TagPageSettings { get; set; }
    public PageSettingTagPageSetting PageSettingTagPageSetting { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<ProfileBank> ProfileBanks { get; set; }
    public DbSet<ProfileHistory> ProfileHistories { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<ReasonRegisterInSystem> ReasonRegisterInSystems { get; set; }
    public DbSet<Referral> Referrals { get; set; }
    public DbSet<RoleGold> RoleGolds { get; set; }
    public DbSet<RoleMoney> RoleMonies { get; set; }
    public DbSet<Shop> Shops { get; set; }
    public DbSet<SubSystemLocal> SubSystemLocals { get; set; }
    public DbSet<TalaSootSettings> TalaSootSettings { get; set; }
    public DbSet<TypeRoleGold> TypeRoleGolds { get; set; }
    public DbSet<TypeRoleMoney> TypeRoleMonies { get; set; }
    public DbSet<UserAssets> UserAssets { get; set; }


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

        modelBuilder.Entity<TagPageSetting>()
            .HasIndex(x => x.NameEn)
            .IsUnique(unique: true);
        
        modelBuilder.Entity<Domain.PageSetting>()
            .HasMany(x => x.PageSettingTagPageSettings)
            .WithOne(x => x.PageSetting)
            .HasForeignKey(x => x.PageSettingId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Domain.TagPageSetting>()
            .HasMany(x => x.PageSettingTagPageSettings)
            .WithOne(x => x.TagPageSetting)
            .HasForeignKey(x => x.TagPageSettingId)
            .OnDelete(DeleteBehavior.NoAction);
        
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

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(p => p.Products)
            .HasForeignKey(m => m.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<ProductBranch>()
            .HasOne(p => p.Product)
            .WithMany(p => p.ProductBranches)
            .HasForeignKey(m => m.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<ProductBranch>()
            .HasOne(p => p.Branch)
            .WithMany(p => p.ProductBranches)
            .HasForeignKey(m => m.BranchId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Branch>()
            .HasOne(p => p.City)
            .WithMany(p => p.Branches)
            .HasForeignKey(m => m.CityId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<CartItem>()
            .HasOne(p => p.Product)
            .WithMany(p => p.CartItems)
            .HasForeignKey(m => m.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Order>()
            .HasOne(p => p.OrderStatus)
            .WithMany(p => p.Orders)
            .HasForeignKey(m => m.OrderStatusId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<OrderItem>()
            .HasOne(p => p.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(m => m.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<DocumentDetail>()
            .HasOne(p => p.Document)
            .WithMany(p => p.DocumentDetails)
            .HasForeignKey(m => m.DocumentId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<DocumentDetail>()
            .HasOne(p => p.AccountCoding)
            .WithMany(p => p.DocumentDetails)
            .HasForeignKey(m => m.AccountCodingId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<DocumentDetail>()
            .HasOne(p => p.AccountCoding)
            .WithMany(p => p.DocumentDetails)
            .HasForeignKey(m => m.AccountCodingId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<DocumentDetail>()
            .HasOne(p => p.SubSystemLocal)
            .WithMany(p => p.DocumentDetails)
            .HasForeignKey(m => m.SubSystemLocalId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<GoldRequest>()
            .HasOne(p => p.Branch)
            .WithMany(p => p.GoldRequests)
            .HasForeignKey(m => m.BranchId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<GoldRequest>()
            .HasOne(p => p.GoldValue)
            .WithMany(p => p.GoldRequests)
            .HasForeignKey(m => m.GoldValueId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<GoldRequest>()
            .HasOne(p => p.GoldRequestStatus)
            .WithMany(p => p.GoldRequests)
            .HasForeignKey(m => m.GoldRequestStatusId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<UserAssets>()
            .HasOne(p => p.Document)
            .WithMany(p => p.UserAssetsList)
            .HasForeignKey(m => m.DocumentId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<UserAssets>()
            .HasOne(p => p.Profile)
            .WithMany(p => p.UserAssetsList)
            .HasForeignKey(m => m.ProfileId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Order>()
            .HasOne(p => p.Profile)
            .WithMany(p => p.Orders)
            .HasForeignKey(m => m.ProfileId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<GoldRequest>()
            .HasOne(p => p.Profile)
            .WithMany(p => p.GoldRequests)
            .HasForeignKey(m => m.ProfileId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<CartItem>()
            .HasOne(p => p.Profile)
            .WithMany(p => p.CartItems)
            .HasForeignKey(m => m.ProfileId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<AccountCoding>()
            .HasOne(p => p.Parent)
            .WithMany(p => p.Children)
            .HasForeignKey(m => m.ParentId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Bank>()
            .HasMany(p => p.ProfileBanks)
            .WithOne(p => p.Bank)
            .HasForeignKey(m => m.BankId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Profile>()
            .HasMany(p => p.ProfileBanks)
            .WithOne(p => p.Profile)
            .HasForeignKey(m => m.ProfileId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Profile>()
            .HasMany(p => p.Transactions)
            .WithOne(p => p.Profile)
            .HasForeignKey(m => m.ProfileId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Document>()
            .HasMany(p => p.RelationalDocumentDetails)
            .WithOne(p => p.ParentDocument)
            .HasForeignKey(m => m.ParentDocumentId)
            .OnDelete(DeleteBehavior.NoAction);
        
        base.OnModelCreating(modelBuilder);
    }
}