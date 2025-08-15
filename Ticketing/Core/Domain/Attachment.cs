using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Constants;
using Domain.Base;
using Enums.Marketplace;
using Resources;

namespace Domain;

/// <summary>
///     پیوست
/// </summary>
public class Attachment : BaseAttachment
{
    // فیلد پشتیبان برای مقداردهی نگهداری مقدار اینام ست شده
    private AttachmentSubjectEnum _attachmentSubjectEnumBackingField;
#pragma warning disable CS8618, CS9264
    public Attachment()
#pragma warning restore CS8618, CS9264
    {
    }

    // *********************************************
    /// <summary>
    ///     شناسه زیرسیستم
    /// </summary>
    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.SubSystem))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]
    public string SubSystemLocalId { get; set; }

    /// <summary>
    ///     شناسه زیرسیستم
    /// </summary>
    public SubSystemLocal SubSystemLocal { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     شناسه جدول
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Relation))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    [MaxLength(
        FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string RelationId { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     نوع پیوست
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.AttachmentSubject))]

    // [Required(
    //     ErrorMessageResourceType = typeof(Resources.Messages),
    //     ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    [MaxLength(
        FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string? AttachmentSubjectId { get; set; }

    public AttachmentSubject? AttachmentSubject { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     از این پراپرتی برای خواندن و نوشتن راحت تر و جدول مربوط به فایل ها استفاده میشود
    ///     این در زمان ثبت و در زمان گرفتن از دیتابیس از ریلیشن به اینام و بالعکس تبدیل میشود
    /// </summary>
    [NotMapped]
    public AttachmentSubjectEnum AttachmentSubjectEnum
    {
        get
        {
            if (AttachmentSubject == null || string.IsNullOrEmpty(AttachmentSubject.Code))
            {
                return AttachmentSubjectEnum.None;
            }

            if (Enum.TryParse<AttachmentSubjectEnum>(AttachmentSubject.Code, out var result))
            {
                return result;
            }

            return AttachmentSubjectEnum.None;
        }

        set => _attachmentSubjectEnumBackingField = value;
    }
}