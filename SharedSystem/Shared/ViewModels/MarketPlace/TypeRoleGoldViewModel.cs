using System.ComponentModel.DataAnnotations;
using Constants;
using Resources;
using SampleResult;
using Utilities;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.Marketplace;

public class TypeRoleGoldResponseViewModel : BaseResponseViewModel<TypeRoleGoldRequestViewModel>
{
    // *********************************************
    /// <summary>
    ///     مقدار مناسب برای سرچ در دیتابیس
    ///     Both, Buy, Sell
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Code))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        MaxLength.Description,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Code { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     نام
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Name))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        MaxLength.Description,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Name { get; set; }
    // *********************************************

    public override TypeRoleGoldRequestViewModel ToRequest()
    {
        var result = new TypeRoleGoldRequestViewModel
        {
            Description = Description,
            Ordering = Ordering,
            IsActive = IsActive,
            Name = Name,
            Code = Code,
            Id = Id
        };
        return result;
    }
}

/// <summary>
///     دیتای مورد نیاز برای ایجاد یک محصول (Request)
/// </summary>
public class TypeRoleGoldRequestViewModel : BaseRequestViewModel
{
    // *********************************************
    /// <summary>
    ///     مقدار مناسب برای سرچ در دیتابیس
    ///     Both, Buy, Sell
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Code))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        MaxLength.Description,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Code { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     نام
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Name))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        MaxLength.Description,
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

        return result.ConvertToSampleResult();
    }
}