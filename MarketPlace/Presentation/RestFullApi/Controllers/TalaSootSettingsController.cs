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
public class TalaSootSettingsController : BaseControllerApi
{
	#region DI Settings & Constructor

	public TalaSootSettingsController(
		IMapper mapper,
		HttpClient httpClient, IConfiguration configuration,
		IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork,
		ILogDetailManager logDetailManager, ILogServerManager logServerManager)
		: base(mapper, httpClient, configuration, httpContextAccessor, unitOfWork, logDetailManager, logServerManager)
	{
	}

	#endregion
	
	#region GET : {id}

	/// <summary>
	/// دریافت تنظیمات
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	public async Task<IActionResult> GetByIdAsync()
	{
		var result = new Result<TalaSootSettingsResponseViewModel>();

		var entity = await UnitOfWork
			.TalaSootSettingsRepository.FindFirstRecordAsync();

		var value =
			Mapper.Map<TalaSootSettingsResponseViewModel>(entity);

		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion /GET : by-id

	#region PUT : /

	/// <summary>
	/// ویرایش
	/// </summary>
	/// <param name="model">روی مدل درخواستی تغییرات خود را اعمال کنید</param>
	/// <returns>مدل نهایی و جدید</returns>
	[HttpPut]
	[ServiceFilter<TalaSootSettingsViewModelFilterAction>]
	public async Task<IActionResult> UpdateAsync([FromForm] TalaSootSettingsRequestViewModel model)
	{
		var result = new Result<TalaSootSettingsResponseViewModel>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as TalaSootSettings;

		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		Mapper.Map(model, entity);

		await UnitOfWork.SaveAsync();

		entity = await UnitOfWork
			.TalaSootSettingsRepository.FindAsync(entity.Id);
		
		var value =
			Mapper.Map<TalaSootSettingsResponseViewModel>(entity);

		result.WithValue(value);

		var successMessage = string.Format(
			Messages.UpdateMessageSuccess, DataDictionary.TalaSootSettings);

		result.WithSuccess(successMessage);

		return FluentResult(result);
	}

	#endregion /PUT : /
}