using Domain;
using Utilities;
using RequestFeatures;
using Persistence.Abstracts;
using ViewModels.ModelParameters;
using Microsoft.EntityFrameworkCore;
using BaseProject.Model.ViewModel.Public;
using FluentResults;
using ViewModels.Marketplace;

namespace Persistence.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    internal CategoryRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public override Task<IEnumerable<Category?>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// نمایش DropDown های جدول
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
    /// دریافت دسته بندی ها به صورت صفحه بندی با قابلیت سرچ پیشرفته
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PagedList<Category>> GetAllInPageAsync(
        CategoryParameters parameters, CancellationToken cancellationToken = default)
    {
        var date = parameters.Text.StringToDateTimeMiladi();

        var monthNumberShamsi =
            parameters.Text.ChangeMonthNameShamsiToNumberMonth();

        int? monthNumberMiladi = null;

        if (monthNumberShamsi.HasValue == true)
        {
            var dateString = $"1403/{monthNumberShamsi.Value.ToString().PadLeft(2, '0')}/01";

            monthNumberMiladi = dateString.StringToDateTimeMiladi()!.Value.Month;
        }

        var source = DbSet
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
            <Category>.ToPagedList(source, parameters, cancellationToken);

        return result;
    }

    /// <summary>
    /// Validates that the given category name is unique within its entire tree branch.
    /// </summary>
    /// <param name="entity">The category to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result indicating whether the category can be added</returns>
    public async Task<Result> IsOkForAddAsync(CategoryRequestViewModel entity,
        CancellationToken cancellationToken = default)
    {
        var result = new Result();

        if (await IsNameExist(entity, cancellationToken))
        {
            var errorMessage = string.Format(Resources.Messages.RepeatError,
                Resources.DataDictionary.NameFA + " " + Resources.DataDictionary.OR + " " +
                Resources.DataDictionary.NameEN);
            result.WithError(errorMessage);
        }

        return result;
    }

    /// <summary>
    /// Checks whether the category name exists anywhere within the same tree (parent to children).
    /// </summary>
    private async Task<bool> IsNameExist(CategoryRequestViewModel entity,
        CancellationToken cancellationToken = default)
    {
        var isExist = await DbSet
            .AnyAsync(x => x.Name == entity.Name, cancellationToken);

        return isExist;
    }
    
    
}