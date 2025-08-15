using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain;

/// <summary>
/// قوانین طلا - خرید و فروش طلای آب شده
/// </summary>
public class RoleGold : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	public RoleGold() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	{
	}
	
	// *********************************************
	/// <summary>
	/// تاریخ شروع
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.StartDateTime))]
	
	public DateTime? StartDateTime { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// تاریخ پایان
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.EndDateTime))]
	
	public DateTime? EndDateTime { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// ساعت شروع
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.StartTime))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	public TimeSpan StartTime { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// ساعت پایان
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.EndTime))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	public TimeSpan EndTime { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// کمترین مقدار سوت
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Max))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	public int Min { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// بیشترین مقدار سوت
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Max))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	public int Max { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// شناسه نوع قانون کیف پول
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.TypeRoleMoney))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string TypeRoleGoldId { get; set; }

	/// <summary>
	/// قانون دارایی طلا
	/// </summary>
	public TypeRoleGold? TypeRoleGold { get; set; }
	// *********************************************
}