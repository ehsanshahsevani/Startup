using AutoMapper;
using BaseProject.Model.ViewModel.Public;
using Constants;
using Domain;
using FluentResults;
using Infrastructure;
using Infrastructure.Filters.FilterActions;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using PersistenceSeedworks.LogManager;
using RequestFeatures;
using Resources;
using ViewModels.Marketplace;
using ViewModels.ModelParameters;
using ViewModels.Shared;

namespace RestFullApi.Controllers;

/// <summary>
///     مدیریت قوانین خرید طلا
/// </summary>
public class RoleGoldController : BaseControllerApi
{
	#region DI Settings & Constructor

	public RoleGoldController(
		IMapper mapper,
		HttpClient httpClient, IConfiguration configuration,
		IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork,
		ILogDetailManager logDetailManager, ILogServerManager logServerManager)
		: base(mapper, httpClient, configuration, httpContextAccessor, unitOfWork, logDetailManager, logServerManager)
	{
	}

	#endregion

	#region GET : /

	/// <summary>
	///     دریافت لیست همه به صورت صفحه بندی شده
	///     مدل خروجی با دو کلید با نام های
	///     - data, metaData
	/// ارسال میشوند
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	public async Task<IActionResult> GetAsync([FromQuery] RoleGoldParameters parameters)
	{
		var result = new Result<PagedListResult<RoleGoldResponseViewModel>>();

		var entities =
			await UnitOfWork.RoleGoldRepository.GetAllInPageAsync(parameters);

		var values =
			Mapper.Map<PagedList<RoleGoldResponseViewModel>>(entities);

		result.WithValue(new PagedListResult<RoleGoldResponseViewModel>(values, entities.MetaData));

		return FluentResult(result);
	}

	#endregion GET : /

	#region GET : {id}

	/// <summary>
	///  دریافت با استفاده از شناسه آن
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpGet("{id}")]
	[ServiceFilter<CheckRoleGoldIdActionFilter>]
	public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
	{
		var result = new Result<RoleGoldResponseViewModel>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as RoleGold;

		var value =
			Mapper.Map<RoleGoldResponseViewModel>(entity);

		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion /GET : by-id

	#region GET : /time-data

	/// <summary>
	/// لیست ساعت های مجاز برای ثبت قانون
	///     - dropdown, ect
	/// </summary>
	/// <returns></returns>
	[HttpGet("time-data")]
	public IActionResult GetDropDownDataAsync()
	{
		var result = new Result<List<UiSelectModel>>();

		var value = new List<UiSelectModel>();
		
		for(int i = 0; i <= 23; i++)
		{
			string hour = i.ToString("00");
			
			value.Add(new UiSelectModel($"{hour}:00",$"{hour}:00"));
			value.Add(new UiSelectModel($"{hour}:30",$"{hour}:30"));
		}

		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion /GET : /time-data
	
	#region POST : /

	/// <summary>
	///     ایجاد
	/// </summary>
	/// <param name="model">مدل با ویژگی های لازم</param>
	/// <returns></returns>
	[HttpPost]
	[ServiceFilter<RoleGoldViewModelFilterAction>]
	public async Task<IActionResult> CreateAsync([FromForm] RoleGoldRequestViewModel model)
	{
		var result =
			new Result<RoleGoldResponseViewModel>();

		var entity = Mapper.Map<RoleGold>(model);

		await UnitOfWork.RoleGoldRepository.AddAsync(entity);

		await UnitOfWork.SaveAsync();

		var successMessage = string.Format(
			Messages.CreateSuccessMessage,
			DataDictionary.RoleGold);

		result.WithSuccess(successMessage);

		var entityId = entity.Id;

		entity = await UnitOfWork.RoleGoldRepository.FindAsync(entityId);

		var value =
			Mapper.Map<RoleGoldResponseViewModel>(entity);

		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion POST : /

	#region PUT : /

	/// <summary>
	///     ویرایش
	/// </summary>
	/// <param name="model">روی مدل درخواستی تغییرات خود را اعمال کنید</param>
	/// <returns>مدل نهایی و جدید</returns>
	[HttpPut]
	[ServiceFilter<RoleGoldViewModelFilterAction>]
	public async Task<IActionResult> UpdateAsync([FromForm] RoleGoldRequestViewModel model)
	{
		var result = new Result<RoleGoldResponseViewModel>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as RoleGold;

		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		Mapper.Map(model, entity);

		await UnitOfWork.SaveAsync();

		entity = await UnitOfWork
			.RoleGoldRepository.FindAsync(entity.Id);
		
		var value =
			Mapper.Map<RoleGoldResponseViewModel>(entity);

		result.WithValue(value);

		var successMessage = string.Format(
			Messages.UpdateMessageSuccess, DataDictionary.RoleGold);

		result.WithSuccess(successMessage);

		return FluentResult(result);
	}

	#endregion /PUT : /

	#region PUT : /change-activation/{id}

	/// <summary>
	///     تغییر وضعیت
	/// </summary>
	/// <param name="id">شناسه</param>
	/// <returns>مدل با دیتای جدید</returns>
	/// <exception cref="ArgumentNullException"></exception>
	[HttpPut("change-activation/{id}")]
	[ServiceFilter<CheckRoleGoldIdActionFilter>]
	public async Task<IActionResult> ChangeActivationAsync([FromRoute] string id)
	{
		var result = new Result<RoleGoldResponseViewModel>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as RoleGold;

		if (entity == null) throw new ArgumentNullException(nameof(entity));

		entity.IsActive = !entity.IsActive;
		entity.UpdateDateTime = DateTime.Now;

		var value = Mapper.Map<RoleGoldResponseViewModel>(entity);

		await UnitOfWork.SaveAsync();

		var successMessage = string.Format(
			Messages.UpdateMessageSuccess, DataDictionary.RoleGold);

		result.WithSuccess(successMessage);

		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion /PUT : /change-activation/{id}

	#region DELETE : /

	/// <summary>
	///     حذف با شناسه آن
	/// </summary>
	/// <param name="id">شناسه</param>
	/// <returns>در صورت حذف آیدی به شما برگردانده میشود</returns>
	[HttpDelete("{id}")]
	[ServiceFilter<CheckRoleGoldIdActionFilter>]
	public async Task<IActionResult> DeleteAsync([FromRoute] string id)
	{
		var result = new Result<string>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as RoleGold;

		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		await UnitOfWork.RoleGoldRepository.RemoveAsync(entity);
		await UnitOfWork.SaveAsync();

		var successMessage =
			string.Format(Messages.DeleteMessageSuccess, DataDictionary.RoleGold);

		result.WithSuccess(successMessage);

		result.WithValue(entity.Id);

		return FluentResult(result);
	}

	#endregion /DELETE : /
}