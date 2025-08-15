using System.ComponentModel.DataAnnotations;
using Constants;
using Domain.Base;
using Resources;

namespace Domain;

public class Status : BaseEntity
{
    // *********************************************
    public Status(string name)
    {
        Name = name;
        Tickets = [];
    }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     نام وضعیت
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Name))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Name { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     لیست تیکت های مربوط به این وضعیت
    /// </summary>
    public List<Ticket> Tickets { get; set; }
    // *********************************************
}