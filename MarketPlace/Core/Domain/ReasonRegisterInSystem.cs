using Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain;

/// <summary>
/// دلیل آشنایی و ثبت نام در سیستم
/// تبلیغات تلوزیون
/// دوستان و آشنایان
/// جستجو در گوگل
/// </summary>
public class ReasonRegisterInSystem : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	public ReasonRegisterInSystem() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	{
		Profiles = new List<Profile>();
	}
	
	// *********************************************    
	/// <summary>
	/// عنوان
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Title))]

	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	[MaxLength(
		length: Constants.MaxLength.Title,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string Title { get; set; }
	// *********************************************

	// *********************************************
	/// <summary>
	/// لیست لاگ های این دلیل
	/// </summary>

	public List<Profile> Profiles { get; set; }
	// *********************************************
}