using ViewmodelSeedworks.Response;
using Enums.ProjectManager;
using System.ComponentModel.DataAnnotations;
using SampleResult;
using ViewmodelSeedworks.Request;

namespace ViewModels.ProjectManager;

public class CommutingResponseViewModel  : BaseResponseViewModel<CommutingRequestViewModel>
{
#pragma warning disable CS8618, CS9264
    public CommutingResponseViewModel()
#pragma warning restore CS8618, CS9264
    {
    }
    
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

    // *********************************************
    /// <summary>
    /// نوع فعالیت
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CommutingType))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    public CommutingType CommutingType { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// توکن
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.TokenFA))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    public string Token { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// کد OTP
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.OtpCode))]
    
    public string? OptCode { get; set; }
    // *********************************************

    public override CommutingRequestViewModel ToRequest()
    {
        throw new NotImplementedException();
    }
}

public class CommutingRequestViewModel : BaseRequestViewModel
{
    public override Result Validate()
    {
        throw new NotImplementedException();
    }
}