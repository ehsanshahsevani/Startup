using Domain;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;
using RequestFeatures;
using Resources;
using Utilities;
using ViewModels.Marketplace;
using ViewModels.ModelParameters;

namespace Persistence.Repositories;

public class RoleMoneyRepository : Repository<RoleMoney>, IRoleMoneyRepository
{
    internal RoleMoneyRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public override async Task<RoleMoney?> FindAsync(object id, CancellationToken cancellationToken = default)
    {
        var result = await DbSet
            .Include(current => current.TypeRoleMoney)
            .Where(current => current.Id == id.ToString())
            .Where(current => current.IsDeleted == false)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public override Task<IEnumerable<RoleMoney?>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     دریافت دسته بندی ها به صورت صفحه بندی با قابلیت سرچ پیشرفته
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PagedList<RoleMoney>> GetAllInPageAsync(
        RoleMoneyParameters parameters, CancellationToken cancellationToken = default)
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
            .Include(current => current.TypeRoleMoney)
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
            <RoleMoney>.ToPagedList(source, parameters, cancellationToken);

        return result;
    }

    /// <summary>
    ///     Validates that the given RoleMoney name is unique within its entire tree RoleMoney.
    /// </summary>
    /// <param name="entity">The RoleMoney to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result indicating whether the RoleMoney can be added</returns>
    public async Task<Result> IsOkForAddAsync(RoleMoneyRequestViewModel entity,
        CancellationToken cancellationToken = default)
    {
        var result = new Result();

        if (await IsNameExist(entity, cancellationToken))
        {
            var errorMessage = string.Format(Messages.RepeatError,
                DataDictionary.Min + " " + DataDictionary.And + " " +
                DataDictionary.Max);
            result.WithError(errorMessage);
        }

        return result;
    }

    /// <summary>
    ///     Checks whether the RoleMoney name exists anywhere within the same tree (parent to children).
    /// </summary>
    private async Task<bool> IsNameExist(RoleMoneyRequestViewModel entity,
        CancellationToken cancellationToken = default)
    {
        // var isExist = await DbSet
        //     .AnyAsync(x => x.Min == entity.Min &&  x.Max == entity.Max , cancellationToken);

        // return isExist;

        return true;
    }
}