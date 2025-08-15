using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

/// <summary>
/// تنظیمات خرید و فروش در طلا سوت
/// و مدیریت معاملات
/// </summary>
public class TalaSootSettings : BaseEntity
{
	public TalaSootSettings() : base()
	{
	}
	
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
}