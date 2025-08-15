using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Marketplace;

public class ProductAttributeValueResponseViewModel : BaseResponseViewModel<ProductAttributeValueRequestViewModel>
{
	// *********************************************
	/// <summary>
	/// شناسه عنوان محصول
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ProductTitle))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
	
	public string ProductTitleId { get; set; }
	
	public string? ProductTitleDisplayName { get; set; }
	// *********************************************


	/// <summary>
	/// شناسه مقدار ویژگی
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ValueOfAttribute))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
	
	public string AttributeValueId { get; set; }
	
	public string? AttributeValueDisplayName { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// ویژگی تکرار شونده
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.RepeatFeature))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	public bool HasRepeatFeature { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// تاثیر در قیمت
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ImpactOnPrice))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	public bool HasImpactOnPrice { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// شناسه فروشگاه
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Shop))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
	
	public string ShopId { get; set; }
	
	public string? ShopDisplayName { get; set; }
	// *********************************************   
	
	public override ProductAttributeValueRequestViewModel ToRequest()
	{
		var result = new ProductAttributeValueRequestViewModel
		{
			Id = Id,
			ShopId = ShopId,
			IsActive = IsActive,
			Ordering = Ordering,
			Description = Description,
			ProductTitleId = ProductTitleId,
			HasImpactOnPrice = HasImpactOnPrice,
			HasRepeatFeature = HasRepeatFeature,
			AttributeValueId = AttributeValueId,
		};
		
		return result;
	}
}

public class ProductAttributeValueRequestViewModel : BaseRequestViewModel
{
	// *********************************************
	/// <summary>
	/// شناسه فروشگاه
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Shop))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string ShopId { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// شناسه عنوان محصول
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ProductTitle))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string ProductTitleId { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// شناسه مقدار ویژگی
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ValueOfAttribute))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string AttributeValueId { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// ویژگی تکرار شونده
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.RepeatFeature))]
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	public bool HasRepeatFeature { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// تاثیر در قیمت
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ImpactOnPrice))]
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	public bool HasImpactOnPrice { get; set; }
	// *********************************************

	public override Result Validate()
	{
		var result = new FluentResults.Result();

		var checkValidationResult =
			Utilities.ValidationHelper.GetValidationResults(this);

		if (checkValidationResult.Any() == true)
		{
			result.WithErrors(checkValidationResult.Select(x => x.ErrorMessage));
		}

		return result.ConvertToSampleResult();
	}
}