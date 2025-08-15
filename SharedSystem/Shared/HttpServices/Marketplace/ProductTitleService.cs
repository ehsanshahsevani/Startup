using SampleResult;
using ViewModels.Shared;
using ViewModels.Marketplace;
using InfrastructureSeedworks;
using ViewModels.ModelParameters;
using BaseProject.Model.ViewModel.Public;
using Enums.SharedService;

namespace HttpServices.Marketplace;

/// <summary>
/// مدیریت مقادیر
/// </summary>
public class ProductTitleService : HttpServiceSeedworks.HttpService
{
	public ProductTitleService() : base(ServerSettings.DomainApiMarketPlace)
	{
		SetBaseApi(nameof(Resources.DataDictionary.ProductTitle));
	}

	#region GET : /

	/// <summary>
	/// دریافت لیست همه عنوان محصول ها به صورت صفحه بندی شده
	/// </summary>
	/// <returns></returns>
	public async Task<Result<PagedListResult<ProductTitleResponseViewModel>>> GetAsync(ProductTitleParameters parameters)
	{
		string url = "";

		var queries =
			GetPropertiesWithValues(parameters);

		var result =
			await base.GetAsync
				<Result<PagedListResult<ProductTitleResponseViewModel>>>(url, queries);

		return result;
	}

	#endregion

	#region GET : {id}

	/// <summary>
	/// دریافت یک عنوان محصول با استفاده از شناسه آن
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<Result<ProductTitleResponseViewModel>> GetByIdAsync(string id)
	{
		string url = $"{id}";

		var result =
			await base.GetAsync<Result<ProductTitleResponseViewModel>>(url);

		return result;
	}

	#endregion

	#region GET : /dropdown-data

	/// <summary>
	/// دریافت لیست نام و آیدی های هر عنوان محصول برای استفاده در بخش های مختلف دیزاین
	/// </summary>
	/// <returns></returns>
	public async Task<Result<List<UiSelectModel>>> GetDropDownDataForShopIdAsync(string shopId)
	{
		string url = $"dropdown-data-by-shop?shopId={shopId}";

		var result =
			await base.GetAsync<Result<List<UiSelectModel>>>(url);

		return result;
	}
	
	/// <summary>
	/// دریافت لیست نام و آیدی های هر عنوان محصول برای استفاده در بخش های مختلف دیزاین
	/// </summary>
	/// <returns></returns>
	public async Task<Result<List<UiSelectModel>>> GetDropDownDataAsync()
	{
		string url = $"dropdown-data";

		var result =
			await base.GetAsync<Result<List<UiSelectModel>>>(url);

		return result;
	}

	#endregion
	
	#region POST : /

	/// <summary>
	/// ایجاد عنوان محصول
	/// </summary>
	/// <param name="model">عنوان محصول</param>
	/// <returns></returns>
	public async Task<Result<ProductTitleResponseViewModel>> CreateAsync(ProductTitleRequestViewModel model)
	{
		string url = "";

		var result = await PostAsync<ProductTitleRequestViewModel, Result<ProductTitleResponseViewModel>>(
			url,
			model,
			ContentType.MultipartFormData
		);

		return result!;
	}

	#endregion

	#region PUT : /

	/// <summary>
	/// ویرایش عنوان محصول
	/// </summary>
	/// <param name="model">مدل درخواستی تغییرات خود را اعمال کنید</param>
	/// <returns>مدل نهایی و جدید</returns>
	public async Task<Result<ProductTitleResponseViewModel>> UpdateAsync(ProductTitleRequestViewModel model)
	{
    string url = ""; // مقداردهی URL به API

    var result = await PutAsync<ProductTitleRequestViewModel, Result<ProductTitleResponseViewModel>>(
        url,
        model,
        ContentType.MultipartFormData
    );

    return result;
}

	#endregion

	#region PUT : /change-activation/{id}

	/// <summary>
	/// تغییر وضعیت عنوان محصول
	/// </summary>
	/// <param name="id">شناسه عنوان محصول</param>
	/// <returns>عنوان محصول با دیتای جدید</returns>
	public async Task<Result<ProductTitleResponseViewModel>> ChangeActivationAsync(string id)
	{
		string url = $"/change-activation/{id}";

		var result =
			await PutAsync
				<object, Result<ProductTitleResponseViewModel>>
				(url, new { });

		return result;
	}

	#endregion
	
	#region DELETE : /

	/// <summary>
	/// حذف یک عنوان محصول با شناسه آن
	/// </summary>
	/// <param name="id">شناسه عنوان محصول</param>
	/// <returns>در صورت حذف این عنوان محصول آیدی به شما برگردانده میشود</returns>
	public async Task<Result<string>> DeleteAsync(string id)
	{
		string url = $"{id}";

		var result =
			await base.DeleteAsync<Result<string>>(url);

		return result;
	}

	#endregion
}