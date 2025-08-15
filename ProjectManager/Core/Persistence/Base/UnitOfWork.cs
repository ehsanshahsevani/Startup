// using System.Text.Json;
// using System.Text.Json.Serialization;

using DatabaseContextSeedworks;
using Domain;
using Microsoft.EntityFrameworkCore;
using PersistenceSeedworks.LogManager;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence.Base;

public abstract class UnitOfWork : PersistenceSeedworks.IUnitOfWork
{
    public UnitOfWork(Tools.Options options) : base()
    {
        Options = options;
    }

    // **********
    protected Tools.Options Options { get; set; }
    // **********

    // **********
    // **********
    // **********
    private DatabaseContext? _databaseContext;
    // **********

    // **********
    /// <summary>
    /// Lazy Loading = Lazy Initialization
    /// </summary>
    internal DatabaseContext DatabaseContext
    {
        get
        {
            if (_databaseContext is null)
            {
                var optionsBuilder =
                    new DbContextOptionsBuilder<BaseDbContextIdentity<User, Role, string>>();

                switch (Options.Provider)
                {
                    case Tools.Enums.Provider.SqlServer:
                    {
                        optionsBuilder.UseSqlServer
                            (connectionString: Options.ConnectionString);

                        break;
                    }

                    case Tools.Enums.Provider.MySql:
                    {
                        //optionsBuilder.UseMySql
                        //	(connectionString: Options.ConnectionString);

                        break;
                    }

                    case Tools.Enums.Provider.Oracle:
                    {
                        //optionsBuilder.UseOracle
                        //	(connectionString: Options.ConnectionString);

                        break;
                    }

                    case Tools.Enums.Provider.PostgreSQL:
                    {
                        //optionsBuilder.UsePostgreSQL
                        //	(connectionString: Options.ConnectionString);

                        break;
                    }

                    case Tools.Enums.Provider.InMemory:
                    {
                        // optionsBuilder.UseInMemoryDatabase(databaseName: "Temp");

                        break;
                    }
                }

                _databaseContext =
                    new DatabaseContext(
                        optionsBuilder.Options as DbContextOptions<BaseDbContextIdentity<User, Role, string>>);
            }

            return _databaseContext;
        }
    }

    // **********
    /// <summary>
    /// To detect redundant calls
    /// </summary>
    public bool IsDisposed { get; protected set; }
    // **********

    private IDbContextTransaction? Transaction { get; set; }

    // **********
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        Transaction ??=
            await DatabaseContext.Database.BeginTransactionAsync();

        return Transaction;
    }
    // **********

    // **********
    public async Task MigrateAsync()
    {
        await DatabaseContext.Database.MigrateAsync();
    }
    // **********

    /// <summary>
    /// Public implementation of Dispose pattern callable by consumers.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            // TODO: dispose managed state (managed objects).

            if (DatabaseContext != null)
            {
                DatabaseContext.Dispose();
            }
        }

        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.

        IsDisposed = true;
    }

    public async Task<SampleResult.Result> SaveAsync()
    {
        var result = new SampleResult.Result<int>();
        
        result.Value = await DatabaseContext.SaveChangesAsync();
        
        return result;
    }

    // // private IUserRoleRepository _userRoleRepository;
    // public IUserRoleRepository UserRoleRepository
    // {
    //     get
    //     {
    //         _userRoleRepository ??= new UserRoleRepository(DatabaseContext);
    //         return _userRoleRepository;
    //     }
    // }

    ~UnitOfWork()
    {
        Dispose(false);
    }
}