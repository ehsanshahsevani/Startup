using Domain.Base;
using Enums.ProjectManager;
using System.ComponentModel.DataAnnotations;

namespace Domain;

/// <summary>
/// وضعیت ورود و خروج
/// بخشی برای مدیریت نشست های فعال
/// </summary>
public class Commuting : BaseEntity
{
#pragma warning disable CS8618, CS9264
    public Commuting():base()
#pragma warning restore CS8618, CS9264
    {
    }
    
    // *********************************************
    /// <summary>
    /// نام فارسی
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.NameFA))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        Constants.MaxLength.Name,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string Name { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// کاربر
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.User))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string UserId { get; set; }

    /// <summary>
    /// کاربر
    /// </summary>
    public User User { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// کاربر
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.User))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string ClosedByUserId { get; set; }

    /// <summary>
    /// کاربر
    /// </summary>
    public User ClosedByUser { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// نوع فعالیت
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CommutingType))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    public CommutingType CommutingType { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// توکن
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.TokenFA))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.MaxLength.Token,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string Token { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// کد OTP
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.OtpCode))]
    
    [MaxLength(
        length: Constants.MaxLength.OptCode,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string? OptCode { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// آدرس آی پی
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.IpAddress))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.MaxLength.IP,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string IpAddress { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// نام دستگاه
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.DeviceModel))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.MaxLength.Title,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string DeviceModel { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// نام مرورگر
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Browser))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.MaxLength.Title,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string Browser { get; set; }
    // *********************************************
}