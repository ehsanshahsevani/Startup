using BaseProject.Model.ViewModel.Public;
using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface IProvinceRepository : IRepository<Province>
{
	/// <summary>
	///     نمایش DropDown های جدول
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>  مدلی از نام فارسی | انگلیسی و شناسه جدول  </returns>
	Task<List<UiSelectModel>> GetSelectValuesAsync(CancellationToken cancellationToken = default);
}