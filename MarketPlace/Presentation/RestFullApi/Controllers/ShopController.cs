using AutoMapper;
using BaseProject.Model.ViewModel.Public;
using Constants;
using Domain;
using Enums.Marketplace;
using Persistence;
using FluentResults;
using Infrastructure;
using Infrastructure.Filters.FilterActions;
using Microsoft.AspNetCore.Mvc;
using PersistenceSeedworks.LogManager;
using RequestFeatures;
using ViewModels.Marketplace;
using ViewModels.ModelParameters;
using ViewModels.Shared;

namespace RestFullApi.Controllers;

/// <summary>
/// مدیریت فروشگاه
/// </summary>
public class ShopController : BaseControllerApi
{
	#region DI Settings & Constructor

	public ShopController(
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
	/// مدل خروجی با دو کلید با نام های
	/// - data, metaData
	/// ارسال میشوند
	///
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	public async Task<IActionResult> GetAsync([FromQuery] ShopParameters parameters)
	{
		var result = new Result<PagedListResult<ShopResponseViewModel>>();

		var entities =
			await UnitOfWork.ShopRepository.GetAllInPageAsync(parameters);

		var value =
			Mapper.Map<PagedList<ShopResponseViewModel>>(entities);

		result.WithValue(new PagedListResult<ShopResponseViewModel>(value, entities.MetaData));

		return FluentResult(result);
	}

	#endregion GET : /

	#region GET : {id}

	/// <summary>
	/// دریافت با استفاده از شناسه آن
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpGet(template: "{id}")]
	[ServiceFilter<CheckShopIdActionFilter>]
	public IActionResult GetByIdAsync([FromRoute] string id)
	{
		var result = new Result<ShopRequestViewModel>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as Shop;

		var value = Mapper.Map<ShopRequestViewModel>(entity);

		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion /GET : by-id

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
			await UnitOfWork.ShopRepository.GetSelectValues();

		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion /GET : /dropdown-data

	#region GET : /tags

	/// <summary>
	/// دریافت تگ ها برای مدیریت مکان و نمایش
	/// </summary>
	/// <returns></returns>
	[HttpGet(template: "tags")]
	public IActionResult GetTagsAsync()
	{
		var result = new Result<Dictionary<string, string>>();

		var list = TagManager.Shop.GetList();

		result.WithValue(list);

		return FluentResult(result);
	}

	#endregion

	#region POST : /

	/// <summary>
	/// ایجاد
	/// </summary>
	/// <param name="model">مدل با ویژگی های لازم</param>
	/// <returns></returns>
	[HttpPost]
	[ServiceFilter<ShopViewModelFilterAction>]
	public async Task<IActionResult> CreateAsync([FromForm] ShopRequestViewModel model)
	{
		var result = new Result<ShopResponseViewModel>();
		var entity = Mapper.Map<Shop>(model);
		
		var attachments = new List<Attachment>();

		var imageResult = await SetShopImages(model, entity.Id);

		if (imageResult.IsSuccess == true)
		{
			attachments.AddRange(imageResult.Value);
		}
		else
		{
			result.WithErrors(imageResult.Errors);
		}

		if (result.IsSuccess == true)
		{
			if (attachments.Count > 0)
			{
				await UnitOfWork.AttachmentRepository.AddRangeAsync(attachments);
			}
			
			await UnitOfWork.ShopRepository.AddAsync(entity);

			await UnitOfWork.SaveAsync();

			var successMessage = string.Format(
				Resources.Messages.CreateSuccessMessage,
				Resources.DataDictionary.Shop);

			var value =
				Mapper.Map<ShopResponseViewModel>(entity);

			result.WithValue(value);

			result.WithSuccess(successMessage);
		}

		return FluentResult(result);
	}

	#endregion POST : /

	#region PUT : /

	/// <summary>
	/// ویرایش
	/// </summary>
	/// <param name="model">روی مدل درخواستی تغییرات خود را اعمال کنید</param>
	/// <returns>مدل نهایی و جدید</returns>
	[HttpPut]
	[ServiceFilter<ShopViewModelFilterAction>]
	public async Task<IActionResult> UpdateAsync([FromForm] ShopRequestViewModel model)
	{
		var result = new Result<ShopResponseViewModel>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as Shop;

		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		Mapper.Map(model, entity);

		var attachments = new List<Attachment>();
		var removeAttachments = new List<Attachment>();

		var deleteImageResult = await RemoveImages(model, entity.Id);
		if (deleteImageResult.IsFailed == true)
		{
			result.WithErrors(deleteImageResult.Errors);
		}
		else
		{
			var attachmentIds = deleteImageResult.Value;

			removeAttachments =
				await UnitOfWork.AttachmentRepository.FindAllAttachmentsByIdsAndSubSystemName(attachmentIds,
					nameof(Shop));
		}

		var imageResult = await SetShopImages(model, entity.Id);

		if (imageResult.IsSuccess == true)
		{
			attachments.AddRange(imageResult.Value);
		}
		else
		{
			result.WithErrors(imageResult.Errors);
		}

		if (result.IsSuccess == true)
		{
			if (removeAttachments.Count > 0)
			{
				await UnitOfWork.AttachmentRepository.RemoveRangeAsync(removeAttachments);
			}

			if (attachments.Count > 0)
			{
				await UnitOfWork.AttachmentRepository.AddRangeAsync(attachments);
			}

			await UnitOfWork.SaveAsync();

			var value =
				Mapper.Map<ShopResponseViewModel>(entity);

			result.WithValue(value);

			var successMessage = string.Format(
				Resources.Messages.UpdateMessageSuccess, Resources.DataDictionary.Shop);

			result.WithSuccess(successMessage);
		}

		return FluentResult(result);
	}

	#endregion /PUT : /

	#region PUT : /set-tags - []

	/// <summary>
	/// ویرایش تگ ها
	/// </summary>
	/// <param name="id">شناسه</param>
	/// <param name="tags">تگ های سیستمی</param>
	/// <returns>مدل ویرایش شده</returns>
	[HttpPut(template: "set-tags/{id}")]
	[ServiceFilter<CheckShopIdActionFilter>]
	public async Task<IActionResult> SetTagsAsync([FromRoute] string id, [FromBody] List<string>? tags)
	{
		var result = new Result<ShopResponseViewModel>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as Shop;

		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		var value = Mapper.Map<ShopResponseViewModel>(entity);

		await UnitOfWork.SaveAsync();

		var successMessage = string.Format(
			Resources.Messages.UpdateMessageSuccess, Resources.DataDictionary.Shop);

		result.WithSuccess(successMessage);

		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion /PUT : /set-tags

	#region PUT : /change-activation/{id}

	/// <summary>
	/// تغییر وضعیت
	/// </summary>
	/// <param name="id">شناسه</param>
	/// <returns>مدل با دیتای جدید</returns>
	/// <exception cref="ArgumentNullException"></exception>
	[HttpPut(template: "change-activation/{id}")]
	[ServiceFilter<CheckShopIdActionFilter>]
	public async Task<IActionResult> ChangeActivationAsync([FromRoute] string id)
	{
		var result = new Result<ShopResponseViewModel>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as Shop;

		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		entity.IsActive = !entity.IsActive;
		entity.UpdateDateTime = DateTime.Now;

		var value = Mapper.Map<ShopResponseViewModel>(entity);

		await UnitOfWork.SaveAsync();

		var successMessage = string.Format(
			Resources.Messages.UpdateMessageSuccess, Resources.DataDictionary.Shop);

		result.WithSuccess(successMessage);

		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion /PUT : /change-activation/{id}

	#region PUT : /change-confirmation/{id}

	/// <summary>
	/// تغییر وضعیت تایید
	/// </summary>
	/// <param name="model"></param>
	/// <returns>مدل با دیتای جدید</returns>
	/// <exception cref="ArgumentNullException"></exception>
	[HttpPut(template: "change-confirmation")]
	[ServiceFilter<CheckShopConfirmModelActionFilter>]
	public async Task<IActionResult> ChangeConfirmationAsync([FromBody] ShopConfirmationViewModel model)
	{
		var result = new Result<ShopResponseViewModel>();

		var entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as Shop;

		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		var userId = GetCurrentUserId();

		entity.IsConfirmed = !entity.IsConfirmed;

		if (!entity.IsConfirmed)
		{
			entity.ConfirmReason = model.ConfirmReason;
			entity.ConfirmedByProfileId = userId;
		}

		entity.UpdateDateTime = DateTime.Now;

		var value = Mapper.Map<ShopResponseViewModel>(entity);

		await UnitOfWork.ShopRepository.ConfirmAsync(entity);

		await UnitOfWork.SaveAsync();

		var successMessage = string.Format(
			Resources.Messages.ConfirmMessageSuccess, Resources.DataDictionary.Shop);

		result.WithSuccess(successMessage);

		result.WithValue(value);

		return FluentResult(result);
	}

	#endregion /PUT : /change-confirmation/{id}
	
	#region DELETE : /

	/// <summary>
	/// حذف با شناسه آن
	/// </summary>
	/// <param name="id">شناسه</param>
	/// <returns>در صورت حذف آیدی به شما برگردانده میشود</returns>
	[HttpDelete(template: "{id}")]
	[ServiceFilter<CheckShopIdActionFilter>]
	public async Task<IActionResult> DeleteAsync([FromRoute] string id)
	{
		var result = new Result<string>();

		Shop? entity =
			HttpContext.Items[ProjectKeyName.ObjectKey] as Shop;

		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}
		
		if (result.IsSuccess == true)
		{
			await UnitOfWork.ShopRepository.RemoveAsync(entity);
			await UnitOfWork.SaveAsync();

			var successMessage =
				string.Format(Resources.Messages.DeleteMessageSuccess, Resources.DataDictionary.Shop);

			result.WithSuccess(successMessage);

			result.WithValue(entity.Id);
		}

		return FluentResult(result);
	}

	#endregion /DELETE : /

	// check and meeting
	private async Task<Result<List<string>>> RemoveImages(ShopRequestViewModel model, string shopId)
	{
		var result = new Result<List<string>>();

		var service =
			new HttpServices.AttachmentManager.AttachmentService();

		var attachmentIds = new List<string>();

		var existAttachments = await UnitOfWork
			.AttachmentRepository.GetAllAttachmentsByShopId(shopId, nameof(Shop));

		var serverId = Domain.Base.ServerKeyConstant.Key;

		var nationalCardBack =
			existAttachments.FirstOrDefault
				(p => p.AttachmentSubjectEnum == AttachmentSubjectEnum.NationalCardBack);

		if (string.IsNullOrEmpty(model.NationalCardBack?.FileName ?? "") == false && nationalCardBack is not null)
		{
			var resultDelete = await service.DeleteAsync(model.NationalCardBack!.FileName, serverId);

			if (resultDelete.IsFailed == true)
			{
				result.WithErrors(resultDelete.Errors);
			}
			else
			{
				attachmentIds.Add(nationalCardBack.Id);
			}
		}

		var nationalCardFront =
			existAttachments.FirstOrDefault
				(p => p.AttachmentSubjectEnum == AttachmentSubjectEnum.NationalCardFront);

		if (string.IsNullOrEmpty(model.NationalCardFront?.FileName ?? "") == false && nationalCardFront is not null)
		{
			var resultDelete = await service.DeleteAsync(model.NationalCardFront!.FileName, serverId);

			if (resultDelete.IsFailed == true)
			{
				result.WithErrors(resultDelete.Errors);
			}
			else
			{
				attachmentIds.Add(nationalCardFront.Id);
			}
		}

		var vatCertificateImage =
			existAttachments.FirstOrDefault(p => p.AttachmentSubjectEnum == AttachmentSubjectEnum.VatCertificateImage);

		if (string.IsNullOrEmpty(model.VatCertificateImage?.FileName ?? "") == false && vatCertificateImage is not null)
		{
			var resultDelete = await service.DeleteAsync(model.VatCertificateImage!.FileName, serverId);

			if (resultDelete.IsFailed == true)
			{
				result.WithErrors(resultDelete.Errors);
			}
			else
			{
				attachmentIds.Add(vatCertificateImage.Id);
			}
		}

		var officialGazetteImage =
			existAttachments.FirstOrDefault
				(p => p.AttachmentSubjectEnum == AttachmentSubjectEnum.OfficialGazetteImage);

		if (string.IsNullOrEmpty(model.OfficialGazetteImage?.FileName ?? "") == false &&
		    officialGazetteImage is not null)
		{
			var resultDelete = await service.DeleteAsync(model.OfficialGazetteImage!.FileName, serverId);

			if (resultDelete.IsFailed == true)
			{
				result.WithErrors(resultDelete.Errors);
			}
			else
			{
				attachmentIds.Add(officialGazetteImage.Id);
			}
		}

		result.WithValue(attachmentIds);
		return result;
	}

	private async Task<Result<List<Attachment>>> SetShopImages(ShopRequestViewModel model, string shopId)
	{
		var result = new Result<List<Attachment>>();

		var attachments = new List<Attachment>();

		if (model.NationalCardFront is not null)
		{
			var imageResult =
				await UploadAndCreateAttachmentByFormFileAsync(
					model.NationalCardFront, AttachmentSubjectEnum.NationalCardFront, nameof(Shop), shopId);

			if (imageResult.IsSuccess == true)
			{
				attachments.Add(imageResult.Value);
			}
			else
			{
				result.WithErrors(imageResult.Errors);
			}
		}

		if (model.NationalCardBack is not null)
		{
			var imageResult = await UploadAndCreateAttachmentByFormFileAsync
				(model.NationalCardBack, AttachmentSubjectEnum.NationalCardBack, nameof(Shop), shopId);

			if (imageResult.IsSuccess == true)
			{
				attachments.Add(imageResult.Value);
			}
			else
			{
				result.WithErrors(imageResult.Errors);
			}
		}

		if (model.VatCertificateImage is not null)
		{
			var imageResult =
				await UploadAndCreateAttachmentByFormFileAsync(
					model.VatCertificateImage, AttachmentSubjectEnum.VatCertificateImage, nameof(Shop), shopId);

			if (imageResult.IsSuccess == true)
			{
				attachments.Add(imageResult.Value);
			}
			else
			{
				result.WithErrors(imageResult.Errors);
			}
		}

		if (model.OfficialGazetteImage is not null)
		{
			var imageResult = await UploadAndCreateAttachmentByFormFileAsync
				(model.OfficialGazetteImage, AttachmentSubjectEnum.OfficialGazetteImage, nameof(Shop), shopId);

			if (imageResult.IsSuccess == true)
			{
				attachments.Add(imageResult.Value);
			}
			else
			{
				result.WithErrors(imageResult.Errors);
			}
		}

		result.WithValue(attachments);

		return result;
	}
}