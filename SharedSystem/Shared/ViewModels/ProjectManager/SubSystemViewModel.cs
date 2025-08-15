using System.ComponentModel.DataAnnotations;
using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.ProjectManager;

public class SubSystemResponseViewModel : BaseResponseViewModel<SubSystemRequestViewModel>
{
	public SubSystemResponseViewModel(string nameEn, string nameFa)
	{
		NameFA = nameFa;
		NameEN = nameEn;
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
	
	private SubSystemResponseViewModel()
	{
	}
	
	public override SubSystemRequestViewModel ToRequest()
	{
		throw new NotImplementedException();
	}
}

public class SubSystemRequestViewModel : BaseRequestViewModel
{
	public override Result Validate()
	{
		throw new NotImplementedException();
	}
}