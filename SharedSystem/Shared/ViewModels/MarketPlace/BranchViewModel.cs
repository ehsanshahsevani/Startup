using System.ComponentModel.DataAnnotations;
using Constants;
using Resources;
using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.Marketplace;

public class BranchResponseViewModel : BaseResponseViewModelWithImage<BranchRequestViewModel>
{
    // *********************************************
    /// <summary>
    ///     نام شعبه
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.BranchName))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        MaxLength.Title,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Name { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     شناسه شهر
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.City))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]

    public string CityId { get; set; }

    public string CityDisplayName { get; set; }


    /// <summary>
    /// استان
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Province))]
    [MaxLength(
        FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]
    public string ProvinceId { get; set; }

    public string ProvinceDisplayName { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     آدرس
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Address))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]

    public string Address { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     تلفن
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

    public string Phone { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     ساعت شروع کاری
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.FromTimeInDay))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        MaxLength.TimeInDay,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string FromTimeInDay { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     ساعت پایان کار
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.ToTimeInDay))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        MaxLength.TimeInDay,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]
    public string ToTimeInDay { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     روز شروع کار در هفته
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.FromDayOfWeek))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        MaxLength.DayOfWeek,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]
    public string FromDayOfWeek { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     روز پایان کار در هفته
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.ToDayOfWeek))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        MaxLength.DayOfWeek,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]
    public string ToDayOfWeek { get; set; }
    // *********************************************

    public override BranchRequestViewModel ToRequest()
    {
        var result = new BranchRequestViewModel
        {
            Id = Id,
            Name = Name,
            IsActive = IsActive,
            Ordering = Ordering,
            ToTimeInDay = ToTimeInDay,
            FromTimeInDay = FromTimeInDay,
            Phone = Phone,
            ToDayOfWeek = ToDayOfWeek,
            FromDayOfWeek = FromDayOfWeek,
            Address = Address,
            ProvinceId = ProvinceId,
            CityId = CityId,
            Description = Description,
        };
        return result;
    }
}

public class BranchRequestViewModel : BaseRequestViewModelWithImage
{
    #region Constants

    public static int MinTimeInDay = 6;
    public static int MaxTimeInDay = 23;
    public static int MinDayInWeek = 1;
    public static int MaxDayInWeek = 7;

    #endregion

    // *********************************************
    /// <summary>
    ///     نام شعبه
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.BranchName))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        MaxLength.Title,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Name { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     شناسه شهر
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.City))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string CityId { get; set; }

    public string CityDisplayName { get; set; }
    // *********************************************

    /// <summary>
    /// استان
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Province))]
    [MaxLength(
        FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string ProvinceId { get; set; }

    public string ProvinceDisplayName { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     آدرس
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Address))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]

    public string Address { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     تلفن
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

    public string Phone { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     ساعت شروع کاری
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.FromTimeInDay))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        MaxLength.TimeInDay,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]
    public string FromTimeInDay { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     ساعت پایان کار
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.ToTimeInDay))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        MaxLength.TimeInDay,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]
    public string ToTimeInDay { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// روز شروع کار در هفته
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.FromDayOfWeek))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        MaxLength.DayOfWeek,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]
    public string FromDayOfWeek { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// روز پایان کار در هفته
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.ToDayOfWeek))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        MaxLength.DayOfWeek,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]
    public string ToDayOfWeek { get; set; }

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

        if (int.Parse(FromTimeInDay) < MinTimeInDay)
        {
            var errorMessage =
                string.Format(Resources.Messages.FieldMinValueError,
                    Resources.DataDictionary.FromTimeInDay, MinTimeInDay);

            result.WithError(errorMessage);
        }

        if (int.Parse(ToTimeInDay) > MaxTimeInDay)
        {
            var errorMessage =
                string.Format(Resources.Messages.FieldMinValueError,
                    Resources.DataDictionary.ToTimeInDay, MaxTimeInDay);

            result.WithError(errorMessage);
        }

        if (int.Parse(FromDayOfWeek) < MinDayInWeek)
        {
            var errorMessage =
                string.Format(Resources.Messages.FieldMinValueError,
                    Resources.DataDictionary.FromDayOfWeek, MinDayInWeek);

            result.WithError(errorMessage);
        }

        if (int.Parse(ToDayOfWeek) > MaxDayInWeek)
        {
            var errorMessage =
                string.Format(Resources.Messages.FieldMinValueError,
                    Resources.DataDictionary.ToDayOfWeek, MaxDayInWeek);

            result.WithError(errorMessage);
        }

        return result.ConvertToSampleResult();
    }
}