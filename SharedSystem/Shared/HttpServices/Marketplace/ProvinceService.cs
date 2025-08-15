using SampleResult;
using HttpServiceSeedworks;
using InfrastructureSeedworks;
using BaseProject.Model.ViewModel.Public;

namespace HttpServices.Marketplace
{
    public class ProvinceService : HttpService
    {
        public ProvinceService() : base(ServerSettings.DomainApiMarketPlace)
        {
            SetBaseApi(nameof(Resources.DataDictionary.Province));
        }

        #region GET : /dropdown-data

        /// <summary>v
        /// دریافت لیست نام و آیدی ها برای استفاده در بخش های مختلف دیزاین
        /// </summary>
        /// <returns>نتیجه عملیات شامل لیست نام و آیدی‌ها</returns>
        public async Task<Result<List<UiSelectModel>>> GetDropDownDataAsync()
        {
            string url = $"dropdown-data";

            var result = await base.GetAsync<Result<List<UiSelectModel>>>(url);

            return result;
        }

        #endregion GET : /dropdown-data
    }
}
