using System.ComponentModel.DataAnnotations;

namespace Enums.Marketplace;

/// <summary>
/// نوع اسناد مختلف در سیستم حسابداری طلاسوت
/// </summary>
public enum DocumentType : byte
{
	/// <summary>
	/// درخواست شارژ کیف پول
	/// </summary>
	[Display(Name = "درخواست شارژ کیف پول")]
	Deposit = 0,
	
	/// <summary>
	/// درخواست برداشت پول از کیف پول
	/// </summary>
	[Display(Name = "درخواست برداشت پول از کیف پول")]
	Withdraw = 1,
	
	/// <summary>
	/// خرید طلای آب شده به صورت آنلاین
	/// </summary>
	[Display(Name = "خرید طلای آب شده به صورت آنلاین")]
	GoldPurchase = 10,
	
	/// <summary>
	/// تحویل طلای آب شده به صورت فیزیکی
	/// </summary>
	[Display(Name = "تحویل طلای آب شده به صورت فیزیکی")]
	TransferBuy = 12,
	
	/// <summary>
	/// فروش طلای آب شده به صورت آنلاین
	/// </summary>
	[Display(Name = "فروش طلای آب شده به صورت آنلاین")]
	SaleOfGoldCode = 13,
	
	/// <summary>
	/// خرید کالا از فروشگاه
	/// </summary>
	[Display(Name = "خرید کالا از فروشگاه")]
	BuyFromShop = 14,

	/// <summary>
	/// هزینه رفرال
	/// </summary>
	[Display(Name = "هزینه رفرال و دعوت دوستان")]
	Referal = 15,
}