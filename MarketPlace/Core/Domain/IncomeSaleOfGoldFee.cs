using Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

/// <summary>
/// کارمزد فروش طلا
/// </summary>
public class IncomeSaleOfGoldFee : BaseEntity
{
	public IncomeSaleOfGoldFee(decimal amount) : base()
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