using SampleResult;
using ViewModels.Shared;
using Enums.SharedService;
using ViewModels.Marketplace;
using InfrastructureSeedworks;
using Microsoft.AspNetCore.Http;
using ViewModels.ModelParameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpServices.Marketplace;

/// <summary>
/// مدیریت تاریخچه محصولات
/// </summary>
public class ProductHistoryService : HttpServiceSeedworks.HttpService
{
	public ProductHistoryService() : base(ServerSettings.DomainApiMarketPlace)
	{
		SetBaseApi(nameof(Resources.DataDictionary.ProductHistory));
	}

	#region GET : /list/{productId}

	/// <summary>
	/// دریافت لیست همه به صورت صفحه بندی شده
	/// مدل خروجی با دو کلید با نام های
	/// - data , metaData
	/// ارسال میشوند
	/// </summary>
	/// <param name="productId">شناسه محصول</param>
	/// <param name="parameters">پارامترهای صفحه‌بندی و فیلتر</param>
	/// <returns>نتیجه عملیات شامل صفحه‌بندی لیست تاریخچه محصول</returns>
	public async Task<Result<PagedListResult<ProductHistoryResponseViewModel>>>
		GetAsync(string productId, ProductHistoryParameters parameters)
	{
		string url = $"list/{productId}";

		var queries = GetPropertiesWithValues(parameters);

		var result = await base.GetAsync<Result<PagedListResult<ProductHistoryResponseViewModel>>>(url, queries);

		return result;
	}

	#endregion GET : /list/{productId}

	#region GET : {id}

	/// <summary>
	/// دریافت با استفاده از شناسه آن
	/// </summary>
	/// <param name="id">شناسه تاریخچه محصول</param>
	/// <returns>نتیجه عملیات شامل مدل تاریخچه محصول</returns>
	public async Task<Result<ProductHistoryResponseViewModel>> GetByIdAsync(string id)
	{
		string url = $"{id}";

		var result = await base.GetAsync<Result<ProductHistoryResponseViewModel>>(url);

		return result;
	}

	#endregion GET : {id}

	#region POST : /

	/// <summary>
	/// ایجاد
	/// </summary>
	/// <param name="model">مدل تاریخچه محصول</param>
	/// <returns>نتیجه عملیات ایجاد تاریخچه محصول</returns>
	public async Task<Result<ProductResponseViewModel>> CreateAsync(ProductHistoryRequestViewModel model)
	{
		string url = "";

		var result = await PostAsync<ProductHistoryRequestViewModel, Result<ProductResponseViewModel>>(url, model, ContentType.MultipartFormData);

		return result!;
	}

	#endregion POST : /

	#region PUT : /

	/// <summary>
	/// ویرایش
	/// </summary>
	/// <param name="model">مدل تاریخچه محصول برای ویرایش</param>
	/// <returns>نتیجه عملیات ویرایش تاریخچه محصول</returns>
	public async Task<Result<ProductResponseViewModel>> UpdateAsync(ProductHistoryRequestViewModel model)
	{
		string url = "";

		var result = await PutAsync<ProductHistoryRequestViewModel, Result<ProductResponseViewModel>>(url, model, ContentType.MultipartFormData);

		return result;
	}

	#endregion PUT : /

	#region DELETE : /

	/// <summary>
	/// حذف با شناسه آن
	/// </summary>
	/// <param name="id">شناسه تاریخچه محصول</param>
	/// <returns>نتیجه عملیات حذف شامل شناسه حذف شده</returns>
	public async Task<Result<string>> DeleteAsync(string id)
	{
		string url = $"{id}";

		var result = await base.DeleteAsync<Result<string>>(url);

		return result;
	}

	#endregion DELETE : /

	#region POST : /price-changer

	/// <summary>
	/// تغییر وضعیت قیمت
	/// </summary>
	/// <param name="requestModel">مدل درخواست تغییر قیمت</param>
	/// <returns>نتیجه عملیات شامل مدل به‌روزرسانی شده تاریخچه محصول</returns>
	/// <exception cref="ArgumentNullException"></exception>
	public async Task<Result<ProductResponseViewModel>> PriceChangerAsync(PriceChangeRequestModel requestModel)
	{
		string url = "price-changer";

		var result = await PostAsync<PriceChangeRequestModel, Result<ProductResponseViewModel>>(url, requestModel);

		return result!;
	}

	#endregion POST : /price-changer

	#region POST : /change-request-check-price/{productId}

	/// <summary>
	/// تغییر به وضعیت استعلام قیمت
	/// </summary>
	/// <param name="productId">شناسه محصول</param>
	/// <returns>نتیجه عملیات شامل مدل تاریخچه محصول</returns>
	/// <exception cref="ArgumentNullException"></exception>
	public async Task<Result<ProductResponseViewModel>> ChangeToRequestForCheckPriceAsync(string productId)
	{
		string url = $"change-request-check-price/{productId}";

		var result = await PostAsync<object?, Result<ProductResponseViewModel>>(url, null);

		return result!;
	}

	#endregion POST : /change-request-check-price/{productId}
}