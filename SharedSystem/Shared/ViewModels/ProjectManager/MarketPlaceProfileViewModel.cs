using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.ProjectManager;

public class MarketPlaceProfileResponseViewModel : BaseResponseViewModel<MarketPlaceProfileRequestViewModel>
{
#pragma warning disable CS8618, CS9264
    public MarketPlaceProfileResponseViewModel()
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
    /// نام کاربر
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.UserName))]
    public string? DisplayName { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// جنسیت
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Gender))]
    public string? GenderId { get; set; }
    /// <summary>
    /// جنسیت
    /// </summary>
    public GenderResponseViewModel? GenderViewModel { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// نام شغل
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.JobPost))]
    public string? JobString { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// تاریخ تولد
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.BirhtDate))]
    public DateTime? BirthDate { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// شماره شبا
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Shaba))]
    public string? Shaba { get; set; }
    // *********************************************

    public override MarketPlaceProfileRequestViewModel ToRequest()
    {
        throw new NotImplementedException();
    }
}

public class MarketPlaceProfileRequestViewModel : BaseRequestViewModel
{
    public override Result Validate()
    {
        throw new NotImplementedException();
    }
}