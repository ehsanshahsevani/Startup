using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Marketplace;

public class ProductTitleResponseViewModel : BaseResponseViewModel<ProductTitleRequestViewModel>
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
	
	// *********************************************
	/// <summary>
	///  شناسه کاربر
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.User))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string UserId { get; private set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// دسته بندی
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Category))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string CategoryId { get; set; }
	
	public string? CategoryDisplayName { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// توضیحات سئو
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.SeoDescription))]

	[MaxLength(
		length: Constants.MaxLength.Description,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? SeoDescription { get; set; }
	// *********************************************

	public override ProductTitleRequestViewModel ToRequest()
	{
		var result = new ProductTitleRequestViewModel
		{
			Id = Id,
			Name = Name,
			IsActive = IsActive,
			Description = Description,
			Ordering = Ordering,
			CategoryId = CategoryId,
			SeoDescription = SeoDescription,
			UserId = UserId,
		};

		return result;
	}
}

public class ProductTitleRequestViewModel : BaseRequestViewModel
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
	
	// *********************************************
	/// <summary>
	///  شناسه کاربر :
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.User))]
	
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? UserId { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// دسته بندی
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Category))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string CategoryId { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// توضیحات سئو
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.SeoDescription))]

	[MaxLength(
		length: Constants.MaxLength.Description,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? SeoDescription { get; set; }
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

		if (string.IsNullOrEmpty(CategoryId) == true)
		{
			var errorMessage =
				string.Format(Resources.Messages.RequiredError, Resources.DataDictionary.Category);

			result.WithError(errorMessage);
		}

		return result.ConvertToSampleResult();
	}
}