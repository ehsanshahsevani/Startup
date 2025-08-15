using SampleResult;
using Enums.SharedService;
using HttpServiceSeedworks;
using InfrastructureSeedworks;

namespace HttpServices.Marketplace;

public class IncomeManagerService : HttpService
{
	public IncomeManagerService() : base(ServerSettings.DomainApiMarketPlace)
	{
		SetBaseApi("IncomeManager");
	}

	#region GET : /

	/// <summary>
	/// دریافت کارمزدها از بخش‌های مختلف (Box Data)
	/// </summary>
	/// <returns>Result شامل IncomeBoxResponseViewModel</returns>
	public async Task<Result<IncomeBoxResponseViewModel>> GetBoxDataAsync()
	{
		string url = "";

		var result =
			await base.GetAsync<Result<IncomeBoxResponseViewModel>>(url);

		return result!;
	}

	#endregion

	#region PUT : /

	/// <summary>
	/// به‌روزرسانی کارمزدها از بخش‌های مختلف
	/// </summary>
	/// <param name="model">IncomeBoxRequestViewModel</param>
	/// <returns>Result شامل IncomeBoxResponseViewModel به‌روزشده</returns>
	public async Task<Result<IncomeBoxResponseViewModel>> UpdateBoxDataAsync(IncomeBoxRequestViewModel model)
	{
		string url = ""; // روت اصلی کنترلر

		var result = await PutAsync<IncomeBoxRequestViewModel, Result<IncomeBoxResponseViewModel>>(
			url,
			model,
			ContentType.Json);

		return result!;
	}

	#endregion
}