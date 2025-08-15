using Enums.SharedService;
using SampleResult;
using ViewModels.Marketplace;
using InfrastructureSeedworks;
using ViewModels.ModelParameters;
using ViewModels.Shared;

namespace HttpServices.Marketplace;

public class DiscountService : HttpServiceSeedworks.HttpService
{
    public DiscountService() : base(ServerSettings.DomainApiMarketPlace)
    {
        SetBaseApi(nameof(Resources.DataDictionary.Discount));
    }

    #region GET : /

    /// <summary>
    /// دریافت لیست همه به صورت صفحه بندی شده
    /// مدل خروجی با دو کلید با نام های
    /// - data , metaData
    /// ارسال میشوند
    /// </summary>
    /// <param name="parameters">پارامترهای صفحه بندی</param>
    /// <returns>نتیجه عملیات شامل لیست صفحه بندی شده</returns>
    public async Task<Result<PagedListResult<DiscountResponseViewModel>>> GetAsync(DiscountParameters parameters)
    {
        string url = "";

        var queries = GetPropertiesWithValues(parameters);

        var result = await base.GetAsync<Result<PagedListResult<DiscountResponseViewModel>>>(url, queries);

        return result;
    }

    #endregion GET : /

    #region GET : {id}

    /// <summary>
    /// دریافت با استفاده از شناسه آن
    /// </summary>
    /// <param name="id">شناسه تخفیف</param>
    /// <returns>نتیجه عملیات شامل مدل تخفیف</returns>
    public async Task<Result<DiscountResponseViewModel>> GetByIdAsync(string id)
    {
        string url = $"{id}";

        var result = await base.GetAsync<Result<DiscountResponseViewModel>>(url);

        return result;
    }

    #endregion GET : {id}

    #region POST : /

    /// <summary>
    /// ایجاد تخفیف جدید
    /// </summary>
    /// <param name="model">مدل تخفیف</param>
    /// <returns>نتیجه عملیات و مدل تخفیف ایجاد شده</returns>
    public async Task<Result<DiscountResponseViewModel>> CreateAsync(DiscountRequestViewModel model)
    {
        string url = "";

        var result = await PostAsync<DiscountRequestViewModel, Result<DiscountResponseViewModel>>(
            url,
            model,
            ContentType.MultipartFormData);

        return result!;
    }

    #endregion POST : /

    #region PUT : /

    /// <summary>
    /// ویرایش تخفیف
    /// </summary>
    /// <param name="model">مدل تخفیف ویرایش شده</param>
    /// <returns>نتیجه عملیات و مدل تخفیف ویرایش شده</returns>
    public async Task<Result<DiscountResponseViewModel>> UpdateAsync(DiscountRequestViewModel model)
    {
        string url = "";

        var result = await PutAsync<DiscountRequestViewModel, Result<DiscountResponseViewModel>>(
            url,
            model,
            ContentType.MultipartFormData);

        return result;
    }

    #endregion PUT : /

    #region PUT : change-activation/{id}

    /// <summary>
    /// تغییر وضعیت فعال یا غیرفعال بودن
    /// </summary>
    /// <param name="id">شناسه تخفیف</param>
    /// <returns>نتیجه عملیات و مدل تخفیف به‌روز شده</returns>
    public async Task<Result<DiscountResponseViewModel>> ChangeActivationAsync(string id)
    {
        string url = $"change-activation/{id}";

        var result = await PutAsync<object, Result<DiscountResponseViewModel>>(url, new { });

        return result;
    }

    #endregion PUT : change-activation/{id}

    #region DELETE : {id}

    /// <summary>
    /// حذف تخفیف با شناسه
    /// </summary>
    /// <param name="id">شناسه تخفیف</param>
    /// <returns>نتیجه عملیات شامل شناسه حذف شده</returns>
    public async Task<Result<string>> DeleteAsync(string id)
    {
        string url = $"{id}";

        var result = await base.DeleteAsync<Result<string>>(url);

        return result;
    }

    #endregion DELETE : {id}
}