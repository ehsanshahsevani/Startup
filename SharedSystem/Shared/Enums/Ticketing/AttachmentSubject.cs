using System.ComponentModel.DataAnnotations;

namespace Enums.Ticketing;

/// <summary>
/// این اینام جهت مدیریت کل اتچمنت های مربوط به مارکت پلیس ساخته شده است
/// توجه داشته باشید که تمام دیسپلی نام های این اینامم و کلید های ان مستقیم به یک جدول معادل متصل است
/// پس در صورت هر تغییری در کلید ها ممکن است دیتایی بهم بریزد
/// لذا در هر گونه تغییری در این بخش محطاطانه تصمیم گیری کنید.
/// </summary>
public enum AttachmentSubjectEnum : byte
{
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Ticket))]
    Ticket = 0,
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.None))]
    None,
}