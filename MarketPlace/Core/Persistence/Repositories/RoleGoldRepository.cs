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

public class RoleGoldRepository : Repository<RoleGold>, IRoleGoldRepository
{
    internal RoleGoldRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public override async Task<RoleGold?> FindAsync(object id, CancellationToken cancellationToken = default)
    {
        var result = await DbSet
            .Include(current => current.TypeRoleGold)
            .Where(current => current.Id == id.ToString())
            .Where(current => current.IsDeleted == false)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public override Task<IEnumerable<RoleGold?>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     دریافت دسته بندی ها به صورت صفحه بندی با قابلیت سرچ پیشرفته
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PagedList<RoleGold>> GetAllInPageAsync(
        RoleGoldParameters parameters, CancellationToken cancellationToken = default)
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
            .Include(current => current.TypeRoleGold)
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
            <RoleGold>.ToPagedList(source, parameters, cancellationToken);

        return result;
    }

    /// <summary>
    ///     Validates that the given RoleGold name is unique within its entire tree RoleGold.
    /// </summary>
    /// <param name="entity">The RoleGold to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result indicating whether the RoleGold can be added</returns>
    public async Task<Result> IsOkForAddAsync(RoleGoldRequestViewModel entity,
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
    ///     Checks whether the RoleGold name exists anywhere within the same tree (parent to children).
    /// </summary>
    private async Task<bool> IsNameExist(RoleGoldRequestViewModel entity,
        CancellationToken cancellationToken = default)
    {
        // var isExist = await DbSet
        //     .AnyAsync(x => x.Min == entity.Min &&  x.Max == entity.Max , cancellationToken);

        // return isExist;

        return true;
    }
}
