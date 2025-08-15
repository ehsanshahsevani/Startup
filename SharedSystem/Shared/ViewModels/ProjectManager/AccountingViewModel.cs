using ViewmodelSeedworks.Base;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.ProjectManager;

public class AccountingViewModel : BaseViewModel
{
    // **************************************************
    /// <summary>
    /// شماره موبایل
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.PhoneNumber))]
    
    [StringLength(
        maximumLength: 11,
        MinimumLength = 11,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.FixedLengthError))]
    
    [RegularExpression(
        pattern: Constants.RegularExpression.CellPhoneNumber,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.FixedLengthError))]
    
    public string? PhoneNumber { get; set; }
    // **************************************************
    
    // **************************************************
    /// <summary>
    /// کد یکبار مصرف
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.OtpCode))]
    
    [StringLength(
        maximumLength: 5,
        MinimumLength = 5,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.FixedLengthError))]
    
    public string? OtpCode { get; set; }
    // **************************************************
    
    // **************************************************
    /// <summary>
    /// کد امنیتی
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Captcha))]
    
    [StringLength(
        maximumLength: 5,
        MinimumLength = 5,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.FixedLengthError))]
    
    public string? Captcha { get; set; }
    // **************************************************
}
