using AutoMapper;
using BaseProject.Model.ViewModel.Public;
using Constants;
using Domain;
using Domain.Base;
using Enums.Marketplace;
using FluentResults;
using HttpServices.AttachmentManager;
using Infrastructure;
using Infrastructure.Filters.FilterActions;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using PersistenceSeedworks.LogManager;
using RequestFeatures;
using Resources;
using Utilities;
using ViewModels.Marketplace;
using ViewModels.ModelParameters;
using ViewModels.Shared;

namespace RestFullApi.Controllers;

/// <summary>
/// مدیریت شعبه
/// </summary>
public class BranchController : BaseControllerApi
{
	#region DI Settings & Constructor

	public BranchController(
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
	/// دریافت لیست همه به صورت صفحه بندی شده
	///     مدل خروجی با دو کلید با نام های
	///     - data, metaData
	///     ارسال میشوند
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	public async Task<IActionResult> GetAsync([FromQuery] BranchParameters parameters)
	{
		var result = new Result<PagedListResult<BranchResponseViewModel>>();

		var entities =
			await UnitOfWork.BranchRepository.GetAllInPageAsync(parameters);

		var values =
			Mapper.Map<PagedList<BranchResponseViewModel>>(entities);

		await FetchAndSetAttachmentAsync
			<BranchResponseViewModel, BranchRequestViewModel>(nameof(Branch), values);

		result.WithValue(new PagedListResult<BranchResponseViewModel>(values, entities.MetaData));

		return FluentResult(result);
	}

	#endregion GET : /

	#region GET : {id}

	/// <summary>
	///     دریافت با استفاده از شناسه آن
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpGet("{id}")]
	[ServiceFilter<CheckBranchIdActionFilter>]
	public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
	{
		var result = new Result<BranchResponseViewModel>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as Branch;

		var value =
			Mapper.Map<BranchResponseViewModel>(entity);

		await FetchAndSetAttachmentAsync
			<BranchResponseViewModel, BranchRequestViewModel>(nameof(Branch), [value]);

		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion /GET : by-id

	#region GET : /dropdown-data

	/// <summary>
	///     دریافت لیست نام و آیدی ها برای استفاده در بخش های مختلف دیزاین
	///     - dropdown, ect
	/// </summary>
	/// <returns></returns>
	[HttpGet("dropdown-data")]
	public async Task<IActionResult> GetDropDownDataAsync()
	{
		var result = new Result<List<UiSelectModel>>();

		var value =
			await UnitOfWork.BranchRepository.GetSelectValues();

		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion /GET : /dropdown-data

	#region GET : /get-time-in-day

	/// <summary>
	/// دریافت ساعات کاری مجاز برای هر شعبه
	/// </summary>
	/// <returns></returns>
	[HttpGet("get-time-in-day")]
	public IActionResult GetTimeInDayAsync()
	{
		var result = new Result<List<UiSelectModel>>();

		var value = new List<UiSelectModel>();

		for (int i = 6; i < 24; i++)
		{
			value.Add(new UiSelectModel(i.ToString(), i.ToString()));
		}
		
		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion /GET : /get-time-in-day
	
	#region GET : /get-time-in-day

	/// <summary>
	/// دریافت روزهای کاری در هفته برای هر شعبه
	/// </summary>
	/// <returns></returns>
	[HttpGet("get-day-in-week")]
	public IActionResult GetDayInWeekAsync()
	{
		var result = new Result<List<UiSelectModel>>();

		var value = new List<UiSelectModel>();

		for (int i = 1; i < 8; i++)
		{
			value.Add(new UiSelectModel(i.ChangeNumberWeekToNameWeekShamsi(), i.ToString()));
		}
		
		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion /GET : /get-time-in-day

	#region POST : /

	/// <summary>
	///     ایجاد
	/// </summary>
	/// <param name="model">مدل با ویژگی های لازم</param>
	/// <returns></returns>
	[HttpPost]
	[ServiceFilter<BranchViewModelFilterAction>]
	public async Task<IActionResult> CreateAsync([FromForm] BranchRequestViewModel model)
	{
		var result =
			new Result<BranchResponseViewModel>();

		var entity = Mapper.Map<Branch>(model);

		await UnitOfWork.BranchRepository.AddAsync(entity);

		if (model.FileUpload != null)
		{
			var resultUploadAttachment = await UploadAndCreateAttachmentByFormFileAsync
				(model.FileUpload, AttachmentSubjectEnum.BranchProfile, nameof(Branch), entity.Id);

			result.WithErrors(resultUploadAttachment.Errors);

			if (resultUploadAttachment.IsSuccess)
			{
				await UnitOfWork.AttachmentRepository
					.AddAsync(resultUploadAttachment.Value);
			}
		}

		if (result.IsSuccess)
		{
			await UnitOfWork.SaveAsync();

			var successMessage = string.Format(
				Messages.CreateSuccessMessage,
				DataDictionary.Branch);

			result.WithSuccess(successMessage);

			var entityId = entity.Id;
			
			entity = await UnitOfWork.BranchRepository.FindAsync(entityId);
			
			var value =
				Mapper.Map<BranchResponseViewModel>(entity);

			await FetchAndSetAttachmentAsync
				<BranchResponseViewModel, BranchRequestViewModel>(nameof(Branch), [value]);

			result.WithValue(value);
		}

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
	[ServiceFilter<BranchViewModelFilterAction>]
	public async Task<IActionResult> UpdateAsync([FromForm] BranchRequestViewModel model)
	{
		var result = new Result<BranchResponseViewModel>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as Branch;

		if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

		Mapper.Map(model, entity);

		await UnitOfWork.SaveAsync();

		var value =
			Mapper.Map<BranchResponseViewModel>(entity);

		result.WithValue(value);

		var successMessage = string.Format(
			Messages.UpdateMessageSuccess, DataDictionary.Branch);

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
	[ServiceFilter<CheckBranchIdActionFilter>]
	public async Task<IActionResult> ChangeActivationAsync([FromRoute] string id)
	{
		var result = new Result<BranchResponseViewModel>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as Branch;

		if (entity == null) throw new ArgumentNullException(nameof(entity));

		entity.IsActive = !entity.IsActive;
		entity.UpdateDateTime = DateTime.Now;

		var value = Mapper.Map<BranchResponseViewModel>(entity);

		await UnitOfWork.SaveAsync();

		var successMessage = string.Format(
			Messages.UpdateMessageSuccess, DataDictionary.Branch);

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
	[ServiceFilter<CheckBranchIdActionFilter>]
	public async Task<IActionResult> DeleteAsync([FromRoute] string id)
	{
		var result = new Result<string>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as Branch;

		if (entity == null){
            throw new ArgumentNullException(nameof(entity));
        }
		
			await UnitOfWork.BranchRepository.RemoveAsync(entity);var deleteImageResult = await RemoveAttachmentAsync(nameof(Branch), id);
        if (deleteImageResult.IsSuccess == true)
        {
			await UnitOfWork.SaveAsync();

			var successMessage =
				string.Format(Messages.DeleteMessageSuccess, DataDictionary.Branch);

			result.WithSuccess(successMessage);

			result.WithValue(entity.Id);
		}

		result.WithErrors(deleteImageResult.Errors);return FluentResult(result);
	}

	#endregion /DELETE : /

	#region PUT : update-profile/{id}

	/// <summary>
	///     تغییر تصویر
	/// </summary>
	/// <param name="id">شناسه</param>
	/// <param name="file">فایل جدید</param>
	/// <returns>مدل حاوی تصویر جدید</returns>
	[HttpPut("update-profile/{id}")]
	[ServiceFilter<CheckBranchIdActionFilter>]
	public async Task<IActionResult> UpdateProfileAsync(string id, IFormFile? file)
	{
		var result = new Result<BranchResponseViewModel>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as Branch;

		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		if (file is not null)
		{
			var imageResult = await UploadAndCreateAttachmentByFormFileAsync
				(file, AttachmentSubjectEnum.BranchProfile, nameof(Branch), entity.Id);

			if (imageResult.IsSuccess)
			{
				await UnitOfWork.AttachmentRepository.AddAsync(imageResult.Value);
			}
			else
			{
				result.WithErrors(imageResult.Errors);
			}
		}
		else
		{
			var errorMessage = string.Format
				(Messages.RequiredError, DataDictionary.Image);

			result.WithError(errorMessage);

			return FluentResult(result);
		}

		var attachments =
			await UnitOfWork
				.AttachmentRepository
				.FindAllAttachmentsByIdsAndSubSystemName([id], nameof(Branch));

		if (attachments.Any() == false)
		{
			throw new NullReferenceException(nameof(attachments));
		}

		if (result.IsSuccess)
		{
			await UnitOfWork
				.AttachmentRepository.RemoveRangeAsync(attachments);

			await UnitOfWork.SaveAsync();

			foreach (var profile in attachments)
			{
				var service =
					new AttachmentService();

				var resultSaveFile = await service.DeleteAsync
					(profile.FileName!, ServerKeyConstant.Key);

				if (resultSaveFile.IsFailed)
				{
					result.WithErrors(resultSaveFile.Errors);
				}
			}

			var successMessage =
				string.Format(Messages.UpdateMessageSuccess, DataDictionary.Product);

			result.WithSuccess(successMessage);

			var value =
				Mapper.Map<BranchResponseViewModel>(entity);

			await FetchAndSetAttachmentAsync
				<BranchResponseViewModel, BranchRequestViewModel>(nameof(Branch), [value]);

			result.WithValue(value);
		}

		return FluentResult(result);
	}

	#endregion /PUT : /update-profile/{id}
}