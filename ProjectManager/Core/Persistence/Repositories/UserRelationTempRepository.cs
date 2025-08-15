using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class UserRelationTempRepository : Repository<UserRelationTemp> , IUserRelationTempRepository
{
    internal UserRelationTempRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    /// <summary>
    /// حذف دیتاهای یک سرور که به جدول اصلی منتقل شده است بر اساس شناسه سرور
    /// </summary>
    /// <param name="serverId">شناسه سرور</param>
    /// <param name="cancellationToken"></param>
    public async Task ClearTempAsync(string serverId, CancellationToken cancellationToken = default)
    {
        var result = await DbSet

            .ToListAsync();
        
        DbSet.RemoveRange(result);
    }

    /// <summary>
    /// پیدا کردن داده های موجود در تم ریلیشن بر اساس آیدی سرور میزبان
    /// </summary>
    /// <param name="serverId">شناسه سرور</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<UserRelationTemp>>
        FindByServerIdAsync(string serverId, CancellationToken cancellationToken = default)
    {
        var result = await DbSet

            .Where(current => current.ServerId == serverId)
            .Where(current => current.IsDeleted == false)
            .Where(current => current.IsActive == true)
                
            .ToListAsync(cancellationToken);

        return result;
    }
}