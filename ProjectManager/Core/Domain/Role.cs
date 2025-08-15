using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain;

/// <summary>
/// نقش
/// </summary>
public class Role : IdentityRole<string>
{
#pragma warning disable CS8618, CS9264
	public Role()
#pragma warning restore CS8618, CS9264
	{
		Id = Guid.NewGuid().ToString();

		CreateDateTime = DateTime.Now;
		UpdateDateTime = DateTime.Now;

		IsActive = true;
		IsDeleted = false;
		Ordering = Constants.MaxValue.Ordering;

		// UserRoles = new List<UserRole>();
	}

	// **************************************************
	/// <summary>
	/// شناسه دیتابیس
	/// </summary>

	[Key]
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Guid))]
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public override string Id { get; set; }
	// **************************************************


	// **************************************************
	/// <summary>
	/// تاریخ ایجاد
	/// </summary>
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CreateDate))]

	public DateTime CreateDateTime { get; private set; }
	// **************************************************

	// **************************************************
	/// <summary>
	/// تاریخ بروزرسانی
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UpdateDate))]

	public DateTime UpdateDateTime { get; set; }

	/// <summary>
	/// توضیحات
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Description))]

	public string? Description { get; set; }
	// **************************************************

	/// <summary>
	/// فعال بودن یا نبودن
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActive))]

	public bool IsActive { get; set; }

	/// <summary>
	/// وضیعت حذف
	/// </summary>

	public bool IsDeleted { get; set; }

	/// <summary>
	/// ترتیب نمایش
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Order))]

	public int Ordering { get; set; }
	// **************************************************

	// **************************************************
	public int TokenTimeMinutes { get; set; }
	// **************************************************

	// **************************************************
	/// <summary>
	/// لیست دسترسی های مربوط به این نقش در اکشن و کنترلرها
	/// </summary>
	public List<SubSystemRoleAccess> SubSystemRoleAccesses { get; set; }
	// **************************************************

	// **************************************************
	/// <summary>
	/// لیست دسترسی های مربوط به این نقش در داشبودر مورد نظر
	/// </summary>
	public List<DashboardPageRole> DashboardPageRoles { get; set; }
	// **************************************************	
}