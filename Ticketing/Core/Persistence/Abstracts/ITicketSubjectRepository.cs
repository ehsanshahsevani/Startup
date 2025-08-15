using Domain;
using PersistenceSeedworks;
using RequestFeatures;
using ViewModels.Ticketing.ModelParameters;

namespace Persistence.Abstracts;

public interface ITicketSubjectRepository : IRepository<TicketSubject>
{
    // <summary>
    // /// دریافت دسته بندی ها به صورت صفحه بندی با قابلیت سرچ پیشرفته
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PagedList<TicketSubject>> GetAllInPageAsync(
        TicketSubjectParameters parameters, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Retrieves the TagPageSetting entity with the specified name from the repository asynchronously.
    /// </summary>
    /// <param name="name">The name of the TagPageSetting entity to be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The TagPageSetting entity with the specified name, or null if no such entity exists.</returns>
    Task<TicketSubject?> FindByNameAsync(string name, CancellationToken cancellationToken = default);
}