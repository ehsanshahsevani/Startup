using SampleResult;
using ViewModels.Marketplace;
using InfrastructureSeedworks;
using BaseProject.Model.ViewModel.Public;

namespace HttpServices.Marketplace
{
	public class TypeRoleGoldService : HttpServiceSeedworks.HttpService
	{
		public TypeRoleGoldService() : base(ServerSettings.DomainApiMarketPlace)
		{
			SetBaseApi(nameof(Resources.DataDictionary.TypeRoleGold));
		}
		
		#region GET : /dropdown-data

		/// <summary>
		/// دریافت لیست نام و آیدی‌ها برای استفاده در بخش‌های مختلف
		/// </summary>
		/// <returns>نتیجه عملیات و لیست انتخابی</returns>
		public async Task<Result<List<UiSelectModel>>> GetDropDownDataAsync()
		{
			string url = "dropdown-data";

			var result = await base.GetAsync<Result<List<UiSelectModel>>>(url);

			return result;
		}

		#endregion GET : /dropdown-data
	}
}