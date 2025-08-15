using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

/// <summary>
/// لیست جنسیت ها
/// </summary>
public class Gender : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	public Gender() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	{
	}
	
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
		length: Constants.MaxLength.Name,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string Name { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// لیست پروفایل هایی که این جنسیت را دارند
	/// </summary>
	public List<Profile> Profiles { get; set; }
	// *********************************************
}