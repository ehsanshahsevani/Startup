// using Domain;
// using Constants;
// using AutoMapper;
// using Persistence;
// using FluentResults;
// using Infrastructure;
// using RequestFeatures;
// using ViewModels.Shared;
// using ViewModels.Marketplace;
// using Microsoft.AspNetCore.Mvc;
// using ViewModels.ModelParameters;
// using PersistenceSeedworks.LogManager;
// using Infrastructure.Filters.FilterActions;
//
// namespace RestFullApi.Controllers;
//
// /// <summary>
// /// تنظیمات صفحه
// /// </summary>
// public class PageSettingController : BaseControllerApi
// {
// 	#region DI Settings & Constructor
//
// 	public PageSettingController(
// 		IMapper mapper,
// 		HttpClient httpClient, IConfiguration configuration,
// 		IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork,
// 		ILogDetailManager logDetailManager, ILogServerManager logServerManager)
// 		: base(mapper, httpClient, configuration, httpContextAccessor, unitOfWork, logDetailManager, logServerManager)
// 	{
// 	}
//
// 	#endregion
//
// 	#region GET : /
//
// 	/// <summary>
// 	/// دریافت لیست همه به صورت صفحه بندی شده
// 	/// مدل خروجی با دو کلید با نام های
// 	/// - data , metaData
// 	/// ارسال میشوند
// 	///
// 	/// </summary>
// 	/// <returns></returns>
// 	[HttpGet]
// 	public async Task<IActionResult> GetAsync([FromQuery] PageSettingParameters parameters)
// 	{
// 		var result = new Result<PagedListResult<PageSettingResponseViewModel>>();
//
// 		var entities =
// 			await UnitOfWork.PageSettingRepository.GetAllInPageAsync(parameters);
//
// 		var value =
// 			Mapper.Map<PagedList<PageSettingResponseViewModel>>(entities);
//
// 		result.WithValue(new PagedListResult<PageSettingResponseViewModel>(value, entities.MetaData));
//
// 		return FluentResult(result);
// 	}
//
// 	#endregion GET : /
//
// 	#region GET : {id}
//
// 	/// <summary>
// 	/// دریافت با استفاده از شناسه آن
// 	/// </summary>
// 	/// <param name="id">شناسه</param>
// 	/// <returns></returns>
// 	[HttpGet(template: "{id}")]
// 	[ServiceFilter<CheckPageSettingIdActionFilter>]
// 	public IActionResult GetByIdAsync([FromRoute] string id)
// 	{
// 		var result = new Result<PageSettingResponseViewModel>();
//
// 		var entity =
// 			HttpContext.Items[ProjectKeyName.ObjectKey] as PageSetting;
//
// 		var value = Mapper.Map<PageSettingResponseViewModel>(entity);
//
// 		result.WithValue(value);
//
// 		return FluentResult(result);
// 	}
//
// 	#endregion /GET : by-id
//
// 	#region GET : /tags
//
// 	/// <summary>
// 	/// دریافت تگ ها برای مدیریت مکان و نمایش
// 	/// </summary>
// 	/// <returns></returns>
// 	[HttpGet(template: "tags")]
// 	public IActionResult GetTagsAsync()
// 	{
// 		var result = new Result<Dictionary<string, string>>();
//
// 		var list = TagManager.PageSetting.GetList();
//
// 		result.WithValue(list);
//
// 		return FluentResult(result);
// 	}
//
// 	#endregion
//
// 	#region POST : /
//
// 	/// <summary>
// 	/// ایجاد
// 	/// </summary>
// 	/// <param name="model">مدل نهایی</param>
// 	/// <returns></returns>
// 	[HttpPost]
// 	[ServiceFilter<PageSettingViewModelFilterAction>]
// 	public async Task<IActionResult> CreateAsync([FromForm] PageSettingRequestViewModel model)
// 	{
// 		var result = new Result<PageSettingResponseViewModel>();
//
// 		var entity = Mapper.Map<PageSetting>(model);
//
// 		if (model.FileWeb is not null)
// 		{
// 			var service =
// 				new HttpServices.AttachmentManager.AttachmentService();
//
// 			var resultSaveFile = await service.UploadAsync
// 				(model.FileWeb, Domain.Base.ServerKeyConstant.Key, nameof(PageSetting));
//
// 			if (resultSaveFile.IsFailed == true)
// 			{
// 				result.WithErrors(resultSaveFile.Errors);
// 			}
//
// 			if (resultSaveFile.IsSuccess == true)
// 			{
// 				var uploadValue = resultSaveFile.Value;
//
// 				if (uploadValue is not null)
// 				{
// 					entity.FileOriginalNameWeb = uploadValue.FileName;
// 					entity.FileOriginalNameWeb = uploadValue.FileOriginalName;
// 					entity.FileThumbnailNameWeb = uploadValue.FileThumbnailName;
// 				}
// 			}
// 		}
//
// 		if (model.FileMobile is not null)
// 		{
// 			var service =
// 				new HttpServices.AttachmentManager.AttachmentService();
//
// 			var resultSaveFile = await service.UploadAsync
// 				(model.FileMobile, Domain.Base.ServerKeyConstant.Key, nameof(PageSetting));
//
// 			if (resultSaveFile.IsFailed == true)
// 			{
// 				result.WithErrors(resultSaveFile.Errors);
// 			}
//
// 			if (resultSaveFile.IsSuccess == true)
// 			{
// 				var uploadValue = resultSaveFile.Value;
//
// 				if (uploadValue is not null)
// 				{
// 					entity.FileOriginalNameMobile = uploadValue.FileName;
// 					entity.FileOriginalNameMobile = uploadValue.FileOriginalName;
// 					entity.FileThumbnailNameMobile = uploadValue.FileThumbnailName;
// 				}
// 			}
// 		}
//
// 		if (result.IsSuccess == true)
// 		{
// 			entity.ClearAndSetTags(model.Tags.Select(x => x.Key).ToArray());
//
// 			await UnitOfWork.PageSettingRepository.AddAsync(entity);
// 			await UnitOfWork.SaveAsync();
//
// 			var successMessage = string.Format(
// 				Resources.Messages.CreateSuccessMessage,
// 				Resources.DataDictionary.PageSetting);
//
// 			var value =
// 				Mapper.Map<PageSettingResponseViewModel>(entity);
//
// 			result.WithValue(value);
//
// 			result.WithSuccess(successMessage);
// 		}
//
// 		return FluentResult(result);
// 	}
//
// 	#endregion POST : /
//
// 	#region PUT : /
//
// 	/// <summary>
// 	/// ویرایش
// 	/// </summary>
// 	/// <param name="model">روی مدل درخواستی تغییرات خود را اعمال کنید</param>
// 	/// <returns>مدل نهایی و جدید</returns>
// 	[HttpPut]
// 	[ServiceFilter<PageSettingViewModelFilterAction>]
// 	public async Task<IActionResult> UpdateAsync([FromForm] PageSettingRequestViewModel model)
// 	{
// 		var result = new Result<PageSettingResponseViewModel>();
//
// 		var service =
// 			new HttpServices.AttachmentManager.AttachmentService();
//
// 		var serverId = Domain.Base.ServerKeyConstant.Key;
//
// 		var entity =
// 			HttpContext.Items[ProjectKeyName.ObjectKey] as PageSetting;
//
// 		if (entity == null)
// 		{
// 			throw new ArgumentNullException(nameof(entity));
// 		}
//
// 		Mapper.Map(model, entity);
//
// 		string? fileNameWebForDelete = null;
// 		string? fileNameMobileForDelete = null;
//
// 		if (model.FileWeb is not null)
// 		{
// 			fileNameWebForDelete = entity.FileNameWeb;
//
// 			// if (string.IsNullOrEmpty(entity.FileNameWeb) == false)
// 			// {
// 			// 	var resultDelete =
// 			// 		await service.DeleteAsync(entity.FileNameWeb, serverId);
// 			//
// 			// 	if (resultDelete.IsFailed == true)
// 			// 	{
// 			// 		result.WithErrors(resultDelete.Errors);
// 			// 	}
// 			// }
//
// 			// if (result.IsSuccess == true)
// 			// {
// 			// }
// 			
// 			var resultSaveFile = await service.UploadAsync
// 				(model.FileWeb, serverId, nameof(PageSetting));
//
// 			if (resultSaveFile.IsFailed == true)
// 			{
// 				result.WithErrors(resultSaveFile.Errors);
// 			}
//
// 			if (resultSaveFile.IsSuccess == true)
// 			{
// 				var uploadValue = resultSaveFile.Value;
//
// 				if (uploadValue is not null)
// 				{
// 					entity.FileNameWeb = uploadValue.FileName;
// 					entity.FileOriginalNameWeb = uploadValue.FileOriginalName;
// 					entity.FileThumbnailNameWeb = uploadValue.FileThumbnailName;
// 				}
// 			}
// 		}
//
// 		if (model.FileMobile is not null)
// 		{
// 			fileNameMobileForDelete = entity.FileNameMobile;
// 			
// 			var resultSaveFile = await service.UploadAsync
// 				(model.FileMobile, serverId, nameof(PageSetting));
//
// 			if (resultSaveFile.IsFailed == true)
// 			{
// 				result.WithErrors(resultSaveFile.Errors);
// 			}
//
// 			if (resultSaveFile.IsSuccess == true)
// 			{
// 				var uploadValue = resultSaveFile.Value;
//
// 				if (uploadValue is not null)
// 				{
// 					entity.FileNameMobile = uploadValue.FileName;
// 					entity.FileOriginalNameMobile = uploadValue.FileOriginalName;
// 					entity.FileThumbnailNameMobile = uploadValue.FileThumbnailName;
// 				}
// 			}
// 		}
// 		
// 		if (result.IsSuccess == true)
// 		{
// 			entity.ClearAndSetTags(model.Tags.Select(x => x.Key).ToArray());
//
// 			await UnitOfWork.SaveAsync();
//
// 			if (string.IsNullOrEmpty(fileNameWebForDelete) == false)
// 			{
// 				var resultDelete =
// 					await service.DeleteAsync(fileNameWebForDelete, serverId);
// 			
// 				if (resultDelete.IsFailed == true)
// 				{
// 					// result.WithErrors(resultDelete.Errors);
// 				}
// 			}
// 			
// 			if (string.IsNullOrEmpty(fileNameMobileForDelete) == false)
// 			{
// 				var resultDelete =
// 					await service.DeleteAsync(fileNameMobileForDelete, serverId);
// 			
// 				if (resultDelete.IsFailed == true)
// 				{
// 					// result.WithErrors(resultDelete.Errors);
// 				}
// 			}
// 			
// 			var value =
// 				Mapper.Map<PageSettingResponseViewModel>(entity);
//
// 			result.WithValue(value);
//
// 			var successMessage = string.Format(
// 				Resources.Messages.UpdateMessageSuccess, Resources.DataDictionary.PageSetting);
//
// 			result.WithSuccess(successMessage);
// 		}
//
// 		return FluentResult(result);
// 	}
//
// 	#endregion /PUT : /
//
// 	#region PUT : /set-tags - []
//
// 	/// <summary>
// 	/// ویرایش تگ ها
// 	/// </summary>
// 	/// <param name="id">شناسه</param>
// 	/// <param name="tags">تگ های سیستمی</param>
// 	/// <returns>مدل ویرایش شده</returns>
// 	[HttpPut(template: "set-tags/{id}")]
// 	[ServiceFilter<CheckPageSettingIdActionFilter>]
// 	public async Task<IActionResult> SetTagsAsync([FromRoute] string id, [FromBody] List<string>? tags)
// 	{
// 		var result = new Result<PageSettingResponseViewModel>();
//
// 		var entity =
// 			HttpContext.Items[ProjectKeyName.ObjectKey] as PageSetting;
//
// 		if (entity == null)
// 		{
// 			throw new ArgumentNullException(nameof(entity));
// 		}
//
// 		if (tags is null || tags.Count == 0)
// 		{
// 			entity.ClearAndSetTags();
// 		}
// 		else
// 		{
// 			entity.ClearAndSetTags(tags.ToArray());
// 		}
//
// 		var value = Mapper.Map<PageSettingResponseViewModel>(entity);
//
// 		await UnitOfWork.SaveAsync();
//
// 		var successMessage = string.Format(
// 			Resources.Messages.UpdateMessageSuccess, Resources.DataDictionary.PageSetting);
//
// 		result.WithSuccess(successMessage);
//
// 		result.WithValue(value);
//
// 		return FluentResult(result);
// 	}
//
// 	#endregion /PUT : /set-tags
//
// 	#region PUT : /change-activation/{id}
//
// 	/// <summary>
// 	/// تغییر وضعیت
// 	/// </summary>
// 	/// <param name="id">شناسه</param>
// 	/// <returns>مدل با دیتای جدید</returns>
// 	/// <exception cref="ArgumentNullException"></exception>
// 	[HttpPut(template: "change-activation/{id}")]
// 	[ServiceFilter<CheckPageSettingIdActionFilter>]
// 	public async Task<IActionResult> ChangeActivationAsync([FromRoute] string id)
// 	{
// 		var result = new Result<PageSettingResponseViewModel>();
//
// 		var entity =
// 			HttpContext.Items[ProjectKeyName.ObjectKey] as PageSetting;
//
// 		if (entity == null)
// 		{
// 			throw new ArgumentNullException(nameof(entity));
// 		}
//
// 		entity.IsActive = !entity.IsActive;
// 		entity.UpdateDateTime = DateTime.Now;
//
// 		var value = Mapper.Map<PageSettingResponseViewModel>(entity);
//
// 		await UnitOfWork.SaveAsync();
//
// 		var successMessage = string.Format(
// 			Resources.Messages.UpdateMessageSuccess, Resources.DataDictionary.PageSetting);
//
// 		result.WithSuccess(successMessage);
//
// 		result.WithValue(value);
//
// 		return FluentResult(result);
// 	}
//
// 	#endregion /PUT : /change-activation/{id}
//
// 	#region PUT : /delete-image/{id}
//
// 	/// <summary>
// 	/// حذف تصویر
// 	/// </summary>
// 	/// <param name="id">شناسه</param>
// 	/// <returns>مدل تغییر یافته</returns>
// 	/// <exception cref="ArgumentNullException"></exception>
// 	[HttpPut(template: "delete-image/{id}")]
// 	[ServiceFilter<CheckPageSettingIdActionFilter>]
// 	public async Task<IActionResult> DeleteImageAsync([FromRoute] string id)
// 	{
// 		var result = new Result<PageSettingResponseViewModel>();
//
// 		var service =
// 			new HttpServices.AttachmentManager.AttachmentService();
// 		
// 		var entity =
// 			HttpContext.Items[ProjectKeyName.ObjectKey] as PageSetting;
//
// 		if (entity == null)
// 		{
// 			throw new ArgumentNullException(nameof(entity));
// 		}
//
// 		if (entity.FileIsExistWeb() == true)
// 		{
// 			var resultSaveFile = await service.DeleteAsync
// 				(entity.FileNameWeb!, Domain.Base.ServerKeyConstant.Key);
//
// 			if (resultSaveFile.IsSuccess == true)
// 			{
// 				entity.FileNameWeb = null;
// 				entity.FileOriginalNameWeb = null;
// 				entity.FileThumbnailNameWeb = null;
// 			}
//
// 			if (resultSaveFile.IsFailed == true)
// 			{
// 				result.WithErrors(resultSaveFile.Errors);
// 			}
// 		}
//
// 		if (entity.FileIsExistMobile() == true)
// 		{
// 			var resultSaveFile = await service.DeleteAsync
// 				(entity.FileNameMobile!, Domain.Base.ServerKeyConstant.Key);
//
// 			if (resultSaveFile.IsSuccess == true)
// 			{
// 				entity.FileNameMobile = null;
// 				entity.FileOriginalNameMobile = null;
// 				entity.FileThumbnailNameMobile = null;
// 			}
//
// 			if (resultSaveFile.IsFailed == true)
// 			{
// 				result.WithErrors(resultSaveFile.Errors);
// 			}
// 		}
// 		
// 		if (result.IsSuccess == true)
// 		{
// 			entity.UpdateDateTime = DateTime.Now;
//
// 			var value = Mapper.Map<PageSettingResponseViewModel>(entity);
//
// 			await UnitOfWork.SaveAsync();
//
// 			var successMessage = string.Format(
// 				Resources.Messages.UpdateMessageSuccess, Resources.DataDictionary.PageSetting);
//
// 			result.WithSuccess(successMessage);
//
// 			result.WithValue(value);
// 		}
//
// 		return FluentResult(result);
// 	}
//
// 	#endregion /PUT : /delete-image/{id}
//
// 	#region DELETE : /
//
// 	/// <summary>
// 	/// حذف با شناسه آن
// 	/// </summary>
// 	/// <param name="id">شناسه</param>
// 	/// <returns>در صورت حذف آیدی به شما برگردانده میشود</returns>
// 	[HttpDelete(template: "{id}")]
// 	[ServiceFilter<CheckPageSettingIdActionFilter>]
// 	public async Task<IActionResult> DeleteAsync([FromRoute] string id)
// 	{
// 		var result = new Result<string>();
//
// 		var entity =
// 			HttpContext.Items[ProjectKeyName.ObjectKey] as PageSetting;
//
// 		if (entity == null)
// 		{
// 			throw new ArgumentNullException(nameof(entity));
// 		}
//
// 		if (entity.FileIsExistWeb() == true)
// 		{
// 			var service =
// 				new HttpServices.AttachmentManager.AttachmentService();
//
// 			var resultSaveFile = await service.DeleteAsync
// 				(entity.FileNameWeb!, Domain.Base.ServerKeyConstant.Key);
//
// 			if (resultSaveFile.IsFailed == true)
// 			{
// 				result.WithErrors(resultSaveFile.Errors);
// 			}
// 		}
// 		
// 		if (entity.FileIsExistMobile() == true)
// 		{
// 			var service =
// 				new HttpServices.AttachmentManager.AttachmentService();
//
// 			var resultSaveFile = await service.DeleteAsync
// 				(entity.FileNameMobile!, Domain.Base.ServerKeyConstant.Key);
//
// 			if (resultSaveFile.IsFailed == true)
// 			{
// 				result.WithErrors(resultSaveFile.Errors);
// 			}
// 		}
//
// 		if (result.IsSuccess == true)
// 		{
// 			await UnitOfWork.PageSettingRepository.RemoveAsync(entity);
// 			await UnitOfWork.SaveAsync();
//
// 			var successMessage =
// 				string.Format(Resources.Messages.DeleteMessageSuccess, Resources.DataDictionary.PageSetting);
//
// 			result.WithSuccess(successMessage);
//
// 			result.WithValue(entity.Id);
// 		}
//
// 		return FluentResult(result);
// 	}
//
// 	#endregion /DELETE : /
// }

