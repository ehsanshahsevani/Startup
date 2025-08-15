using Domain;
using PersistenceSeedworks;
using RequestFeatures;
using ViewModels.Ticketing.ModelParameters;

namespace Persistence.Abstracts;

public interface ITicketMessageRepository : IRepository<TicketMessage>
{
    // <summary>
    // /// دریافت دسته بندی ها به صورت صفحه بندی با قابلیت سرچ پیشرفته
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PagedList<TicketMessage>> GetAllInPageAsync(
        TicketMessageParameters parameters, CancellationToken cancellationToken = default);
}