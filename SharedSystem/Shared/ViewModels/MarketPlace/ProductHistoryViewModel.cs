using ViewmodelSeedworks.Response;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Enums.Shared;
using SampleResult;
using Utilities;
using ViewmodelSeedworks.Request;
using ValueType = Enums.Shared.ValueType;

namespace ViewModels.Marketplace;

public class ProductHistoryResponseViewModel : BaseResponseViewModel<ProductHistoryRequestViewModel>
{
	// *********************************************
	/// <summary>
	/// قیمت خرید
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.PurchasePrice))]

	public int? PurchasePrice { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// قیمت فروش
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.SellingPrice))]

	public int? SellingPrice { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// مبلغ برای نقش خاص
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CurrentCount))]

	public int? RolePrice { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// اولین موجودی
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.InitialCount))]

	public int? InitialCount { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	///موجودی جاری
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CurrentCount))]

	public int? CuurentCount { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	///درخواست استعلام قیمت
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.RequestForCheckPrice))]

	public bool HasRequestForCheckPrice { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// شناسه محصول
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Product))]
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string ProductId { get; set; }
	// *********************************************

	// *********************************************
	public int? PriceWithDiscount { get; set; }

	public DateTime? DiscountFromDate { get; set; }

	public DateTime? DiscountToDate { get; set; }

	public bool HasDiscount { get; set; }
	// *********************************************

	// *********************************************
	public override ProductHistoryRequestViewModel ToRequest()
	{
		var request = new ProductHistoryRequestViewModel
		{
			Id = Id,
			Ordering = Ordering,
			IsActive = IsActive,
			Description = Description,

			HasRequestForCheckPrice = HasRequestForCheckPrice,

			ProductId = ProductId,
			InitialCount = InitialCount,
			CurrentCount = CuurentCount,
			PurchasePrice = PurchasePrice,
			SellingPrice = SellingPrice,
		};

		return request;
	}
}

public class ProductHistoryRequestViewModel : BaseRequestViewModel
{
	// *********************************************
	/// <summary>
	/// قیمت خرید
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.PurchasePrice))]

	public int? PurchasePrice { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// قیمت فروش
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.SellingPrice))]

	public int? SellingPrice { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// اولین موجودی
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.InitialCount))]

	public int? InitialCount { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	///موجودی جاری
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CurrentCount))]

	public int? CurrentCount { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	///درخواست استعلام قیمت
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.RequestForCheckPrice))]

	public bool HasRequestForCheckPrice { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// شناسه محصول
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Product))]
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? ProductId { get; set; }
// *********************************************

	public override Result Validate()
	{
		var result = new FluentResults.Result();

		if (string.IsNullOrEmpty(ProductId) == true)
		{
			var errorMessage =
				string.Format(Resources.Messages.RequestNotValid);

			result.WithError(errorMessage);
		}

		if (CurrentCount.HasValue == false)
		{
			var errorMessage =
				string.Format(Resources.Messages.RequiredError, Resources.DataDictionary.CurrentCount);

			result.WithError(errorMessage);
		}

		if (InitialCount.HasValue == false)
		{
			var errorMessage =
				string.Format(Resources.Messages.RequiredError, Resources.DataDictionary.InitialCount);

			result.WithError(errorMessage);
		}

		if (SellingPrice.HasValue == false)
		{
			var errorMessage =
				string.Format(Resources.Messages.RequiredError, Resources.DataDictionary.SellingPrice);

			result.WithError(errorMessage);
		}

		if (PurchasePrice.HasValue == false)
		{
			var errorMessage =
				string.Format(Resources.Messages.RequiredError, Resources.DataDictionary.PurchasePrice);

			result.WithError(errorMessage);
		}

		var res = result.ConvertToSampleResult();

		return res;
	}
}

/// <summary>
/// مدل تغییر قیمت شامل قیمت جدید، درصد تغییر، نوع مقدار، جهت تغییر و تأثیر بر قیمت خرید
/// </summary>
public class PriceChangeRequestModel : BaseRequestViewModel
{
	/// <summary>
	/// قیمت جدید پس از اعمال تغییر
	/// </summary>
	public int? NewPrice { get; set; }

	/// <summary>
	/// مقدار درصدی تغییر (مثبت یا منفی بر اساس جهت)
	/// </summary>
	public int? Percent { get; set; }

	/// <summary>
	/// نوع مقداردهی: قیمت یا درصد
	/// </summary>
	public ValueType ValueType { get; set; }

	/// <summary>
	/// نوع تغییر: افزایشی، کاهشی، بدون تغییر یا خنثی
	/// </summary>
	public ChangeDirection ChangeDirection { get; set; }

	public int? CurrentCount { get; set; }

	/// <summary>
	/// آیا این تغییر روی قیمت خرید نیز تأثیر می‌گذارد؟
	/// </summary>
	public bool AffectPurchasePrice { get; set; }

	/// <summary>
	/// شناسه محصول
	/// </summary>
	public string? ProductId { get; set; }

	#region Constant Values

	private const int MinPrice = 1_000;
	private const int MaxPrice = 5_000_000;

	private const int MinPercent = 1;
	private const int MaxPercent = 100;

	#endregion

	public override Result Validate()
	{
		var result = new FluentResults.Result();

		if (ValueType == ValueType.Percent && Percent < MinPercent || Percent > MaxPercent)
		{
			string errorMessage =
				string.Format(Resources.Messages.ErrorPercentValue);

			result.WithError(errorMessage);
		}

		if (ValueType == ValueType.Price && NewPrice <= MinPrice || NewPrice > MaxPrice)
		{
			string errorMessage =
				string.Format(
					Resources.Messages.MinAndMaxValueFieldError,
					Resources.DataDictionary.Price,
					MinPrice.SeparateThousands(),
					MaxPrice.SeparateThousands());

			result.WithError(errorMessage);
		}

		return result.ConvertToSampleResult();
	}
}