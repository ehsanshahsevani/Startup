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

public class CartItemRepository : Repository<CartItem>, ICartItemRepository
{
    internal CartItemRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }


    public Task<List<UiSelectModel>> GetSelectValues(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     دریافت دسته بندی ها به صورت صفحه بندی با قابلیت سرچ پیشرفته
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PagedList<CartItem>> GetAllInPageAsync(
        CartItemParameters parameters, CancellationToken cancellationToken = default)
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
                string.IsNullOrEmpty(parameters.Text)
                ||
                current.Id.Contains(parameters.Text)
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
            <CartItem>.ToPagedList(source, parameters, cancellationToken);

        return result;
    }

    public async Task<Result> IsOkForAddAsync(CartItemRequestViewModel entity,
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

    public async Task<IEnumerable<CartItem>?> FindByProductIdAsync(string productId,
        CancellationToken cancellationToken = default)
    {
        var result =await DbSet
            .Where(current => current.IsDeleted == false)
            .Where(current => current.ProductId == productId)
            .ToListAsync(cancellationToken);
        
        return result; 
    }

    /// <summary>
    ///     Checks whether the Branch name exists anywhere within the same tree (parent to children).
    /// </summary>
    private async Task<bool> IsNameExist(CartItemRequestViewModel entity,
        CancellationToken cancellationToken = default)
    {
        var isExist = await DbSet
            .AnyAsync(x => x.ProductId == entity.ProductId, cancellationToken);

        return isExist;
    }
}