using Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain;

/// <summary>
/// مدلی برای مدیریت تمام ارتباطات جدول کاربر با پروژه های دیگر در بخش های دیگر
/// از طریق این جدول متوجه میشیم که این کاربر در چه جداولی و همینطور در چه پروژه هایی چه وابستگی هایی دارد
/// این جدول این موضوع را برایمان شبیه سازی میکند که:
/// کاربر 10 در جدول 50 از سرور 30 رکورد 7 را دارد
/// </summary>
public class UserRelation : BaseEntity
{
#pragma warning disable CS8618, CS9264
    public UserRelation() : base()
#pragma warning restore CS8618, CS9264
    {
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

    public Server Server { get; set; }
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
    
    public string SubSystemId { get; set; }
    
    /// <summary>
    /// زیر سیستم
    /// </summary>
    public SubSystem? SubSystem { get; set; }
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

    /// <summary>
    /// کاربر
    /// </summary>
    public User? User { get; set; }
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
}