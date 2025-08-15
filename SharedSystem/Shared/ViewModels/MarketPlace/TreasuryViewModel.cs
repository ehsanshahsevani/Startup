using Resources;
using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// باکس همه موجودی های سیستم
/// داده ها ارسال میشوند و داده های جدید از طریق ریکوعست همین درخواست برمیگردد
/// </summary>
public class TreasuryBoxResponseViewModel
	: BaseResponseViewModel<TreasuryBoxRequestViewModel>
{
	public TreasuryBoxResponseViewModel() : base()
	{
	}

	public TreasuryBoxResponseViewModel(
		int? treasuryGoldOnline,
		int? treasuryGoldReceive,
		int? talaSootBankAccountAmount)
	{
		TreasuryGoldOnline = treasuryGoldOnline;
		TreasuryGoldReceive = treasuryGoldReceive;
		TalaSootBankAccountAmount = talaSootBankAccountAmount;
	}

	// **************************************************
	/// <summary>
	 /// دارایی خزانه طلا خرید و فروش آنلاین
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(DataDictionary.TreasuryGoldOnline))]
	
	public int? TreasuryGoldOnline { get; set; }
	// **************************************************

	// **************************************************
	/// <summary>
	/// دارایی خزانه طلا جهت تحویل فیزیکی
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(DataDictionary.TreasuryGoldReceive))]
	
	public int? TreasuryGoldReceive { get; set; }
	// **************************************************
	
	// **************************************************
	/// <summary>
	/// موجودی حساب بانکی طلاسوت
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(DataDictionary.TalaSootBankAccountAmount))]
	
	public int? TalaSootBankAccountAmount { get; set; }
	// **************************************************
	
	/// <summary>
	/// تبدیل به درخواست قابل ارسال به سرور
	/// </summary>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public override TreasuryBoxRequestViewModel ToRequest()
	{
		var request = new TreasuryBoxRequestViewModel
		{
			Id = Guid.NewGuid().ToString(),
			TreasuryGoldOnline = TreasuryGoldOnline,
			TreasuryGoldReceive = TreasuryGoldReceive,
			TalaSootBankAccountAmount = TalaSootBankAccountAmount
		};

		return request;
	}
}

public class TreasuryBoxRequestViewModel : BaseRequestViewModel
{
	public TreasuryBoxRequestViewModel() : base()
	{
	}

	public TreasuryBoxRequestViewModel(
		int? treasuryGoldOnline,
		int? treasuryGoldReceive,
		int? talaSootBankAccountAmount)
	{
		TreasuryGoldOnline = treasuryGoldOnline;
		TreasuryGoldReceive = treasuryGoldReceive;
		TalaSootBankAccountAmount = talaSootBankAccountAmount;
	}

	// **************************************************
	/// <summary>
	/// دارایی خزانه طلا خرید و فروش آنلاین
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(DataDictionary.TreasuryGoldOnline))]

	public int? TreasuryGoldOnline { get; set; }
	// **************************************************

	// **************************************************
	/// <summary>
	/// دارایی خزانه طلا جهت تحویل فیزیکی
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(DataDictionary.TreasuryGoldReceive))]
		
	public int? TreasuryGoldReceive { get; set; }
	// **************************************************
	
	// **************************************************
	/// <summary>
	/// موجودی حساب بانکی طلاسوت
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(DataDictionary.TalaSootBankAccountAmount))]
	
	public int? TalaSootBankAccountAmount { get; set; }
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
		
		if (TreasuryGoldOnline.HasValue == false)
		{
			var errorMessage =
				string.Format(
					Messages.RequiredError,
					Resources.DataDictionary.TreasuryGoldOnline);
			
			result.WithError(errorMessage);
		}

		if (TreasuryGoldReceive.HasValue == false)
		{
			var errorMessage =
				string.Format(
					Resources.Messages.RequiredError,
					Resources.DataDictionary.TreasuryGoldReceive);
			
			result.WithError(errorMessage);
		}

		if (TalaSootBankAccountAmount.HasValue == false)
		{
			var errorMessage = string.Format(
				Resources.Messages.RequiredError,
				Resources.DataDictionary.TalaSootBankAccountAmount);
			
			result.WithError(errorMessage);
		}
		
		return result.ConvertToSampleResult();
	}
}
