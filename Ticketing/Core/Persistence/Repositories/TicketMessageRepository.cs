using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;
using RequestFeatures;
using Utilities;
using ViewModels.Ticketing.ModelParameters;

namespace Persistence.Repositories;

public class TicketMessageRepository : Repository<TicketMessage>, ITicketMessageRepository
{
	internal TicketMessageRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
	
	public override async Task<TicketMessage?> FindAsync(object id, CancellationToken cancellationToken = default)
    {
        var result = await DbSet
            .Where(current => current.Id == id.ToString())
            .Where(current => current.IsDeleted == false)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public override async Task<IEnumerable<TicketMessage?>> GetAllAsync(CancellationToken cancellationToken = default)
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
    public async Task<PagedList<TicketMessage>> GetAllInPageAsync(
        TicketMessageParameters parameters, CancellationToken cancellationToken = default)
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
            <TicketMessage>.ToPagedList(source, parameters, cancellationToken);

        return result;
    }

}