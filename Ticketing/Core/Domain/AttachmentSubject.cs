using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Constants;
using Domain.Base;
using Resources;

namespace Domain;

/// <summary>
///     موضوع فایل ها
/// </summary>
public class AttachmentSubject : BaseEntity
{
#pragma warning disable CS8618, CS9264
    public AttachmentSubject()
#pragma warning restore CS8618, CS9264
    {
        Attachments = new List<Attachment>();
    }

    /// <summary>
    ///     نام نوع معادل اینام درون پروژه
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.AttachmentSubject))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        MaxLength.FullName,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Code { get; set; }


    /// <summary>
    ///     اتریبیوت های فارسی مربوط به اینام مربوطه
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.AttachmentSubject))]
    [MaxLength(
        MaxLength.FullName,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string? CodeDisplay { get; set; }

    public List<Attachment> Attachments { get; set; }

    [NotMapped]
    public string DisplayName
    {
        get
        {
            var result = Code;

            if (string.IsNullOrEmpty(CodeDisplay) == false) result = result + " | " + CodeDisplay;

            return result;
        }
    }
}