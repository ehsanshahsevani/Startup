using Domain;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class SubSystemRepository : Repository<SubSystem> , ISubSystemRepository
{
    internal SubSystemRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public async Task<Result> AddRangeAsync(
        IEnumerable<SubSystem?> entities, string serverId, CancellationToken cancellationToken = default)
    {
        var listExisted = new List<SubSystem>();
        
        var result = new Result();
        
        foreach (SubSystem? entity in entities)
        {
            if (entity is not null)
            {
                var entitySearch = await DbSet
                        
                    .Where(current => current.IsDeleted == false)
                    .Where(current => current.IsActive == true)
                        
                    .Where(current => current.ServerId == serverId)
                    .Where(current => current.Id == entity.Id || current.NameEN.Equals(entity.NameEN))

                    .FirstOrDefaultAsync(cancellationToken);

                if (entitySearch is not null)
                {
                    if (entity.NameEN == entitySearch.NameEN && entity.Id == entitySearch.Id)
                    {
                        // ok continue;
                        listExisted.Add(entity);
                    }
                    else if (entity.NameEN != entitySearch.NameEN && entity.Id == entitySearch.Id)
                    {
                        // ok continue;
                        entity.NameEN = entitySearch.NameEN;
                        entity.NameFA = entitySearch.NameFA;

                        await DatabaseContext.SaveChangesAsync(cancellationToken);
                        
                        listExisted.Add(entity);
                    }
                    else
                    {
                        result.WithError($"duplicate name en: {entitySearch.NameEN} and id: {entitySearch.Id}, register Project Manager In new server OR Change local ServerId");
                    }
                }
            }
        }

        if (result.IsSuccess == true)
        {
            List<SubSystem> entitiesToAdd = entities.Where(d => 
                    listExisted.Select(current => current.Id)
                        .Contains(d.Id) == false).ToList();
        
            await DbSet.AddRangeAsync(entitiesToAdd, cancellationToken);
        }
        
        return result;
    }
    
    public async Task<bool> CheckSubSystemAndServerIdAsync(
        string domainName, string serverId, CancellationToken cancellationToken = default)
    {
        var result = await DbSet
            
            .Where(current => current.IsDeleted == false)
            .Where(current => current.IsActive == true)
            
            .Where(current => current.NameEN.ToLower().Equals(domainName.ToLower()))
            .Where(current => current.ServerId == serverId)
        
            .AnyAsync(cancellationToken);
            
        return result;
    }

    /// <summary>
    /// Finds a subsystem by its name and associated server ID in the database.
    /// </summary>
    /// <param name="domainName">The name of the subsystem to search for.</param>
    /// <param name="serverId">The ID of the server associated with the subsystem.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the matching SubSystem entity if found; otherwise, null.</returns>
    public async Task<SubSystem?> FindSubSystemByNameAndServerIdAsync(
        string domainName, string serverId, CancellationToken cancellationToken = default)
    {
        var result = await DbSet
            
            .Where(current => current.IsDeleted == false)
            .Where(current => current.IsActive == true)
            
            .Where(current => current.NameEN.ToLower().Equals(domainName.ToLower()))
            .Where(current => current.ServerId == serverId)
        
            .FirstOrDefaultAsync(cancellationToken);
            
        return result;
    }

    public async Task<SubSystem?> FindByNameAsync(string modelSubSystemName, CancellationToken cancellationToken = default)
    {
        var result = await DbSet
            .Where(current => current.IsDeleted == false)
            .Where(current => current.IsActive == true)
            .Where(current => current.NameEN.Equals(modelSubSystemName))
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }
}