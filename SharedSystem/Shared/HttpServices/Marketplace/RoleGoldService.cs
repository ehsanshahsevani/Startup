using BaseProject.Model.ViewModel.Public;
using SampleResult;
using ViewModels.Shared;
using Enums.SharedService;
using ViewModels.Marketplace;
using InfrastructureSeedworks;
using ViewModels.ModelParameters;

namespace HttpServices.Marketplace
{
    public class RoleGoldService : HttpServiceSeedworks.HttpService
    {
        public RoleGoldService() : base(ServerSettings.DomainApiMarketPlace)
        {
            SetBaseApi(nameof(Resources.DataDictionary.RoleGold));
        }

        #region GET : /

        /// <summary>
        /// دریافت لیست همه به صورت صفحه بندی شده
        /// </summary>
        /// <param name="parameters">پارامترهای صفحه بندی</param>
        /// <returns>نتیجه عملیات شامل لیست صفحه بندی شده</returns>
        public async Task<Result<PagedListResult<RoleGoldResponseViewModel>>> GetAsync(RoleGoldParameters parameters)
        {
            string url = "";

            var queries = GetPropertiesWithValues(parameters);

            var result = await base.GetAsync<Result<PagedListResult<RoleGoldResponseViewModel>>>(url, queries);

            return result;
        }

        #endregion GET : /

        #region GET : {id}

        /// <summary>
        /// دریافت قوانین خرید طلا با استفاده از شناسه آن
        /// </summary>
        /// <param name="id">شناسه قانون خرید طلا</param>
        /// <returns>نتیجه عملیات شامل مدل قانون خرید طلا</returns>
        public async Task<Result<RoleGoldResponseViewModel>> GetByIdAsync(string id)
        {
            string url = $"{id}";

            var result = await base.GetAsync<Result<RoleGoldResponseViewModel>>(url);

            return result;
        }

        #endregion GET : {id}

        #region GET : /dropdown-data

        /// <summary>v
        /// دریافت لیست ساعت های مورد نیاز برای ثبت قوانین
        /// </summary>
        /// <returns>نتیجه عملیات شامل لیست ساعت هاست</returns>
        public async Task<Result<List<UiSelectModel>>> GetDropDownTimeDataAsync()
        {
            string url = $"time-data";

            var result = await base.GetAsync<Result<List<UiSelectModel>>>(url);

            return result;
        }

        #endregion GET : /dropdown-data
        
        #region POST : /

        /// <summary>
        /// ایجاد قانون خرید طلا
        /// </summary>
        /// <param name="model">مدل قانون خرید طلا</param>
        /// <returns>نتیجه عملیات و مدل ایجاد شده</returns>
        public async Task<Result<RoleGoldResponseViewModel>> CreateAsync(RoleGoldRequestViewModel model)
        {
            string url = "";

            var result = await PostAsync<RoleGoldRequestViewModel, Result<RoleGoldResponseViewModel>>(
                url,
                model,
                ContentType.MultipartFormData);

            return result;
        }

        #endregion POST : /

        #region PUT : /

        /// <summary>
        /// ویرایش قانون خرید طلا
        /// </summary>
        /// <param name="model">مدل ویرایش شده</param>
        /// <returns>نتیجه عملیات و مدل ویرایش شده</returns>
        public async Task<Result<RoleGoldResponseViewModel>> UpdateAsync(RoleGoldRequestViewModel model)
        {
            string url = "";

            var result = await PutAsync<RoleGoldRequestViewModel, Result<RoleGoldResponseViewModel>>(
                url,
                model,
                ContentType.MultipartFormData);

            return result;
        }

        #endregion PUT : /

        #region PUT : change-activation/{id}

        /// <summary>
        /// تغییر وضعیت قانون خرید طلا
        /// </summary>
        /// <param name="id">شناسه قانون خرید طلا</param>
        /// <returns>نتیجه عملیات و مدل به روز شده</returns>
        public async Task<Result<RoleGoldResponseViewModel>> ChangeActivationAsync(string id)
        {
            string url = $"change-activation/{id}";

            var result = await PutAsync<object, Result<RoleGoldResponseViewModel>>(url, new { });

            return result;
        }

        #endregion PUT : change-activation/{id}

        #region DELETE : {id}

        /// <summary>
        /// حذف قانون خرید طلا با شناسه
        /// </summary>
        /// <param name="id">شناسه قانون خرید طلا</param>
        /// <returns>نتیجه عملیات شامل شناسه حذف شده</returns>
        public async Task<Result<string>> DeleteAsync(string id)
        {
            string url = $"{id}";

            var result = await base.DeleteAsync<Result<string>>(url);

            return result;
        }

        #endregion DELETE : {id}
    }
}
