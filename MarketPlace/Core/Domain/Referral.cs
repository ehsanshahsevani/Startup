using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

/// <summary>
/// قوانین معرفی با کد رفرال 
/// </summary>
public class Referral : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Referral() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
    
    // *********************************************
    /// <summary>
    /// تعداد استفاده شده 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CountUsed))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    public int CountUsed { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// خداکثر تعداد قابل استفاده 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.MaxUsageCount))]
    
    public int? MaxUserUse { get; set; }
    // *********************************************
    
    
    // *********************************************
    /// <summary>
    /// تاریخ شروع 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.StartDate))]
    
    public DateTime? StartDate { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// تاریخ پایان 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ToDate))]
    
    public DateTime? EndDate { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// مقدار سوت طلا 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.GoldSoot))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    public decimal GoldSoot { get; set; }
    // *********************************************
    
    /// <summary>
    /// بررسی اعتبار کد معرف براساس تاریخ و تعداد استفاده
    /// </summary>
    private bool IsValid()
    {
        var now = DateTime.Now;

        if (StartDate.HasValue && StartDate.Value > now)
        {
            return false;
        }

        if (EndDate.HasValue && EndDate.Value < now)
        {
            return false;
        }

        if (MaxUserUse.HasValue && CountUsed >= MaxUserUse.Value)
        {
            return false;
        }

        if (IsDeleted == true)
        {
            return false;
        }

        if (IsActive == false)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// بررسی امکان استفاده از کد معرف
    /// </summary>
    public bool CanUse()
    {
        if (IsValid() == false)
        {
            return false;
        }

        return true;
    }
}