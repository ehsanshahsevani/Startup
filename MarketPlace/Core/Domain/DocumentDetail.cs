using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain;

public class DocumentDetail : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public DocumentDetail() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
    
    // *********************************************
    /// <summary>
    /// شناسه کدحسابداری
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.AccountCoding))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string AccountCodingId { get; set; }

    /// <summary>
    /// شناسه کدحسابداری
    /// </summary>
    public AccountCoding AccountCoding { get; set; }
    // *********************************************
    
    
    // *********************************************
    /// <summary>
    /// بستانکار
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.IsCreditor))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    public bool IsCreditor { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// بدهکار
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.IsDebtor))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    public bool IsDebtor { get; set; }
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

    [Column(TypeName = "decimal(18,2)")]
    
    public decimal GoldSoot { get; set; }
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

    [Column(TypeName = "decimal(18,2)")]
    
    public decimal GoldPriceInThisTime { get; set; }
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
    /// شناسه سند
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Document))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string DocumentId { get; set; }

    /// <summary>
    /// شناسه سند
    /// </summary>
    public Document Document { get; set; }
    // *********************************************
   
    // *********************************************
    /// <summary>
    /// شناسه دلیل سند اصلی
    /// در صورت نال بودن این شناسه یعنی این زیر سند به سند دیگری وابسته نیست
    /// در تراکنش های شارژ کیف پول و برداشت از کیف پول این شناسه پر میشود
    /// این سند ها سند های چند بخشی و نسبتا پیچیده تری هستند
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Document))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string? ParentDocumentId { get; set; }

    /// <summary>
    /// سند والد یا سندی که این سند به آن وابسته است
    /// </summary>
    public Document? ParentDocument { get; set; }
    // *********************************************
    
    
    // *********************************************
    /// <summary>
    /// شناسه زیرسیستم
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.SubSystemLocal))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string SubSystemLocalId { get; set; }

    /// <summary>
    /// شناسه زیرسیستم
    /// </summary>
    public SubSystemLocal SubSystemLocal { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// شناسه جدول
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Relation))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string RelationId { get; set; }
    // *********************************************
}