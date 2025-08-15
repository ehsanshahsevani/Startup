using SampleResult;
using ViewModels.Shared;
using HttpServiceSeedworks;
using ViewModels.Marketplace;
using InfrastructureSeedworks;
using ViewModels.ModelParameters;
using BaseProject.Model.ViewModel.Public;
using Enums.SharedService;

namespace HttpServices.Marketplace
{
    public class CityService : HttpService
    {
        public CityService() : base(ServerSettings.DomainApiMarketPlace)
        {
            SetBaseApi(nameof(Resources.DataDictionary.City));
        }

        #region GET : /dropdown-data

        /// <summary>
        /// دریافت لیست نام و آیدی ها برای استفاده در بخش های مختلف دیزاین
        /// </summary>
        /// <returns>نتیجه عملیات شامل لیست نام و آیدی‌ها</returns>
        public async Task<Result<List<UiSelectModel>>> GetDropDownDataAsync(string provinceId)
        {
            string url = $"dropdown-data/{provinceId}";

            var result = await base.GetAsync<Result<List<UiSelectModel>>>(url);

            return result;
        }

        #endregion GET : /dropdown-data
    }
}
