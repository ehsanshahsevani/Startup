using System.ComponentModel.DataAnnotations;

namespace DomainSeedworks;

public class BaseAttachment : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	public BaseAttachment() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	{
	}

     /// <summary>
     /// نام مستعار فایل
     /// </summary>

     [Display(
         ResourceType = typeof(Resources.DataDictionary),
         Name = nameof(Resources.DataDictionary.GuidName))]
     
     [MaxLength(
         Constants.MaxLength.Link,
         ErrorMessageResourceType = typeof(Resources.Messages),
         ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

     public string? FileName { get; set; }

     /// <summary>
     /// نام مستعار فایل کوچک شده
     /// </summary>

     [Display(
         ResourceType = typeof(Resources.DataDictionary),
         Name = nameof(Resources.DataDictionary.GuidName))]
     
     [MaxLength(
         Constants.MaxLength.Link,
         ErrorMessageResourceType = typeof(Resources.Messages),
         ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

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
     
     public bool FileIsExist()
     {
         if (FileOriginalName is not null && string.IsNullOrEmpty(FileName) == false)
         {
             return true;
         }

         return false;
     }
}