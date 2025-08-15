using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;
using System.ComponentModel.DataAnnotations;
using Enums.Shared;

namespace ViewModels.Marketplace;

public class ValueResponseViewModel : BaseResponseViewModelWithImage<ValueRequestViewModel>
{
    // *********************************************
    /// <summary>
    /// عنوان
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Title))]
    
    [MaxLength(
        length: Constants.MaxLength.Title,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    public string? AmountEN { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// مقدار
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Value))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.MaxLength.Title,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string AmountFA { get; set; }
    // *********************************************
    
    
    // *********************************************
    /// <summary>
    /// ذخیره شناسه کاربر ایجاد کننده با تابع:
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.User))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string? CreatedByUserId { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// تاریخ تایید:
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CheckedDateTime))]
    
    [MaxLength(
        length: Constants.FixedLength.DateShamsi,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public DateTime? CheckedDateTime { get; set; }
    // *********************************************

    
    // *********************************************
    /// <summary>
    /// ذخیره شناسه کاربر ادمین  با تابع:
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CheckedByUserId))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string? CheckedByUserId { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// نوع نمایش مقادیر برای کاربران سامانه
    /// </summary>
    public DisplayNameType DisplayNameType { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// تایید شده توسط ادمین
    /// درصورتی که یک محصول دارای یک مقدار تایید نشده باشد
    /// آن محصول در سامانه نمایش داده نمیشود
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CheckedByUserId))]
    
    public bool IsConfirmed { get; set; }
    // *********************************************
    
    // *********************************************
    public string? UnitDisplayName { get; set; }
    // *********************************************
    
    // *********************************************
    public override ValueRequestViewModel ToRequest()
    {
        var result = new ValueRequestViewModel
        {
            Id = Id,
            AmountEN = AmountEN,
            AmountFA = AmountFA,
            IsActive = IsActive,
            Ordering = Ordering,
            Description = Description,
            CreatedByUserId = CreatedByUserId,
            DisplayNameType = DisplayNameType,
            ImageTitle = ImageTitle,
            Alternative = Alternative,
            IsConfirmed = IsConfirmed
        };

        return result;
    }
    // *********************************************
}

public class ValueRequestViewModel : BaseRequestViewModelWithImage
{
    // *********************************************
    /// <summary>
    /// عنوان
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Title))]
    
    [MaxLength(
        length: Constants.MaxLength.Title,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string? AmountEN { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// تایید شده توسط ادمین
    /// درصورتی که یک محصول دارای یک مقدار تایید نشده باشد
    /// آن محصول در سامانه نمایش داده نمیشود
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Confirm))]
    
    public bool IsConfirmed { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// مقدار
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Value))]
    
    [MaxLength(
        length: Constants.MaxLength.Title,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string? AmountFA { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// نوع نمایش مقادیر برای کاربران سامانه
    /// </summary>
    public DisplayNameType DisplayNameType { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// شناسه واحد اندازه گیری
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Units))]
    public string? UnitId { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// کاربر ثبت کننده این مقدار
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.User))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string? CreatedByUserId { get; set; }
    // *********************************************
    
    public override Result Validate()
    {
        var result = new FluentResults.Result();

        var checkValidationRsult =
            Utilities.ValidationHelper.GetValidationResults(this);

        if (checkValidationRsult.Any())
        {
            result.WithErrors(checkValidationRsult.Select(x => x.ErrorMessage));
        }

        if (string.IsNullOrEmpty(AmountFA) == true && string.IsNullOrEmpty(AmountEN) == true)
        {
            var errorMessage =
                string.Format(Resources.Messages.RequiredError, Resources.DataDictionary.Amount);
            
            result.WithError(errorMessage);
        }
        
        return result.ConvertToSampleResult();
    }
}