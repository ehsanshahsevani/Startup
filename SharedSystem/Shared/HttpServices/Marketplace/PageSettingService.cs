using SampleResult;
using ViewModels.Shared;
using Enums.SharedService;
using ViewModels.Marketplace;
using InfrastructureSeedworks;
using Microsoft.AspNetCore.Http;
using ViewModels.ModelParameters;

namespace HttpServices.Marketplace;

/// <summary>
/// مدیریت تنظیمات صفحه
/// </summary>
public class PageSettingService : HttpServiceSeedworks.HttpService
{
	public PageSettingService() : base(ServerSettings.DomainApiMarketPlace)
	{
		SetBaseApi(nameof(Resources.DataDictionary.PageSetting));
	}

	#region GET : /

	/// <summary>
	/// دریافت لیست همه به صورت صفحه بندی شده
	/// </summary>
	public async Task<Result<PagedListResult<PageSettingResponseViewModel>>> GetAsync(PageSettingParameters parameters)
	{
		string url = "";
		var queries = GetPropertiesWithValues(parameters);
		var result = await base.GetAsync<Result<PagedListResult<PageSettingResponseViewModel>>>(url, queries);
		return result;
	}

	#endregion

	#region GET : {id}

	/// <summary>
	/// دریافت با شناسه
	/// </summary>
	public async Task<Result<PageSettingResponseViewModel>> GetByIdAsync(string id)
	{
		string url = $"{id}";
		var result = await base.GetAsync<Result<PageSettingResponseViewModel>>(url);
		return result;
	}

	#endregion

	#region GET : tags
	
	/// <summary>
	/// دریافت تگ ها
	/// </summary>
	public async Task<Result<List<TagPageSettingResponseViewModel>>> GetTagsAsync()
	{
		string url = "tags";
		
		var result =
			await base
				.GetAsync<Result<List<TagPageSettingResponseViewModel>>>(url);
		
		return result;
	}
	
	#endregion /tags
	
	#region GET : tags-by-start-with-name/{name}
	
	/// <summary>
	/// دریافت تگ ها
	/// </summary>
	public async Task<Result<List<TagPageSettingResponseViewModel>>> GetTagsByStartNameAsync(string name)
	{
		string url = $"tags-by-start-with-name/{name}";
		
		var result =
			await base
				.GetAsync<Result<List<TagPageSettingResponseViewModel>>>(url);
		
		return result;
	}
	
	#endregion /tags-by-start-with-name/{name}

	#region POST : /

	/// <summary>
	/// ایجاد
	/// </summary>
	public async Task<Result<PageSettingResponseViewModel>> CreateAsync(PageSettingRequestViewModel model)
	{
		string url = "";

		var fileDict = new Dictionary<string, IFormFile>();
		if (model.FileWeb is not null) fileDict["FileWeb"] = model.FileWeb;
		if (model.FileMobile is not null) fileDict["FileMobile"] = model.FileMobile;

		var result = await PostAsync<PageSettingRequestViewModel, Result<PageSettingResponseViewModel>>(
			url,
			model,
			ContentType.MultipartFormData,
			fileDict,
			null);

		return result!;
	}

	#endregion

	#region PUT : /

	/// <summary>
	/// ویرایش
	/// </summary>
	public async Task<Result<PageSettingResponseViewModel>> UpdateAsync(PageSettingRequestViewModel model)
	{
		string url = "";

		var fileDict = new Dictionary<string, IFormFile>();
		if (model.FileWeb is not null) fileDict["FileWeb"] = model.FileWeb;
		if (model.FileMobile is not null) fileDict["FileMobile"] = model.FileMobile;

		var result = await PutAsync<PageSettingRequestViewModel, Result<PageSettingResponseViewModel>>(
			url,
			model,
			ContentType.MultipartFormData,
			fileDict,
			null);

		return result;
	}

	#endregion

	#region PUT : set-tags/{id}

	/// <summary>
	/// ویرایش تگ‌ها
	/// </summary>
	public async Task<Result<PageSettingResponseViewModel>> SetTagsAsync(string id, List<string> tags)
	{
		string url = $"set-tags/{id}";
		var result = await PutAsync<List<string>, Result<PageSettingResponseViewModel>>(url, tags);
		return result;
	}

	#endregion

	#region PUT : change-activation/{id}

	/// <summary>
	/// تغییر وضعیت
	/// </summary>
	public async Task<Result<PageSettingResponseViewModel>> ChangeActivationAsync(string id)
	{
		string url = $"change-activation/{id}";
		var result = await PutAsync<object, Result<PageSettingResponseViewModel>>(url, new { });
		return result;
	}

	#endregion

	#region PUT : delete-image/{id}

	/// <summary>
	/// حذف تصاویر
	/// </summary>
	public async Task<Result<PageSettingResponseViewModel>> DeleteImageAsync(string id)
	{
		string url = $"delete-image/{id}";
		var result = await PutAsync<object, Result<PageSettingResponseViewModel>>(url, new { });
		return result;
	}

	#endregion

	#region DELETE : {id}

	/// <summary>
	/// حذف با شناسه
	/// </summary>
	public async Task<Result<string>> DeleteAsync(string id)
	{
		string url = $"{id}";
		var result = await base.DeleteAsync<Result<string>>(url);
		return result;
	}

	#endregion

	// ========================= FAQ =========================

	#region GET : /faq

	public async Task<Result<PagedListResult<FaqResponseViewModel>>> GetFaqsAsync(PageSettingParameters parameters)
	{
		string url = "faq";
		var queries = GetPropertiesWithValues(parameters);
		var result = await base.GetAsync<Result<PagedListResult<FaqResponseViewModel>>>(url, queries);
		return result;
	}

	#endregion

	#region POST : /faq

	public async Task<Result<FaqResponseViewModel>> CreateFaqAsync(FaqRequestViewModel model)
	{
		string url = "faq";

		var files = new Dictionary<string, IFormFile>();
		if (model.Icon is not null) files["Icon"] = model.Icon;

		var result = await PostAsync<FaqRequestViewModel, Result<FaqResponseViewModel>>(
			url,
			model,
			ContentType.MultipartFormData,
			files,
			null);

		return result!;
	}

	#endregion

	#region PUT : /faq

	public async Task<Result<FaqResponseViewModel>> UpdateFaqAsync(FaqRequestViewModel model)
	{
		string url = "faq";

		var files = new Dictionary<string, IFormFile>();
		if (model.Icon is not null) files["Icon"] = model.Icon;

		var result = await PutAsync<FaqRequestViewModel, Result<FaqResponseViewModel>>(
			url,
			model,
			ContentType.MultipartFormData,
			files,
			null);

		return result;
	}

	#endregion

	// ========================= Banner =========================

	#region GET : /banner

	public async Task<Result<PagedListResult<BannerResponseViewModel>>> GetBannersAsync(PageSettingParameters parameters)
	{
		string url = "banner";
		var queries = GetPropertiesWithValues(parameters);
		var result = await base.GetAsync<Result<PagedListResult<BannerResponseViewModel>>>(url, queries);
		return result;
	}

	#endregion

	#region POST : /banner

	public async Task<Result<BannerResponseViewModel>> CreateBannerAsync(BannerRequestViewModel model)
	{
		string url = "banner";

		var files = new Dictionary<string, IFormFile>();
		if (model.FileWeb is not null)
		{
			files["FileWeb"] = model.FileWeb;
		}

		if (model.FileMobile is not null)
		{
			files["FileMobile"] = model.FileMobile;
		}

		var result = await PostAsync<BannerRequestViewModel, Result<BannerResponseViewModel>>(
			url,
			model,
			ContentType.MultipartFormData,
			files,
			null);

		return result!;
	}

	#endregion

	#region PUT : /banner

	public async Task<Result<BannerResponseViewModel>> UpdateBannerAsync(BannerRequestViewModel model)
	{
		string url = "banner";

		var files = new Dictionary<string, IFormFile>();
		if (model.FileWeb is not null)
		{
			files["FileWeb"] = model.FileWeb;
		}

		if (model.FileMobile is not null)
		{
			files["FileMobile"] = model.FileMobile;
		}

		var result = await PutAsync<BannerRequestViewModel, Result<BannerResponseViewModel>>(
			url,
			model,
			ContentType.MultipartFormData,
			files,
			null);

		return result;
	}

	#endregion

	// ========================= Social =========================

	#region GET : /social

	public async Task<Result<PagedListResult<SocialResponseViewModel>>> GetSocialsAsync(PageSettingParameters parameters)
	{
		string url = "social";
		var queries = GetPropertiesWithValues(parameters);
		var result = await base.GetAsync<Result<PagedListResult<SocialResponseViewModel>>>(url, queries);
		return result;
	}

	#endregion

	#region POST : /social

	public async Task<Result<SocialResponseViewModel>> CreateSocialAsync(SocialRequestViewModel model)
	{
		string url = "social";

		var files = new Dictionary<string, IFormFile>();
		if (model.Icon is not null)
		{
			files["Icon"] = model.Icon;
		}

		var result = await PostAsync<SocialRequestViewModel, Result<SocialResponseViewModel>>(
			url,
			model,
			ContentType.MultipartFormData,
			files,
			null);

		return result!;
	}

	#endregion

	#region PUT : /social

	public async Task<Result<SocialResponseViewModel>> UpdateSocialAsync(SocialRequestViewModel model)
	{
		string url = "social";

		var files = new Dictionary<string, IFormFile>();
		if (model.Icon is not null) files["Icon"] = model.Icon;

		var result = await PutAsync<SocialRequestViewModel, Result<SocialResponseViewModel>>(
			url,
			model,
			ContentType.MultipartFormData,
			files,
			null);

		return result;
	}

	#endregion

	// ========================= TextDynamic =========================

	#region GET : /text-dynamic

	public async Task<Result<PagedListResult<TextDynamicResponseViewModel>>> GetTextDynamicsAsync(PageSettingParameters parameters)
	{
		string url = "text-dynamic";
		var queries = GetPropertiesWithValues(parameters);
		var result = await base.GetAsync<Result<PagedListResult<TextDynamicResponseViewModel>>>(url, queries);
		return result;
	}

	#endregion

	#region POST : /text-dynamic

	public async Task<Result<TextDynamicResponseViewModel>> CreateTextDynamicAsync(TextDynamicRequestViewModel model)
	{
		string url = "text-dynamic";

		var result = await PostAsync<TextDynamicRequestViewModel, Result<TextDynamicResponseViewModel>>(
			url,
			model,
			ContentType.MultipartFormData,
			null,
			null);

		return result!;
	}

	#endregion

	#region PUT : /text-dynamic

	public async Task<Result<TextDynamicResponseViewModel>> UpdateTextDynamicAsync(TextDynamicRequestViewModel model)
	{
		string url = "text-dynamic";
		
		var result = await PutAsync<TextDynamicRequestViewModel, Result<TextDynamicResponseViewModel>>(
			url,
			model,
			ContentType.MultipartFormData,
			null,
			null);

		return result;
	}

	#endregion
}
