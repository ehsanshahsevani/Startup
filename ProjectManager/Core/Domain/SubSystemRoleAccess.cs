using Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain;

/// <summary>
/// جدول مدیریت سطح دسترسی پروژه های دیگر برای بخش اکشن و کنترلرها
/// </summary>
public class SubSystemRoleAccess : BaseEntity
{
	// **************************************************
	/// <summary>
	/// سرور
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Server))]

	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string ServerId { get; set; }

	/// <summary>
	/// سرور
	/// </summary>
	public Server? Server { get; set; }
	// **************************************************

	// **************************************************
	/// <summary>
	/// نقش
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Role))]

	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string RoleId { get; set; }

	/// <summary>
	/// نقش
	/// </summary>
	public Role? Role { get; set; }
	// **************************************************

	// **************************************************
	/// <summary>
	///  اکشن
	/// "{ActionType}-{ControllerName}-{Template}-{ServerId}"
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Action))]
	
	[MaxLength(
		length: Constants.MaxLength.Description,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? ActionCode { get; set; }
	// **************************************************

	// **************************************************
	/// <summary>
	///  کنترلر
	/// "ControllerName"
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Action))]

	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	[MaxLength(
		length: Constants.MaxLength.Description,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string ControllerName { get; set; }
	// **************************************************
}