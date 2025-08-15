using System.ComponentModel.DataAnnotations;
using Constants;
using Resources;
using SampleResult;
using Utilities;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.Ticketing;

public class TicketResponseViewModel : BaseResponseViewModel<TicketRequestViewModel>
{
    // *********************************************
    /// <summary>
    /// موضوع مربوط به این تیکت
    /// </summary>
    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.TicketSubject))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        length: FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]
    public string TicketSubjectId { get; set; }

    public string TicketSubjectDisplayName { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// کد رهگیری
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Code))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        length: FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Code { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// وضعیت تیکت
    /// </summary>
    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Status))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        length: FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]
    public string StatusId { get; set; }

    public string StatusDisplayName { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     وضعیت روئیت تیکت
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.IsSeen))]
    public bool IsSeen { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// کاربر
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.User))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        length: FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string UserId { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// پیام های ارسال شده برای این تیکت
    /// </summary>
    public List<TicketMessageResponseViewModel>? TicketMessageResponseViewModels { get; set; }
    // *********************************************

    public override TicketRequestViewModel ToRequest()
    {
        var result = new TicketRequestViewModel
        {
            Id = Id,
            Code = Code,
            TicketSubjectId = TicketSubjectId,
            StatusId = StatusId,
            Ordering = Ordering,
            Description = Description,
            IsActive = IsActive,
            IsSeen = IsSeen,
            TicketMessageRequestViewModels = TicketMessageResponseViewModels.Select(x => x.ToRequest()).ToList(),
        };
        return result;
    }
}

public class TicketRequestViewModel : BaseRequestViewModel
{
    // *********************************************
    /// <summary>
    /// موضوع مربوط به این تیکت
    /// </summary>
    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.TicketSubject))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        length: FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]
    public string TicketSubjectId { get; set; }

    public string TicketSubjectDisplayName { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// کد رهگیری
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Code))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        length: FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Code { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// وضعیت تیکت
    /// </summary>
    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Status))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        length: FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]
    public string StatusId { get; set; }

    public string StatusDisplayName { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     وضعیت روئیت تیکت
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.IsSeen))]
    public bool IsSeen { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// کاربر
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.User))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        length: FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string UserId { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// پیام های ارسال شده برای این تیکت
    /// </summary>
    public List<TicketMessageRequestViewModel>? TicketMessageRequestViewModels { get; set; }
    // *********************************************
    
    public override Result Validate()
    {
        var result = new FluentResults.Result();

        var checkValidationResult =
            ValidationHelper.GetValidationResults(this);

        if (checkValidationResult.Any()) result.WithErrors(checkValidationResult.Select(x => x.ErrorMessage));

        if (string.IsNullOrEmpty(TicketSubjectId))
        {
            var errorMessage =
                string.Format(
                    Messages.RequiredError,
                    DataDictionary.TicketSubject);

            result.WithError(errorMessage);
        }

        if (string.IsNullOrEmpty(Code))
        {
            var errorMessage =
                string.Format(
                    Messages.RequiredError,
                    DataDictionary.Code);

            result.WithError(errorMessage);
        }

        if (string.IsNullOrEmpty(StatusId))
        {
            var errorMessage =
                string.Format(
                    Messages.RequiredError,
                    DataDictionary.State);

            result.WithError(errorMessage);
        }

        return result.ConvertToSampleResult();
    }
}