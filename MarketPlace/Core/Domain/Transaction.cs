using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;
using Enums.Marketplace;

namespace Domain;

/// <summary>
/// تراکنش مالی
/// </summary>
public class Transaction : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Transaction() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
    
    // *********************************************
    /// <summary>
    /// نام
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Name))]
	
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    [MaxLength(
        length: Constants.MaxLength.Title,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string Name { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// نوع تراکنش 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.TransactionType))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    public TransactionType Type { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// میزان طلا به سوت
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.GoldSoot))]

    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    public int GoldSoot { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// قیمت لحظه ای طلا
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.GoldPriceInThisTime))]

    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    public int GoldPriceInThisTime { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// معادل تومان
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
    /// تاریخ پرداخت 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.PayDate))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
    ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
        
    public DateTime PayDate { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// مالکیت حساب 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Authority))]
    
    public string? Authority { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// وضعیت پرداخت 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.IsPay))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    public bool IsPay { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// شناسه سفارش
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Order))]
   
    public Order? Order { get; set; }

    /// <summary>
    /// شناسه زیرسیستم
    /// </summary>
    public string? OrderId { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// شناسه پروفایل
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
    // /// <summary>
    // /// شبا از جدول ProfileBank
    // /// </summary>
    //
    // [Display(
    //     ResourceType = typeof(Resources.DataDictionary),
    //     Name = nameof(Resources.DataDictionary.Shaba))]
    //
    // [Required(
    //     ErrorMessageResourceType = typeof(Resources.Messages),
    //     ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    //
    // [MaxLength(
    //     length: Constants.MaxLength.Title,
    //     ErrorMessageResourceType = typeof(Resources.Messages),
    //     ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    //
    // public string Shaba { get; set; }
    // *********************************************
    
    
}