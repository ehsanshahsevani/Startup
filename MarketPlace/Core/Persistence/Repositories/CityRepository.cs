using BaseProject.Model.ViewModel.Public;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class CityRepository : Repository<City>, ICityRepository
{
	internal CityRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}

	/// <summary>
	///     نمایش DropDown های جدول
	/// </summary>
	/// <param name="provinceId">شناسه استان</param>
	/// <param name="cancellationToken"></param>
	/// <returns>  مدلی از نام فارسی | انگلیسی و شناسه جدول  </returns>
	public async Task<List<UiSelectModel>> GetSelectValuesAsync(string provinceId, CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			.Where(p => p.IsDeleted == false)
			.Where(current => current.ProvinceId == provinceId)
			.Select(p => new UiSelectModel(p.Name, p.Id))
			.ToListAsync(cancellationToken);

		return result;
	}
}