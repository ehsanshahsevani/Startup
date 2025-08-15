using System.ComponentModel.DataAnnotations;
using Constants;
using Domain.Base;
using Resources;

namespace Domain;

/// <summary>
///     تیکت
/// </summary>
public class Ticket : BaseEntity
{
#pragma warning disable CS8618, CS9264
    public Ticket()
#pragma warning restore CS8618, CS9264
    {
    }

    // *********************************************
    /// <summary>
    ///     موضوع مربوط به این تیکت
    /// </summary>
    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.TicketSubject))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]
    public string TicketSubjectId { get; set; }

    /// <summary>
    ///     موضوع مربوط به این تیکت
    /// </summary>
    public TicketSubject TicketSubject { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     کد رهگیری
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Code))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Code { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     وضعیت تیکت
    /// </summary>
    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Status))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]
    public string StatusId { get; set; }

    /// <summary>
    ///     وضعیت تیکت
    /// </summary>
    public Status Status { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     کاربر
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.User))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string UserId { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     وضعیت روئیت تیکت
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.IsSeen))]
    public bool IsSeen { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     پیام های ارسال شده برای این تیکت
    /// </summary>
    public List<TicketMessage> TicketMessages { get; set; }
    // *********************************************
}