using ViewmodelSeedworks.Response;
using System.ComponentModel.DataAnnotations;
using SampleResult;
using ViewmodelSeedworks.Request;

namespace ViewModels.ProjectManager;

public class AddressResponseViewModel : BaseResponseViewModel<AddressRequestViewModel>
{
#pragma warning disable CS8618, CS9264
    public AddressResponseViewModel()
#pragma warning restore CS8618, CS9264
    {
    }
    
    // *********************************************
    /// <summary>
    /// شهر
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CityLbl))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    public string CityId { get; set; }

    /// <summary>
    /// شهر
    /// </summary>
    public CityResponseViewModel? CityViewModel { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// کد پستی
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.PostalCode))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    public string ZipCode { get; set; }
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
    public UserResponseViewModel UserViewModel { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// شماره دریافت کننده
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ReciverMobileNumber))]
    public string? ReciverMobileNumber { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// نام دریافت کننده
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Recipient))]
    
    public string? ReciverName { get; set; }
    // *********************************************

    public override AddressRequestViewModel ToRequest()
    {
        throw new NotImplementedException();
    }
}

public class AddressRequestViewModel : BaseRequestViewModel
{
    public override Result Validate()
    {
        throw new NotImplementedException();
    }
}