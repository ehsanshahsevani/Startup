using SampleResult;
using Enums.Marketplace;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Marketplace;

public class AttachmentResponseViewModel
    : BaseResponseViewModelWithImage<AttachmentRequestViewModel>
{
    // *********************************************
    /// <summary>
    /// شناسه زیرسیستم
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.SubSystem))]
   
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string SubSystemLocalId { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// شناسه جدول
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Relation))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string RelationId { get; set; }
    // *********************************************
   
    // *********************************************
    /// <summary>
    /// نوع پیوست
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.AttachmentSubject))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string AttachmentSubjectId { get; set; }

    public string? AttachmentSubjectDisplayName { get; set; }
    // *********************************************
    
    public override AttachmentRequestViewModel ToRequest()
    {
        var result = new AttachmentRequestViewModel
        {
            Id = Id,
            IsActive = IsActive,
            Ordering = Ordering,

            RelationId = RelationId,
            SubSystemLocalId = SubSystemLocalId,
            AttachmentSubjectId = AttachmentSubjectId,
            
            ImageTitle = ImageTitle,
            Alternative = Alternative,
        };
        
        return result;
    }
}

public class AttachmentRequestViewModel : BaseRequestViewModelWithImage
{
    // *********************************************
    /// <summary>
    /// شناسه زیرسیستم
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.SubSystem))]
   
    public string? SubSystemLocalId { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// شناسه جدول
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Relation))]

    public string? RelationId { get; set; }
    // *********************************************
   
    // *********************************************
    /// <summary>
    /// نوع پیوست
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.AttachmentSubject))]

    public string? AttachmentSubjectId { get; set; }
    // *********************************************

    // *********************************************
    public override Result Validate()
    {
        var result = new FluentResults.Result();

        if (string.IsNullOrEmpty(SubSystemLocalId) == true)
        {
            string errorMessage = string.Format(
                Resources.Messages.RequiredError, Resources.DataDictionary.SubSystemLocal);

            result.WithError(errorMessage);
        }

        if (string.IsNullOrEmpty(RelationId) == true)
        {
            string errorMessage = string.Format(
                Resources.Messages.RequiredError, Resources.DataDictionary.Guid);

            result.WithError(errorMessage);
        }

        if (string.IsNullOrEmpty(AttachmentSubjectId) == true)
        {
            string errorMessage = string.Format(
                Resources.Messages.RequiredError, Resources.DataDictionary.AttachmentSubject);

            result.WithError(errorMessage);
        }

        if (FileUpload is null)
        {
            string errorMessage = string.Format(
                Resources.Messages.RequiredError, Resources.DataDictionary.File);

            result.WithError(errorMessage);
        }

        return result.ConvertToSampleResult();
    }
    // *********************************************
}