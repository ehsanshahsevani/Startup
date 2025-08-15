using Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain;

/// <summary>
/// تنظیمات صفحه
/// </summary>
public class PageSetting : BaseEntity
{
#pragma warning disable CS8618, CS9264
	public PageSetting() : base()
#pragma warning restore CS8618, CS9264
	{
		PageSettingTagPageSettings = new List<PageSettingTagPageSetting>();
	}

	// *********************************************
	/// <summary>
	/// نام
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Name))]
		
	[MaxLength(
		length: Constants.MaxLength.Title,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? Name { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// عنوان صفحه
	/// در سربرگ مرورگر
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.PageTitle))]
	
	[MaxLength(
		length: Constants.MaxLength.Description,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? PageTitle { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// محتوا
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Content))]

	// NVARCHAR(MAX)
	// ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
	public string? Content { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// آدرس صفحه
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.LinkURL))]
	
	[MaxLength(
		length: Constants.MaxLength.Link,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? Link { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// رنگ پس زمینه
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.BackgroundColor))]
	
	[MaxLength(
		length: Constants.MaxLength.Title,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? BackgroundColor { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// رنگ پیش زمینه
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ForegroundColor))]
	
	[MaxLength(
		length: Constants.MaxLength.Title,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? ForegroundColor { get; set; }
	// *********************************************

	// **************************************************
	public List<PageSettingTagPageSetting> PageSettingTagPageSettings { get; set; }
	// **************************************************

	#region Booleans

	// *********************************************
	/// <summary>
	/// باز کردن لینک در صفحه جدید
	/// target="_blank"
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.TargetIsBlank))]

	public bool TargetIsBlank { get; set; }
	// *********************************************

	#endregion /Booleans

	#region FAQ

	// *********************************************
	/// <summary>
	/// محتوا سوال
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Question))]
	[MaxLength(
		length: Constants.MaxLength.Description,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? Question { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// محتوا پاسخ
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Answer))]

	// NVARCHAR(MAX)
	// ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
	public string? Answer { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// عنوان گروه بندی
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.GroupByName))]
	[MaxLength(
		length: Constants.MaxLength.Title,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? GroupByName { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// تعداد بازدیدها
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.SeenCount))]

	public int SeenCount { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// زمان مطالعه
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ReadTime))]

	public int ReadTime { get; set; }
	// *********************************************

	#endregion /FAQ

	#region Image Mobile

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

	public string? FileNameMobile { get; set; }

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

	public string? FileThumbnailNameMobile { get; set; }

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

	public string? FileOriginalNameMobile { get; set; }

	public bool FileIsExistMobile()
	{
		if (FileOriginalNameMobile is not null && string.IsNullOrEmpty(FileNameMobile) == false)
		{
			return true;
		}

		return false;
	}

	#endregion /Image Mobile

	#region Image Web

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

	public string? FileNameWeb { get; set; }

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

	public string? FileThumbnailNameWeb { get; set; }

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

	public string? FileOriginalNameWeb { get; set; }

	public bool FileIsExistWeb()
	{
		if (FileOriginalNameWeb is not null && string.IsNullOrEmpty(FileNameWeb) == false)
		{
			return true;
		}

		return false;
	}

	#endregion /Image Web

	#region Icon

	/// <summary>
	/// نام مستعار آیکن
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.GuidName))]
	[MaxLength(
		Constants.MaxLength.Link,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? FileNameIcon { get; set; }

	/// <summary>
	/// نام اصلی آیکن
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.FileName))]
	[MaxLength(
		Constants.MaxLength.Link,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? FileOriginalNameIcon { get; set; }

	public bool FileIsExistIcon()
	{
		if (FileOriginalNameIcon is not null && string.IsNullOrEmpty(FileNameIcon) == false)
		{
			return true;
		}

		return false;
	}

	#endregion /Icon

	#region Image Seo

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

	#endregion /Image Seo
}