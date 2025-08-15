using System.ComponentModel.DataAnnotations;
using DomainSeedworks;

namespace Domain;
/// <summary>
/// جدوال موجود در سیستم
/// </summary>
public class SubSystem : BaseSubSystem
{
#pragma warning disable CS8618, CS9264
    public SubSystem() : base()
#pragma warning restore CS8618, CS9264
    {
        UserRelationTemps = new List<UserRelationTemp>();
    }
    
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
    
    public new string ServerId { get; set; }
    public Server? Server { get; set; }
    
    public List<UserRelation> UserRelations { get; set; }
    public List<UserRelationTemp> UserRelationTemps { get; set; }
    public List<Action> Actions { get; set; }
}