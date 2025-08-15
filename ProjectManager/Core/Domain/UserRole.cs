using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain;
/// <summary>
/// نقش های کاربر
/// </summary>
public class UserRole : IdentityUserRole<string>
{
#pragma warning disable CS8618, CS9264
    public UserRole()
#pragma warning restore CS8618, CS9264
    {
        CreateDateTime = DateTime.Now;
        UpdateDateTime = DateTime.Now;
        
        IsActive = true;
        IsDeleted = false;
        Ordering = Constants.MaxValue.Ordering;        
    }
    
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

    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
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
}