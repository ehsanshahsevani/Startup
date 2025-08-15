using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain;

/// <summary>
/// اکشن های مربوط به هرکدام از کنترلر های پروژه های دیگر
/// </summary>
public class Action : BaseEntity
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
	/// زیر سیستم
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Server))]

	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string? SubSystemId { get; set; }

	/// <summary>
	/// زیر سیستم
	/// </summary>
	public SubSystem? SubSystem { get; set; }
	// **************************************************

	// **************************************************
	/// <summary>
	/// عنوان
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
	// **************************************************

	// **************************************************
	/// <summary>
	/// قالب
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Template))]
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	[MaxLength(
		length: Constants.MaxLength.Description,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string Template { get; set; }
	// *********************************************

	// *********************************************    
	/// <summary>
	/// نام کنترلر
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ControllerName))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

	[MaxLength(
		length: Constants.MaxLength.Description,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string ControllerName { get; set; }
	// *********************************************

	// *********************************************    
	/// <summary>
	/// ورب و نوع اکشن : گت / پست / پوت / دیلیت
	/// GET / POST / POT / DELETE
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ActionType))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.MaxLength.FullName,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string ActionType { get; set; }
	// *********************************************

	// *********************************************
	[NotMapped]
	public string ActionCode
	{
		get
		{
			var result = $"{ActionType}-{ControllerName}-{Template}-{ServerId}";

			return result;
		}
	}
	// *********************************************
}