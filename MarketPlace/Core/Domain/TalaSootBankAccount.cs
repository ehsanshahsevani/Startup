using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain;

/// <summary>
/// اطلاعات حساب بانکی کارفرما
/// فقط یک رکورد در سیستم ایجاد میشود برای طلا سوت
/// اگر ادمین بخواهد روند خرید و فروش و ... را روشن کند باید ابتدا اطلاعات حساب خود را وارد کند
/// </summary>
public class TalaSootBankAccount : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public TalaSootBankAccount() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
    
    // *********************************************
    /// <summary>
    /// نام بانک
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.BankName))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.MaxLength.Title,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string BankName { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// نام صاحب حساب
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.FullName))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.MaxLength.Title,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string FullName { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// شماره حساب
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.AccountNumber))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.MaxLength.Title,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string AccountNumber { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// شبا - 26 کاراکتر
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Shaba))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.MaxLength.ShabaNumber,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string Shaba { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// شماره کارت 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CardNumber))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.MaxLength.ShabaNumber,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string CardNumber { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// مقدار 
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
}