using Domain;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;
using RequestFeatures;
using Resources;
using Utilities;
using ViewModels.Marketplace;
using ViewModels.ModelParameters;
using ViewModels.Ticketing;
using ViewModels.Ticketing.ModelParameters;

namespace Persistence.Repositories;

public class TicketRepository : Repository<Ticket>, ITicketRepository
{
    internal TicketRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public override async Task<Ticket?> FindAsync(object id, CancellationToken cancellationToken = default)
    {
        var result = await DbSet
            .Include(current => current.TicketMessages)
            .Where(current => current.Id == id.ToString())
            .Where(current => current.IsDeleted == false)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task<PagedList<Ticket>?> FindBySubjectStatusAsync(string? statusId, string? ticketSubjectId,
        TicketParameters parameters,CancellationToken cancellationToken = default)
    {
        var source = DbSet
            .Include(current => current.TicketMessages)
            .Where(current => statusId != null && current.StatusId == statusId)
            .Where(current => ticketSubjectId != null && current.TicketSubjectId == ticketSubjectId)
            .Where(current => current.IsDeleted == false);
        
        var result = await PagedList
            <Ticket>.ToPagedList(source, parameters, cancellationToken);
        
        return result;
    }


    public override async Task<IEnumerable<Ticket?>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await DbSet
            .Where(current => current.IsDeleted == false)
            .ToListAsync(cancellationToken: cancellationToken);

        return result;
    }

    /// <summary>
    ///     دریافت دسته بندی ها به صورت صفحه بندی با قابلیت سرچ پیشرفته
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PagedList<Ticket>> GetAllInPageAsync(
        TicketParameters parameters, CancellationToken cancellationToken = default)
    {
        var date = parameters.Text.StringToDateTimeMiladi();

        var monthNumberShamsi =
            parameters.Text.ChangeMonthNameShamsiToNumberMonth();

        int? monthNumberMiladi = null;

        if (monthNumberShamsi.HasValue)
        {
            var dateString = $"1403/{monthNumberShamsi.Value.ToString().PadLeft(2, '0')}/01";

            monthNumberMiladi = dateString.StringToDateTimeMiladi()!.Value.Month;
        }

        var source = DbSet
            .Include(current => current.TicketSubject)
            .Include(current => current.Status)
            .Include(current => current.TicketMessages)
            .Where(current => current.IsDeleted == false)
            .Where(current =>
                string.IsNullOrEmpty(parameters.Text) == true
                ||
                (
                    string.IsNullOrEmpty(current.Description) == false
                    && current.Description.Contains(parameters.Text))
            )
            .Where(current =>
                date.HasValue == false
                || current.CreateDateTime == date.Value
                || current.CreateDateTime == date.Value
                || monthNumberMiladi.HasValue == false
                || current.CreateDateTime.Month == monthNumberMiladi.Value
                || current.CreateDateTime.Month == monthNumberMiladi.Value)
            .OrderBy(o => o.Ordering)
            .ThenByDescending(p => p.CreateDateTime);

        var result = await PagedList
            <Ticket>.ToPagedList(source, parameters, cancellationToken);

        return result;
    }
}