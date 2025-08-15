using System.ComponentModel.DataAnnotations;
using AngleSharp.Dom;
using Constants;
using Microsoft.AspNetCore.Http;
using Resources;
using SampleResult;
using Utilities;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;
using Result = FluentResults.Result;

namespace ViewModels.Marketplace;

public class PageSettingResponseViewModel :
	BaseResponseViewModel<PageSettingRequestViewModel>
{
	// *********************************************
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	public PageSettingResponseViewModel() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	{
		TagNames = new List<string>();
	}
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// نام فارسی
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.NameFA))]
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	[MaxLength(
		Constants.MaxLength.Name,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string Name { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// محتوا
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.SubSystem))]

	// NVARCHAR(MAX)
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

	// *********************************************
	/// <summary>
	/// یک صفحه مربوط به دسته بندی خاص
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.SubSystem))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? CategoryId { get; set; }

	/// <summary>
	/// دسته بندی
	/// </summary>
	public string? CategoryDisplayName { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// عرض تصویر در وب
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.width))]

	public int WidthImageInWeb { get; set; }

	/// <summary>
	/// ظول تصویر در وب
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Height))]

	public int HeightImageInWeb { get; set; }
	// *********************************************

	#region Tag

	// *********************************************
	/// <summary>
	/// تگ
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.PinInMobile))]

	public string? Tag { get; set; }
	// *********************************************
	
	// *********************************************
	public List<string> TagNames { get; set; }
	// *********************************************

	#endregion /Tag

	#region Booleans

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
	/// فعال در وب
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActiveInWeb))]

	public bool IsActiveInWeb { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// فعال در موبایل
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActiveInMobile))]

	public bool IsActiveInMobile { get; set; }
	// *********************************************

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
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.Question))]
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
	[MaxLength(
		length: MaxLength.Description,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string Question { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// محتوا پاسخ
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.Answer))]
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]

	// NVARCHAR(MAX)
	// ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
	public string Answer { get; set; }
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


	// *********************************************
	/// <summary>
	/// عنوان گروه بندی
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.GroupByName))]
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
	[MaxLength(
		length: MaxLength.Title,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string GroupByName { get; set; }
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

	public bool FileMobileHasExist => string.IsNullOrEmpty(FileNameMobile) == false;
	public bool FileThumbnailMobileHasExist => string.IsNullOrEmpty(FileThumbnailNameMobile) == false;

	public string FileThumbnailMobileUrl =>
		$"{DomainApiAttachmentManager}/{UploadsFolderName}/{ServerId}/{FileThumbnailNameMobile}";

	public string FileMobileUrl =>
		$"{DomainApiAttachmentManager}/{UploadsFolderName}/{ServerId}/{FileNameMobile}";

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

	public bool FileWebHasExist => string.IsNullOrEmpty(FileNameWeb) == false;
	public bool FileThumbnailWebHasExist => string.IsNullOrEmpty(FileThumbnailNameWeb) == false;

	public string FileThumbnailWebUrl =>
		$"{DomainApiAttachmentManager}/{UploadsFolderName}/{ServerId}/{FileThumbnailNameWeb}";

	public string FileWebUrl =>
		$"{DomainApiAttachmentManager}/{UploadsFolderName}/{ServerId}/{FileNameWeb}";

	#endregion /Image Web

	#region Icon

	/// <summary>
	/// نام مستعار آیکن
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.GuidName))]
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
	[MaxLength(
		MaxLength.Link,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string? FileNameIcon { get; set; }

	/// <summary>
	/// نام اصلی آیکن
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.FileName))]
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
	[MaxLength(
		MaxLength.Link,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string? FileOriginalNameIcon { get; set; }

	public bool FileIsExistIcon()
	{
		if (FileOriginalNameIcon is not null && string.IsNullOrEmpty(FileNameIcon) == false)
		{
			return true;
		}

		return false;
	}

	public bool FileIconHasExist => string.IsNullOrEmpty(FileNameIcon) == false;

	public string FileUrlIcon =>
		$"{DomainApiAttachmentManager}/{UploadsFolderName}/{ServerId}/{FileNameIcon}";

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

	public override PageSettingRequestViewModel ToRequest()
	{
		var model = new PageSettingRequestViewModel
		{
			Id = Id,
			Name = Name,
			CategoryId = CategoryId,
			IsActive = IsActive,
			Description = Description,
			Link = Link,
			PageTitle = PageTitle,
			Alternative = Alternative,
			ImageTitle = ImageTitle,
			BackgroundColor = BackgroundColor,
			ForegroundColor = ForegroundColor,
			Content = Content,
			HeightImageInWeb = HeightImageInWeb,
			WidthImageInWeb = WidthImageInWeb,
			IsActiveInMobile = IsActiveInMobile,
			IsActiveInWeb = IsActiveInWeb,
			Ordering = Ordering,
			ReadTime = ReadTime,
			SeenCount = SeenCount,
			TargetIsBlank = TargetIsBlank,
			
			// TagNames = TagNames,
		};

		return model;
	}

	public FaqResponseViewModel ToFaqResponse()
	{
		var model = new FaqResponseViewModel()
		{
			Id = Id,
			IsActive = IsActive,
			Ordering = Ordering,
			ReadTime = ReadTime,
			SeenCount = SeenCount,
			Description = Description,

			ServerId = ServerId,

			FileNameIcon = FileNameIcon,
			FileOriginalNameIcon = FileOriginalNameIcon,

			Answer = Answer,
			Question = Question,
			GroupByName = GroupByName,
		};

		return model;
	}

	public BannerResponseViewModel ToBannerResponse()
	{
		var model = new BannerResponseViewModel()
		{
			Id = Id,
			IsActive = IsActive,
			Ordering = Ordering,
			Description = Description,

			ServerId = ServerId,

			FileNameWeb = FileNameWeb,
			FileOriginalNameWeb = FileOriginalNameWeb,
			FileNameMobile = FileNameMobile,
			FileOriginalNameMobile = FileOriginalNameMobile,
			FileThumbnailNameMobile = FileThumbnailNameMobile,
			FileThumbnailNameWeb = FileThumbnailNameWeb,

			Name = Name,
			Content = Content,

			TargetIsBlank = TargetIsBlank,
			
			TagNames = TagNames,
		};

		return model;
	}
	
	public TextDynamicResponseViewModel ToTextDynamicResponse()
	{
		var model = new TextDynamicResponseViewModel()
		{
			Id = Id,
			IsActive = IsActive,
			Ordering = Ordering,
			Description = Description,

			ServerId = ServerId,
			
			Content = Content,
			
			TagNames = TagNames,
		};

		return model;
	}


	public SocialResponseViewModel ToSocialResponse()
	{
		var model = new SocialResponseViewModel()
		{
			Id = Id,
			IsActive = IsActive,
			Ordering = Ordering,
			Description = Description,

			ServerId = ServerId,

			FileNameIcon = FileNameIcon,
			FileOriginalNameIcon = FileOriginalNameIcon,

			Link = Link,
		};

		return model;
	}
}

public class PageSettingRequestViewModel : BaseRequestViewModel
{
	// *********************************************
	/// <summary>
	/// نام فارسی
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.NameFA))]
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	[MaxLength(
		Constants.MaxLength.Name,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string Name { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// محتوا
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.SubSystem))]

	// NVARCHAR(MAX)
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

	// *********************************************
	/// <summary>
	/// یک صفحه مربوط به دسته بندی خاص
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.SubSystem))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? CategoryId { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// عرض تصویر در وب
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.width))]

	public int WidthImageInWeb { get; set; }

	/// <summary>
	/// ظول تصویر در وب
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Height))]

	public int HeightImageInWeb { get; set; }
	// *********************************************

	#region Tag

	// *********************************************
	public Dictionary<string, string> Tags { get; set; }
	// *********************************************

	#endregion /Tag

	#region Booleans

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
	/// فعال در وب
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActiveInWeb))]

	public bool IsActiveInWeb { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// فعال در موبایل
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActiveInMobile))]

	public bool IsActiveInMobile { get; set; }
	// *********************************************

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

	public int? ReadTime { get; set; }
	// *********************************************

	#endregion /FAQ

	#region Image Mobile

	public IFormFile? FileMobile { get; set; }

	#endregion /Image Mobile

	#region Image Web

	public IFormFile? FileWeb { get; set; }

	#endregion /Image Web

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

	public override SampleResult.Result Validate()
	{
		var result = new Result();
		if (string.IsNullOrEmpty(Name) == true)
		{
			var errorMessage = string.Format(
				Resources.Messages.RequiredError, Resources.DataDictionary.Name);

			result.WithError(errorMessage);
		}

		return result.ConvertToSampleResult();
	}
}

#region Faq

public class FaqResponseViewModel : BaseResponseViewModel<FaqRequestViewModel>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	public FaqResponseViewModel() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	{
	}

	// *********************************************
	/// <summary>
	/// محتوا سوال
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.Question))]
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
	[MaxLength(
		length: MaxLength.Description,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string Question { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// محتوا پاسخ
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.Answer))]
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]

	// NVARCHAR(MAX)
	// ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
	public string Answer { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// عنوان گروه بندی
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.GroupByName))]
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
	[MaxLength(
		length: MaxLength.Title,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string GroupByName { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// تعداد بازدیدها
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.SeenCount))]

	public int? SeenCount { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// زمان مطالعه
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.ReadTime))]

	public int? ReadTime { get; set; }
	// *********************************************

	// *********************************************

	#region Icon

	/// <summary>
	/// نام مستعار آیکن
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.GuidName))]
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
	[MaxLength(
		MaxLength.Link,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string? FileNameIcon { get; set; }

	/// <summary>
	/// نام اصلی آیکن
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.FileName))]
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
	[MaxLength(
		MaxLength.Link,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string? FileOriginalNameIcon { get; set; }

	public bool FileIsExistIcon()
	{
		if (FileOriginalNameIcon is not null && string.IsNullOrEmpty(FileNameIcon) == false)
		{
			return true;
		}

		return false;
	}

	public bool FileIconHasExist => string.IsNullOrEmpty(FileNameIcon) == false;

	public string FileUrlIcon =>
		$"{DomainApiAttachmentManager}/{UploadsFolderName}/{ServerId}/{FileNameIcon}";

	#endregion /Icon

	// *********************************************

	public override FaqRequestViewModel ToRequest()
	{
		var request = new FaqRequestViewModel
		{
			Id = Id,
			IsActive = IsActive,
			Ordering = Ordering,
			Description = Description,

			Answer = Answer,
			ReadTime = ReadTime,
			Question = Question,
			GroupByName = GroupByName,

			Icon = null,
		};

		return request;
	}
}

public class FaqRequestViewModel : BaseRequestViewModel
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	public FaqRequestViewModel() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	{
	}

	// *********************************************
	/// <summary>
	/// محتوا سوال
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.Question))]
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
	[MaxLength(
		length: MaxLength.Description,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string Question { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// محتوا پاسخ
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.Answer))]
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]

	// NVARCHAR(MAX)
	// ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
	public string Answer { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// عنوان گروه بندی
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.GroupByName))]
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
	[MaxLength(
		length: MaxLength.Title,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string GroupByName { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// زمان مطالعه
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.ReadTime))]

	public int? ReadTime { get; set; }
	// *********************************************

	#region Icon

	// *********************************************
	/// <summary>
	/// نام مستعار آیکن
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.GuidName))]

	public IFormFile? Icon { get; set; }

	// *********************************************

	#endregion /Icon

	// *********************************************
	public override SampleResult.Result Validate()
	{
		var result = new Result();

		var resultValidate =
			ValidationHelper.GetValidationResults(this);

		result.WithErrors(resultValidate.Select(x => x.ErrorMessage));

		if (Id is null && Icon is null)
		{
			var errorMessage =
				string.Format(
					Messages.RequiredError,
					DataDictionary.File);

			result.WithError(errorMessage);
		}

		return result.ConvertToSampleResult();
	}
	// *********************************************
}

#endregion /Faq

#region Banner

public class BannerResponseViewModel : BaseResponseViewModel<BannerRequestViewModel>
{
	public BannerResponseViewModel() : base()
	{
		TagNames = new List<string>();
	}

	// *********************************************
	/// <summary>
	/// نام
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.Name))]
	[MaxLength(
		length: MaxLength.Title,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string? Name { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// لیست تگ های موجود
	/// </summary>
	public List<string> TagNames { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// محتوا
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.Content))]

	// NVARCHAR(MAX)
	// ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
	public string? Content { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// آدرس صفحه
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.LinkURL))]
	[MaxLength(
		length: MaxLength.Link,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string? Link { get; set; }
	// *********************************************

	#region Image Mobile

	/// <summary>
	/// نام مستعار فایل
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
	/// نام مستعار فایل کوچک شده
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
	/// نام اصلی فایل
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
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.GuidName))]
	[MaxLength(
		MaxLength.Link,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string? FileNameWeb { get; set; }

	/// <summary>
	/// نام مستعار فایل کوچک شده
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
	/// نام اصلی فایل
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
		if (FileOriginalNameWeb is not null && string.IsNullOrEmpty(FileNameWeb) == false)
		{
			return true;
		}

		return false;
	}

	#endregion /Image Web

	// *********************************************
	/// <summary>
	/// باز کردن لینک در صفحه جدید
	/// target="_blank"
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.TargetIsBlank))]

	public bool TargetIsBlank { get; set; }
	// *********************************************

	// *********************************************
	public bool FileMobileHasExist => string.IsNullOrEmpty(FileNameMobile) == false;
	public bool FileThumbnailMobileHasExist => string.IsNullOrEmpty(FileThumbnailNameMobile) == false;

	public string FileThumbnailMobileUrl =>
		$"{DomainApiAttachmentManager}/{UploadsFolderName}/{ServerId}/{FileThumbnailNameMobile}";

	public string FileUrlMobile =>
		$"{DomainApiAttachmentManager}/{UploadsFolderName}/{ServerId}/{FileNameMobile}";


	public bool FileWebHasExist => string.IsNullOrEmpty(FileNameWeb) == false;
	public bool FileThumbnailWebHasExist => string.IsNullOrEmpty(FileThumbnailNameWeb) == false;

	public string FileThumbnailWebUrl =>
		$"{DomainApiAttachmentManager}/{UploadsFolderName}/{ServerId}/{FileThumbnailNameWeb}";

	public string FileUrlWeb =>
		$"{DomainApiAttachmentManager}/{UploadsFolderName}/{ServerId}/{FileNameWeb}";
	// *********************************************

	public override BannerRequestViewModel ToRequest()
	{
		var request = new BannerRequestViewModel
		{
			Id = Id,
			IsActive = IsActive,
			Ordering = Ordering,
			Description = Description,

			Link = Link,
			Name = Name,
			Content = Content,
			TargetIsBlank = TargetIsBlank,

			TagNames = TagNames,
		};

		return request;
	}
}

public class BannerRequestViewModel : BaseRequestViewModel
{
	public BannerRequestViewModel() : base()
	{
		TagNames = new List<string>();
	}

	// *********************************************
	public List<string> TagNames { get; set; }

	public Dictionary<string, string> TagNamesDictionary()
	{ 
		var result = new Dictionary<string, string>();

			foreach (var item in TagNames.Distinct())
			{
				result.Add(item, string.Empty);
			}

			return result;
	}
	// *********************************************

	// *********************************************
	/// <summary>
	/// نام
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.Name))]
	[MaxLength(
		length: MaxLength.Title,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string? Name { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// محتوا
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.Content))]

	// NVARCHAR(MAX)
	// ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
	public string? Content { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// آدرس صفحه
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.LinkURL))]
	[MaxLength(
		length: MaxLength.Link,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string? Link { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// باز کردن لینک در صفحه جدید
	/// target="_blank"
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.TargetIsBlank))]

	public bool TargetIsBlank { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// فایل مناسب سایز وبسایت
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.FileWeb))]

	public IFormFile? FileWeb { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// فایل با سایز مناسب حالت موبایل
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.FileMobile))]

	public IFormFile? FileMobile { get; set; }
	// *********************************************

	public override SampleResult.Result Validate()
	{
		var result = new Result();

		var resultValidate =
			ValidationHelper.GetValidationResults(this);

		result.WithErrors(resultValidate.Select(x => x.ErrorMessage));

		if (Id is null && FileMobile is null)
		{
			var errorMessage =
				string.Format(
					Messages.RequiredError,
					DataDictionary.FileWeb);

			result.WithError(errorMessage);
		}

		if (Id is null && FileMobile is null)
		{
			var errorMessage =
				string.Format(
					Messages.RequiredError,
					DataDictionary.FileMobile);

			result.WithError(errorMessage);
		}

		if (TagNames.Count == 0)
		{
			var errorMessage =
				string.Format(
					Messages.RequiredError,
					DataDictionary.TagPageSetting);

			result.WithError(errorMessage);
		}

		if (TagNames.Any(x => x.StartsWith(nameof(DataDictionary.Banner)) == false))
		{
			var errorMessage =
				string.Format(
					Messages.RequestNotValid);

			result.WithError(errorMessage);
		}

		return result.ConvertToSampleResult();
	}
}

#endregion /Banner

#region Social

public class SocialResponseViewModel : BaseResponseViewModel<SocialRequestViewModel>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	public SocialResponseViewModel() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	{
	}

	// *********************************************
	/// <summary>
	/// آدرس صفحه
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.LinkURL))]
	[MaxLength(
		length: MaxLength.Link,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string? Link { get; set; }
	// *********************************************

	// *********************************************

	#region Icon

	/// <summary>
	/// نام مستعار آیکن
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.GuidName))]
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
	[MaxLength(
		MaxLength.Link,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string? FileNameIcon { get; set; }

	/// <summary>
	/// نام اصلی آیکن
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.FileName))]
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
	[MaxLength(
		MaxLength.Link,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string? FileOriginalNameIcon { get; set; }

	public bool FileIsExistIcon()
	{
		if (FileOriginalNameIcon is not null && string.IsNullOrEmpty(FileNameIcon) == false)
		{
			return true;
		}

		return false;
	}

	public bool FileIconHasExist => string.IsNullOrEmpty(FileNameIcon) == false;

	public string FileUrlIcon =>
		$"{DomainApiAttachmentManager}/{UploadsFolderName}/{ServerId}/{FileNameIcon}";

	#endregion /Icon

	// *********************************************

	public override SocialRequestViewModel ToRequest()
	{
		var request = new SocialRequestViewModel
		{
			Id = Id,
			IsActive = IsActive,
			Ordering = Ordering,
			Description = Description,

			Link = Link,
		};

		return request;
	}
}

public class SocialRequestViewModel : BaseRequestViewModel
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	public SocialRequestViewModel() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	{
	}

	// *********************************************
	/// <summary>
	/// آدرس صفحه
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.LinkURL))]
	[MaxLength(
		length: MaxLength.Link,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string? Link { get; set; }
	// *********************************************

	#region Icon

	// *********************************************
	/// <summary>
	/// نام مستعار آیکن
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.File))]
	public IFormFile? Icon { get; set; }

	// *********************************************

	#endregion /Icon

	// *********************************************
	public override SampleResult.Result Validate()
	{
		var result = new Result();

		var resultValidate =
			ValidationHelper.GetValidationResults(this);

		result.WithErrors(resultValidate.Select(x => x.ErrorMessage));

		if (Id is null && Icon is null)
		{
			var errorMessage =
				string.Format(Messages.RequiredError, DataDictionary.File);

			result.WithError(errorMessage);
		}

		if (string.IsNullOrEmpty(Link) == true)
		{
			var errorMessage =
				string.Format(Messages.RequiredError, DataDictionary.Link);

			result.WithError(errorMessage);
		}

		return result.ConvertToSampleResult();
	}
	// *********************************************
}

#endregion /Social

#region TextDynamic

public class TextDynamicResponseViewModel : BaseResponseViewModel<TextDynamicRequestViewModel>
{
	public TextDynamicResponseViewModel() : base()
	{
		TagNames = new List<string>();
	}

	// *********************************************
	/// <summary>
	/// نام
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.Name))]
	[MaxLength(
		length: MaxLength.Title,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string? Name { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// لیست تگ ها
	/// </summary>
	public List<string> TagNames { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// محتوا
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.Content))]

	// NVARCHAR(MAX)
	// ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
	public string? Content { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// آدرس صفحه
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.LinkURL))]
	[MaxLength(
		length: MaxLength.Link,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string? Link { get; set; }
	// *********************************************

	#region Image Mobile

	/// <summary>
	/// نام مستعار فایل
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
	/// نام مستعار فایل کوچک شده
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
	/// نام اصلی فایل
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
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.GuidName))]
	[MaxLength(
		MaxLength.Link,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string? FileNameWeb { get; set; }

	/// <summary>
	/// نام مستعار فایل کوچک شده
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
	/// نام اصلی فایل
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
		if (FileOriginalNameWeb is not null && string.IsNullOrEmpty(FileNameWeb) == false)
		{
			return true;
		}

		return false;
	}

	#endregion /Image Web

	// *********************************************
	/// <summary>
	/// باز کردن لینک در صفحه جدید
	/// target="_blank"
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.TargetIsBlank))]

	public bool TargetIsBlank { get; set; }
	// *********************************************

	// *********************************************
	public bool FileMobileHasExist => string.IsNullOrEmpty(FileNameMobile) == false;
	public bool FileThumbnailMobileHasExist => string.IsNullOrEmpty(FileThumbnailNameMobile) == false;

	public string FileThumbnailMobileUrl =>
		$"{DomainApiAttachmentManager}/{UploadsFolderName}/{ServerId}/{FileThumbnailNameMobile}";

	public string FileUrlMobile =>
		$"{DomainApiAttachmentManager}/{UploadsFolderName}/{ServerId}/{FileNameMobile}";


	public bool FileWebHasExist => string.IsNullOrEmpty(FileNameWeb) == false;
	public bool FileThumbnailWebHasExist => string.IsNullOrEmpty(FileThumbnailNameWeb) == false;

	public string FileThumbnailWebUrl =>
		$"{DomainApiAttachmentManager}/{UploadsFolderName}/{ServerId}/{FileThumbnailNameWeb}";

	public string FileUrlWeb =>
		$"{DomainApiAttachmentManager}/{UploadsFolderName}/{ServerId}/{FileNameWeb}";
	// *********************************************

	public override TextDynamicRequestViewModel ToRequest()
	{
		var request = new TextDynamicRequestViewModel
		{
			Id = Id,
			IsActive = IsActive,
			Ordering = Ordering,
			Description = Description,

			Content = Content,

			TagNames = TagNames,
		};
		
		return request;
	}
}

public class TextDynamicRequestViewModel : BaseRequestViewModel
{
	public TextDynamicRequestViewModel() : base()
	{
		TagNames = new List<string>();
	}

	// *********************************************
	public List<string> TagNames { get; set; }
	
	public Dictionary<string, string> TagNamesDictionary()
	{ 
		var result = new Dictionary<string, string>();

		foreach (var item in TagNames.Distinct())
		{
			result.Add(item, string.Empty);
		}

		return result;
	}
	// *********************************************

	// *********************************************

	// *********************************************
	/// <summary>
	/// محتوا
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.Content))]

	// NVARCHAR(MAX)
	// ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
	public string? Content { get; set; }
	// *********************************************

	public override SampleResult.Result Validate()
	{
		var result = new Result();

		var resultValidate =
			ValidationHelper.GetValidationResults(this);

		result.WithErrors(resultValidate.Select(x => x.ErrorMessage));

		if (TagNames.Count == 0)
		{
			var errorMessage =
				string.Format(Messages.RequiredError, DataDictionary.TagPageSetting);

			result.WithError(errorMessage);
		}

		if (TagNames.Any(x => x.StartsWith(nameof(DataDictionary.TextDynamic)) == false))
		{
			var errorMessage =
				string.Format(Messages.RequestNotValid);

			result.WithError(errorMessage);
		}

		return result.ConvertToSampleResult();
	}
}

#endregion /TextDynamic

#region TagPageSettings

public class TagPageSettingResponseViewModel : BaseResponseViewModel<TagPageSettingRequestViewModel>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	public TagPageSettingResponseViewModel() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	{
	}


	// **************************************************
	/// <summary>
	/// نام انگلیسی
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.NameEN))]
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
	[MaxLength(
		MaxLength.Name,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string NameEn { get; set; }
	// **************************************************

	// **************************************************
	/// <summary>
	/// نام فارسی
	/// </summary>

	[Display(
		ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.NameFA))]
	[Required(
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.RequiredError))]
	[MaxLength(
		MaxLength.Name,
		ErrorMessageResourceType = typeof(Messages),
		ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

	public string NameFa { get; set; }
	// **************************************************

	public override TagPageSettingRequestViewModel ToRequest()
	{
		throw new NotImplementedException();
	}
}

public class TagPageSettingRequestViewModel : BaseRequestViewModel
{
	public TagPageSettingRequestViewModel() : base()
	{
	}

	public override SampleResult.Result Validate()
	{
		throw new NotImplementedException();
	}
}

#endregion /TagPageSettings