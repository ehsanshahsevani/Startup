using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class SubSystemLocalRepository : Repository<SubSystemLocal>, ISubSystemLocalRepository
{
    internal SubSystemLocalRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public async Task AddByNamesAsync(List<string> domains, CancellationToken cancellationToken = default)
    {
        List<string> listExisted = await DbSet

            .Where(current => current.IsDeleted == false)
            .Where(current => current.IsActive == true)

            .Where(current => domains.Contains(current.NameEN) == true)

            .Select(current => current.NameEN)
            
            .ToListAsync(cancellationToken);

        var domainsToAdd =
            domains.Where(d => listExisted.Contains(d) == false).ToList();
        
        List<SubSystemLocal> list = [];
        
        foreach (var subSystem in domainsToAdd)
        {
            var subSystemLocal = new SubSystemLocal()
            {
                IsActive = true,
                NameEN = subSystem,
                NameFA = subSystem,
            };
            
            list.Add(subSystemLocal);
        }
        
        await AddRangeAsync(list, cancellationToken);
    }

    public override async Task<IEnumerable<SubSystemLocal?>>
        GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await DbSet
            .Where(current => current.IsDeleted == false)
            .Where(current => current.IsActive == true)
            .ToListAsync(cancellationToken);
        
        return result;
    }
}