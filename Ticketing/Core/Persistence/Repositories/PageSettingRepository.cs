using Domain;
using Persistence.Abstracts;
using RequestFeatures;
using Utilities;
using ViewModels.ModelParameters;

namespace Persistence.Repositories;

public class PageSettingRepository : Repository<PageSetting>, IPageSettingRepository
{
    internal PageSettingRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }
	
    /// <summary>
	/// دریافت دسته بندی ها به صورت صفحه بندی با قابلیت سرچ پیشرفته
	/// </summary>
	/// <param name="parameters"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public async Task<PagedList<PageSetting>> GetAllInPageAsync(
		PageSettingParameters parameters, CancellationToken cancellationToken = default)
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
					string.IsNullOrEmpty(current.FileOriginalNameMobile) == false
					&& current.FileOriginalNameMobile.Contains(parameters.Text)
				)
				||
				(
					string.IsNullOrEmpty(current.FileOriginalNameWeb) == false
					&& current.FileOriginalNameWeb.Contains(parameters.Text)
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
			<PageSetting>.ToPagedList(source, parameters, cancellationToken);

		return result;
	}
}