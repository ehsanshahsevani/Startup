using SampleResult;
using ViewModels.Shared;
using Enums.SharedService;
using ViewModels.Marketplace;
using InfrastructureSeedworks;
using Microsoft.AspNetCore.Http;
using ViewModels.ModelParameters;
using BaseProject.Model.ViewModel.Public;

namespace HttpServices.Marketplace
{
    public class BranchService : HttpServiceSeedworks.HttpService
    {
        public BranchService() : base(ServerSettings.DomainApiMarketPlace)
        {
            SetBaseApi(nameof(Resources.DataDictionary.Branch));
        }

        #region GET : /

        /// <summary>
        /// دریافت لیست همه به صورت صفحه بندی شده
        /// </summary>
        /// <param name="parameters">پارامترهای صفحه بندی</param>
        /// <returns>نتیجه عملیات شامل لیست صفحه بندی شده</returns>
        public async Task<Result<PagedListResult<BranchResponseViewModel>>> GetAsync(BranchParameters parameters)
        {
            string url = "";

            var queries = GetPropertiesWithValues(parameters);

            var result = await base.GetAsync<Result<PagedListResult<BranchResponseViewModel>>>(url, queries);

            return result;
        }

        #endregion GET : /

        #region GET : {id}

        /// <summary>
        /// دریافت شعبه با استفاده از شناسه آن
        /// </summary>
        /// <param name="id">شناسه شعبه</param>
        /// <returns>نتیجه عملیات شامل مدل شعبه</returns>
        public async Task<Result<BranchResponseViewModel>> GetByIdAsync(string id)
        {
            string url = $"{id}";

            var result = await base.GetAsync<Result<BranchResponseViewModel>>(url);

            return result;
        }

        #endregion GET : {id}

        #region GET : /dropdown-data

        /// <summary>
        /// دریافت لیست نام و آیدی ها برای استفاده در بخش های مختلف دیزاین
        /// </summary>
        /// <returns>نتیجه عملیات شامل لیست نام و آیدی‌ها</returns>
        public async Task<Result<List<UiSelectModel>>> GetDropDownDataAsync()
        {
            string url = "dropdown-data";

            var result = await base.GetAsync<Result<List<UiSelectModel>>>(url);

            return result;
        }

        #endregion GET : /dropdown-data

        #region GET : /get-time-in-day

        /// <summary>
        /// دریافت ساعات کاری مجاز برای هر شعبه
        /// </summary>
        /// <returns>لیستی از ساعات کاری مجاز</returns>
        public async Task<Result<List<UiSelectModel>>> GetTimeInDayAsync()
        {
            string url = "get-time-in-day";

            var result = await base.GetAsync<Result<List<UiSelectModel>>>(url);

            return result;
        }

        #endregion GET : /get-time-in-day

        #region GET : /get-day-in-week

        /// <summary>
        /// دریافت روزهای کاری در هفته برای هر شعبه
        /// </summary>
        /// <returns>لیستی از روزهای کاری</returns>
        public async Task<Result<List<UiSelectModel>>> GetDayInWeekAsync()
        {
            string url = "get-day-in-week";

            var result = await base.GetAsync<Result<List<UiSelectModel>>>(url);

            return result;
        }

        #endregion GET : /get-day-in-week

        #region POST : /

        /// <summary>
        /// ایجاد شعبه جدید
        /// </summary>
        /// <param name="model">مدل شعبه</param>
        /// <returns>نتیجه عملیات و مدل شعبه ایجاد شده</returns>
        public async Task<Result<BranchResponseViewModel>> CreateAsync(BranchRequestViewModel model)
        {
            string url = "";

            var files = new Dictionary<string, IFormFile>();
		
            if (model.FileUpload is not null)
            {
                files["fileUpload"] = model.FileUpload;
            }
            
            var result = await PostAsync<BranchRequestViewModel, Result<BranchResponseViewModel>>(
                url,
                model,
                ContentType.MultipartFormData,
                files);

            return result;
        }

        #endregion POST : /

        #region PUT : /

        /// <summary>
        /// ویرایش شعبه
        /// </summary>
        /// <param name="model">مدل شعبه ویرایش شده</param>
        /// <returns>نتیجه عملیات و مدل شعبه ویرایش شده</returns>
        public async Task<Result<BranchResponseViewModel>> UpdateAsync(BranchRequestViewModel model)
        {
            string url = "";

            var result = await PutAsync<BranchRequestViewModel, Result<BranchResponseViewModel>>(
                url,
                model,
                ContentType.MultipartFormData);

            return result;
        }

        #endregion PUT : /

        #region PUT : change-activation/{id}

        /// <summary>
        /// تغییر وضعیت شعبه
        /// </summary>
        /// <param name="id">شناسه شعبه</param>
        /// <returns>نتیجه عملیات و مدل شعبه به‌روز شده</returns>
        public async Task<Result<BranchResponseViewModel>> ChangeActivationAsync(string id)
        {
            string url = $"change-activation/{id}";

            var result = await PutAsync<object, Result<BranchResponseViewModel>>(url, new { });

            return result;
        }

        #endregion PUT : change-activation/{id}

        #region DELETE : {id}

        /// <summary>
        /// حذف شعبه با شناسه
        /// </summary>
        /// <param name="id">شناسه شعبه</param>
        /// <returns>نتیجه عملیات شامل شناسه حذف شده</returns>
        public async Task<Result<string>> DeleteAsync(string id)
        {
            string url = $"{id}";

            var result = await base.DeleteAsync<Result<string>>(url);

            return result;
        }

        #endregion DELETE : {id}
        
        #region PUT : update-profile/{id}

        /// <summary>
        /// تغییر تصویر شعبه
        /// </summary>
        /// <param name="id">شناسه شعبه</param>
        /// <param name="file">فایل جدید</param>
        /// <returns>مدل حاوی تصویر جدید</returns>
        public async Task<Result<BranchResponseViewModel>> UpdateProfileAsync(string id, IFormFile file)
        {
            string url = $"update-profile/{id}";

            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var fileDict = new Dictionary<string, IFormFile>
            {
                { "file", file }
            };

            var result = await PutAsync<object, Result<BranchResponseViewModel>>(
                url,
                null,  // دیگر داده‌ها در اینجا ارسال نمی‌شود
                ContentType.MultipartFormData,
                fileDict,
                null);

            return result;
        }

        #endregion PUT : update-profile/{id}
    }
}
