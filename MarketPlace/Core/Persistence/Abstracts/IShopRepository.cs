using BaseProject.Model.ViewModel.Public;
using Domain;
using PersistenceSeedworks;
using RequestFeatures;
using SampleResult;
using ViewModels.Marketplace;
using ViewModels.ModelParameters;

namespace Persistence.Abstracts;

public interface IShopRepository : IRepository<Shop>
{
    Task<List<UiSelectModel>> GetSelectValues(CancellationToken cancellationToken = default);

    /// <summary>
    /// دریافت فروشگاه به صورت صفحه بندی با قابلیت سرچ پیشرفته
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PagedList<Shop>> GetAllInPageAsync(
        ShopParameters parameters, CancellationToken cancellationToken = default);

    Task<Result> ConfirmAsync(Shop? entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// بررسی تکراری بودن مقادیر فیلدها
    /// </summary>
    /// <param name="entity">دامین</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<FluentResults.Result> IsOkForAddAsync(ShopRequestViewModel entity, CancellationToken cancellationToken = default);
}