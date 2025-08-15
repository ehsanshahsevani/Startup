using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;
using RequestFeatures;
using Utilities;
using ViewModels.Ticketing.ModelParameters;

namespace Persistence.Repositories;

public class TicketSubjectRepository : Repository<TicketSubject>, ITicketSubjectRepository
{
    internal TicketSubjectRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    /// <summary>
    ///     Retrieves the TagPageSetting entity with the specified name from the repository asynchronously.
    /// </summary>
    /// <param name="name">The name of the TagPageSetting entity to be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The TagPageSetting entity with the specified name, or null if no such entity exists.</returns>
    public async Task<TicketSubject?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var result = await DbSet
            .Where(x => x.IsDeleted == false)
            .Where(x => x.IsActive == true)
            .Where(current => current.Name == name)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public override async Task<TicketSubject?> FindAsync(object id, CancellationToken cancellationToken = default)
    {
        var result = await DbSet
            .Where(current => current.Id == id.ToString())
            .Where(current => current.IsDeleted == false)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public override async Task<IEnumerable<TicketSubject?>> GetAllAsync(CancellationToken cancellationToken = default)
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
    public async Task<PagedList<TicketSubject>> GetAllInPageAsync(
        TicketSubjectParameters parameters, CancellationToken cancellationToken = default)
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
            <TicketSubject>.ToPagedList(source, parameters, cancellationToken);

        return result;
    }
}