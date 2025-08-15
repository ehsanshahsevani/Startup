using System.ComponentModel.DataAnnotations;
using Constants;
using Resources;
using SampleResult;
using Utilities;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.Ticketing;

public class TicketSubjectResponseViewModel : BaseResponseViewModel<TicketSubjectRequestViewModel>
{
    // *********************************************
    /// <summary>
    ///     نام وضعیت
    /// </summary>
    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Name))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Name { get; set; }
    // *********************************************

    public override TicketSubjectRequestViewModel ToRequest()
    {
        var result = new TicketSubjectRequestViewModel
        {
            Id = Id,
            Name = Name,
            Ordering = Ordering,
            Description = Description,
            IsActive = IsActive
        };
        return result;
    }
}

public class TicketSubjectRequestViewModel : BaseRequestViewModel
{
    // *********************************************
    /// <summary>
    ///     نام وضعیت
    /// </summary>
    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Name))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Name { get; set; }
    // *********************************************

    public override Result Validate()
    {
        var result = new FluentResults.Result();

        var checkValidationResult =
            ValidationHelper.GetValidationResults(this);

        if (checkValidationResult.Any()) result.WithErrors(checkValidationResult.Select(x => x.ErrorMessage));

        if (string.IsNullOrEmpty(Name))
        {
            var errorMessage =
                string.Format(
                    Messages.RequiredError,
                    DataDictionary.Name);

            result.WithError(errorMessage);
        }

        return result.ConvertToSampleResult();
    }
}