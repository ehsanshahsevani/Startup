using Enums.SharedService;
using HttpServiceSeedworks;
using InfrastructureSeedworks;
using SampleResult;

namespace HttpServices.Marketplace;

public class TreasuryManagerService : HttpService
{
	public TreasuryManagerService() : base(ServerSettings.DomainApiMarketPlace)
	{
		SetBaseApi("TreasuryManager");
	}

	#region GET : /

	/// <summary>
	/// دریافت کارمزدها از بخش‌های مختلف (Box Data)
	/// </summary>
	/// <returns>نتیجه عملیات شامل TreasuryBoxResponseViewModel</returns>
	public async Task<Result<TreasuryBoxResponseViewModel>> GetBoxDataAsync()
	{
		string url = "";

		var result = await base.GetAsync<Result<TreasuryBoxResponseViewModel>>(url);

		return result;
	}

	#endregion

	#region PUT : /

	/// <summary>
	/// به‌روزرسانی کارمزدها از بخش‌های مختلف
	/// </summary>
	/// <param name="model">TreasuryBoxRequestViewModel</param>
	/// <returns>نتیجه عملیات و TreasuryBoxResponseViewModel به‌روزشده</returns>
	public async Task<Result<TreasuryBoxResponseViewModel>> UpdateBoxDataAsync(TreasuryBoxRequestViewModel model)
	{
		string url = "";

		var result = await PutAsync<TreasuryBoxRequestViewModel, Result<TreasuryBoxResponseViewModel>>(
			url,
			model,
			ContentType.Json); // [FromBody] => JSON

		return result;
	}

	#endregion
}