using Domain;
using Microsoft.EntityFrameworkCore;
using Utilities;
using RequestFeatures;
using Persistence.Abstracts;
using ViewModels.ModelParameters;

namespace Persistence.Repositories;

public class PageSettingRepository : Repository<PageSetting>, IPageSettingRepository
{
    internal PageSettingRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public override async Task<PageSetting?> FindAsync(object id, CancellationToken cancellationToken = default)
    {
	    var result = await DbSet
		    .Include(current => current.PageSettingTagPageSettings)
		    .ThenInclude(current => current.TagPageSetting)
			    
		    .Where(current => current.IsDeleted == false)
		    .Where(current => current.IsActive == true)

		    .Where(current => current.Id == id.ToString())

		    .FirstOrDefaultAsync(cancellationToken);
    
	    return result;
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
			var dateString = $"1403/{monthNumberShamsi.Value.ToString().PadLeft(totalWidth: 2, paddingChar: '0')}/01";

			monthNumberMiladi = dateString.StringToDateTimeMiladi()!.Value.Month;
		}

		var source = DbSet
				
			.Include(current => current.PageSettingTagPageSettings)
			.ThenInclude(current => current.TagPageSetting)
				
			.Where(current => current.IsDeleted == false)
			
			.Where(current =>
				string.IsNullOrEmpty(parameters.Text) == true
				||
				current.PageSettingTagPageSettings
					.Any(x => x.TagPageSetting != null && x.TagPageSetting.NameEn.ToLower() == parameters.Text.ToLower())
				||
				current.PageSettingTagPageSettings
					.Any(x => x.TagPageSetting != null && x.TagPageSetting.NameFa.ToLower() == parameters.Text.ToLower())
				||
				current.Id.Contains(parameters.Text) == true
				||
				(
					string.IsNullOrEmpty(current.Name) == false
					&& current.Name.Contains(parameters.Text)
				)
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