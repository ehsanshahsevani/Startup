using System.ComponentModel.DataAnnotations;

namespace Enums.Marketplace;

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
        Name = nameof(Resources.DataDictionary.NationalCardFront))]
    NationalCardFront = 0,
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.NationalCardBack))]
    NationalCardBack = 1,
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ProductCategory))]
    ProductCategory = 4,
    
    BusinessLicenseImage = 5,
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.VatCertificateImage))]
    VatCertificateImage = 6,
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.OfficialGazetteImage))]
    OfficialGazetteImage = 7,
    
    ShopLogo = 8,
    ShopOwnerSignature = 9,
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ProductProfile))]
    ProductProfile = 10,
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ProductListImage))]
    ProductListImage = 11,

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Profile))]
    
    ImageProfile = 12,

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.OldImageProfileHistory))]
    
    OldImageProfileHistory = 13,

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.NewImageProfileHistory))]
    
    NewImageProfileHistory = 14,

    /// <summary>
    /// لوگو بانک
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.LogoBank))]
    
    LogoBank = 15,
    
    /// <summary>
    /// صفحه اول شناسنامه
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.PageOneIdentityBoolLet))]
    
    PageOneIdentityBoolLet = 16,
    
    /// <summary>
    /// قبض کارت ملی 
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.NationalCardArrived))]
    
    NationalCardArrived = 17,
    
    /// <summary>
    /// قبض کارت ملی 
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.BranchProfile))]
    
    BranchProfile = 18,
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.None))]
    None,
}