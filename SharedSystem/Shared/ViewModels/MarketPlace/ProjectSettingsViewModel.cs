using Utilities;
using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Marketplace;

public class TalaSootSettingsResponseViewModel : BaseResponseViewModel<TalaSootSettingsRequestViewModel>
{
	// *********************************************
	/// <summary>
	/// خرید و فروش طلای آب شده
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ClosingSaleAndPurchaseMoltenGold))]

	public bool ClosingSaleAndPurchaseMoltenGold { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// برداشت از کیف پول
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.WithdrawalFromWallet))]

	public bool WithdrawalFromWallet { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// واریز و برداشت از کیف پول
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.WithdrawalAndDepositFromWallet))]

	public bool WithdrawalAndDepositFromWallet { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// ارسال درخواست طلای آب شده
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.SendGoldDeliveryRequest))]

	public bool SendGoldDeliveryRequest { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// خرید از فروشگاه
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.BuyFromTheStore))]

	public bool BuyFromTheStore { get; set; }
	// *********************************************

	public override TalaSootSettingsRequestViewModel ToRequest()
	{
		var result = new TalaSootSettingsRequestViewModel
		{
			Id = Id,
			Ordering = Ordering,
			IsActive = IsActive,
			Description = Description,
			
			BuyFromTheStore = BuyFromTheStore,
			WithdrawalFromWallet = WithdrawalFromWallet,
			SendGoldDeliveryRequest = SendGoldDeliveryRequest,
			WithdrawalAndDepositFromWallet = WithdrawalAndDepositFromWallet,
			ClosingSaleAndPurchaseMoltenGold = ClosingSaleAndPurchaseMoltenGold,
		};

		return result;
	}
}

/// <summary>
///     دیتای مورد نیاز برای ایجاد یک محصول (Request)
/// </summary>
public class TalaSootSettingsRequestViewModel : BaseRequestViewModel
{
	// *********************************************
	/// <summary>
	/// خرید و فروش طلای آب شده
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ClosingSaleAndPurchaseMoltenGold))]

	public bool ClosingSaleAndPurchaseMoltenGold { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// برداشت از کیف پول
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.WithdrawalFromWallet))]

	public bool WithdrawalFromWallet { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// واریز و برداشت از کیف پول
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.WithdrawalAndDepositFromWallet))]

	public bool WithdrawalAndDepositFromWallet { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// ارسال درخواست طلای آب شده
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.SendGoldDeliveryRequest))]

	public bool SendGoldDeliveryRequest { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// خرید از فروشگاه
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.BuyFromTheStore))]

	public bool BuyFromTheStore { get; set; }
	// *********************************************

	public override Result Validate()
	{
		var result = new FluentResults.Result();

		var checkValidationResult =
			ValidationHelper.GetValidationResults(this);

		if (checkValidationResult.Any())
		{
			result.WithErrors(checkValidationResult.Select(x => x.ErrorMessage));
		}

		return result.ConvertToSampleResult();
	}
}