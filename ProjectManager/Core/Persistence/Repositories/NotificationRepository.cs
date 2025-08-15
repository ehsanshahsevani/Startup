using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;
using Persistence.ModelParameters;
using RequestFeatures;
using Utilities;

namespace Persistence.Repositories;

public class NotificationRepository: Repository<Notification> , INotificationRepository
{
    internal NotificationRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }
    
    public async Task<PagedList<Notification>> GetAllInPageAsync(
        RequestParameter requestParameters
        ,CancellationToken cancellationToken = default)
    {
        var date = requestParameters.Text.StringToDateTimeMiladi();

        var monthNumberShamsi =
            requestParameters.Text.ChangeMonthNameShamsiToNumberMonth();

        int? monthNumberMiladi = null;

        if (monthNumberShamsi.HasValue == true)
        {
            var dateString = $"1403/{monthNumberShamsi.Value.ToString().PadLeft(2, '0')}/01";

            monthNumberMiladi = dateString.StringToDateTimeMiladi()!.Value.Month;
        }

        var source = DbSet
            .Include(x => x.SavedByUser)
            .Where(current => current.IsActive)
            .Where(current =>
                string.IsNullOrEmpty(requestParameters.Text)
                || current.Title.Contains(requestParameters.Text)
                || current.Description.Contains(requestParameters.Text)
                ||
                (
                    !string.IsNullOrEmpty(current.SavedByUser!.UserName)
                    && current.SavedByUser.UserName.Contains(requestParameters.Text)
                )
                || current.SavedByUser.PhoneNumber.Contains(requestParameters.Text)
                || current.SavedByUser.UserName.Contains(requestParameters.Text))
            .Where(current =>
                !date.HasValue
                || (current.CreateDateTime == date.Value)
                || current.CreateDateTime == date.Value
                || !monthNumberMiladi.HasValue
                || (current.CreateDateTime.Month == monthNumberMiladi.Value)
                || current.CreateDateTime.Month == monthNumberMiladi.Value)
            .OrderBy(o => o.Ordering)
            .ThenByDescending(p => p.CreateDateTime);
            
        var result = await PagedList
            <Notification>.ToPagedList(source, requestParameters);
        
        return result;
    }
    
    /// <summary>
    /// دریافت همه بر اساس نوع اعلان
    /// </summary>
    /// <param name="notificationTypeId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<Notification>> FindByNotificationTypeIdAsync
        (string notificationTypeId, CancellationToken cancellationToken = default)
    {
        var result = await DbSet
                .Where(current => current.IsActive == true)
                .OrderBy(current => current.Ordering)
                .ThenByDescending(current => current.CreateDateTime)
                .ToListAsync();

        return result;
    }

    public async Task<Notification> FindForSendAsync(string notificationId, CancellationToken cancellationToken = default)
    {
        var result = await DbSet
            .Include(current => current.SavedByUserId)
            
            .Where(current => current.IsDeleted == false)
            
            .Where(current => current.IsActive == true)

            .Where(current => current.Id == notificationId)

            .FirstOrDefaultAsync(cancellationToken);
        
        return result;
    }
}