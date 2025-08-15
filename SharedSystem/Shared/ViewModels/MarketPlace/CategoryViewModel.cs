using Constants;
using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Marketplace;

public class CategoryResponseViewModel :
    BaseResponseViewModel<CategoryRequestViewModel>
{
    public CategoryResponseViewModel() : base()
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
        MaxLength.Name,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string Name { get; set; }
    // *********************************************


    public override CategoryRequestViewModel ToRequest()
    {
        var result = new CategoryRequestViewModel
        {
            Id = Id,
            Name = Name,
            Ordering = Ordering,
            IsActive = IsActive,
            Description = Description,
        };

        return result;
    }
}

public class CategoryRequestViewModel : BaseRequestViewModel
{
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

    public override Result Validate()
    {
        var result = new FluentResults.Result();

        var checkValidationResult =
            Utilities.ValidationHelper.GetValidationResults(this);

        if (checkValidationResult.Any())
        {
            result.WithErrors(checkValidationResult.Select(x => x.ErrorMessage));
        }

        if (string.IsNullOrEmpty(Name) == true)
        {
            var errorMessage =
                string.Format(Resources.Messages.RequiredError, Resources.DataDictionary.Name);

            result.WithError(errorMessage);
        }

        return result.ConvertToSampleResult();
    }
}