using SampleResult;
using ViewModels.ProjectManager;
using InfrastructureSeedworks;
using ViewModels.Marketplace;

namespace HttpServices.Marketplace;

/// <summary>
/// سرویس مدیریت ساب سیستم‌های محلی
/// </summary>
public class SubSystemLocalService : HttpServiceSeedworks.HttpService
{
    public SubSystemLocalService() : base(ServerSettings.DomainApiMarketPlace)
    {
        SetBaseApi(nameof(Resources.DataDictionary.SubSystemLocal));
    }

    #region GET : /

    /// <summary>
    /// دریافت لیست بخش های مختلف پروژه
    /// </summary>
    /// <returns></returns>
    public async Task<Result<List<SubSystemResponseViewModel>>> GetAsync()
    {
        string url = "";

        var result =
            await base.GetAsync<Result<List<SubSystemResponseViewModel>>>(url);

        return result;
    }

    #endregion

    #region GET : /description/{subsystemName}

    /// <summary>
    /// دریافت توضیحات مربوط به زیر سیستم مورد نظر
    /// </summary>
    /// <param name="subSystemName">نام زیر سیستم مورد نظر</param>
    /// <returns></returns>
    public async Task<Result<string?>> GetDescriptionAsync(string subSystemName)
    {
        string url = $"description/{subSystemName}";

        var result =
            await base.GetAsync<Result<string?>>(url);

        return result;
    }

    #endregion

    #region PUT : /update-description/{id}

    /// <summary>
    /// ویرایش توضیحات اضافی یک زیر سیستم برای پنل ادمین
    /// در تمامی صفحات خوانده میشود
    /// هر صفحه توضیحات و نکات مربوط به خودش را دارد
    /// که ادمین های مختلف باید آنهارا در هر صفحه ببینند
    /// </summary>
    /// <param name="id">شناسه زیر سیستم</param>
    /// <param name="description">توضیحات</param>
    /// <returns></returns>
    public async Task<Result<SubSystemLocalResponseViewModel>> UpdateDescriptionAsync(string id, string description)
    {
        string url = $"update-description/{id}";

        var result =
            await PutAsync<string, Result<SubSystemLocalResponseViewModel>>(url, description);

        return result;
    }

    #endregion
}