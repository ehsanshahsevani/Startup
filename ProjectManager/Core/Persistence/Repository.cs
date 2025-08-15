using System.Linq.Expressions;
using RequestFeatures;
using DomainSeedworks;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class Repository<TEntity> : Base.Repository<TEntity> where TEntity : BaseEntity
{
    internal Repository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public override async Task<bool> RemoveByIdAsync
        (object id, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await DbSet

            .Where(current => current.Id == id)

            .FirstOrDefaultAsync();

        if (entity is null)
        {
            throw new ArgumentNullException(paramName: nameof(id));
        }

        entity.IsDeleted = true;
        entity.UpdateDateTime = DateTime.UtcNow;

        return true;
    }

    public override async Task RemoveAsync
        (TEntity? entity, CancellationToken cancellationToken = default)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(paramName: nameof(entity));
        }

        TEntity? searchEntity =

            await DbSet

            .Where(current => current.Id == entity.Id)

            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (searchEntity is null)
        {
            throw new ArgumentNullException(paramName: nameof(searchEntity));
        }

        searchEntity.IsDeleted = true;
        searchEntity.UpdateDateTime = DateTime.UtcNow;
    }

    public override async Task RemoveRangeAsync
        (IEnumerable<TEntity?> entities, CancellationToken cancellationToken = default)
    {
        foreach (var item in entities)
        {
            await RemoveAsync(item, cancellationToken);
        }
    }

    public override async Task
        UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            var attachedEntity = DatabaseContext.Attach(entity: entity);

            if (attachedEntity.State != EntityState.Modified)
            {
                attachedEntity.State = EntityState.Modified;
            }
        }, cancellationToken: cancellationToken);
    }

    public override async Task<IEnumerable<TEntity?>>
        GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await DbSet

            .Where(current => current.IsDeleted == false)

            .ToListAsync(cancellationToken: cancellationToken);

        return result;
    }

    public override async Task<TEntity?>
        FindAsync(object id, CancellationToken cancellationToken = default)
    {
        var result = await DbSet

            .Where(current => current.IsDeleted == false)

            .Where(current => current.Id == id)

            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return result;
    }

    public virtual async Task<PagedList<TEntity>> GetAllInPageAsync(
        RequestParameters parameters, CancellationToken cancellationToken = default)
    {
        var source = DbSet

            .Where(current => current.IsDeleted == false)

            .OrderByDescending(current => current.CreateDateTime)

            ;

        var result = await PagedList<TEntity>
            .ToPagedList(source, parameters, cancellationToken);

        return result;
    }

    /// <summary>
    /// توسعه پیدا نکند
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException">این تابع به دلایل فنی خاموش شده است و قابل استفاده نیست</exception>
    public override Task<IEnumerable<TEntity?>> Find(Expression<Func<TEntity?, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("The method or operation is not implemented");
    }
}