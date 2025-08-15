using BaseProject.Model.ViewModel.Public;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;
using RequestFeatures;
using Utilities;
using ViewModels.Marketplace;
using ViewModels.ModelParameters;
using ViewModels.ProjectManager;
using Result = SampleResult.Result;

namespace Persistence.Repositories;

public class ShopRepository : Repository<Shop>, IShopRepository
{
    public ShopRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public override Task<IEnumerable<Shop?>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return base.GetAllAsync(cancellationToken);
    }

    /// <summary>
    /// نمایش DropDown های جدول
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>  مدلی از نام فارسی | انگلیسی و شناسه جدول  </returns>
    public async Task<List<UiSelectModel>> GetSelectValues(CancellationToken cancellationToken = default)
    {
        var result = new List<UiSelectModel>();

        result = await DbSet
            .Where(p => p.IsActive == true)
            .Where(p => p.IsDeleted == false)
            .Select(p => new UiSelectModel(p.ShopDisplayName, p.Id))
            .ToListAsync(cancellationToken);

        return result;
    }

    /// <summary>
    /// دریافت ویژگی ها به صورت صفحه بندی با قابلیت سرچ پیشرفته
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PagedList<Shop>> GetAllInPageAsync(
        ShopParameters parameters, CancellationToken cancellationToken = default)
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
            <Shop>.ToPagedList(source, parameters, cancellationToken);

        return result;
    }

    public override async Task<Result> AddAsync(Shop? entity, CancellationToken cancellationToken = default)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        var service = new HttpServices.ProjectManager.UserRelationService();

        var relation =
            new UserRelationResponseViewModel(
                Domain.Base.ServerKeyConstant.Key,
                nameof(Shop), entity.CreatedByProfileId,
                entity.Id, nameof(entity.CreatedByProfileId));

        var result = await service.AddAsync(relation);

        if (result.IsSuccess == true)
        {
            await base.AddAsync(entity, cancellationToken);
        }

        return result;
    }

    public async Task<Result> ConfirmAsync(Shop? entity, CancellationToken cancellationToken = default)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        var service = new HttpServices.ProjectManager.UserRelationService();

        var relation =
            new UserRelationResponseViewModel(
                Domain.Base.ServerKeyConstant.Key,
                nameof(Shop), entity.ConfirmedByProfileId,
                entity.Id, nameof(entity.ConfirmedByProfileId));

        var result = await service.AddAsync(relation);

        if (result.IsSuccess == true)
        {
            await base.UpdateAsync(entity, cancellationToken);
        }

        return result;
    }

    /// <summary>
    /// بررسی تکراری بودن مقادیر فیلدها
    /// </summary>
    /// <param name="entity">دامین</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<FluentResults.Result> IsOkForAddAsync(ShopRequestViewModel entity,
        CancellationToken cancellationToken = default)
    {
        var result = new FluentResults.Result();

        var nameFaIsExist = await DbSet
            .Where(current => current.IsDeleted == false)
            .Where(current => current.Id != entity.Id)
            .Where(current => current.Name.Trim().ToLower()
                .Equals(entity.Name.Trim().ToLower()))
            .AnyAsync(cancellationToken);
        
        if (nameFaIsExist == true)
        {
            var errorMessage =
                string.Format(Resources.Messages.RepeatError, Resources.DataDictionary.NameFA);

            result.WithError(errorMessage);
        }
        
        return result;
    }
}