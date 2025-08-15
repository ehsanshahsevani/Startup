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
public class AttributeService : HttpServiceSeedworks.HttpService
{
	public AttributeService() : base(ServerSettings.DomainApiMarketPlace)
	{
		SetBaseApi(nameof(Resources.DataDictionary.Attribute));
	}

	#region GET : /

	/// <summary>
	/// دریافت لیست همه ویژگی محصول ها به صورت صفحه بندی شده
	/// </summary>
	/// <returns></returns>
	public async Task<Result<PagedListResult<AttributeResponseViewModel>>> GetAsync(AttributeParameters parameters)
	{
		string url = "";

		var queries =
			GetPropertiesWithValues(parameters);

		var result =
			await base.GetAsync
				<Result<PagedListResult<AttributeResponseViewModel>>>(url, queries);

		return result;
	}

	#endregion

	#region GET : {id}

	/// <summary>
	/// دریافت یک ویژگی محصول با استفاده از شناسه آن
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<Result<AttributeResponseViewModel>> GetByIdAsync(string id)
	{
		string url = $"{id}";

		var result =
			await base.GetAsync<Result<AttributeResponseViewModel>>(url);

		return result;
	}

	#endregion

	#region GET : /dropdown-data

	/// <summary>
	/// دریافت لیست نام و آیدی های هر ویژگی محصول برای استفاده در بخش های مختلف دیزاین
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
	/// ایجاد ویژگی محصول
	/// </summary>
	/// <param name="model">ویژگی محصول</param>
	/// <returns></returns>
	public async Task<Result<AttributeResponseViewModel>> CreateAsync(AttributeRequestViewModel model)
	{
		string url = "";

		var result = await PostAsync<AttributeRequestViewModel, Result<AttributeResponseViewModel>>(
			url,
			model,
			ContentType.MultipartFormData
		);

		return result!;
	}

	#endregion

	#region PUT : /

	/// <summary>
	/// ویرایش ویژگی محصول
	/// </summary>
	/// <param name="model">مدل درخواستی تغییرات خود را اعمال کنید</param>
	/// <returns>مدل نهایی و جدید</returns>
	public async Task<Result<AttributeResponseViewModel>> UpdateAsync(AttributeRequestViewModel model)
	{
    string url = ""; // مقداردهی URL به API

    var result = await PutAsync<AttributeRequestViewModel, Result<AttributeResponseViewModel>>(
        url,
        model,
        ContentType.MultipartFormData
    );

    return result;
}

	#endregion

	#region PUT : /change-activation/{id}

	/// <summary>
	/// تغییر وضعیت ویژگی محصول
	/// </summary>
	/// <param name="id">شناسه ویژگی محصول</param>
	/// <returns>ویژگی محصول با دیتای جدید</returns>
	public async Task<Result<AttributeResponseViewModel>> ChangeActivationAsync(string id)
	{
		string url = $"/change-activation/{id}";

		var result =
			await PutAsync
				<object, Result<AttributeResponseViewModel>>
				(url, new { });

		return result;
	}

	#endregion
	
	#region DELETE : /

	/// <summary>
	/// حذف یک ویژگی محصول با شناسه آن
	/// </summary>
	/// <param name="id">شناسه ویژگی محصول</param>
	/// <returns>در صورت حذف این ویژگی محصول آیدی به شما برگردانده میشود</returns>
	public async Task<Result<string>> DeleteAsync(string id)
	{
		string url = $"{id}";

		var result =
			await base.DeleteAsync<Result<string>>(url);

		return result;
	}

	#endregion
}