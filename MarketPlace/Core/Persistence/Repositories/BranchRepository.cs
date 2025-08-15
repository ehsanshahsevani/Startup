using BaseProject.Model.ViewModel.Public;
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

public class BranchRepository : Repository<Branch>, IBranchRepository
{
    internal BranchRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public override async Task<Branch?> FindAsync(object id, CancellationToken cancellationToken = default)
    {
        var result = await DbSet
            .Include(current => current.City)
            .Include(current => current.City.Province)
            .Where(current => current.Id == id.ToString())
            .Where(current => current.IsDeleted == false)
            .FirstOrDefaultAsync(cancellationToken);
        
        return result;
    }

    public override Task<IEnumerable<Branch?>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     نمایش DropDown های جدول
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>  مدلی از نام فارسی | انگلیسی و شناسه جدول  </returns>
    public async Task<List<UiSelectModel>> GetSelectValues(CancellationToken cancellationToken = default)
    {
        var result = await DbSet
            .Where(p => p.IsDeleted == false)
            .Select(p => new UiSelectModel(p.Name, p.Id))
            .ToListAsync(cancellationToken);

        return result;
    }

    /// <summary>
    ///     دریافت دسته بندی ها به صورت صفحه بندی با قابلیت سرچ پیشرفته
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PagedList<Branch>> GetAllInPageAsync(
        BranchParameters parameters, CancellationToken cancellationToken = default)
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
            .Include(current => current.City)
            .Include(current => current.City.Province)
            .Where(current => current.IsDeleted == false)
            .Where(current =>
                string.IsNullOrEmpty(parameters.Text) == true
                ||
                current.Id.Contains(parameters.Text) == true
                ||
                (
                    string.IsNullOrEmpty(current.Name) == false
                    && current.Name.Contains(parameters.Text)
                )
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
            <Branch>.ToPagedList(source, parameters, cancellationToken);

        return result;
    }

    /// <summary>
    ///     Validates that the given Branch name is unique within its entire tree branch.
    /// </summary>
    /// <param name="entity">The Branch to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result indicating whether the Branch can be added</returns>
    public async Task<Result> IsOkForAddAsync(BranchRequestViewModel entity,
        CancellationToken cancellationToken = default)
    {
        var result = new Result();

        if (await IsNameExist(entity, cancellationToken))
        {
            var errorMessage = string.Format(Messages.RepeatError,
                DataDictionary.NameFA + " " + DataDictionary.OR + " " +
                DataDictionary.NameEN);
            result.WithError(errorMessage);
        }

        return result;
    }

    /// <summary>
    ///     Checks whether the Branch name exists anywhere within the same tree (parent to children).
    /// </summary>
    private async Task<bool> IsNameExist(BranchRequestViewModel entity,
        CancellationToken cancellationToken = default)
    {
        var isExist = await DbSet
            .AnyAsync(x => x.Name == entity.Name, cancellationToken);

        return isExist;
    }
}