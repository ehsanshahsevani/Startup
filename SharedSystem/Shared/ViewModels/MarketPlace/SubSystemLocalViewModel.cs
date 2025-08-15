using System.ComponentModel.DataAnnotations;
using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.Marketplace;

public class SubSystemLocalResponseViewModel : BaseResponseViewModel<SubSystemLocalRequestViewModel>
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
    
	public string NameFA { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// نام انگلیسی
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.NameEN))]
    
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
	[MaxLength(
		Constants.MaxLength.Name,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
	public string NameEN { get; set; }
	// *********************************************
	
	public override SubSystemLocalRequestViewModel ToRequest()
	{
		throw new NotImplementedException();
	}
}

public class SubSystemLocalRequestViewModel : BaseRequestViewModel
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
    
	public string NameFA { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// نام انگلیسی
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.NameEN))]
    
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
	[MaxLength(
		Constants.MaxLength.Name,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
	public string NameEN { get; set; }
	// *********************************************
	
	public override Result Validate()
	{
		throw new NotImplementedException();
	}
}