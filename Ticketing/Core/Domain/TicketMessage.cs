using System.ComponentModel.DataAnnotations;
using Constants;
using Domain.Base;
using Resources;

namespace Domain;

/// <summary>
///     تیکت
/// </summary>
public class TicketMessage : BaseEntity
{
#pragma warning disable CS8618, CS9264
    public TicketMessage()
#pragma warning restore CS8618, CS9264
    {
    }

    // *********************************************
    /// <summary>
    ///     متن پیام
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Text))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Text { get; set; }
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
    public string TicketId { get; set; }

    /// <summary>
    ///     وضعیت تیکت
    /// </summary>
    public Ticket Ticket { get; set; }
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
}