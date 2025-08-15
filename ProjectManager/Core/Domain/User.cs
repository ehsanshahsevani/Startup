using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain;

/// <summary>
/// کاربرها
/// </summary>
public class User : IdentityUser<string>
{
#pragma warning disable CS8618, CS9264
    public User() : base()
#pragma warning restore CS8618, CS9264
    {
        Id = Guid.NewGuid().ToString();
        
        UserCode = GenerateCode();
        
        CreateDateTime = DateTime.Now;
        UpdateDateTime = DateTime.Now;
        
        IsActive = true;
        IsDeleted = false;
        Ordering = Constants.MaxValue.Ordering;
        
        Commutings = new List<Commuting>();
        Notifications = new List<Notification>();
        
        UserRelations = new List<UserRelation>();
        UserRelationTemps = new List<UserRelationTemp>();
        
        ReferalLogs = new List<ReferalLog>();
        ReferalLogsForOthers = new List<ReferalLog>();
    }
    
    // **************************************************
    /// <summary>
    /// شناسه دیتابیس
    /// </summary>
    
    [Key]

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Guid))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public override string Id { get; set; }
    // **************************************************
    
    // **************************************************
    /// <summary>
    /// شناسه منحصر بفرد کاربر
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.UserCode))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string UserCode { get; private set; }
    // **************************************************
    
    // **************************************************
    /// <summary>
    /// کد معرف
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ReferalCode))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string ReferalCode { get; set; }
    // **************************************************
    
    /// <summary>
    /// تاریخ ایجاد
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CreateDate))]
    
    public DateTime CreateDateTime { get; private set; }

    /// <summary>
    /// تاریخ بروزرسانی
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.UpdateDate))]

    public DateTime UpdateDateTime { get; set; }

    /// <summary>
    /// توضیحات
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Description))]

    public string? Description { get; set; }
    
    /// <summary>
    /// فعال بودن یا نبودن
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.IsActive))]

    public bool IsActive { get; set; }

    /// <summary>
    /// وضیعت حذف
    /// </summary>
    
    public bool IsDeleted { get; set; }

    /// <summary>
    /// ترتیب نمایش
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Order))]

    public int Ordering { get; set; }
    
    // **************************************************
    /// <summary>
    /// کد یکبار مصرف
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.OtpCode))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.MaxLength.OptCode,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string OtpCode { get; set; }
    // **************************************************
    
    // **************************************************
    public  List<Commuting> Commutings   { get; set; }
    // **************************************************
    
    // **************************************************
    public  List<Commuting> CommutingsClosed   { get; set; }
    // **************************************************
    
    // **************************************************
    public  List<Notification>  Notifications   { get; set; }
    // **************************************************
    
    // **************************************************
    public List<UserRelation> UserRelations { get; set; }
    public List<UserRelationTemp> UserRelationTemps { get; set; }
    // **************************************************
    
    // **************************************************
    // public List<UserRole> UserRoles { get; set; }
    // **************************************************
    
    // **************************************************
    public List<ReferalLog> ReferalLogs { get; set; }
    public List<ReferalLog> ReferalLogsForOthers { get; set; }
    // **************************************************
    
    // **************************************************
    private string GenerateCode()
    {
        var code = $"talasoot-{Guid.NewGuid().ToString().Split('-')[0]}";

        return code;
    }
    // **************************************************
}