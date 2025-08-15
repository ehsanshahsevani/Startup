using Domain;
using Constants;
using AutoMapper;
using Persistence;
using FluentResults;
using Infrastructure;
using RequestFeatures;
using ViewModels.Shared;
using ViewModels.Marketplace;
using Microsoft.AspNetCore.Mvc;
using ViewModels.ModelParameters;
using PersistenceSeedworks.LogManager;
using Infrastructure.Filters.FilterActions;

namespace RestFullApi.Controllers;

/// <summary>
/// تنظیمات صفحه
/// </summary>
public class PageSettingController : BaseControllerApi
{
	#region DI Settings & Constructor

	public PageSettingController(
		IMapper mapper,
		HttpClient httpClient, IConfiguration configuration,
		IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork,
		ILogDetailManager logDetailManager, ILogServerManager logServerManager)
		: base(mapper: mapper, httpClient: httpClient, configuration: configuration,
			httpContextAccessor: httpContextAccessor, unitOfWork: unitOfWork, logDetailManager: logDetailManager,
			logServerManager: logServerManager)
	{
	}

	#endregion

	#region GET : /

	/// <summary>
	/// دریافت لیست همه به صورت صفحه بندی شده
	/// مدل خروجی با دو کلید با نام های
	/// - data , metaData
	/// ارسال میشوند
	///
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	public async Task<IActionResult> GetAsync([FromQuery] PageSettingParameters parameters)
	{
		var result = new Result<PagedListResult<PageSettingResponseViewModel>>();

		var entities =
			await UnitOfWork.PageSettingRepository.GetAllInPageAsync(parameters: parameters);

		var value =
			Mapper.Map<PagedList<PageSettingResponseViewModel>>(source: entities);

		result.WithValue(
			value: new PagedListResult<PageSettingResponseViewModel>(Data: value, MetaData: entities.MetaData));

		return FluentResult(result: result);
	}

	#endregion GET : /

	#region GET : {id}

	/// <summary>
	/// دریافت با استفاده از شناسه آن
	/// </summary>
	/// <param name="id">شناسه</param>
	/// <returns></returns>
	[HttpGet(template: "{id}")]
	[ServiceFilter<CheckPageSettingIdActionFilter>]
	public IActionResult GetByIdAsync([FromRoute] string id)
	{
		var result = new Result<PageSettingResponseViewModel>();

		var entity =
			HttpContext.Items[key: ProjectKeyName.ObjectKey] as PageSetting;

		var value = Mapper.Map<PageSettingResponseViewModel>(source: entity);

		result.WithValue(value: value);

		return FluentResult(result: result);
	}

	#endregion /GET : by-id

	#region GET : /tags

	/// <summary>
	/// دریافت تگ ها برای مدیریت مکان و نمایش
	/// </summary>
	/// <returns></returns>
	[HttpGet(template: "tags")]
	public async Task<IActionResult> GetTagsAsync()
	{
		var result = new Result<List<TagPageSettingResponseViewModel>>();

		var list =
			await UnitOfWork.TagPageSettingRepository.GetAllAsync();

		var value = Mapper.Map<List<TagPageSettingResponseViewModel>>(source: list);
		
		result.WithValue(value);

		return FluentResult(result: result);
	}

	#endregion /GET : /tags
	
	#region GET : /tags-by-start-with-name/{name}

	/// <summary>
	/// دریافت تگ ها برای مدیریت مکان و نمایش
	/// </summary>
	/// <returns></returns>
	[HttpGet(template: "tags-by-start-with-name/{name}")]
	public async Task<IActionResult> GetTagsAsync(string name)
	{
		var result = new Result<List<TagPageSettingResponseViewModel>>();

		var list =
			await UnitOfWork.TagPageSettingRepository.FindByStartWithNamesAsync(name);

		var value = Mapper.Map<List<TagPageSettingResponseViewModel>>(source: list);
		
		result.WithValue(value);

		return FluentResult(result: result);
	}

	#endregion /GET : /tags

	#region POST : /

	/// <summary>
	/// ایجاد
	/// </summary>
	/// <param name="model">مدل نهایی</param>
	/// <returns></returns>
	[HttpPost]
	[ServiceFilter<PageSettingViewModelFilterAction>]
	public async Task<IActionResult> CreateAsync([FromForm] PageSettingRequestViewModel model)
	{
		var result = new Result<PageSettingResponseViewModel>();

		var entity = Mapper.Map<PageSetting>(source: model);

		if (model.FileWeb is not null)
		{
			var service =
				new HttpServices.AttachmentManager.AttachmentService();

			var resultSaveFile = await service.UploadAsync
				(file: model.FileWeb, serverId: Domain.Base.ServerKeyConstant.Key, domainName: nameof(PageSetting));

			if (resultSaveFile.IsFailed == true)
			{
				result.WithErrors(errors: resultSaveFile.Errors);
			}

			if (resultSaveFile.IsSuccess == true)
			{
				var uploadValue = resultSaveFile.Value;

				if (uploadValue is not null)
				{
					entity.FileOriginalNameWeb = uploadValue.FileName;
					entity.FileOriginalNameWeb = uploadValue.FileOriginalName;
					entity.FileThumbnailNameWeb = uploadValue.FileThumbnailName;
				}
			}
		}

		if (model.FileMobile is not null)
		{
			var service =
				new HttpServices.AttachmentManager.AttachmentService();

			var resultSaveFile = await service.UploadAsync
				(file: model.FileMobile, serverId: Domain.Base.ServerKeyConstant.Key, domainName: nameof(PageSetting));

			if (resultSaveFile.IsFailed == true)
			{
				result.WithErrors(errors: resultSaveFile.Errors);
			}

			if (resultSaveFile.IsSuccess == true)
			{
				var uploadValue = resultSaveFile.Value;

				if (uploadValue is not null)
				{
					entity.FileOriginalNameMobile = uploadValue.FileName;
					entity.FileOriginalNameMobile = uploadValue.FileOriginalName;
					entity.FileThumbnailNameMobile = uploadValue.FileThumbnailName;
				}
			}
		}

		if (result.IsSuccess == true)
		{
			// entity.ClearAndSetTags(model.Tags.Select(x => x.Key).ToArray());

			await UnitOfWork.PageSettingRepository.AddAsync(entity: entity);
			await UnitOfWork.SaveAsync();

			var successMessage = string.Format(
				format: Resources.Messages.CreateSuccessMessage,
				arg0: Resources.DataDictionary.PageSetting);

			var value =
				Mapper.Map<PageSettingResponseViewModel>(source: entity);

			result.WithValue(value: value);

			result.WithSuccess(successMessage: successMessage);
		}

		return FluentResult(result: result);
	}

	#endregion POST : /

	#region PUT : /

	/// <summary>
	/// ویرایش
	/// </summary>
	/// <param name="model">روی مدل درخواستی تغییرات خود را اعمال کنید</param>
	/// <returns>مدل نهایی و جدید</returns>
	[HttpPut]
	[ServiceFilter<PageSettingViewModelFilterAction>]
	public async Task<IActionResult> UpdateAsync([FromForm] PageSettingRequestViewModel model)
	{
		var result = new Result<PageSettingResponseViewModel>();

		var service =
			new HttpServices.AttachmentManager.AttachmentService();

		var serverId = Domain.Base.ServerKeyConstant.Key;

		var entity =
			HttpContext.Items[key: ProjectKeyName.ObjectKey] as PageSetting;

		if (entity == null)
		{
			throw new ArgumentNullException(paramName: nameof(entity));
		}

		Mapper.Map(source: model, destination: entity);

		string? fileNameWebForDelete = null;
		string? fileNameMobileForDelete = null;

		if (model.FileWeb is not null)
		{
			fileNameWebForDelete = entity.FileNameWeb;

			var resultSaveFile = await service.UploadAsync
				(file: model.FileWeb, serverId: serverId, domainName: nameof(PageSetting));

			if (resultSaveFile.IsFailed == true)
			{
				result.WithErrors(errors: resultSaveFile.Errors);
			}

			if (resultSaveFile.IsSuccess == true)
			{
				var uploadValue = resultSaveFile.Value;

				if (uploadValue is not null)
				{
					entity.FileNameWeb = uploadValue.FileName;
					entity.FileOriginalNameWeb = uploadValue.FileOriginalName;
					entity.FileThumbnailNameWeb = uploadValue.FileThumbnailName;
				}
			}
		}

		if (model.FileMobile is not null)
		{
			fileNameMobileForDelete = entity.FileNameMobile;

			var resultSaveFile = await service.UploadAsync
				(file: model.FileMobile, serverId: serverId, domainName: nameof(PageSetting));

			if (resultSaveFile.IsFailed == true)
			{
				result.WithErrors(errors: resultSaveFile.Errors);
			}

			if (resultSaveFile.IsSuccess == true)
			{
				var uploadValue = resultSaveFile.Value;

				if (uploadValue is not null)
				{
					entity.FileNameMobile = uploadValue.FileName;
					entity.FileOriginalNameMobile = uploadValue.FileOriginalName;
					entity.FileThumbnailNameMobile = uploadValue.FileThumbnailName;
				}
			}
		}

		if (result.IsSuccess == true)
		{
			// entity.ClearAndSetTags(model.Tags.Select(x => x.Key).ToArray());

			await UnitOfWork.SaveAsync();

			if (string.IsNullOrEmpty(value: fileNameWebForDelete) == false)
			{
				var resultDelete =
					await service.DeleteAsync(fileName: fileNameWebForDelete, serverId: serverId);

				if (resultDelete.IsFailed == true)
				{
					// result.WithErrors(resultDelete.Errors);
				}
			}

			if (string.IsNullOrEmpty(value: fileNameMobileForDelete) == false)
			{
				var resultDelete =
					await service.DeleteAsync(fileName: fileNameMobileForDelete, serverId: serverId);

				if (resultDelete.IsFailed == true)
				{
					// result.WithErrors(resultDelete.Errors);
				}
			}

			var value =
				Mapper.Map<PageSettingResponseViewModel>(source: entity);

			result.WithValue(value: value);

			var successMessage = string.Format(
				format: Resources.Messages.UpdateMessageSuccess, arg0: Resources.DataDictionary.PageSetting);

			result.WithSuccess(successMessage: successMessage);
		}

		return FluentResult(result: result);
	}

	#endregion /PUT : /

	#region PUT : /change-activation/{id}

	/// <summary>
	/// تغییر وضعیت
	/// </summary>
	/// <param name="id">شناسه</param>
	/// <returns>مدل با دیتای جدید</returns>
	/// <exception cref="ArgumentNullException"></exception>
	[HttpPut(template: "change-activation/{id}")]
	[ServiceFilter<CheckPageSettingIdActionFilter>]
	public async Task<IActionResult> ChangeActivationAsync([FromRoute] string id)
	{
		var result = new Result<PageSettingResponseViewModel>();

		var entity =
			HttpContext.Items[key: ProjectKeyName.ObjectKey] as PageSetting;

		if (entity == null)
		{
			throw new ArgumentNullException(paramName: nameof(entity));
		}

		entity.IsActive = !entity.IsActive;
		entity.UpdateDateTime = DateTime.Now;

		var value = Mapper.Map<PageSettingResponseViewModel>(source: entity);

		await UnitOfWork.SaveAsync();

		var successMessage = string.Format(
			format: Resources.Messages.UpdateMessageSuccess, arg0: Resources.DataDictionary.PageSetting);

		result.WithSuccess(successMessage: successMessage);

		result.WithValue(value: value);

		return FluentResult(result: result);
	}

	#endregion /PUT : /change-activation/{id}

	#region PUT : /delete-image/{id}

	/// <summary>
	/// حذف تصویر
	/// </summary>
	/// <param name="id">شناسه</param>
	/// <returns>مدل تغییر یافته</returns>
	/// <exception cref="ArgumentNullException"></exception>
	[HttpPut(template: "delete-image/{id}")]
	[ServiceFilter<CheckPageSettingIdActionFilter>]
	public async Task<IActionResult> DeleteImageAsync([FromRoute] string id)
	{
		var result = new Result<PageSettingResponseViewModel>();

		var service =
			new HttpServices.AttachmentManager.AttachmentService();

		var entity =
			HttpContext.Items[key: ProjectKeyName.ObjectKey] as PageSetting;

		if (entity == null)
		{
			throw new ArgumentNullException(paramName: nameof(entity));
		}

		if (entity.FileIsExistWeb() == true)
		{
			var resultSaveFile = await service.DeleteAsync
				(fileName: entity.FileNameWeb!, serverId: Domain.Base.ServerKeyConstant.Key);

			if (resultSaveFile.IsSuccess == true)
			{
				entity.FileNameWeb = null;
				entity.FileOriginalNameWeb = null;
				entity.FileThumbnailNameWeb = null;
			}

			if (resultSaveFile.IsFailed == true)
			{
				result.WithErrors(errors: resultSaveFile.Errors);
			}
		}

		if (entity.FileIsExistMobile() == true)
		{
			var resultSaveFile = await service.DeleteAsync
				(fileName: entity.FileNameMobile!, serverId: Domain.Base.ServerKeyConstant.Key);

			if (resultSaveFile.IsSuccess == true)
			{
				entity.FileNameMobile = null;
				entity.FileOriginalNameMobile = null;
				entity.FileThumbnailNameMobile = null;
			}

			if (resultSaveFile.IsFailed == true)
			{
				result.WithErrors(errors: resultSaveFile.Errors);
			}
		}

		if (entity.FileIsExistIcon() == true)
		{
			var resultSaveFile = await service.DeleteAsync
				(fileName: entity.FileNameIcon!, serverId: Domain.Base.ServerKeyConstant.Key);

			if (resultSaveFile.IsSuccess == true)
			{
				entity.FileNameIcon = null;
				entity.FileOriginalNameIcon = null;
			}

			if (resultSaveFile.IsFailed == true)
			{
				result.WithErrors(errors: resultSaveFile.Errors);
			}
		}

		if (result.IsSuccess == true)
		{
			entity.UpdateDateTime = DateTime.Now;

			var value = Mapper.Map<PageSettingResponseViewModel>(source: entity);

			await UnitOfWork.SaveAsync();

			var successMessage = string.Format(
				format: Resources.Messages.UpdateMessageSuccess, arg0: Resources.DataDictionary.PageSetting);

			result.WithSuccess(successMessage: successMessage);

			result.WithValue(value: value);
		}

		return FluentResult(result: result);
	}

	#endregion /PUT : /delete-image/{id}

	#region DELETE : /

	/// <summary>
	/// حذف با شناسه آن
	/// </summary>
	/// <param name="id">شناسه</param>
	/// <returns>در صورت حذف آیدی به شما برگردانده میشود</returns>
	[HttpDelete(template: "{id}")]
	[ServiceFilter<CheckPageSettingIdActionFilter>]
	public async Task<IActionResult> DeleteAsync([FromRoute] string id)
	{
		var result = new Result<string>();

		var entity =
			HttpContext.Items[key: ProjectKeyName.ObjectKey] as PageSetting;

		if (entity == null)
		{
			throw new ArgumentNullException(paramName: nameof(entity));
		}

		if (entity.FileIsExistWeb() == true)
		{
			var service =
				new HttpServices.AttachmentManager.AttachmentService();

			var resultSaveFile = await service.DeleteAsync
				(fileName: entity.FileNameWeb!, serverId: Domain.Base.ServerKeyConstant.Key);

			if (resultSaveFile.IsFailed == true)
			{
				result.WithErrors(errors: resultSaveFile.Errors);
			}
		}

		if (entity.FileIsExistMobile() == true)
		{
			var service =
				new HttpServices.AttachmentManager.AttachmentService();

			var resultSaveFile = await service.DeleteAsync
				(fileName: entity.FileNameMobile!, serverId: Domain.Base.ServerKeyConstant.Key);

			if (resultSaveFile.IsFailed == true)
			{
				result.WithErrors(errors: resultSaveFile.Errors);
			}
		}

		if (result.IsSuccess == true)
		{
			await UnitOfWork
				.PageSettingTagPageSettingRepository
				.ClearAllTagsForThisPageAndSetNewListAsync(entity.Id, tagIds: new List<string>());

			await UnitOfWork.PageSettingRepository.RemoveAsync(entity: entity);

			await UnitOfWork.SaveAsync();

			var successMessage =
				string.Format(format: Resources.Messages.DeleteMessageSuccess,
					arg0: Resources.DataDictionary.PageSetting);

			result.WithSuccess(successMessage: successMessage);

			result.WithValue(value: entity.Id);
		}

		return FluentResult(result: result);
	}

	#endregion /DELETE : /

	#region FAQ

	#region GET : /faq

	/// <summary>
	/// دریافت لیست سوالات متداول - صفحه بندی شده
	/// - data , metaData
	/// ارسال میشوند
	/// </summary>
	/// <returns></returns>
	[HttpGet("faq")]
	public async Task<IActionResult> GetّFaqsAsync([FromQuery] PageSettingParameters parameters)
	{
		var result = new Result<PagedListResult<FaqResponseViewModel>>();

		parameters.Text = nameof(Resources.DataDictionary.Faq);

		var entities =
			await UnitOfWork.PageSettingRepository.GetAllInPageAsync(parameters: parameters);

		var value =
			Mapper.Map<PagedList<FaqResponseViewModel>>(source: entities);

		result.WithValue(value: new PagedListResult<FaqResponseViewModel>(Data: value, MetaData: entities.MetaData));

		return FluentResult(result: result);
	}

	#endregion GET : /faq

	#region POST : /faq

	/// <summary>
	/// ایجاد سوالات متداول
	/// </summary>
	/// <param name="model">مدل نهایی</param>
	/// <returns></returns>
	[HttpPost("faq")]
	[ServiceFilter<FaqViewModelFilterAction>]
	public async Task<IActionResult> CreateFaqAsync([FromForm] FaqRequestViewModel model)
	{
		var faqTag =
			HttpContext.Items[key: nameof(Resources.DataDictionary.Faq)] as TagPageSetting;

		var result = new Result<FaqResponseViewModel>();

		var entity = Mapper.Map<PageSetting>(source: model);

		if (model.Icon is not null)
		{
			var service =
				new HttpServices.AttachmentManager.AttachmentService();

			var resultSaveFile = await service.UploadAsync
				(file: model.Icon, serverId: Domain.Base.ServerKeyConstant.Key, domainName: nameof(PageSetting));

			if (resultSaveFile.IsFailed == true)
			{
				result.WithErrors(errors: resultSaveFile.Errors);
			}

			if (resultSaveFile.IsSuccess == true)
			{
				var uploadValue = resultSaveFile.Value;

				if (uploadValue is not null)
				{
					entity.FileNameIcon = uploadValue.FileName;
					entity.FileOriginalNameIcon = uploadValue.FileOriginalName;
				}
			}
		}

		if (result.IsSuccess == true)
		{
			await UnitOfWork
				.PageSettingTagPageSettingRepository
				.ClearAllTagsForThisPageAndSetNewListAsync(pageSettingId: entity.Id, tagIds: [faqTag!.Id]);

			await UnitOfWork.PageSettingRepository.AddAsync(entity: entity);
			await UnitOfWork.SaveAsync();

			var successMessage = string.Format(
				format: Resources.Messages.CreateSuccessMessage,
				arg0: Resources.DataDictionary.Faq);

			var value =
				Mapper.Map<FaqResponseViewModel>(source: entity);

			result.WithValue(value: value);

			result.WithSuccess(successMessage: successMessage);
		}

		return FluentResult(result: result);
	}

	#endregion POST : /faq

	#region PUT : /faq

	/// <summary>
	/// ویرایش سوالات متداول
	/// </summary>
	/// <param name="model">روی مدل درخواستی تغییرات خود را اعمال کنید</param>
	/// <returns>مدل نهایی و جدید</returns>
	[HttpPut(template: "faq")]
	[ServiceFilter<FaqViewModelFilterAction>]
	public async Task<IActionResult> UpdateFaqAsync([FromForm] FaqRequestViewModel model)
	{
		var result = new Result<FaqResponseViewModel>();

		var service =
			new HttpServices.AttachmentManager.AttachmentService();

		var serverId = Domain.Base.ServerKeyConstant.Key;

		var entity =
			HttpContext.Items[key: ProjectKeyName.ObjectKey] as PageSetting;

		if (entity == null)
		{
			throw new ArgumentNullException(paramName: nameof(entity));
		}

		Mapper.Map(source: model, destination: entity);

		string? fileNameIconForDelete = null;

		if (model.Icon is not null)
		{
			fileNameIconForDelete = entity.FileNameIcon;

			var resultSaveFile = await service.UploadAsync
				(file: model.Icon, serverId: serverId, domainName: nameof(PageSetting));

			if (resultSaveFile.IsFailed == true)
			{
				result.WithErrors(errors: resultSaveFile.Errors);
			}

			if (resultSaveFile.IsSuccess == true)
			{
				var uploadValue = resultSaveFile.Value;

				if (uploadValue is not null)
				{
					entity.FileNameIcon = uploadValue.FileName;
					entity.FileOriginalNameIcon = uploadValue.FileOriginalName;
				}
			}
		}

		if (result.IsSuccess == true)
		{
			await UnitOfWork.SaveAsync();

			if (string.IsNullOrEmpty(value: fileNameIconForDelete) == false)
			{
				var resultDelete =
					await service.DeleteAsync(fileName: fileNameIconForDelete, serverId: serverId);

				if (resultDelete.IsFailed == true)
				{
				}
			}

			var value =
				Mapper.Map<FaqResponseViewModel>(source: entity);

			result.WithValue(value: value);

			var successMessage = string.Format(
				format: Resources.Messages.UpdateMessageSuccess, arg0: Resources.DataDictionary.Faq);

			result.WithSuccess(successMessage: successMessage);
		}

		return FluentResult(result: result);
	}

	#endregion /PUT : /faq

	#endregion /FAQ

	#region Banner

	#region GET : /banner

	/// <summary>
	/// دریافت لیست بنرها - صفحه بندی شده
	/// - data , metaData
	/// ارسال میشوند
	/// </summary>
	/// <returns></returns>
	[HttpGet("banner")]
	public async Task<IActionResult> GetّBannersAsync([FromQuery] PageSettingParameters parameters)
	{
		var result = new Result<PagedListResult<BannerResponseViewModel>>();

		parameters.Text = nameof(Resources.DataDictionary.Banner);

		var entities =
			await UnitOfWork.PageSettingRepository.GetAllInPageAsync(parameters: parameters);

		var value =
			Mapper.Map<PagedList<BannerResponseViewModel>>(source: entities);

		result.WithValue(value:
			new PagedListResult<BannerResponseViewModel>
				(Data: value, MetaData: entities.MetaData));

		return FluentResult(result: result);
	}

	#endregion GET : /banner

	#region POST : /banner

	/// <summary>
	/// ایجاد بنر
	/// </summary>
	/// <param name="model">مدل نهایی</param>
	/// <returns></returns>
	[HttpPost(template: "banner")]
	[ServiceFilter<BannerViewModelFilterAction>]
	public async Task<IActionResult> CreateBannerAsync([FromForm] BannerRequestViewModel model)
	{
		var listTags =
			HttpContext.Items[key: nameof(Resources.DataDictionary.Banner)] as List<TagPageSetting>;

		var result = new Result<BannerResponseViewModel>();

		var entity = Mapper.Map<PageSetting>(source: model);

		if (model.FileWeb is not null)
		{
			var service =
				new HttpServices.AttachmentManager.AttachmentService();

			var resultSaveFile = await service.UploadAsync
				(file: model.FileWeb, serverId: Domain.Base.ServerKeyConstant.Key, domainName: nameof(PageSetting));

			if (resultSaveFile.IsFailed == true)
			{
				result.WithErrors(errors: resultSaveFile.Errors);
			}

			if (resultSaveFile.IsSuccess == true)
			{
				var uploadValue = resultSaveFile.Value;

				if (uploadValue is not null)
				{
					entity.FileOriginalNameWeb = uploadValue.FileName;
					entity.FileOriginalNameWeb = uploadValue.FileOriginalName;
					entity.FileThumbnailNameWeb = uploadValue.FileThumbnailName;
				}
			}
		}

		if (model.FileMobile is not null)
		{
			var service =
				new HttpServices.AttachmentManager.AttachmentService();

			var resultSaveFile = await service.UploadAsync
				(file: model.FileMobile, serverId: Domain.Base.ServerKeyConstant.Key, domainName: nameof(PageSetting));

			if (resultSaveFile.IsFailed == true)
			{
				result.WithErrors(errors: resultSaveFile.Errors);
			}

			if (resultSaveFile.IsSuccess == true)
			{
				var uploadValue = resultSaveFile.Value;

				if (uploadValue is not null)
				{
					entity.FileOriginalNameMobile = uploadValue.FileName;
					entity.FileOriginalNameMobile = uploadValue.FileOriginalName;
					entity.FileThumbnailNameMobile = uploadValue.FileThumbnailName;
				}
			}
		}

		if (result.IsSuccess == true)
		{
			await UnitOfWork
				.PageSettingTagPageSettingRepository
				.ClearAllTagsForThisPageAndSetNewListAsync(
					pageSettingId: entity.Id, tagIds: listTags!.Select(x => x.Id).ToList());

			await UnitOfWork.PageSettingRepository.AddAsync(entity: entity);

			await UnitOfWork.SaveAsync();

			var successMessage = string.Format(
				format: Resources.Messages.CreateSuccessMessage,
				arg0: Resources.DataDictionary.Banner);

			var value =
				Mapper.Map<BannerResponseViewModel>(source: entity);

			result.WithValue(value: value);

			result.WithSuccess(successMessage: successMessage);
		}

		return FluentResult(result: result);
	}

	#endregion POST : /banner

	#region PUT : /banner

	/// <summary>
	/// ویرایش بنر مورد نظر
	/// </summary>
	/// <param name="model">روی مدل درخواستی تغییرات خود را اعمال کنید</param>
	/// <returns>مدل نهایی و جدید</returns>
	[HttpPut(template: "banner")]
	[ServiceFilter<BannerViewModelFilterAction>]
	public async Task<IActionResult> UpdateBannerAsync([FromForm] BannerRequestViewModel model)
	{
		var listTags =
			HttpContext.Items[key: nameof(Resources.DataDictionary.Banner)] as List<TagPageSetting>;

		var result = new Result<BannerResponseViewModel>();

		var service =
			new HttpServices.AttachmentManager.AttachmentService();

		var serverId = Domain.Base.ServerKeyConstant.Key;

		var entity =
			HttpContext.Items[key: ProjectKeyName.ObjectKey] as PageSetting;

		if (entity == null)
		{
			throw new ArgumentNullException(paramName: nameof(entity));
		}

		Mapper.Map(source: model, destination: entity);

		string? fileNameWebForDelete = null;
		string? fileNameMobileForDelete = null;

		if (model.FileWeb is not null)
		{
			fileNameWebForDelete = entity.FileNameWeb;

			var resultSaveFile = await service.UploadAsync
				(file: model.FileWeb, serverId: serverId, domainName: nameof(PageSetting));

			if (resultSaveFile.IsFailed == true)
			{
				result.WithErrors(errors: resultSaveFile.Errors);
			}

			if (resultSaveFile.IsSuccess == true)
			{
				var uploadValue = resultSaveFile.Value;

				if (uploadValue is not null)
				{
					entity.FileNameWeb = uploadValue.FileName;
					entity.FileOriginalNameWeb = uploadValue.FileOriginalName;
					entity.FileThumbnailNameWeb = uploadValue.FileThumbnailName;
				}
			}
		}

		if (model.FileMobile is not null)
		{
			fileNameMobileForDelete = entity.FileNameMobile;

			var resultSaveFile = await service.UploadAsync
				(file: model.FileMobile, serverId: serverId, domainName: nameof(PageSetting));

			if (resultSaveFile.IsFailed == true)
			{
				result.WithErrors(errors: resultSaveFile.Errors);
			}

			if (resultSaveFile.IsSuccess == true)
			{
				var uploadValue = resultSaveFile.Value;

				if (uploadValue is not null)
				{
					entity.FileNameMobile = uploadValue.FileName;
					entity.FileOriginalNameMobile = uploadValue.FileOriginalName;
					entity.FileThumbnailNameMobile = uploadValue.FileThumbnailName;
				}
			}
		}

		if (result.IsSuccess == true)
		{
			await UnitOfWork
				.PageSettingTagPageSettingRepository
				.ClearAllTagsForThisPageAndSetNewListAsync(
					pageSettingId: entity.Id, tagIds: listTags!.Select(x => x.Id).ToList());

			await UnitOfWork.SaveAsync();

			if (string.IsNullOrEmpty(value: fileNameWebForDelete) == false)
			{
				var resultDelete =
					await service.DeleteAsync(fileName: fileNameWebForDelete, serverId: serverId);

				if (resultDelete.IsFailed == true)
				{
				}
			}

			if (string.IsNullOrEmpty(value: fileNameMobileForDelete) == false)
			{
				var resultDelete =
					await service.DeleteAsync(fileName: fileNameMobileForDelete, serverId: serverId);

				if (resultDelete.IsFailed == true)
				{
				}
			}

			var value =
				Mapper.Map<BannerResponseViewModel>(source: entity);

			result.WithValue(value: value);

			var successMessage = string.Format(
				format: Resources.Messages.UpdateMessageSuccess, arg0: Resources.DataDictionary.Banner);

			result.WithSuccess(successMessage: successMessage);
		}

		return FluentResult(result: result);
	}

	#endregion /PUT : /faq

	#endregion /Banner

	#region Social

	#region GET : /social

	/// <summary>
	/// دریافت لیست شبکه های اجتماعی - صفحه بندی شده
	/// - data , metaData
	/// ارسال میشوند
	/// </summary>
	/// <returns></returns>
	[HttpGet("social")]
	public async Task<IActionResult> GetّSocialsAsync([FromQuery] PageSettingParameters parameters)
	{
		var result = new Result<PagedListResult<SocialResponseViewModel>>();

		parameters.Text = nameof(Resources.DataDictionary.Social);

		var entities =
			await UnitOfWork.PageSettingRepository.GetAllInPageAsync(parameters: parameters);

		var value =
			Mapper.Map<PagedList<SocialResponseViewModel>>(source: entities);

		result.WithValue(value: new PagedListResult<SocialResponseViewModel>(Data: value, MetaData: entities.MetaData));

		return FluentResult(result: result);
	}

	#endregion GET : /social

	#region POST : /social

	/// <summary>
	/// ایجاد سوالات متداول
	/// </summary>
	/// <param name="model">مدل نهایی</param>
	/// <returns></returns>
	[HttpPost("social")]
	[ServiceFilter<SocialViewModelFilterAction>]
	public async Task<IActionResult> CreateSocialAsync([FromForm] SocialRequestViewModel model)
	{
		var socialTag =
			HttpContext.Items[key: nameof(Resources.DataDictionary.Social)] as TagPageSetting;

		var result = new Result<SocialResponseViewModel>();

		var entity = Mapper.Map<PageSetting>(source: model);

		if (model.Icon is not null)
		{
			var service =
				new HttpServices.AttachmentManager.AttachmentService();

			var resultSaveFile = await service.UploadAsync
				(file: model.Icon, serverId: Domain.Base.ServerKeyConstant.Key, domainName: nameof(PageSetting));

			if (resultSaveFile.IsFailed == true)
			{
				result.WithErrors(errors: resultSaveFile.Errors);
			}

			if (resultSaveFile.IsSuccess == true)
			{
				var uploadValue = resultSaveFile.Value;

				if (uploadValue is not null)
				{
					entity.FileNameIcon = uploadValue.FileName;
					entity.FileOriginalNameIcon = uploadValue.FileOriginalName;
				}
			}
		}

		if (result.IsSuccess == true)
		{
			await UnitOfWork
				.PageSettingTagPageSettingRepository
				.ClearAllTagsForThisPageAndSetNewListAsync(pageSettingId: entity.Id, tagIds: [socialTag!.Id]);

			await UnitOfWork.PageSettingRepository.AddAsync(entity: entity);
			await UnitOfWork.SaveAsync();

			var successMessage = string.Format(
				format: Resources.Messages.CreateSuccessMessage,
				arg0: Resources.DataDictionary.Social);

			var value =
				Mapper.Map<SocialResponseViewModel>(source: entity);

			result.WithValue(value: value);

			result.WithSuccess(successMessage: successMessage);
		}

		return FluentResult(result: result);
	}

	#endregion POST : /social

	#region PUT : /social

	/// <summary>
	/// ویرایش سوالات متداول
	/// </summary>
	/// <param name="model">روی مدل درخواستی تغییرات خود را اعمال کنید</param>
	/// <returns>مدل نهایی و جدید</returns>
	[HttpPut(template: "social")]
	[ServiceFilter<SocialViewModelFilterAction>]
	public async Task<IActionResult> UpdateSocialAsync([FromForm] SocialRequestViewModel model)
	{
		var result = new Result<SocialResponseViewModel>();

		var service =
			new HttpServices.AttachmentManager.AttachmentService();

		var serverId = Domain.Base.ServerKeyConstant.Key;

		var entity =
			HttpContext.Items[key: ProjectKeyName.ObjectKey] as PageSetting;

		if (entity == null)
		{
			throw new ArgumentNullException(paramName: nameof(entity));
		}

		Mapper.Map(source: model, destination: entity);

		string? fileNameIconForDelete = null;

		if (model.Icon is not null)
		{
			fileNameIconForDelete = entity.FileNameIcon;

			var resultSaveFile = await service.UploadAsync
				(file: model.Icon, serverId: serverId, domainName: nameof(PageSetting));

			if (resultSaveFile.IsFailed == true)
			{
				result.WithErrors(errors: resultSaveFile.Errors);
			}

			if (resultSaveFile.IsSuccess == true)
			{
				var uploadValue = resultSaveFile.Value;

				if (uploadValue is not null)
				{
					entity.FileNameIcon = uploadValue.FileName;
					entity.FileOriginalNameIcon = uploadValue.FileOriginalName;
				}
			}
		}

		if (result.IsSuccess == true)
		{
			await UnitOfWork.SaveAsync();

			if (string.IsNullOrEmpty(value: fileNameIconForDelete) == false)
			{
				var resultDelete =
					await service.DeleteAsync(fileName: fileNameIconForDelete, serverId: serverId);

				if (resultDelete.IsFailed == true)
				{
				}
			}

			var value =
				Mapper.Map<SocialResponseViewModel>(source: entity);

			result.WithValue(value: value);

			var successMessage = string.Format(
				format: Resources.Messages.UpdateMessageSuccess, arg0: Resources.DataDictionary.Social);

			result.WithSuccess(successMessage: successMessage);
		}

		return FluentResult(result: result);
	}

	#endregion /PUT : /social

	#endregion /Social

	#region TextDynamic

	#region GET : /text-dynamic

	/// <summary>
	/// دریافت لیست متن های داینامیک - صفحه بندی شده
	/// - data , metaData
	/// ارسال میشوند
	/// </summary>
	/// <returns></returns>
	[HttpGet("text-dynamic")]
	public async Task<IActionResult> GetّTextDynamicsAsync([FromQuery] PageSettingParameters parameters)
	{
		var result = new Result<PagedListResult<TextDynamicResponseViewModel>>();

		parameters.Text = nameof(Resources.DataDictionary.TextDynamic);

		var entities =
			await UnitOfWork.PageSettingRepository.GetAllInPageAsync(parameters: parameters);

		var value =
			Mapper.Map<PagedList<TextDynamicResponseViewModel>>(source: entities);

		result.WithValue(value:
			new PagedListResult<TextDynamicResponseViewModel>
				(Data: value, MetaData: entities.MetaData));

		return FluentResult(result: result);
	}

	#endregion GET : /text-dynamic

	#region POST : /text-dynamic

	/// <summary>
	/// ایجاد متن داینامیک
	/// </summary>
	/// <param name="model">مدل نهایی</param>
	/// <returns></returns>
	[HttpPost(template: "text-dynamic")]
	[ServiceFilter<TextDynamicViewModelFilterAction>]
	public async Task<IActionResult> CreateTextDynamicAsync([FromForm] TextDynamicRequestViewModel model)
	{
		var listTags =
			HttpContext.Items[key: nameof(Resources.DataDictionary.TextDynamic)] as List<TagPageSetting>;

		var result = new Result<TextDynamicResponseViewModel>();

		var entity = Mapper.Map<PageSetting>(source: model);
		
		if (result.IsSuccess == true)
		{
			await UnitOfWork
				.PageSettingTagPageSettingRepository
				.ClearAllTagsForThisPageAndSetNewListAsync(
					pageSettingId: entity.Id, tagIds: listTags!.Select(x => x.Id).ToList());

			await UnitOfWork.PageSettingRepository.AddAsync(entity: entity);

			await UnitOfWork.SaveAsync();

			var successMessage = string.Format(
				format: Resources.Messages.CreateSuccessMessage,
				arg0: Resources.DataDictionary.TextDynamic);

			var value =
				Mapper.Map<TextDynamicResponseViewModel>(source: entity);

			result.WithValue(value: value);

			result.WithSuccess(successMessage: successMessage);
		}

		return FluentResult(result: result);
	}

	#endregion POST : /text-dynamic

	#region PUT : /text-dynamic

	/// <summary>
	/// ویرایش متن داینامیک مورد نظر
	/// </summary>
	/// <param name="model">روی مدل درخواستی تغییرات خود را اعمال کنید</param>
	/// <returns>مدل نهایی و جدید</returns>
	[HttpPut(template: "text-dynamic")]
	[ServiceFilter<TextDynamicViewModelFilterAction>]
	public async Task<IActionResult> UpdateTextDynamicAsync([FromForm] TextDynamicRequestViewModel model)
	{
		var listTags =
			HttpContext.Items[key: nameof(Resources.DataDictionary.TextDynamic)] as List<TagPageSetting>;

		var result = new Result<TextDynamicResponseViewModel>();

		var service =
			new HttpServices.AttachmentManager.AttachmentService();

		var serverId = Domain.Base.ServerKeyConstant.Key;

		var entity =
			HttpContext.Items[key: ProjectKeyName.ObjectKey] as PageSetting;

		if (entity == null)
		{
			throw new ArgumentNullException(paramName: nameof(entity));
		}

		Mapper.Map(source: model, destination: entity);
		
		if (result.IsSuccess == true)
		{
			await UnitOfWork
				.PageSettingTagPageSettingRepository
				.ClearAllTagsForThisPageAndSetNewListAsync(
					pageSettingId: entity.Id, tagIds: listTags!.Select(x => x.Id).ToList());

			await UnitOfWork.SaveAsync();
			
			var value =
				Mapper.Map<TextDynamicResponseViewModel>(source: entity);

			result.WithValue(value: value);

			var successMessage = string.Format(
				format: Resources.Messages.UpdateMessageSuccess, arg0: Resources.DataDictionary.TextDynamic);

			result.WithSuccess(successMessage: successMessage);
		}

		return FluentResult(result: result);
	}

	#endregion /PUT : /text-dynamic

	#endregion /TextDynamic
}