using Domain;
using FluentResults;
using PersistenceSeedworks;
using RequestFeatures;
using ViewModels.Ticketing;
using ViewModels.Ticketing.ModelParameters;

namespace Persistence.Abstracts;

public interface IStatusRepository : IRepository<Status>
{
    /// <summary>
    // /// دریافت دسته بندی ها به صورت صفحه بندی با قابلیت سرچ پیشرفته
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PagedList<Status>> GetAllInPageAsync(
        StatusParameters parameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the Status entity with the specified name from the repository asynchronously.
    /// </summary>
    /// <param name="name">The name of the Status entity to be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The Status entity with the specified name, or null if no such entity exists.</returns>
    Task<Status?> FindByNameAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the Status entity with the specified name from the repository asynchronously.
    /// </summary>
    /// <param name="">The name of the Status entity to be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The Status entity with the specified name, or null if no such entity exists.</returns>
    Task<Status?> FindUnderReviewAsync( CancellationToken cancellationToken = default);
    
}