using Domain;
using FluentResults;
using PersistenceSeedworks;
using RequestFeatures;
using ViewModels.Marketplace;
using ViewModels.ModelParameters;

namespace Persistence.Abstracts;

public interface IRoleMoneyRepository : IRepository<RoleMoney>
{
    /// <summary>
    // /// دریافت دسته بندی ها به صورت صفحه بندی با قابلیت سرچ پیشرفته
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PagedList<RoleMoney>> GetAllInPageAsync(
        RoleMoneyParameters parameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// این سرچ در شاخه ها اتفاق میوفتد و اجازه نمیدهد یک مقدارتکراری در یک شاخه بوجود بیاید
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result> IsOkForAddAsync(RoleMoneyRequestViewModel entity, CancellationToken cancellationToken = default);
}