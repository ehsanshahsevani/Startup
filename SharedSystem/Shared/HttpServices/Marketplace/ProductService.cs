using SampleResult;
using ViewModels.Shared;
using Enums.SharedService;
using ViewModels.Marketplace;
using InfrastructureSeedworks;
using Microsoft.AspNetCore.Http;
using ViewModels.ModelParameters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseProject.Model.ViewModel.Public;

namespace HttpServices.Marketplace;

public class ProductService : HttpServiceSeedworks.HttpService
{
	public ProductService() : base(ServerSettings.DomainApiMarketPlace)
	{
		SetBaseApi(nameof(Resources.DataDictionary.Product));
	}

	#region GET : /

	/// <summary>
	/// دریافت لیست همه به صورت صفحه بندی شده
	/// مدل خروجی با دو کلید با نام های
	/// - data , metaData
	/// ارسال میشوند
	/// </summary>
	/// <returns></returns>
	public async Task<Result<PagedListResult<ProductResponseViewModel>>> GetAsync(ProductParameters parameters)
	{
		string url = "";

		var queries = GetPropertiesWithValues(parameters);

		var result = await base.GetAsync<Result<PagedListResult<ProductResponseViewModel>>>(url, queries);

		return result;
	}

	#endregion GET : /

	#region GET : tags

	/// <summary>
	/// دریافت تگ ها برای مدیریت مکان و نمایش
	/// </summary>
	/// <returns></returns>
	public async Task<Result<Dictionary<string, string>>> GetTagsAsync()
	{
		string url = "tags";

		var result = await GetAsync<Result<Dictionary<string, string>>>(url);

		return result;
	}

	#endregion GET : tags

	#region GET : get-update-data?productId={id}

	/// <summary>
	/// متد جایگزین کنترلر ولی خالی است (مانند کنترلر)
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<Result<object>> GetUpdateDataAsync(string id)
	{
		string url = $"get-update-data?id={id}";

		var result = await GetAsync<Result<object>>(url);

		return result;
	}

	#endregion GET : get-update-data?productId={id}

	#region GET : {id}

	/// <summary>
	/// دریافت با استفاده از شناسه آن
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<Result<ProductResponseViewModel>> GetByIdAsync(string id)
	{
		string url = $"{id}";

		var result = await GetAsync<Result<ProductResponseViewModel>>(url);

		return result;
	}

	#endregion GET : {id}

	#region GET : dropdown-data

	/// <summary>
	/// دریافت لیست نام و آیدی ها برای استفاده در بخش های مختلف دیزاین
	/// - dropdown, ect
	/// </summary>
	/// <returns>لیستی از مدل‌های UiSelect برای استفاده در بخش‌های مختلف دیزاین</returns>
	public async Task<Result<List<UiSelectModel>>> GetDropDownDataAsync()
	{
		string url = "dropdown-data";

		var result = await GetAsync<Result<List<UiSelectModel>>>(url);

		return result;
	}

	#endregion GET : dropdown-data
	
	#region POST : /

	/// <summary>
	/// ایجاد
	/// </summary>
	/// <param name="model">مدل محصول</param>
	/// <returns></returns>
	public async Task<Result<ProductResponseViewModel>> CreateAsync(ProductRequestViewModel model)
	{
		string url = "";

		var files = new Dictionary<string, IFormFile>();
		
		if (model.FileUpload is not null)
		{
			files["fileUpload"] = model.FileUpload;
		}
		
		var result = await PostAsync<ProductRequestViewModel, Result<ProductResponseViewModel>>(
			url,
			model,
			ContentType.MultipartFormData,
			files,
			null);

		return result!;
	}

	#endregion POST : /

	#region PUT : /

	/// <summary>
	/// ویرایش
	/// </summary>
	/// <param name="model">روی مدل درخواستی تغییرات خود را اعمال کنید</param>
	/// <returns>مدل نهایی و جدید</returns>
	public async Task<Result<ProductResponseViewModel>> UpdateAsync(ProductRequestViewModel model)
	{
		string url = "";


		var result = await PutAsync<ProductRequestViewModel, Result<ProductResponseViewModel>>(
			url,
			model,
			ContentType.MultipartFormData);

		return result!;
	}

	#endregion PUT : /

	#region PUT : change-activation/{id}

	/// <summary>
	/// تغییر وضعیت
	/// </summary>
	/// <param name="id">شناسه</param>
	/// <returns>مدل با دیتای جدید</returns>
	public async Task<Result<ProductResponseViewModel>> ChangeActivationAsync(string id)
	{
		string url = $"change-activation/{id}";

		var result = await PutAsync<object, Result<ProductResponseViewModel>>(url, new { });

		return result;
	}

	#endregion PUT : change-activation/{id}

	#region PUT : update-profile/{id}

	/// <summary>
	/// تغییر تصویر
	/// </summary>
	/// <param name="id">شناسه شعبه</param>
	/// <param name="file">فایل جدید</param>
	/// <returns>مدل حاوی تصویر جدید</returns>
	public async Task<Result<ProductResponseViewModel>> UpdateProfileAsync(string id, IFormFile file)
	{
		string url = $"update-profile/{id}";

		if (file is null)
		{
			throw new ArgumentNullException(nameof(file));
		}

		var fileDict = new Dictionary<string, IFormFile>
		{
			{ "file", file }
		};

		var result = await PutAsync<object?, Result<ProductResponseViewModel>>(
			url,
			null,  // دیگر داده‌ها در اینجا ارسال نمی‌شود
			ContentType.MultipartFormData,
			fileDict,
			null);

		return result;
	}

	#endregion PUT : update-profile/{id}

	#region PUT : set-tags/{id}

	/// <summary>
	/// ویرایش تگ ها
	/// </summary>
	/// <param name="id">شناسه محصول</param>
	/// <param name="tags">تگ های سیستمی</param>
	/// <returns>دسته بندی ویرایش شده</returns>
	public async Task<Result<ProductResponseViewModel>> SetTagsAsync(string id, List<string> tags)
	{
		string url = $"set-tags/{id}";

		var result = await PutAsync<List<string>, Result<ProductResponseViewModel>>(url, tags);

		return result;
	}

	#endregion PUT : set-tags/{id}

	#region DELETE : {id}

	/// <summary>
	/// حذف با شناسه آن
	/// </summary>
	/// <param name="id">شناسه</param>
	/// <returns>در صورت حذف آیدی به شما برگردانده میشود</returns>
	public async Task<Result<string>> DeleteAsync(string id)
	{
		string url = $"{id}";

		var result = await base.DeleteAsync<Result<string>>(url);

		return result;
	}

	#endregion DELETE : {id}

	#region PUT : delete-other-images/{id}

	/// <summary>
	/// حذف تمام تصاویر بجز تصویر پروفایل
	/// </summary>
	/// <param name="id">شناسه</param>
	/// <returns>مدل تغییر یافته از</returns>
	public async Task<Result<List<string>>> DeleteOtherImagesAsync(string id)
	{
		string url = $"delete-other-images/{id}";

		var result = await PutAsync<object?, Result<List<string>>>(url, null);

		return result;
	}

	#endregion PUT : delete-other-images/{id}
}