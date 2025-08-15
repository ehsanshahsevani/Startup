using Resources;
using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// باکس همه درآمد های سیستم
/// داده ها ارسال میشوند و داده های جدید از طریق ریکوعست همین درخواست برمیگردد
/// </summary>
public class IncomeBoxResponseViewModel
	: BaseResponseViewModel<IncomeBoxRequestViewModel>
{
	public IncomeBoxResponseViewModel() : base()
	{
	}

	public IncomeBoxResponseViewModel(
		decimal commissionFeeAmount,
		decimal selleOfGoldFeeAmount,
		decimal purchaseGoldFeeAmount,
		decimal rechargeWalletFeeAmount,
		decimal maintenanceAndInsuranceFeeAmount)
	{
		CommissionFeeAmount = commissionFeeAmount;
		SelleOfGoldFeeAmount = selleOfGoldFeeAmount;
		PurchaseGoldFeeAmount = purchaseGoldFeeAmount;
		RechargeWalletFeeAmount = rechargeWalletFeeAmount;
		MaintenanceAndInsuranceFeeAmount = maintenanceAndInsuranceFeeAmount;
	}
	
	// **************************************************
	/// <summary>
	/// کارمزد کمسیون
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(DataDictionary.CommissionFee))]
	
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
		
	[Range(
		minimum: 0,
		maximum: 100,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RangeFieldError))]

	public decimal CommissionFeeAmount { get; set; }
	// **************************************************

	// **************************************************
	/// <summary>
	/// کارمزد فروش طلا
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(DataDictionary.IncomeSaleOfGoldFee))]
	
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
		
	[Range(
		minimum: 0,
		maximum: 100,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RangeFieldError))]

	public decimal SelleOfGoldFeeAmount { get; set; }
	// **************************************************
	
	// **************************************************
	/// <summary>
	/// کارمزد خرید طلا
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(DataDictionary.GoldPurchaseFee))]
	
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
		
	[Range(
		minimum: 0,
		maximum: 100,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RangeFieldError))]

	public decimal PurchaseGoldFeeAmount { get; set; }
	// **************************************************
	
	// **************************************************
	/// <summary>
	/// کارمزد شارژ کیف پول
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(DataDictionary.WalletRechargeFee))]
	
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
		
	[Range(
		minimum: 0,
		maximum: 100,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RangeFieldError))]

	public decimal RechargeWalletFeeAmount { get; set; }
	// **************************************************
	
	// **************************************************
	/// <summary>
	/// هزینه بیمه و نگهداری
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(DataDictionary.MaintenanceAndInsuranceFee))]
	
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
		
	[Range(
		minimum: 0,
		maximum: 100,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RangeFieldError))]

	public decimal MaintenanceAndInsuranceFeeAmount { get; set; }
	// **************************************************
	
	/// <summary>
	/// تبدیل به درخواست قابل ارسال به سرور
	/// </summary>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public override IncomeBoxRequestViewModel ToRequest()
	{
		var request = new IncomeBoxRequestViewModel
		{
			Id = Guid.NewGuid().ToString(),
			CommissionFeeAmount = CommissionFeeAmount,
			SelleOfGoldFeeAmount = SelleOfGoldFeeAmount,
			PurchaseGoldFeeAmount = PurchaseGoldFeeAmount,
			RechargeWalletFeeAmount = RechargeWalletFeeAmount,
			MaintenanceAndInsuranceFeeAmount = MaintenanceAndInsuranceFeeAmount
		};

		return request;
	}
}

public class IncomeBoxRequestViewModel : BaseRequestViewModel
{
	public IncomeBoxRequestViewModel() : base()
	{
	}

	public IncomeBoxRequestViewModel(
		decimal commissionFeeAmount,
		decimal selleOfGoldFeeAmount,
		decimal purchaseGoldFeeAmount,
		decimal rechargeWalletFeeAmount,
		decimal maintenanceAndInsuranceFeeAmount)
	{
		CommissionFeeAmount = commissionFeeAmount;
		SelleOfGoldFeeAmount = selleOfGoldFeeAmount;
		PurchaseGoldFeeAmount = purchaseGoldFeeAmount;
		RechargeWalletFeeAmount = rechargeWalletFeeAmount;
		MaintenanceAndInsuranceFeeAmount = maintenanceAndInsuranceFeeAmount;
	}

	// **************************************************
	/// <summary>
	/// کارمزد کمسیون
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(DataDictionary.CommissionFee))]
	
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
	
	[Range(
		minimum: 0,
		maximum: 100,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RangeFieldError))]
	
	public decimal CommissionFeeAmount { get; set; }
	// **************************************************

	// **************************************************
	/// <summary>
	/// کارمزد فروش طلا
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(DataDictionary.IncomeSaleOfGoldFee))]
	
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
		
	[Range(
		minimum: 0,
		maximum: 100,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RangeFieldError))]

	public decimal SelleOfGoldFeeAmount { get; set; }
	// **************************************************
	
	// **************************************************
	/// <summary>
	/// کارمزد خرید طلا
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(DataDictionary.GoldPurchaseFee))]
	
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
		
	[Range(
		minimum: 0,
		maximum: 100,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RangeFieldError))]

	public decimal PurchaseGoldFeeAmount { get; set; }
	// **************************************************
	
	// **************************************************
	/// <summary>
	/// کارمزد شارژ کیف پول
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(DataDictionary.WalletRechargeFee))]
	
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
		
	[Range(
		minimum: 0,
		maximum: 100,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RangeFieldError))]

	public decimal RechargeWalletFeeAmount { get; set; }
	// **************************************************
	
	// **************************************************
	/// <summary>
	/// هزینه بیمه و نگهداری
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(DataDictionary.MaintenanceAndInsuranceFee))]
	
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
		
	[Range(
		minimum: 0,
		maximum: 100,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RangeFieldError))]

	public decimal MaintenanceAndInsuranceFeeAmount { get; set; }
	// **************************************************
	
	/// <summary>
	/// بررسی صحت داده
	/// </summary>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public override SampleResult.Result Validate()
	{
		var result = new FluentResults.Result();

		var validationResult = Utilities.ValidationHelper.GetValidationResults(this);

		result.WithErrors(validationResult.Select(x => x.ErrorMessage));

		return result.ConvertToSampleResult();
	}
}
