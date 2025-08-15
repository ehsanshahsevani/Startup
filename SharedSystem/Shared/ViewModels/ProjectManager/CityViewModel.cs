using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.ProjectManager;

public class CityResponseViewModel  : BaseResponseViewModel<CityRequestViewModel>
{
#pragma warning disable CS8618, CS9264
    public CityResponseViewModel()
#pragma warning restore CS8618, CS9264
    {
    }
    
    // *********************************************
    /// <summary>
    /// استان
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ProvinceLbl))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    public string ProvinceId { get; set; }
    
    /// <summary>
    /// استان
    /// </summary>
    
    public ProvinceResponseViewModel? ProvinceViewModel { get; set; }
    // *********************************************

    public override CityRequestViewModel ToRequest()
    {
        throw new NotImplementedException();
    }
}

public class CityRequestViewModel : BaseRequestViewModel
{
    public override Result Validate()
    {
        throw new NotImplementedException();
    }
}