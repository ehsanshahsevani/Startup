using SampleResult;
using ViewModels.Shared;
using ViewModels.Marketplace;
using InfrastructureSeedworks;
using ViewModels.ModelParameters;
using BaseProject.Model.ViewModel.Public;
using Enums.SharedService;

namespace HttpServices.Marketplace;

/// <summary>
/// مدیریت دسته بندی محصولات
/// </summary>
public class CategoryService : HttpServiceSeedworks.HttpService
{
    public CategoryService() : base(ServerSettings.DomainApiMarketPlace)
    {
        SetBaseApi(nameof(Resources.DataDictionary.Category));
    }

    #region GET : /

    /// <summary>
    /// دریافت لیست همه دسته بندی ها به صورت صفحه بندی شده
    /// </summary>
    /// <returns></returns>
    public async Task<Result<PagedListResult<CategoryResponseViewModel>>> GetAsync(CategoryParameters parameters)
    {
        string url = "";

        var queries =
            GetPropertiesWithValues(parameters);

        var result =
            await base.GetAsync
                <Result<PagedListResult<CategoryResponseViewModel>>>(url, queries);

        return result;
    }

    #endregion

    #region GET : {id}

    /// <summary>
    /// دریافت یک دسته بندی با استفاده از شناسه آن
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Result<CategoryResponseViewModel>> GetByIdAsync(string id)
    {
        string url = $"{id}";

        var result =
            await base.GetAsync<Result<CategoryResponseViewModel>>(url);

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

    #region GET : /tags

    /// <summary>
    /// دریافت تگ ها برای مدیریت مکان و نمایش دسته بندی ها
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

    #region POST : /

    /// <summary>
    /// ایجاد دسته بندی
    /// </summary>
    /// <param name="model">دسته بندی</param>
    /// <returns></returns>
    public async Task<Result<CategoryResponseViewModel>> CreateAsync(CategoryRequestViewModel model)
    {
        const string url = "";

        var result = await PostAsync<CategoryRequestViewModel, Result<CategoryResponseViewModel>>(
            url,
            model,
            ContentType.MultipartFormData
        );
        return result!;
    }

    #endregion

    #region PUT : /

    /// <summary>
    /// ویرایش دسته بندی
    /// </summary>
    /// <param name="model">مدل درخواستی تغییرات خود را اعمال کنید</param>
    /// <returns>مدل نهایی و جدید</returns>
    public async Task<Result<CategoryResponseViewModel>> UpdateAsync(CategoryRequestViewModel model)
    {
        const string url = "";

        var result = await PutAsync<CategoryRequestViewModel, Result<CategoryResponseViewModel>>(
            url,
            model,
            ContentType.MultipartFormData
        );

        return result!;
    }

    #endregion

    #region PUT : /set-tags

    /// <summary>
    /// ویرایش تگ ها
    /// </summary>
    /// <param name="id">شناسه محصول</param>
    /// <param name="tags">تگ های سیستمی</param>
    /// <returns>دسته بندی ویرایش شده</returns>
    public async Task<Result<CategoryResponseViewModel>> SetTagsAsync(string id, List<string> tags)
    {
        string url = $"set-tags/{id}";

        var result =
            await PutAsync
                <List<string>, Result<CategoryResponseViewModel>>(url, tags);

        return result;
    }

    #endregion

    #region PUT : /change-activation/{id}

    /// <summary>
    /// تغییر وضعیت دسته بندی
    /// </summary>
    /// <param name="id">شناسه دسته بندی</param>
    /// <returns>دسته بندی با دیتای جدید</returns>
    public async Task<Result<CategoryResponseViewModel>> ChangeActivationAsync(string id)
    {
        string url = $"/change-activation/{id}";

        var result =
            await PutAsync
                <object, Result<CategoryResponseViewModel>>
                (url, new { });

        return result;
    }

    #endregion

    #region PUT : /delete-image/{id}

    /// <summary>
    /// حذف تصویر یک دسته بندی
    /// </summary>
    /// <param name="id">شناسه دسته بندی</param>
    /// <returns>مدل تغییر یافته از دسته بندی</returns>
    public async Task<Result<CategoryResponseViewModel>> DeleteImageAsync(string id)
    {
        string url = $"delete-image/{id}";

        var result =
            await PutAsync
                <object, Result<CategoryResponseViewModel>>
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
    
    #region Feature
    
    #region PUT : /move-category?fromCategoryId={fromCategoryId}&toCategoryId={toCategoryId}

    /// <summary>
    /// جابجایی محصول از یک دسته بندی به دسته بندی دیگر
    /// </summary>
    /// <param name="fromCategoryId">شناسه دسته بندی مبدا</param>
    /// <param name="toCategoryId">شناسه دسته بندی مقصد</param>
    /// <returns>نتیجه عملیات جابجایی محصول بین دسته بندی‌ها</returns>
    public async Task<Result> MoveProductFromCategoryToCategoryAsync(string fromCategoryId, string toCategoryId)
    {
        // URL برای ارسال درخواست به API
        string url = $"move-product-from-category-to-category?{nameof(fromCategoryId)}={fromCategoryId}&{nameof(toCategoryId)}={toCategoryId}";

        // ارسال درخواست PUT به API
        var result = await PutAsync<object?, Result<object>>(url, null);

        return result;
    }

    #endregion PUT : /move-category?fromCategoryId={fromCategoryId}&toCategoryId={toCategoryId}
    
    #region PUT : /move-all-product-to-request-check-price/{categoryId}

    /// <summary>
    /// جابجایی تمامی محصولات یک دسته بندی به وضعیت استعلام قیمت
    /// </summary>
    /// <param name="categoryId">شناسه دسته بندی</param>
    /// <returns>نتیجه عملیات جابجایی محصولات به وضعیت استعلام قیمت</returns>
    public async Task<Result> MoveAllProductToRequestCheckPriceByCategoryIdAsync(string categoryId)
    {
        // URL برای ارسال درخواست PUT به API
        string url = $"move-all-product-to-request-check-price/{categoryId}";

        // ارسال درخواست PUT
        var result = await PutAsync<object, Result<object>>(url, new { });

        return result;
    }

    #endregion PUT : /move-all-product-to-request-check-price/{categoryId}
    
    #endregion /Feature
}