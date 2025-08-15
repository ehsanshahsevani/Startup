using System.ComponentModel.DataAnnotations;
using Constants;
using Resources;
using SampleResult;
using Utilities;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.Ticketing;

public class TicketMessageResponseViewModel : BaseResponseViewModel<TicketMessageRequestViewModel>
{
    // *********************************************
    /// <summary>
    /// متن پیام
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Text))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        length: FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Text { get; set; }
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
    public string TicketId { get; set; }

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

    public override TicketMessageRequestViewModel ToRequest()
    {
        var result = new TicketMessageRequestViewModel
        {
            Id = Id,
            Text = Text,
            TicketId = TicketId,
            Ordering = Ordering,
            Description = Description,
            IsActive = IsActive
        };
        return result;
    }
}

public class TicketMessageRequestViewModel : BaseRequestViewModel
{
    // *********************************************
    /// <summary>
    /// متن پیام
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Text))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        length: FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Text { get; set; }
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
    public string TicketId { get; set; }

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

    public override Result Validate()
    {
        var result = new FluentResults.Result();

        var checkValidationResult =
            ValidationHelper.GetValidationResults(this);

        if (checkValidationResult.Any()) result.WithErrors(checkValidationResult.Select(x => x.ErrorMessage));

        if (string.IsNullOrEmpty(Text))
        {
            var errorMessage =
                string.Format(
                    Messages.RequiredError,
                    DataDictionary.Text);

            result.WithError(errorMessage);
        }

        if (string.IsNullOrEmpty(TicketId))
        {
            var errorMessage =
                string.Format(
                    Messages.RequiredError,
                    DataDictionary.Text);

            result.WithError(errorMessage);
        }

        return result.ConvertToSampleResult();
    }
}