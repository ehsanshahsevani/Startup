using Utilities;
using SampleResult;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ViewModels.Marketplace;

public class DiscountResponseViewModel : BaseResponseViewModel<DiscountRequestViewModel>
{
	// *********************************************
	/// <summary>
	/// تاریخ شروع
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.FromDateTime))]

	// [JsonIgnore]
	
	// add [JsonIgnore]
	// resolve this error:
	// fail: Microsoft.AspNetCore.Components.Web.ErrorBoundary[0]
	// System.Exception: Request failed: The JSON value could not be converted to System.Nullable1[System.DateTime].
	// Path: $.value.fromDate | LineNumber: 0 | BytePositionInLine: 49.
	//    at HttpServiceSeedworks.HttpService.<SendRequest>d__182
	// [[System.Object, System.Private.CoreLib, Version=9.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],
	// [SampleResult.Result1[[ViewModels.Marketplace.DiscountResponseViewModel, ViewModels,
	// Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], SampleResult, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
	// .MoveNext() in /home/user/Projects/Decoyab/SeedworkSystem/HttpServiceSeedworks/HttpService.cs:line 149
	
	public DateTime? FromDate { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// تاریخ شروع
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.FromDateTime))]

	// [JsonIgnore]
	
	// add [JsonIgnore]
	// resolve this error:
	// fail: Microsoft.AspNetCore.Components.Web.ErrorBoundary[0]
	// System.Exception: Request failed: The JSON value could not be converted to System.Nullable1[System.DateTime].
	// Path: $.value.fromDate | LineNumber: 0 | BytePositionInLine: 49.
	//    at HttpServiceSeedworks.HttpService.<SendRequest>d__182
	// [[System.Object, System.Private.CoreLib, Version=9.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],
	// [SampleResult.Result1[[ViewModels.Marketplace.DiscountResponseViewModel, ViewModels,
	// Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], SampleResult, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
	// .MoveNext() in /home/user/Projects/Decoyab/SeedworkSystem/HttpServiceSeedworks/HttpService.cs:line 149
	
	public DateTime? ToDate { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// از قیمت
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.FromPrice))]

	public int? FromPrice { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// تا قیمت
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ToPrice))]

	public int? ToPrice { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// حداقل مبلغ خرید - سبدخرید
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.MinimumCartPrice))]

	public int? MinimumCartPrice { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// مبلغ تخفیف
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Price))]

	public int? Price { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// درصد تخفیف
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Percent))]

	public int? PercentDiscount { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	///  شناسه کاربر:
	/// SetUserId()
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.User))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? UserId { get; set; }

	public string? UserDisplayName { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	///  شناسه نقش کاربر:
	/// SetUserId()
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Role))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? RoleIds { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// شناسه دسته بندی
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Category))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? CategoryId { get; set; }

	/// <summary>
	/// شناسه زیرسیستم
	/// </summary>
	public string? CategoryDisplayName { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// شناسه محصول
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Product))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? ProductId { get; set; }

	/// <summary>
	/// شناسه زیرسیستم
	/// </summary>
	public string? ProductDisplayName { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// نوع تخفیف
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ValueType))]
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	public Enums.Shared.ValueType ValueType { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// کد تخفیف
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Product))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? DiscountCode { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// حداکثر تعداد استفاده از این تخفیف
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.MaxUsageCount))]

	public int? MaxUsageCount { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// تعداد استفاده شده
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UsedCount))]

	public int? UsedCount { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// قابلیت نمایش تخفیفات در سامانه
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsHiddenForUsers))]

	public bool IsHiddenForUsers { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// قابلیت نمایش در پروفایل
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ActiveInProfile))]

	public bool ActiveInProfile { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// قابلیت پیش نمایش در سامانه و اپ
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Preview))]

	public bool Preview { get; set; }
// *********************************************

	// *********************************************
	public string? ToTime
	{
		get
		{
			if (ToDate == null)
			{
				return null;
			}

			return $"{ToDate.Value.TimeOfDay.Hours:00}:{ToDate.Value.TimeOfDay.Minutes:00}";
		}
	}

	public string ToDateShamsi => ToDate.ToShamsi(0);

	public string? FromTime
	{
		get
		{
			if (FromDate == null)
			{
				return null;
			}

			return $"{FromDate.Value.TimeOfDay.Hours.ToString("00")}:{FromDate.Value.TimeOfDay.Minutes.ToString("00")}";
		}
	}

	public string FromDateShamsi => FromDate.ToShamsi(0);
	// *********************************************

	public override DiscountRequestViewModel ToRequest()
	{
		var resquest = new DiscountRequestViewModel()
		{
			FromDate = FromDate?.ToShamsi(),
			ToDate = ToDate?.ToShamsi(),
			FromPrice = FromPrice,
			ToPrice = ToPrice,
			MinimumCartPrice = MinimumCartPrice,
			Price = Price,
			PercentDiscount = PercentDiscount,
			UserId = UserId,
			RoleIds = RoleIds,
			DiscountCode = DiscountCode,
			MaxUsageCount = MaxUsageCount,
			ValueType = ValueType,
			ProductId = ProductId,
			CategoryId = CategoryId,
			ActiveInProfile = ActiveInProfile,
			Preview = Preview,
			Description = Description,
			Id = Id,
			IsHiddenForUsers = IsHiddenForUsers,
			IsActive = IsActive,
			Ordering = Ordering,
			UsedCount = UsedCount,
		};

		return resquest;
	}
}

public class DiscountRequestViewModel : BaseRequestViewModel
{
	// *********************************************
	/// <summary>
	/// تاریخ شروع
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.FromDateTime))]

	public string? FromDate { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// تاریخ شروع
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.FromDateTime))]

	public string? ToDate { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// از قیمت
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.FromPrice))]

	public int? FromPrice { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// تا قیمت
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ToPrice))]

	public int? ToPrice { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// حداقل مبلغ خرید - سبدخرید
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.MinimumCartPrice))]

	public int? MinimumCartPrice { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// مبلغ تخفیف
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Price))]

	public int? Price { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// درصد تخفیف
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Percent))]

	public int? PercentDiscount { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	///  شناسه کاربر:
	/// SetUserId()
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.User))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? UserId { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	///  شناسه نقش کاربر:
	/// SetUserId()
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Role))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? RoleIds { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// شناسه دسته بندی
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Category))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? CategoryId { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// شناسه محصول
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Product))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? ProductId { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// نوع تخفیف
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ValueType))]
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	public Enums.Shared.ValueType ValueType { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// کد تخفیف
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Product))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? DiscountCode { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// حداکثر تعداد استفاده از این تخفیف
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.MaxUsageCount))]

	public int? MaxUsageCount { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// تعداد استفاده شده
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UsedCount))]

	public int? UsedCount { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// قابلیت نمایش تخفیفات در سامانه
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsHiddenForUsers))]

	public bool IsHiddenForUsers { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// قابلیت نمایش در پروفایل
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ActiveInProfile))]

	public bool ActiveInProfile { get; set; }
// *********************************************

	// *********************************************
	/// <summary>
	/// قابلیت پیش نمایش در سامانه و اپ
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Preview))]

	public bool Preview { get; set; }
// *********************************************

	public override Result Validate()
	{
		var result = new FluentResults.Result();
		
		DateTime? fromDate = FromDate.StringToDateTimeMiladi();
		DateTime? toDate = ToDate.StringToDateTimeMiladi();

		if (string.IsNullOrEmpty(FromDate) == false && fromDate.HasValue == false)
		{
			result.WithError(Resources.Messages.DateFormat);
		}

		if (string.IsNullOrEmpty(ToDate) == false && toDate.HasValue == false)
		{
			result.WithError(Resources.Messages.DateFormat);
		}
		
		// Check if 'FromDate' and 'ToDate' are valid
		if (fromDate.HasValue == true && toDate.HasValue == true)
		{
			if (fromDate.Value > toDate.Value)
			{
				var errorMessage = string.Format(
					Resources.Messages.FieldConflictError,
					Resources.DataDictionary.FromDateTime,
					Resources.DataDictionary.ToDateTime);

				result.WithError(errorMessage);
			}
		}

		// Validate 'FromPrice' and 'ToPrice'
		if (FromPrice.HasValue == true && ToPrice.HasValue == true)
		{
			if (FromPrice > ToPrice)
			{
				var errorMessage = string.Format(
					Resources.Messages.FieldConflictError,
					Resources.DataDictionary.FromPrice,
					Resources.DataDictionary.ToPrice);

				result.WithError(errorMessage);
			}
		}

		// Check 'MinimumCartPrice'
		if (MinimumCartPrice is <= 0)
		{
			var errorMessage =
				string.Format(Resources.Messages.FieldMinValueError, Resources.DataDictionary.MinimumCartPrice, 0);

			result.WithError(errorMessage);
		}

		// Check 'Price' 
		if (Price is <= 0)
		{
			var errorMessage =
				string.Format(Resources.Messages.FieldMinValueError, Resources.DataDictionary.Price, 0);

			result.WithError(errorMessage);
		}

		// Validate 'PercentDiscount'
		if (PercentDiscount is <= 0 or > 100)
		{
			var errorMessage =
				string.Format(Resources.Messages.MinAndMaxValueFieldError, Resources.DataDictionary.Percent, 0, 100);

			result.WithError(errorMessage);
		}

		// Validate 'UserId'
		if (string.IsNullOrWhiteSpace(UserId) == false && UserId.Length != Constants.FixedLength.Guid)
		{
			var errorMessage =
				string.Format(Resources.Messages.RequestNotValid);

			result.WithError(errorMessage);
		}

		if (Preview == true && fromDate.HasValue == false)
		{
			var errorMessage =
				string.Format
					(Resources.Messages.RequiredError,
						Resources.DataDictionary.FromDateTime);

			result.WithError(errorMessage);
		}

		if (ActiveInProfile == true && string.IsNullOrEmpty(DiscountCode) == true)
		{
			var errorMessage =
				string.Format
				(Resources.Messages.RequiredError,
					Resources.DataDictionary.Code);

			result.WithError(errorMessage);
		}
		
		return result.ConvertToSampleResult();
	}
}