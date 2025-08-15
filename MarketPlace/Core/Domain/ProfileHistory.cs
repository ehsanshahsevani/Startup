using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain;

/// <summary>
/// تاریخچه تغییرات پروفایل کاربران
/// </summary>
public class ProfileHistory : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	public ProfileHistory() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	{
	}
	
	// *********************************************
	/// <summary>
	/// شناسه پروفایل
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Profile))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string ProfileId { get; set; }

	/// <summary>
	/// پروفایل
	/// </summary>
	public Profile? Profile { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// آدرس قدیمی
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

	public string OldAddress { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// آدرس جدید
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

	public string NewAddress { get; set; }
	// *********************************************
	
	// *********************************************
	// این رکورد تصویر قدیمی و جدید را نیز باید ثبت کند
	// و به صورت اتچمنت با موضوعی که برای این بخش ثبت شده این اتفاق می افتد
	// *********************************************
}