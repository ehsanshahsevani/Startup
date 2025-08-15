using SampleResult;
using ViewModels.Shared;
using ViewModels.Marketplace;
using InfrastructureSeedworks;
using ViewModels.ModelParameters;
using BaseProject.Model.ViewModel.Public;
using Enums.SharedService;
using Microsoft.AspNetCore.Http;

namespace HttpServices.Marketplace;

/// <summary>
/// مدیریت مقادیر
/// </summary>
public class ValueService : HttpServiceSeedworks.HttpService
{
	public ValueService() : base(ServerSettings.DomainApiMarketPlace)
	{
		SetBaseApi(nameof(Resources.DataDictionary.Value));
	}

	#region GET : /

	/// <summary>
	/// دریافت لیست همه دسته بندی ها به صورت صفحه بندی شده
	/// </summary>
	/// <returns></returns>
	public async Task<Result<PagedListResult<ValueResponseViewModel>>> GetAsync(ValueParameters parameters)
	{
		string url = "";

		var queries =
			GetPropertiesWithValues(parameters);

		var result =
			await base.GetAsync
				<Result<PagedListResult<ValueResponseViewModel>>>(url, queries);

		return result;
	}

	#endregion

	#region GET : {id}

	/// <summary>
	/// دریافت یک دسته بندی با استفاده از شناسه آن
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<Result<ValueResponseViewModel>> GetByIdAsync(string id)
	{
		string url = $"{id}";

		var result =
			await base.GetAsync<Result<ValueResponseViewModel>>(url);

		return result;
	}

	#endregion

	#region GET : /dropdown-data

	/// <summary>
	/// دریافت لیست نام و آیدی های هر دسته بندی برای استفاده در بخش های مختلف دیزاین
	/// </summary>
	/// <returns></returns>
	public async Task<Result<List<UiSelectModel>>> GetDropDownDataAsync()
	{
		string url = "dropdown-data";

		var result =
			await base.GetAsync<Result<List<UiSelectModel>>>(url);

		return result;
	}

	#endregion
	
	#region POST : /

	/// <summary>
	/// ایجاد دسته بندی
	/// </summary>
	/// <param name="model">دسته بندی</param>
	/// <returns></returns>
	public async Task<Result<ValueResponseViewModel>> CreateAsync(ValueRequestViewModel model)
	{
		string url = "";
		
		var files = new Dictionary<string, IFormFile>();

		if (model.FileUpload is not null)
		{
			var file = model.FileUpload;

			files = new Dictionary<string, IFormFile>
			{
				["FileUpload"] = file
			};
		}

		var result = await PostAsync<ValueRequestViewModel, Result<ValueResponseViewModel>>(
			url,
			model,
			ContentType.MultipartFormData,
			files
		);


		// var result =
		// 	await PostAsync
		// 		<ValueRequestViewModel, Result<ValueResponseViewModel>>(url, model,
		// 			ContentType.MultipartFormData);

		return result!;
	}

	#endregion

	#region PUT : /

	/// <summary>
	/// ویرایش دسته بندی
	/// </summary>
	/// <param name="model">مدل درخواستی تغییرات خود را اعمال کنید</param>
	/// <returns>مدل نهایی و جدید</returns>
	public async Task<Result<ValueResponseViewModel>> UpdateAsync(ValueRequestViewModel model)
	{
    string url = ""; // مقداردهی URL به API

    var files = new Dictionary<string, IFormFile>();
        
    var file = model.FileUpload;

    files = new Dictionary<string, IFormFile>
    {
	    ["FileUpload"] = file
    };

    var result = await PutAsync<ValueRequestViewModel, Result<ValueResponseViewModel>>(
        url,
        model,
        ContentType.MultipartFormData,
        files
    );

    return result;
}

	#endregion

	#region PUT : /change-activation/{id}

	/// <summary>
	/// تغییر وضعیت دسته بندی
	/// </summary>
	/// <param name="id">شناسه دسته بندی</param>
	/// <returns>دسته بندی با دیتای جدید</returns>
	public async Task<Result<ValueResponseViewModel>> ChangeActivationAsync(string id)
	{
		string url = $"/change-activation/{id}";

		var result =
			await PutAsync
				<object, Result<ValueResponseViewModel>>
				(url, new { });

		return result;
	}

	#endregion

	#region PUT : /check-confirmed/{id}

	/// <summary>
	///  تایید اطلاعات یک مقدار که توسط فروشگاه های دیگر ثبت شده
	/// </summary>
	/// <param name="id">شناسه مقدار</param>
	/// <param name="isConfirmed">تایید شدن یا نشدن</param>
	/// <returns>نتیجه با تغییرات نهایی</returns>
	/// <exception cref="ArgumentNullException"></exception>
	public async Task<Result<ValueResponseViewModel>> CheckConfirmedAsync(string id, bool isConfirmed)
	{
		string url = $"/check-confirmed/{id}?{nameof(isConfirmed)}={isConfirmed}";

		var result =
			await PutAsync
				<object, Result<ValueResponseViewModel>>
				(url, new { });

		return result;
	}

	#endregion PUT : /check-confirmed/{id}
	
	#region PUT : /delete-image/{id}

	/// <summary>
	/// حذف تصویر یک دسته بندی
	/// </summary>
	/// <param name="id">شناسه دسته بندی</param>
	/// <returns>مدل تغییر یافته از دسته بندی</returns>
	public async Task<Result<ValueResponseViewModel>> DeleteImageAsync(string id)
	{
		string url = $"delete-image/{id}";

		var result =
			await PutAsync
				<object, Result<ValueResponseViewModel>>
				(url, new { });

		return result;
	}

	#endregion

	#region DELETE : /

	/// <summary>
	/// حذف یک دسته بندی با شناسه آن
	/// </summary>
	/// <param name="id">شناسه دسته بندی</param>
	/// <returns>در صورت حذف این دسته بندی آیدی به شما برگردانده میشود</returns>
	public async Task<Result<string>> DeleteAsync(string id)
	{
		string url = $"{id}";

		var result =
			await base.DeleteAsync<Result<string>>(url);

		return result;
	}

	#endregion
}