using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Enums.SharedService;

namespace Domain;

/// <summary>
/// سرور های وابسته
/// </summary>
public class Server : BaseEntity
{
	public Server(ProjectType projectType, string serverId) : base()
	{
		SubSystems = new List<SubSystem>();
		UserRelations = new List<UserRelation>();
		UserRelationTemps = new List<UserRelationTemp>();

		SetId(serverId);
		ServerKey = serverId;
		ProjectType = projectType;
	}

#pragma warning disable CS8618, CS9264
	private Server()
#pragma warning restore CS8618, CS9264
	{
	}

	/// <summary>
	/// شناسه سرور مورد نظر
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ServerId))]
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string ServerKey { get; set; }

	public List<SubSystem> SubSystems { get; set; }
	public List<Role> Roles { get; set; }
	public List<UserRelation> UserRelations { get; set; }
	public List<UserRelationTemp> UserRelationTemps { get; set; }
	public ProjectType ProjectType { get; set; }

	public List<Dashboard> Dashboards { get; set; }

	// *********************************************
	public List<SubSystemRoleAccess> SubSystemRoleAccesses { get; set; }
	// *********************************************

	// *********************************************
	public List<Action> Actions { get; set; }
	// *********************************************
}