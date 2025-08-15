using System.ComponentModel.DataAnnotations;
using Constants;
using Resources;
using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;
using Result = SampleResult.Result;

namespace ViewModels.Marketplace;

public class CartItemResponseViewModel : BaseResponseViewModel<CartItemRequestViewModel>
{
    // *********************************************
    /// <summary>
    /// شناسه کاربر
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Profile))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        length: FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string ProfileId { get; set; }

    // *********************************************

    // *********************************************
    /// <summary>
    /// شناسه محصول
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Product))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        length: FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string ProductId { get; set; }

    // *********************************************

    // *********************************************
    /// <summary>
    /// تعداد محصول
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Count))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]

    public int Count { get; set; }

    public override CartItemRequestViewModel ToRequest()
    {
        var result = new CartItemRequestViewModel
        {
            Id = Id,
            IsActive = IsActive,
            Ordering = Ordering,
            Count = Count,
            Description = Description,
            ProfileId = ProfileId,
            ProductId = ProductId,
        };
        return result;
    }
}

public class CartItemRequestViewModel : BaseRequestViewModel
{
    // *********************************************
    /// <summary>
    /// شناسه کاربر
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Profile))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        length: FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string ProfileId { get; set; }

    // *********************************************


    // *********************************************
    /// <summary>
    /// شناسه محصول
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Product))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        length: FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string ProductId { get; set; }

    // *********************************************

    // *********************************************
    /// <summary>
    /// تعداد محصول
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Count))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]

    public int Count { get; set; }

    public override Result Validate()
    {
        var result = new FluentResults.Result();
        return result.ConvertToSampleResult();
    }
}