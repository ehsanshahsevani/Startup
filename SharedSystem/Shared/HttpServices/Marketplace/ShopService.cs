using BaseProject.Model.ViewModel.Public;
using Enums.SharedService;
using InfrastructureSeedworks;
using Microsoft.AspNetCore.Http;
using SampleResult;
using ViewModels.Marketplace;
using ViewModels.ModelParameters;
using ViewModels.Shared;

namespace HttpServices.Marketplace;

/// <summary>
/// مدیریت فروشگاه ها
/// </summary>
public class ShopService : HttpServiceSeedworks.HttpService
{
    public ShopService() : base(ServerSettings.DomainApiMarketPlace)
    {
        SetBaseApi(nameof(Resources.DataDictionary.Shop));
    }

    #region GET : /

    /// <summary>
    /// دریافت لیست همه فروشگاه هابه صورت صفحه بندی شده
    /// </summary>
    /// <returns></returns>
    public async Task<Result<PagedListResult<ShopResponseViewModel>>> GetAsync(ShopParameters parameters)
    {
        string url = "";

        var queries =
            GetPropertiesWithValues(parameters);

        var result =
            await base.GetAsync
                <Result<PagedListResult<ShopResponseViewModel>>>(url, queries);

        return result;
    }

    #endregion

    #region GET : {id}

    /// <summary>
    /// دریافت یک فروشگاه با استفاده از شناسه آن
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Result<ShopResponseViewModel>> GetByIdAsync(string id)
    {
        string url = $"{id}";

        var result =
            await base.GetAsync<Result<ShopResponseViewModel>>(url);

        return result;
    }

    #endregion


    #region GET : /tags

    /// <summary>
    /// دریافت تگ ها برای مدیریت مکان و نمایش فروشگاه
    /// </summary>
    /// <returns></returns>
    public async Task<Result<Dictionary<string, string>>> GetTagsAsync()
    {
        string url = "tags";

        Result<Dictionary<string, string>> result =
            await GetAsync<Result<Dictionary<string, string>>>(url);

        return result;
    }

    #endregion


    #region GET : /dropdown-data

    /// <summary>
    /// دریافت لیست نام و آیدی ها برای استفاده در بخش های مختلف دیزاین
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
    /// ایجاد فروشگاه
    /// </summary>
    /// <param name="model">عنوان فروشگاه</param>
    /// <returns></returns>
    public async Task<Result<ShopResponseViewModel>> CreateAsync(ShopRequestViewModel model)
    {
        string url = "";
        
        var files = new Dictionary<string, IFormFile>();
        
        files = new Dictionary<string, IFormFile>
        {
            [nameof(model.FileUpload)] = model.FileUpload,
            [nameof(model.NationalCardBack)] = model.NationalCardBack,
            [nameof(model.NationalCardFront)] = model.NationalCardFront,
            [nameof(model.VatCertificateImage)] = model.VatCertificateImage,
            [nameof(model.OfficialGazetteImage)] = model.OfficialGazetteImage,
        };
        
        var result = await PostAsync<ShopRequestViewModel, Result<ShopResponseViewModel>>(
            url,
            model,
            ContentType.MultipartFormData,
            files
        );

        return result!;
    }

    #endregion


    #region PUT : /

    /// <summary>
    /// ویرایش  فروشگاه
    /// </summary>
    /// <param name="model">مدل درخواستی تغییرات خود را اعمال کنید</param>
    /// <returns>مدل نهایی و جدید</returns>
    public async Task<Result<ShopResponseViewModel>> UpdateAsync(ShopRequestViewModel model)
    {
        string url = ""; // مقداردهی URL به API

        var result = await PutAsync<ShopRequestViewModel, Result<ShopResponseViewModel>>(
            url,
            model,
            ContentType.MultipartFormData
        );

        return result;
    }

    #endregion


    #region PUT : /set-tags

    /// <summary>
    /// ویرایش تگ ها
    /// </summary>
    /// <param name="id">شناسه فروشگاه</param>
    /// <param name="tags">تگ های سیستمی</param>
    /// <returns>فروشگاه ویرایش شده</returns>
    public async Task<Result<ShopResponseViewModel>> SetTagsAsync(string id, List<string> tags)
    {
        string url = $"set-tags/{id}";

        var result =
            await PutAsync
                <List<string>, Result<ShopResponseViewModel>>(url, tags);

        return result;
    }

    #endregion
    

    #region PUT : /change-activation/{id}

    /// <summary>
    /// تغییر وضعیت  فروشگاه
    /// </summary>
    /// <param name="id">شناسه فروشگاه</param>
    /// <returns>فروشگاه با دیتای جدید</returns>
    public async Task<Result<ShopResponseViewModel>> ChangeActivationAsync(string id)
    {
        string url = $"/change-activation/{id}";

        var result =
            await PutAsync
                <object, Result<ShopResponseViewModel>>
                (url, new { });

        return result;
    }

    #endregion


    #region PUT : /delete-image/{id}

    /// <summary>
    /// حذف تصویر یک فروشگاه
    /// </summary>
    /// <param name="id">شناسه دسته بندی</param>
    /// <returns>مدل تغییر یافته از دسته بندی</returns>
    public async Task<Result<ShopResponseViewModel>> DeleteImageAsync(string id)
    {
        string url = $"delete-image/{id}";

        var result =
            await PutAsync
                <object, Result<ShopResponseViewModel>>
                (url, new { });

        return result;
    }

    #endregion

    #region DELETE : /

    /// <summary>
    /// حذف یک فروشگاه با شناسه آن
    /// </summary>
    /// <param name="id">شناسه فروشگاه</param>
    /// <returns>در صورت حذف این فروشگاه آیدی به شما برگردانده میشود</returns>
    public async Task<Result<string>> DeleteAsync(string id)
    {
        string url = $"{id}";

        var result =
            await base.DeleteAsync<Result<string>>(url);

        return result;
    }

    #endregion
    
}