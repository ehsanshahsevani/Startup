using Domain;
using PersistenceSeedworks;
using RequestFeatures;
using ViewModels.ModelParameters;

namespace Persistence.Abstracts;

public interface IPageSettingRepository : IRepository<PageSetting>
{
	/// <summary>
	/// دریافت دسته بندی ها به صورت صفحه بندی با قابلیت سرچ پیشرفته
	/// </summary>
	/// <param name="parameters"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<PagedList<PageSetting>> GetAllInPageAsync(
		PageSettingParameters parameters, CancellationToken cancellationToken = default);
}