using System.ComponentModel.DataAnnotations;
using Constants;
using Resources;
using SampleResult;
using Utilities;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.Marketplace;

public class RoleGoldResponseViewModel : BaseResponseViewModel<RoleGoldRequestViewModel>
{
    // *********************************************
    /// <summary>
    ///     ساعت و تاریخ شروع
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.StartDateTime))]

    public TimeSpan StartTime { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     ساعت و تاریخ پایان
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.EndDateTime))]

    public TimeSpan EndTime { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     ساعت و تاریخ پایان
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Min))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]

    public int Min { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     ساعت و تاریخ پایان
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Max))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]

    public int Max { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     شناسه نوع قانون کیف پول
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.TypeRoleMoney))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string TypeRoleGoldId { get; set; }

    public string? TypeRoleGoldDisplayName { get; set; }
    // *********************************************

    public override RoleGoldRequestViewModel ToRequest()
    {
        var result = new RoleGoldRequestViewModel
        {
            Id = Id,
            Max = Max,
            Min = Min,
            EndTime = EndTime.ToString(@"hh\:mm"),
            StartTime = StartTime.ToString(@"hh\:mm"),
            Ordering = Ordering,
            IsActive = IsActive,
            Description = Description,
            TypeRoleGoldId = TypeRoleGoldId,
            TypeRoleGoldDisplayName = TypeRoleGoldDisplayName,
        };

        return result;
    }
}

/// <summary>
///     دیتای مورد نیاز برای ایجاد یک محصول (Request)
/// </summary>
public class RoleGoldRequestViewModel : BaseRequestViewModel
{
    // *********************************************
    /// <summary>
    ///     ساعت شروع
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.StartDateTime))]

    public string? StartTime { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     ساعت پایان
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.EndDateTime))]

    public string? EndTime { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// ساعت و تاریخ پایان
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Min))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]

    public int? Min { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     ساعت و تاریخ پایان
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Max))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]

    public int? Max { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     شناسه نوع قانون کیف پول
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.TypeRoleMoney))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string TypeRoleGoldId { get; set; }

    public string? TypeRoleGoldDisplayName { get; set; }
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

        if (string.IsNullOrEmpty(StartTime) == true)
        {
            var errorMessage =
                string.Format(
                    Messages.RequiredError,
                    DataDictionary.StartDateTime);

            result.WithError(errorMessage);
        }

        if (string.IsNullOrEmpty(EndTime) == true)
        {
            var errorMessage =
                string.Format(
                    Messages.RequiredError,
                    DataDictionary.EndDateTime);

            result.WithError(errorMessage);
        }

        return result.ConvertToSampleResult();
    }
}