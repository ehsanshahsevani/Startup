using System.ComponentModel.DataAnnotations;
using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.Marketplace;

public class AttributeResponseViewModel : BaseResponseViewModel<AttributeRequestViewModel>
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
	
	public override AttributeRequestViewModel ToRequest()
	{

		var result = new AttributeRequestViewModel()
		{
			Id = Id,
			Name = Name,
			IsActive = IsActive,
			Ordering = Ordering,
			Description = Description,
		};
		
		return result;
	}
}

public class AttributeRequestViewModel : BaseRequestViewModel
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
		
		var checkValidationRsult =
			Utilities.ValidationHelper.GetValidationResults(this);
            
		if (checkValidationRsult.Any())
		{
			result.WithErrors(checkValidationRsult.Select(x => x.ErrorMessage));
		}
            
		if (string.IsNullOrEmpty(Name) == true)
		{
			var errorMessage =
				string.Format(Resources.Messages.RequiredError, Resources.DataDictionary.NameFA);
                
			result.WithError(errorMessage);
		}

		return result.ConvertToSampleResult();
	}
}