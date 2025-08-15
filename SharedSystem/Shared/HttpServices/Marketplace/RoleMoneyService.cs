using SampleResult;
using ViewModels.Shared;
using Enums.SharedService;
using ViewModels.Marketplace;
using InfrastructureSeedworks;
using ViewModels.ModelParameters;
using BaseProject.Model.ViewModel.Public;

namespace HttpServices.Marketplace;

public class RoleMoneyService : HttpServiceSeedworks.HttpService
{
    public RoleMoneyService() : base(ServerSettings.DomainApiMarketPlace)
    {
        SetBaseApi(nameof(Resources.DataDictionary.RoleMoney));
    }

    #region GET : /

    /// <summary>
    /// دریافت لیست همه به صورت صفحه بندی شده
    /// </summary>
    public async Task<Result<PagedListResult<RoleMoneyResponseViewModel>>> GetAsync(RoleMoneyParameters parameters)
    {
        string url = "";
        var queries = GetPropertiesWithValues(parameters);

        var result = await GetAsync<Result<PagedListResult<RoleMoneyResponseViewModel>>>(url, queries);

        return result;
    }

    #endregion GET : /

    #region GET : {id}

    /// <summary>
    /// دریافت یک قانون مالی با شناسه آن
    /// </summary>
    public async Task<Result<RoleMoneyResponseViewModel>> GetByIdAsync(string id)
    {
        string url = $"{id}";

        var result = await GetAsync<Result<RoleMoneyResponseViewModel>>(url);

        return result;
    }

    #endregion GET : {id}

    #region GET : /time-data

    /// <summary>
    /// دریافت لیست ساعت‌های مجاز برای ثبت قانون مالی
    /// </summary>
    public async Task<Result<List<UiSelectModel>>> GetTimeDataAsync()
    {
        string url = "time-data";

        var result = await GetAsync<Result<List<UiSelectModel>>>(url);

        return result;
    }

    #endregion GET : /time-data

    #region POST : /

    /// <summary>
    /// ایجاد یک قانون مالی جدید
    /// </summary>
    public async Task<Result<RoleMoneyResponseViewModel>> CreateAsync(RoleMoneyRequestViewModel model)
    {
        string url = "";

        var result = await PostAsync<RoleMoneyRequestViewModel, Result<RoleMoneyResponseViewModel>>(
            url,
            model,
            ContentType.MultipartFormData);

        return result;
    }

    #endregion POST : /

    #region PUT : /

    /// <summary>
    /// ویرایش یک قانون مالی موجود
    /// </summary>
    public async Task<Result<RoleMoneyResponseViewModel>> UpdateAsync(RoleMoneyRequestViewModel model)
    {
        string url = "";

        var result = await PutAsync<RoleMoneyRequestViewModel, Result<RoleMoneyResponseViewModel>>(
            url,
            model,
            ContentType.MultipartFormData);

        return result;
    }

    #endregion PUT : /

    #region PUT : /change-activation/{id}

    /// <summary>
    /// تغییر وضعیت فعال/غیرفعال قانون مالی
    /// </summary>
    public async Task<Result<RoleMoneyResponseViewModel>> ChangeActivationAsync(string id)
    {
        string url = $"change-activation/{id}";

        var result = await PutAsync<object, Result<RoleMoneyResponseViewModel>>(url, new { });

        return result;
    }

    #endregion PUT : /change-activation/{id}

    #region DELETE : /

    /// <summary>
    /// حذف قانون مالی با شناسه آن
    /// </summary>
    public async Task<Result<string>> DeleteAsync(string id)
    {
        string url = $"{id}";

        var result = await DeleteAsync<Result<string>>(url);

        return result;
    }

    #endregion DELETE : /
}