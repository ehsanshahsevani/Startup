using BaseProject.Model.ViewModel.Public;
using Domain;
using FluentResults;
using PersistenceSeedworks;
using RequestFeatures;
using ViewModels.Marketplace;
using ViewModels.ModelParameters;

namespace Persistence.Abstracts;

public interface IProductRepository : IRepository<Product>
{
    Task<List<UiSelectModel>> GetSelectValues(CancellationToken cancellationToken = default);

    /// <summary>
    /// دریافت دسته بندی ها به صورت صفحه بندی با قابلیت سرچ پیشرفته
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PagedList<Product>> GetAllInPageAsync(
        ProductParameters parameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// این سرچ در شاخه ها اتفاق میوفتد و اجازه نمیدهد یک مقدارتکراری در یک شاخه بوجود بیاید
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result> IsOkForAddAsync(ProductRequestViewModel entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// رکورد یک ردیف با دسته بندی و شعبه های آن
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Product> FindWithJoinAsync(object id, CancellationToken cancellationToken = default);
}