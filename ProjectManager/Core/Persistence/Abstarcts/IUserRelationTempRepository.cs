using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface IUserRelationTempRepository : IRepository<UserRelationTemp>
{
    /// <summary>
    /// پیدا کردن داده های موجود در تم ریلیشن بر اساس آیدی سرور میزبان
    /// </summary>
    /// <param name="serverId">شناسه سرور</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<UserRelationTemp>>
        FindByServerIdAsync(string serverId, CancellationToken cancellationToken = default);

    /// <summary>
    /// حذف دیتاهای یک سرور که به جدول اصلی منتقل شده است بر اساس شناسه سرور
    /// </summary>
    /// <param name="serverId">شناسه سرور</param>
    /// <param name="cancellationToken"></param>
    Task ClearTempAsync(string serverId, CancellationToken cancellationToken = default);
}