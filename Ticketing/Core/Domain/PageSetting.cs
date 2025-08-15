using System.ComponentModel.DataAnnotations;
using Constants;
using Domain.Base;
using Resources;

namespace Domain;

/// <summary>
///     تنظیمات صفحه
/// </summary>
public class PageSetting : BaseEntity
{
#pragma warning disable CS8618, CS9264
    public PageSetting()
#pragma warning restore CS8618, CS9264
    {
    }

    // *********************************************
    /// <summary>
    ///     محتوا
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.SubSystem))]

    // NVARCHAR(MAX)
    public string? Content { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     آدرس صفحه
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.LinkURL))]
    [MaxLength(
        MaxLength.Link,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string? Link { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     رنگ پس زمینه
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.BackgroundColor))]
    [MaxLength(
        MaxLength.Title,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string? BackgroundColor { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     رنگ پیش زمینه
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.ForegroundColor))]
    [MaxLength(
        MaxLength.Title,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string? ForegroundColor { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     عرض تصویر در وب
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.width))]
    public int WidthImageInWeb { get; set; }

    /// <summary>
    ///     ظول تصویر در وب
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Height))]
    public int HeightImageInWeb { get; set; }
    // *********************************************

    #region Booleans

    // *********************************************
    /// <summary>
    ///     عنوان صفحه
    ///     در سربرگ مرورگر
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.PageTitle))]
    [MaxLength(
        MaxLength.Description,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string? PageTitle { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     فعال در وب
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.IsActiveInWeb))]

    public bool IsActiveInWeb { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     فعال در موبایل
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.IsActiveInMobile))]

    public bool IsActiveInMobile { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     باز کردن لینک در صفحه جدید
    ///     target="_blank"
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.TargetIsBlank))]

    public bool TargetIsBlank { get; set; }
    // *********************************************

    #endregion /Booleans

    #region FAQ

    // *********************************************
    /// <summary>
    ///     تعداد بازدیدها
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.SeenCount))]

    public int SeenCount { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     زمان مطالعه
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.ReadTime))]

    public int ReadTime { get; set; }
    // *********************************************

    #endregion /FAQ

    #region Image Mobile

    /// <summary>
    ///     نام مستعار فایل
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.GuidName))]
    [MaxLength(
        MaxLength.Link,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string? FileNameMobile { get; set; }

    /// <summary>
    ///     نام مستعار فایل کوچک شده
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.GuidName))]
    [MaxLength(
        MaxLength.Link,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string? FileThumbnailNameMobile { get; set; }

    /// <summary>
    ///     نام اصلی فایل
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.FileName))]
    [MaxLength(
        MaxLength.Link,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string? FileOriginalNameMobile { get; set; }

    public bool FileIsExistMobile()
    {
        if (FileOriginalNameMobile is not null && string.IsNullOrEmpty(FileNameMobile) == false) return true;

        return false;
    }

    #endregion /Image Mobile

    #region Image Web

    /// <summary>
    ///     نام مستعار فایل
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.GuidName))]
    [MaxLength(
        MaxLength.Link,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string? FileNameWeb { get; set; }

    /// <summary>
    ///     نام مستعار فایل کوچک شده
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.GuidName))]
    [MaxLength(
        MaxLength.Link,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string? FileThumbnailNameWeb { get; set; }

    /// <summary>
    ///     نام اصلی فایل
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.FileName))]
    [MaxLength(
        MaxLength.Link,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string? FileOriginalNameWeb { get; set; }

    public bool FileIsExistWeb()
    {
        if (FileOriginalNameWeb is not null && string.IsNullOrEmpty(FileNameWeb) == false) return true;

        return false;
    }

    #endregion /Image Web

    #region Image Seo

    /// <summary>
    ///     توضیحات کاملی درباره تصویر برای گوگل
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Alternative))]
    [MaxLength(
        MaxLength.Description,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string? Alternative { get; set; }

    /// <summary>
    ///     توضیحات تصویر برای زمانی که کاربر روی تصویر با موس ایستاد
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.ImageTitle))]
    [MaxLength(
        MaxLength.Title,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string? ImageTitle { get; set; }

    #endregion /Image Seo
}