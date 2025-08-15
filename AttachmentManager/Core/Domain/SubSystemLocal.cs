using DomainSeedworks;

namespace Domain;

/// <summary>
/// جداول موجود در سیستم
/// این جدول نمونه ی لوکال از جدول SubSystem می باشد که بصورت سیستمی مقدار دهی
/// میشود و نیازی به داشتن تابع های اد یا ویرایش یا خذف ندارد
/// این جدول برای  ارتباط دامین ها با جدول ساب سیستم اصلی که در سرور مدیریت پروژه قرار دارد می باشد  
/// </summary>
public class SubSystemLocal : BaseSubSystem
{
#pragma warning disable CS8618, CS9264
    public SubSystemLocal():base()
#pragma warning restore CS8618, CS9264
    {
    }
}