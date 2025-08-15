using SampleResult;
using Enums.SharedService;
using ViewModels.Marketplace;
using InfrastructureSeedworks;

namespace HttpServices.Marketplace
{
	public class TalaSootSettingsService : HttpServiceSeedworks.HttpService
	{
		public TalaSootSettingsService() : base(ServerSettings.DomainApiMarketPlace)
		{
			SetBaseApi(nameof(Resources.DataDictionary.TalaSootSettings));
		}

		#region GET : /

		/// <summary>
		/// دریافت تنظیمات
		/// </summary>
		public async Task<Result<TalaSootSettingsResponseViewModel>> GetSettingsAsync()
		{
			string url = "";

			var result = await GetAsync<Result<TalaSootSettingsResponseViewModel>>(url);

			return result;
		}

		#endregion /GET : /

		#region PUT : /

		/// <summary>
		/// ویرایش تنظیمات
		/// </summary>
		public async Task<Result<TalaSootSettingsResponseViewModel>> UpdateAsync(TalaSootSettingsRequestViewModel model)
		{
			string url = "";

			var result = await PutAsync<TalaSootSettingsRequestViewModel, Result<TalaSootSettingsResponseViewModel>>(
				url,
				model,
				ContentType.MultipartFormData);

			return result;
		}

		#endregion /PUT : /
	}
}