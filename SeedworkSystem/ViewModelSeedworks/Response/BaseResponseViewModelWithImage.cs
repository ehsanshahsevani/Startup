using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using ViewmodelSeedworks.Request;

namespace ViewmodelSeedworks.Response;

public abstract class BaseResponseViewModelWithImage<T> :
    BaseResponseViewModel<T> where T : BaseRequestViewModel
{

    public string? FileName { get; set; }
    
    public string? FileThumbnailName { get; set; }
    
    
    public string? FileOriginalName { get; set; }

    /// <summary>
    /// توضیحات کاملی درباره تصویر برای گوگل
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Alternative))]
    
    [MaxLength(
        Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string? Alternative { get; set; }

    /// <summary>
    /// توضیحات تصویر برای زمانی که کاربر روی تصویر با موس ایستاد
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ImageTitle))]
    
    [MaxLength(
        Constants.MaxLength.Title,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string? ImageTitle { get; set; }
    
    public bool FileHasExist => string.IsNullOrEmpty(FileName) == false;
    public bool FileThumbnailHasExist => string.IsNullOrEmpty(FileThumbnailName) == false;
    
    public string FileThumbnailUrl =>
        $"{DomainApiAttachmentManager}/{UploadsFolderName}/{ServerId}/{FileThumbnailName}";

    public string FileUrl =>
        $"{DomainApiAttachmentManager}/{UploadsFolderName}/{ServerId}/{FileName}";

    public void SetAttachmentInThisResponse<T>(T? attachment)
        where T : DomainSeedworks.BaseAttachment
    {
        if (attachment is not null)
        {
            this.ImageTitle = attachment.ImageTitle;
            this.Alternative = attachment.Alternative;
            this.FileName = attachment.FileName;
            this.FileThumbnailName = attachment.FileThumbnailName;
            this.FileOriginalName = attachment.FileOriginalName;
        }
    }
}