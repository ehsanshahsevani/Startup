using ViewmodelSeedworks.Response;
using Attribute = System.Attribute;
using System.ComponentModel.DataAnnotations;
using SampleResult;
using ViewmodelSeedworks.Request;

namespace ViewModels.Marketplace;

public class AttributeValueResponseViewModel : BaseResponseViewModel<AttributeValueRequestViewModel>
{
    // *********************************************
    /// <summary>
    /// شناسه ویژگی
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Attribute))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    public string AttributeId { get; set; }

    /// <summary>
    /// شناسه ویژگی
    /// </summary>
    // public Attribute Attribute { get; set; }
    
    public string? AttributeDisplayName { get; set; }
    // *********************************************


    // *********************************************
    /// <summary>
    /// شناسه مقدار
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Value))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string ValueId { get; set; }
    
    public string? ValueDisplayName { get; set; }
    // /// <summary>
    // /// شناسه مقدار
    // /// </summary>
    // public Value Value { get; set; }
    // *********************************************
    
    public override AttributeValueRequestViewModel ToRequest()
    {
        return new AttributeValueRequestViewModel
        {
            Id = Id,
            AttributeId = AttributeId,
             Description = Description,
             IsActive = IsActive,
             ValueId = ValueId,
             Ordering = Ordering
        };
    }
}

public class AttributeValueRequestViewModel : BaseRequestViewModel
{
    // *********************************************
    /// <summary>
    /// شناسه ویژگی
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Attribute))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string AttributeId { get; set; }
    // *********************************************


    // *********************************************
    /// <summary>
    /// شناسه مقدار
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Value))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string ValueId { get; set; }
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

        return result.ConvertToSampleResult();
    }
}