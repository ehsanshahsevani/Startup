using Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

/// <summary>
/// قوانین پول - برداشت و واریز به کیف پول
/// </summary>
public class RoleMoney : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	public RoleMoney() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	{
	}
	
	// *********************************************
	/// <summary>
	/// ساعت و تاریخ شروع
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.StartDateTime))]
	
	public DateTime? StartDateTime { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// ساعت و تاریخ پایان
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
	/// حداقل مبلغ
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Min))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[Column(TypeName = "decimal(18,2)")]
	public decimal Min { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// حداکثر مبلغ
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Max))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[Column(TypeName = "decimal(18,2)")]
	public decimal Max { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// شناسه نوع قانون مالی
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

	public string TypeRoleMoneyId { get; set; }

	/// <summary>
	/// قانون مالی
	/// </summary>
	public TypeRoleMoney? TypeRoleMoney { get; set; }
	// *********************************************
}