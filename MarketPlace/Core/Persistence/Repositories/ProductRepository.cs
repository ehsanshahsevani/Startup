using BaseProject.Model.ViewModel.Public;
using Domain;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;
using RequestFeatures;
using Utilities;
using ViewModels.Marketplace;
using ViewModels.ModelParameters;

namespace Persistence.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
	internal ProductRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}

    /// <summary>
    /// رکورد یک ردیف با دسته بندی و شعبه های آن
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Product?> FindWithJoinAsync(object id, CancellationToken cancellationToken = default)
    {
        var result = await DbSet
            
            .Include(current => current.ProductBranches)
            .ThenInclude(current => current.Branch)
            
            .Include(current => current.Category)
            .Where(current => current.IsDeleted == false)
            .Where(current => current.Id == id.ToString())
            .FirstOrDefaultAsync(cancellationToken);


        if (result is not null)
        {
            result.ProductBranches = result.ProductBranches
                .Where(x => x.IsDeleted == false)
                .ToList();

        }
        
        return result;
    }

    /// <summary>
    /// زمان ثبت و ویرایش استفاده شود
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<Product?> FindAsync(object id, CancellationToken cancellationToken = default)
    {
        var result = await DbSet
            
            .Where(current => current.IsDeleted == false)
            .Where(current => current.Id == id.ToString())
            
            .FirstOrDefaultAsync(cancellationToken);
        
        return result;
    }
    
	public override Task<IEnumerable<Product?>> GetAllAsync(CancellationToken cancellationToken = default)
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
    public async Task<PagedList<Product>> GetAllInPageAsync(
        ProductParameters parameters, CancellationToken cancellationToken = default)
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
            .Include(current => current.Category)
            
            .Include(current => current.ProductBranches)
            .ThenInclude(current => current.Branch)
            
            .Where(current => current.IsDeleted == false)
            // .Where(current => current.ProductBranches.Any(x => x.IsDeleted == false) == true)
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
            <Product>.ToPagedList(source, parameters, cancellationToken);

        result.ForEach(current =>
        {
            current.ProductBranches = current.ProductBranches
                .Where(x => x.IsDeleted == false)
                .ToList();
        });
        
        return result;
    }

    /// <summary>
    /// Validates that the given Product name is unique within its entire tree branch.
    /// </summary>
    /// <param name="entity">The Product to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result indicating whether the Product can be added</returns>
    public async Task<Result> IsOkForAddAsync(ProductRequestViewModel entity,
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
    /// Checks whether the Product name exists anywhere within the same tree (parent to children).
    /// </summary>
    private async Task<bool> IsNameExist(ProductRequestViewModel entity,
        CancellationToken cancellationToken = default)
    {
        var isExist = await DbSet
            .Where(current => current.IsDeleted == false)
            .Where(current => current.Id != entity.Id)
            .AnyAsync(x => x.Name == entity.Name, cancellationToken);

        return isExist;
    }
    
}