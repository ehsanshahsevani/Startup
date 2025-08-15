using BaseProject.Model.ViewModel.Public;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class OrderStatusRepository : Repository<OrderStatus>, IOrderStatusRepository
{
	internal OrderStatusRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}

	/// <summary>
	/// نمایش DropDown های جدول
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>  مدلی از نام فارسی | انگلیسی و شناسه جدول  </returns>
	public async Task<List<UiSelectModel>> GetSelectValues(CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			.Where(p => p.IsActive == true)
			.Where(p => p.IsDeleted == false)
			.Select(p => new UiSelectModel(p.Name, p.Id))
			.ToListAsync(cancellationToken);

		return result;
	}
}