using System.ComponentModel.DataAnnotations;
using ViewmodelSeedworks.Request;

namespace ViewmodelSeedworks.Response;

public abstract class BaseResponseViewModelWithNameAndImage<T> : BaseResponseViewModel<T>
    where T : BaseRequestViewModel
{
    public string? FileName { get; set; }
    
    public string? FileThumbnailName { get; set; }


    /// <summary>
    /// نام اصلی فایل
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.FileName))]
    [MaxLength(
        Constants.MaxLength.Link,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string? FileOrginalName { get; set; }

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
}