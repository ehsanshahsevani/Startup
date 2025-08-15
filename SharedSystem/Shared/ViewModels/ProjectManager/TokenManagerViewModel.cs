using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.ProjectManager;

public class TokenManagerResponseViewModel: BaseResponseViewModel<TokenManagerRequestViewModel>
{
#pragma warning disable CS8618, CS9264
    public TokenManagerResponseViewModel()
#pragma warning restore CS8618, CS9264
    {
    }
    
    // *********************************************
    /// <summary>
    /// توکن
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.TokenFA))]
    public string Token { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// کاربر
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.User))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    public string UserId { get; set; }
    /// <summary>
    /// کاربر
    /// </summary>
    public UserResponseViewModel? UserViewModel { get; set; }
    // *********************************************

    public override TokenManagerRequestViewModel ToRequest()
    {
        throw new NotImplementedException();
    }
}

public class TokenManagerRequestViewModel : BaseRequestViewModel
{
    public override Result Validate()
    {
        throw new NotImplementedException();
    }
}