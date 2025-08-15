using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain;

/// <summary>
/// پروفایل اشخاص
/// </summary>
public class Profile : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	public Profile() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	{
		Transactions = new List<Transaction>();
		ProfileBanks = new List<ProfileBank>();
	}
	
	// *********************************************
	/// <summary>
	/// شناسه کاربر
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.User))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string UserId { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// نام
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.FirstName))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	[MaxLength(
		length: Constants.MaxLength.Title,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string FirstName { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// نام خانوادگی
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.LastName))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	[MaxLength(
		length: Constants.MaxLength.Title,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string LastName { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// کد ملی
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.NationalCode))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	[MaxLength(
		length: Constants.FixedLength.NationalCode,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string NationalCode { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// تاریخ تولد
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.BirthDate))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	public DateTime BirthDate { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// آدرس
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Address))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	[MaxLength(
		length: Constants.MaxLength.Description,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string Address { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// شناسه شهر
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.City))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string CityId { get; set; }

	/// <summary>
	/// شهر
	/// </summary>
	public City? City { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// دلیل آشنایی با طلا سوت
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ReasonRegisterInSystem))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? ReasonRegisterInSystemId { get; set; }

	/// <summary>
	/// دلیل آشنایی با طلا سوت
	/// </summary>

	public ReasonRegisterInSystem? ReasonRegisterInSystem { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// شناسه جنسیت
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Gender))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string GenderId { get; set; }

	/// <summary>
	/// جنسیت
	/// </summary>
	public Gender? Gender { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// لیست حساب های بانکی این شخص
	/// </summary>
	public List<ProfileBank> ProfileBanks { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// لیست تغییرات این پروفایل
	/// </summary>
	public List<ProfileHistory> ProfileHistories { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// لیست آیتم های سبد
	/// </summary>
	public List<CartItem> CartItems { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// لیست درخواست های طلا
	/// </summary>
	public List<GoldRequest> GoldRequests { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// لیست سفارشات
	/// </summary>
	public List<Order> Orders { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// لیست فروشگاه ها
	/// </summary>
	public List<Shop> Shops { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// لیست دارایی های کاربران
	/// </summary>
	public List<UserAssets> UserAssetsList { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// لیست تراکنش های کاربران
	/// </summary>
	public List<Transaction> Transactions { get; set; }
	// *********************************************

	
	// *********************************************
	/// <summary>
	/// نام و نام خانوادگی
	/// </summary>
	[NotMapped]
	public string FullName { get => $"{FirstName} {LastName}"; }
	// *********************************************
}