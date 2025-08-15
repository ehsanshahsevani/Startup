using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain;

/// <summary>
/// حساب های بانکی اشخاص
/// </summary>
public class ProfileBank : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	public ProfileBank() : base()
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
	/// شناسه بانک
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Bank))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string BankId { get; set; }

	/// <summary>
	/// بانک
	/// </summary>
	public Bank? Bank { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// IR24
	/// شماره شبا با 24 کاراکتر عددی و 2 کاراکتر حروف
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Shaba))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.FixedLength.ShabaNumber,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
	
	public string Shaba { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// نامشخص پس ما همان 24 کاراکتر در نظر میگیریم
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.AccountNumber))]
	
	[MaxLength(
		length: Constants.FixedLength.ShabaNumber,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
	
	public string? AccountNumber { get; set; }
	// *********************************************
	
	// *********************************************
	/// <summary>
	/// شماره کارت با 16 کاراکتر ثابت
	/// </summary>
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CardNumber))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.FixedLength.CardNumber,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
	
	public string CardNumber { get; set; }
	// *********************************************
}