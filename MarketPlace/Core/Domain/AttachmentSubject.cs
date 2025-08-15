using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain;

/// <summary>
/// موضوع فایل ها
/// </summary>
public class AttachmentSubject : BaseEntity
{
#pragma warning disable CS8618, CS9264
    public AttachmentSubject():base()
#pragma warning restore CS8618, CS9264
    {
        Attachments = new List<Attachment>();
    }

    /// <summary>
    /// نام نوع معادل اینام درون پروژه
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.AttachmentSubject))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    [MaxLength(
        length: Constants.MaxLength.FullName,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string Code { get; set; }
    
    
    /// <summary>
    /// اتریبیوت های فارسی مربوط به اینام مربوطه
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.AttachmentSubject))]
    
    [MaxLength(
        length: Constants.MaxLength.FullName,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string? CodeDisplay { get; set; }
    
    public List<Attachment> Attachments { get; set; }
    
    [NotMapped]
    public string DisplayName
    {
        get
        {
            string result = Code;

            if (string.IsNullOrEmpty(CodeDisplay) == false)
            {
                result = result + " | " + CodeDisplay;
            }
        
            return result;
        }
    }
}