using Domain;
using Constants;
using AutoMapper;
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
/// مدیریت فایل ها
/// </summary>
public class AttachmentController : BaseControllerApi
{
	#region DI Settings & Constructor

	public AttachmentController(
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
	/// دریافت فایل های مورد نیاز برای یک رکورد از یک زیر سیستم خاص
	/// </summary>
	/// <param name="subSystemName">نام زیر سیستم</param>
	/// <param name="relationId">شناسه مربوط به فایل</param>
	/// <returns></returns>
	[HttpGet]
	public async Task<IActionResult> GetAsync([FromQuery] string subSystemName, [FromQuery] string relationId)
	{
		var result = new Result<List<AttachmentResponseViewModel>>();

		if (string.IsNullOrEmpty(subSystemName) == true)
		{
			var errorMessage = string.Format
				(Resources.Messages.RequiredError, Resources.DataDictionary.SubSystem);

			result.WithError(errorMessage);
		}

		if (string.IsNullOrEmpty(relationId) == true)
		{
			var errorMessage = string.Format
				(Resources.Messages.RequiredError, Resources.DataDictionary.Guid);

			result.WithError(errorMessage);
		}

		if (result.IsFailed == true)
		{
			return FluentResult(result);
		}

		var subSystem = await UnitOfWork
			.SubSystemLocalRepository.FindByNameAsync(subSystemName);

		if (subSystem is null)
		{
			var errorMessage = string.Format
				(Resources.Messages.RequestNotValid);

			result.WithError(errorMessage);

			return FluentResult(result);
		}

		List<Attachment> entities =
			await UnitOfWork
				.AttachmentRepository
					.FindBySubSystemIdAndRelationIdAsync(subSystem.Id, relationId);

		var attachment =
			Mapper.Map<PagedList<AttachmentResponseViewModel>>(entities);

		result.WithValue(attachment);

		return FluentResult(result);
	}

	#endregion GET : /
	
	#region POST : /

	/// <summary>
	/// ایجاد
	/// </summary>
	/// <param name="model">مدل نهایی</param>
	/// <returns></returns>
	[HttpPost]
	[ServiceFilter<AttachmentViewModelFilterAction>]
	public async Task<IActionResult> CreateAsync([FromForm] AttachmentRequestViewModel model)
	{
		var result = new Result<AttachmentResponseViewModel>();

		var entity = Mapper.Map<Attachment>(model);

		var subSystem = await UnitOfWork
			.SubSystemLocalRepository.FindAsync(entity.SubSystemLocalId);

		if (subSystem is null)
		{
			throw new NullReferenceException(nameof(subSystem));
		}
		
		if (model.FileUpload is not null)
		{
			var service =
				new HttpServices.AttachmentManager.AttachmentService();

			var resultSaveFile = await service.UploadAsync
				(model.FileUpload, Domain.Base.ServerKeyConstant.Key, subSystem.NameEN);

			if (resultSaveFile.IsFailed == true)
			{
				result.WithErrors(resultSaveFile.Errors);
			}

			if (resultSaveFile.IsSuccess == true)
			{
				var uploadAttachment = resultSaveFile.Value;

				if (uploadAttachment is not null)
				{
					entity.FileName = uploadAttachment.FileName;
					entity.FileOriginalName = uploadAttachment.FileOriginalName;
					entity.FileThumbnailName = uploadAttachment.FileThumbnailName;
				}
			}
		}

		if (result.IsSuccess != true)
		{
			return FluentResult(result);
		}
		
		await UnitOfWork.AttachmentRepository.AddAsync(entity);
		await UnitOfWork.SaveAsync();

		var successMessage = string.Format(
			Resources.Messages.CreateSuccessMessage,
			Resources.DataDictionary.Attachment);

		var attachment =
			Mapper.Map<AttachmentResponseViewModel>(entity);

		result.WithValue(attachment);

		result.WithSuccess(successMessage);

		return FluentResult(result);
	}

	#endregion POST : /
	
	#region DELETE : /
	/// <summary>
	/// حذف با شناسه آن
	/// </summary>
	/// <param name="id">شناسه</param>
	/// <returns>در صورت حذف آیدی به شما برگردانده میشود</returns>
	[HttpDelete(template: "{id}")]
	[ServiceFilter<CheckAttachmentIdActionFilter>]
	public async Task<IActionResult> DeleteAsync([FromRoute] string id)
	{
		var result = new Result<string>();

		Attachment? entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as Attachment;

		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}
		
		if (result.IsSuccess == true)
		{
			await UnitOfWork.AttachmentRepository.RemoveAsync(entity);
			await UnitOfWork.SaveAsync();

			if (entity.FileIsExist() == true)
			{
				var service =
					new HttpServices.AttachmentManager.AttachmentService();

				await service.DeleteAsync
					(entity.FileName!, Domain.Base.ServerKeyConstant.Key);
			}
			
			var successMessage =
				string.Format(Resources.Messages.DeleteMessageSuccess, Resources.DataDictionary.File);

			result.WithSuccess(successMessage);

			result.WithValue(entity.Id);
		}

		return FluentResult(result);
	}

	#endregion /DELETE : /
}