using Domain;
using PersistenceSeedworks;
using RequestFeatures;
using ViewModels.Ticketing.ModelParameters;

namespace Persistence.Abstracts;

public interface ITicketRepository : IRepository<Ticket>
{
    // <summary>
    // /// دریافت دسته بندی ها به صورت صفحه بندی با قابلیت سرچ پیشرفته
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PagedList<Ticket>> GetAllInPageAsync(
        TicketParameters parameters, CancellationToken cancellationToken = default);

    
    // <summary>
    // /// دریافت دسته بندی ها به صورت صفحه بندی با قابلیت سرچ پیشرفته
    /// </summary>
    /// <param name="statusId"></param>
    /// <param name="ticketSubjectId"></param>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PagedList<Ticket>?> FindBySubjectStatusAsync(string? statusId, string? ticketSubjectId,
        TicketParameters parameters, CancellationToken cancellationToken = default);

}