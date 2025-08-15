using System.ComponentModel.DataAnnotations;
using Constants;
using Domain.Base;
using Resources;

namespace Domain;

/// <summary>
///     موضوع تیکت
/// </summary>
public class TicketSubject : BaseEntity
{
#pragma warning disable CS8618, CS9264
    public TicketSubject()
#pragma warning restore CS8618, CS9264
    {
    }

// *********************************************
    public TicketSubject(string name)
    {
        Name = name;
        Tickets = [];
    }

    // *********************************************
    // *********************************************
    /// <summary>
    ///     نام موضوع
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Name))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        MaxLength.Description,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Name { get; set; }

    // *********************************************

    // *********************************************
    /// <summary>
    ///     لیست تیکت های مربوط به این موضوع
    /// </summary>
    public List<Ticket> Tickets { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     وضعیت اتصال به بخش باگ های سیستمی
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.IsConnectToSystemBugs))]

    public bool IsConnectToSystemBugs { get; set; }
    // *********************************************

    // *********************************************
    public override string ToString()
    {
        return Name;
    }
    // *********************************************
}