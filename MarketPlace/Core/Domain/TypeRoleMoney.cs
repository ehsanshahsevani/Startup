using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

/// <summary>
/// نوع قوانین مالی
/// Both, Deposit, Withdrawal
/// </summary>
public class TypeRoleMoney : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	public TypeRoleMoney() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	{
	}
	
	// *********************************************
	/// <summary>
	/// مقدار مناسب برای سرچ در دیتابیس
	/// Both, Deposit, Withdrawal
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Code))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.MaxLength.Description,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
	
	public string Code { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// نام
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Name))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.MaxLength.Description,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
	
	public string Name { get; set; }
	// *********************************************
	
	// *********************************************
	public List<RoleMoney> RoleMonies { get; set; }
	// *********************************************
}