using System.ComponentModel.DataAnnotations;
using Resources;
using SampleResult;
using Utilities;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.Ticketing;

public class StatusResponseViewModel : BaseResponseViewModel<StatusRequestViewModel>
{
    // *********************************************
    /// <summary>
    /// نام وضعیت
    /// </summary>
    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Name))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Name { get; set; }
    // *********************************************

    public override StatusRequestViewModel ToRequest()
    {
        var result = new StatusRequestViewModel
        {
            Id = Id,
            Name = Name,
            Ordering = Ordering,
            Description = Description,
            IsActive = IsActive,
        };
        return result;
    }
}

public class StatusRequestViewModel : BaseRequestViewModel
{
    // *********************************************
    /// <summary>
    /// نام وضعیت
    /// </summary>
    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Name))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Name { get; set; }
    // *********************************************

    public override Result Validate()
    {
        var result = new FluentResults.Result();

        var checkValidationResult =
            ValidationHelper.GetValidationResults(this);

        if (checkValidationResult.Any())
        {
            result.WithErrors(checkValidationResult.Select(x => x.ErrorMessage));
        }

        if (string.IsNullOrEmpty(Name) == true)
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