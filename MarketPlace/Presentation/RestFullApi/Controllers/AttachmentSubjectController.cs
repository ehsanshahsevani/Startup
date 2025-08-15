using Domain;
using Constants;
using AutoMapper;
using BaseProject.Model.ViewModel.Public;
using Persistence;
using FluentResults;
using Infrastructure;
using RequestFeatures;
using ViewModels.Marketplace;
using Microsoft.AspNetCore.Mvc;
using PersistenceSeedworks.LogManager;
using Infrastructure.Filters.FilterActions;

namespace RestFullApi.Controllers;

/// <summary>
/// موضوع فایل
/// </summary>
public class AttachmentSubjectController : BaseControllerApi
{
	#region DI Settings & Constructor

	public AttachmentSubjectController(
		IMapper mapper,
		HttpClient httpClient, IConfiguration configuration,
		IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork,
		ILogDetailManager logDetailManager, ILogServerManager logServerManager)
		: base(mapper, httpClient, configuration, httpContextAccessor, unitOfWork, logDetailManager, logServerManager)
	{
	}

	#endregion

	#region GET : /dropdown-data

	/// <summary>
	/// دریافت لیست نام و آیدی ها برای استفاده در بخش های مختلف دیزاین
	/// - dropdown, ect
	/// </summary>
	/// <returns></returns>
	[HttpGet(template: "dropdown-data")]
	public async Task<IActionResult> GetDropDownDataAsync()
	{
		var result = new Result<List<UiSelectModel>>();

		var value =
			await UnitOfWork.AttachmentSubjectRepository.GetSelectValues();

		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion /GET : /dropdown-data

	#region GET : /
	[HttpGet(template: "configure-attachment-subjects")]
	public async Task<IActionResult> ConfigureAttachmentSubjectsAsync()
	{
		var result = new Result();

		await UnitOfWork
			.AttachmentSubjectRepository.ConfigureCodeDisplayAsync();

		return FluentResult(result);
	}

	#endregion GET : /
}