using ViewmodelSeedworks.Response;
using System.ComponentModel.DataAnnotations;
using SampleResult;
using ViewmodelSeedworks.Request;

namespace ViewModels.ProjectManager;

public class UserRelationResponseViewModel : BaseResponseViewModel<UserRelationRequestViewModel>
{
#pragma warning disable CS8618, CS9264
	public UserRelationResponseViewModel() : base()
#pragma warning restore CS8618, CS9264
	{
	}

	public UserRelationResponseViewModel
		(string serverId, string subSystemName, string userId, string relationId, string fieldName)
	{
		ServerId = serverId ?? throw new ArgumentNullException(nameof(serverId));
		SubSystemName = subSystemName ?? throw new ArgumentNullException(nameof(subSystemName));
		UserId = userId ?? throw new ArgumentNullException(nameof(userId));
		RelationId = relationId ?? throw new ArgumentNullException(nameof(relationId));
		FieldName = fieldName ?? throw new ArgumentNullException(nameof(fieldName));
	}

	// **************************************************
	/// <summary>
	/// آیدی مشخص از سروری که دامین فعلی درونش قرار دارد
	/// اصولا این آیدی در BaseEntity سرورهای مقصد وجود دارد
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
	// **************************************************

	// **************************************************
	/// <summary>
	/// زیر سیستم - جدول - بخش
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.SubSystem))]
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string SubSystemName { get; set; }
	// **************************************************

	// **************************************************
	/// <summary>
	/// کاربر
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.User))]
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string UserId { get; set; }
	// **************************************************


	// **************************************************
	/// <summary>
	/// شناسه رکورد مربوطه در سرور اعلام شده
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Record))]
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string RelationId { get; set; }
	// **************************************************

	// **************************************************
	/// <summary>
	/// شناسه رکورد مربوطه در سرور اعلام شده
	/// </summary>FieldName

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.FieldName))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.MaxLength.FullName,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string FieldName { get; set; }
	// **************************************************
	public override UserRelationRequestViewModel ToRequest()
	{
		throw new NotImplementedException();
	}
}

public class UserRelationRequestViewModel : BaseRequestViewModel
{
	public override Result Validate()
	{
		throw new NotImplementedException();
	}
}