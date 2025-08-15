using System.ComponentModel.DataAnnotations;
using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.ProjectManager;

public class NotificationTypeResponseViewModel: BaseResponseViewModel<NotificationTypeRequestViewModel>
{
#pragma warning disable CS8618, CS9264
    public NotificationTypeResponseViewModel()
#pragma warning restore CS8618, CS9264
    {
    }
    
    // *********************************************    
    /// <summary>
    /// عنوان
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Title))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    public string Title { get; set; }
    // *********************************************

    // *********************************************    
    /// <summary>
    /// عمومی
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.IsPublic))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    public bool IsPublic { get; set; }
    // *********************************************
    public override NotificationTypeRequestViewModel ToRequest()
    {
        throw new NotImplementedException();
    }
}

public class NotificationTypeRequestViewModel : BaseRequestViewModel
{
    public override Result Validate()
    {
        throw new NotImplementedException();
    }
}