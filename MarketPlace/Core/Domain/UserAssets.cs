using Utilities;
using Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

/// <summary>
/// دارایی طلا و کیف پول کاربر
/// </summary>
public class UserAssets : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public UserAssets() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }

    // *********************************************
    /// <summary>
    /// شناسه کاربر
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Profile))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string ProfileId { get; set; }

    /// <summary>
    /// پروفایل
    /// </summary>
    public Profile? Profile { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// شناسه سند
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Document))]

    // [Required(
    //     ErrorMessageResourceType = typeof(Resources.Messages),
    //     ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string? DocumentId { get; set; }

    /// <summary>
    /// سند
    /// </summary>
    public Document? Document { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// میزان طلا به سوت
    /// از دارایی کیف پول بدست می آید
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.GoldSoot))]

    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    public decimal GoldSoot { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// قیمت لحظه ای طلا
    /// محاسبه از طریق جیبیت
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.GoldPriceInThisTime))]

    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    [Column(TypeName = "decimal(18,2)")]
    
    public decimal GoldPriceInThisTime { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// معادل تومان
    /// از محاسبه دارایی طلای آب شده کاربر بدست می آید
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Amount))]

    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    [Column(TypeName = "decimal(18,2)")]
    
    public decimal Amount { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// دارایی طلای آب شده
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.AssetsGold))]

    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [Column(TypeName = "decimal(18,2)")]
    
    public decimal AssetsGold { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// دارایی کیف پول
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.AssetsWallet))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    [Column(TypeName = "decimal(18,2)")]
    
    public decimal AssetsWallet { get; set; }
    // *********************************************

    // *********************************************
    // /// <summary>
    // /// دلیل تغییر دارایی و ثبت این رکورد در دیتابیس
    // /// </summary>
    //
    // [Display(
    //     ResourceType = typeof(Resources.DataDictionary),
    //     Name = nameof(Resources.DataDictionary.AssetsType))]
    //
    // [Required(
    //     ErrorMessageResourceType = typeof(Resources.Messages),
    //     ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    //
    // public AssetsType AssetsType { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// بررسی امکان خرید طلا به میزان مشخص شده
    /// </summary>
    /// <param name="amountInTomans">مبلغ به تومان</param>
    /// <returns>true if purchase is possible</returns>
    public bool CanBuy(decimal amountInTomans)
    {
        return AssetsWallet >= amountInTomans;
    }

    /// <summary>
    /// بررسی امکان فروش طلا به میزان مشخص شده
    /// </summary>
    /// <param name="sootAmount">میزان طلا به سوت</param>
    /// <returns>true if sale is possible</returns>
    public bool CanSell(int sootAmount)
    {
        return AssetsGold >= sootAmount;
    }
    
    /// <summary>
    /// بررسی امکان خرید طلا به میزان مشخص شده
    /// </summary>
    /// <param name="sootAmount">مبلغ به سوت طلا</param>
    /// <param name="currentGoldPrice">قیمت لحظه ای طلا</param>
    /// <returns>true if purchase is possible</returns>
    public bool CanBuy(decimal sootAmount, decimal currentGoldPrice)
    {
        return AssetsWallet >= sootAmount.GoldToToman(currentGoldPrice);
    }
    
    /// <summary>
    /// بررسی امکان فروش طلا به میزان مشخص شده
    /// </summary>
    /// <param name="amountInTomans">میزان طلا به تومان</param>
    /// <param name="currentGoldPrice">قیمت لحظه ای طلا</param>
    /// <returns>true if sale is possible</returns>
    public bool CanSell(decimal amountInTomans, decimal currentGoldPrice)
    {
        return AssetsGold >= amountInTomans.TomanToGold(currentGoldPrice);
    }
    // *********************************************
}