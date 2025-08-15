using SampleResult;
using ViewModels.Shared;
using Enums.SharedService;
using ViewModels.Marketplace;
using InfrastructureSeedworks;
using ViewModels.ModelParameters;
using BaseProject.Model.ViewModel.Public;

namespace HttpServices.Marketplace;

/// <summary>
/// مدیریت مقادیر
/// </summary>
public class ProductAttributeValueService : HttpServiceSeedworks.HttpService
{
	public ProductAttributeValueService() : base(ServerSettings.DomainApiMarketPlace)
	{
		SetBaseApi(nameof(Resources.DataDictionary.ProductAttributeValue));
	}

	#region GET : /

	/// <summary>
	/// دریافت لیست همه مقادیر ویژگی محصولات ها به صورت صفحه بندی شده
	/// </summary>
	/// <returns></returns>
	public async Task<Result<PagedListResult<ProductAttributeValueResponseViewModel>>> GetAsync(ProductAttributeValueParameters parameters)
	{
		string url = "";

		var queries =
			GetPropertiesWithValues(parameters);

		var result =
			await base.GetAsync
				<Result<PagedListResult<ProductAttributeValueResponseViewModel>>>(url, queries);

		return result;
	}

	#endregion

	#region GET : {id}

	/// <summary>
	/// دریافت یک مقادیر ویژگی محصولات با استفاده از شناسه آن
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<Result<ProductAttributeValueResponseViewModel>> GetByIdAsync(string id)
	{
		string url = $"{id}";

		var result =
			await base.GetAsync<Result<ProductAttributeValueResponseViewModel>>(url);

		return result;
	}

	#endregion

	#region GET : /dropdown-data

	/// <summary>
	/// دریافت لیست نام و آیدی های هر مقادیر ویژگی محصولات برای استفاده در بخش های مختلف دیزاین
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
	/// ایجاد مقادیر ویژگی محصولات
	/// </summary>
	/// <param name="model">مقادیر ویژگی محصولات</param>
	/// <returns></returns>
	public async Task<Result<ProductAttributeValueResponseViewModel>> CreateAsync(ProductAttributeValueRequestViewModel model)
	{
		string url = "";

		var result = await PostAsync<ProductAttributeValueRequestViewModel, Result<ProductAttributeValueResponseViewModel>>(
			url,
			model,
			ContentType.MultipartFormData
		);

		return result!;
	}

	#endregion

	#region PUT : /

	/// <summary>
	/// ویرایش مقادیر ویژگی محصولات
	/// </summary>
	/// <param name="model">مدل درخواستی تغییرات خود را اعمال کنید</param>
	/// <returns>مدل نهایی و جدید</returns>
	public async Task<Result<ProductAttributeValueResponseViewModel>> UpdateAsync(ProductAttributeValueRequestViewModel model)
	{
    string url = ""; // مقداردهی URL به API

    var result = await PutAsync<ProductAttributeValueRequestViewModel, Result<ProductAttributeValueResponseViewModel>>(
        url,
        model,
        ContentType.MultipartFormData
    );

    return result;
}

	#endregion

	#region PUT : /change-activation/{id}

	/// <summary>
	/// تغییر وضعیت مقادیر ویژگی محصولات
	/// </summary>
	/// <param name="id">شناسه مقادیر ویژگی محصولات</param>
	/// <returns>مقادیر ویژگی محصولات با دیتای جدید</returns>
	public async Task<Result<ProductAttributeValueResponseViewModel>> ChangeActivationAsync(string id)
	{
		string url = $"/change-activation/{id}";

		var result =
			await PutAsync
				<object, Result<ProductAttributeValueResponseViewModel>>
				(url, new { });

		return result;
	}

	#endregion
	
	#region DELETE : /

	/// <summary>
	/// حذف یک مقادیر ویژگی محصولات با شناسه آن
	/// </summary>
	/// <param name="id">شناسه مقادیر ویژگی محصولات</param>
	/// <returns>در صورت حذف این مقادیر ویژگی محصولات آیدی به شما برگردانده میشود</returns>
	public async Task<Result<string>> DeleteAsync(string id)
	{
		string url = $"{id}";

		var result =
			await base.DeleteAsync<Result<string>>(url);

		return result;
	}

	#endregion
}