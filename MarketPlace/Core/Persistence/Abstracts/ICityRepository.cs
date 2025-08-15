using BaseProject.Model.ViewModel.Public;
using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface ICityRepository : IRepository<City>
{
	/// <summary>
	///     نمایش DropDown های جدول
	/// </summary>
	/// <param name="provinceId">شناسه استان</param>
	/// <param name="cancellationToken"></param>
	/// <returns>  مدلی از نام فارسی | انگلیسی و شناسه جدول  </returns>
	Task<List<UiSelectModel>> GetSelectValuesAsync(string provinceId, CancellationToken cancellationToken = default);
}