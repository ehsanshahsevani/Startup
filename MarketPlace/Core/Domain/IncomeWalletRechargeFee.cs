using Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

/// <summary>
/// کارمزد شارژ کیف پول
/// </summary>
public class IncomeWalletRechargeFee : BaseEntity
{
	public IncomeWalletRechargeFee(decimal amount) : base()
	{
		Amount = amount;
	}
	
	// *********************************************
	/// <summary>
	/// مقدار
	/// </summary>

	[Column(TypeName = "decimal(10,2)")]
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.WalletRechargeFee))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	public decimal Amount { get; set; }
	// *********************************************
}