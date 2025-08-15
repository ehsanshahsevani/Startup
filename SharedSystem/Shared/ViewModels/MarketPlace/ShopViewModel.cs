using System.ComponentModel.DataAnnotations;
using Constants;
using Enums.Marketplace;
using SampleResult;
using Microsoft.AspNetCore.Http;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.Marketplace;

public class ShopResponseViewModel : BaseResponseViewModelWithImage<ShopRequestViewModel>
{
    // *********************************************
    /// <summary>
    /// نام فارسی
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.NameFA))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        Constants.MaxLength.Name,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string Name { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// درباره فروشگاه
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.AboutShop))]
    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string? AboutShop { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// درباره فروشگاه
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.PersonType))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    public PersonType PersonType { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// ذخیره شناسه کاربر با تابع:
    /// SetUserId()
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.User))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string UserId { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// جهت مشخص کردن وضعیت حقیقی/حقوقی
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.UserPersonType))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    public UserShopType UserShopType { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// آدرس
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Address))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string Address { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// شماره شناسایی شرکت
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CompanyIdentificationNumber))]
    // [Required(
    //     ErrorMessageResourceType = typeof(Resources.Messages),
    //     ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    public string CompanyIdentificationNumber { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// شماره ثبت 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.RegisterNumber))]
    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    public string? RegistraionNumber { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// کد اقتصادی
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.EconomicCode))]
    // [Required(
    //     ErrorMessageResourceType = typeof(Resources.Messages),
    //     ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    public string EconomicCode { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// شبا 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Shaba))]
    [MaxLength(
        length: Constants.MaxLength.ShabaNumber,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    public string? Shaba { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// مالک کارت بانکی 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.BankCardOwner))]
    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    public string? BankCardOwner { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// کد ملی
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.NationalCode))]
    [MaxLength(
        length: Constants.FixedLength.NationalCode,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    public string? NationalCode { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// شناسه نوع فروشگاه
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ShopType))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    public string ShopTypeId { get; set; }

    // *********************************************

    // *********************************************
    /// <summary>
    /// شماره تلفن
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.PhoneNumber))]
    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    public string? PhoneNumber { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// روز کاری
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.WorkingDay))]
    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    public string? WorkingDay { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// ادرس سایت
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Site))]
    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    public string? SiteShop { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// ایمیل 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Email))]
    [MaxLength(
        length: Constants.MaxLength.EmailAddress,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    public string? Email { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// تگ
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.PinInMobile))]

    // [Required(
    //     ErrorMessageResourceType = typeof(Resources.Messages),
    //     ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string? Tag { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// Read This Value For Set From Token JWT
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.User))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string? ConfirmedByUserId { get; set; }
    // *********************************************

    /// <summary>
    /// تایید بودن یا نبودن
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.IsConfirmed))]

    public bool IsConfirmed { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// دلیل عدم تایید
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Address))]
    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string? ConfirmReason { get; set; }
    // *********************************************

    /// <summary>
    /// اسم و فامیل در حالت حقیقی به بک ارسال و در پروفایل کاربر قرار شد آپدیت شود.
    /// با هماهنگی آقای شاهسونی
    /// </summary>
    /// <returns></returns>
    public string? FullName { get; set; }

    // *********************************************

    public string ShopDisplayName => Address + " | " + Name;

    public override ShopRequestViewModel ToRequest()
    {
        var result = new ShopRequestViewModel
        {
            Id = Id,
            Name = Name,
            FullName = FullName,
            IsActive = IsActive,
            Ordering = Ordering,
            Description = Description,
            
            Address = Address,
            AboutShop = AboutShop,
            Alternative = Alternative,
            
            NationalCode = NationalCode,
            BankCardOwner = BankCardOwner,
            
            Email = Email,
            Shaba = Shaba,
            SiteShop = SiteShop,
            WorkingDay = WorkingDay,
            ShopTypeId = ShopTypeId,
            PhoneNumber = PhoneNumber,
            EconomicCode = EconomicCode,
            RegistrationNumber = RegistraionNumber,
            CompanyIdentificationNumber = CompanyIdentificationNumber,

            UserShopType = UserShopType,
            PersonType = PersonType,
            Tags = TagManager.Shop.FindTagField(Tag),
        };

        return result;
    }
}

public class ShopRequestViewModel : BaseRequestViewModelWithImage
{
    public ShopRequestViewModel()
    {
        Tags = new Dictionary<string, string>();
    }
    
    // *********************************************
    /// <summary>
    /// نام فارسی
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.NameFA))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        Constants.MaxLength.Name,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string Name { get; set; }
    // *********************************************


    // *********************************************
    /// <summary>
    /// درباره فروشگاه
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.AboutShop))]
    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string? AboutShop { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// جهت مشخص کردن وضعیت حقیقی/حقوقی
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.UserPersonType))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    public UserShopType UserShopType { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// آدرس
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Address))]

    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string Address { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// شماره شناسایی شرکت
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CompanyIdentificationNumber))]

    // [Required(
    //     ErrorMessageResourceType = typeof(Resources.Messages),
    //     ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string CompanyIdentificationNumber { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// شماره ثبت 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.RegisterNumber))]

    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string? RegistrationNumber { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// کد اقتصادی
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.EconomicCode))]
    
    // [Required(
    //     ErrorMessageResourceType = typeof(Resources.Messages),
    //     ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string EconomicCode { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// شبا 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Shaba))]

    [MaxLength(
        length: Constants.MaxLength.ShabaNumber,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string? Shaba { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// مالک کارت بانکی 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.BankCardOwner))]

    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string? BankCardOwner { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// کد ملی
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.NationalCode))]

    [MaxLength(
        length: Constants.FixedLength.NationalCode,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string? NationalCode { get; set; }

    // *********************************************
    // *********************************************
    /// <summary>
    /// شناسه نوع فروشگاه
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ShopType))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string ShopTypeId { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// شماره تلفن
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.PhoneNumber))]
    
    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string? PhoneNumber { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// روز کاری
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.WorkingDay))]
    
    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string? WorkingDay { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// ادرس سایت
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Site))]
    
    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string? SiteShop { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// ایمیل 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Email))]
    
    [MaxLength(
        length: Constants.MaxLength.EmailAddress,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string? Email { get; set; }
    // *********************************************

    // *********************************************
    public Dictionary<string, string> Tags { get; set; }

    // *********************************************        
    public string ShopDisplayName => Address + " | " + Name;

    // *********************************************
    /// <summary>
    /// عکس روی کارت ملی 
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.NationalCardFront))]
    public IFormFile? NationalCardFront { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// عکس پشت کارت ملی 
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.NationalCardBack))]
    public IFormFile? NationalCardBack { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// عکس گواهی ارزش افزوده 
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.VatCertificateImage))]
    public IFormFile? VatCertificateImage { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// عکس روزنامه رسمی 
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.OfficialGazetteImage))]
    public IFormFile? OfficialGazetteImage { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// اسم و فامیل در حالت حقیقی به بک ارسال و در پروفایل کاربر قرار شد آپدیت شود.
    /// با هماهنگی آقای شاهسونی
    /// </summary>
    /// <returns></returns>
    public string? FullName { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// درباره فروشگاه
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.PersonType))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    public PersonType PersonType { get; set; }
    // *********************************************

    // *********************************************
    public override Result Validate()
    {
        var result = new FluentResults.Result();

        var checkValidationResult =
            Utilities.ValidationHelper.GetValidationResults(this);

        if (checkValidationResult.Any())
        {
            result.WithErrors(checkValidationResult.Select(x => x.ErrorMessage));
        }

        if (FileUpload is not null && FileUpload.Length == 0)
        {
            result.WithError(Resources.Messages.FileError_SelectNewFile);
        }

        if (string.IsNullOrEmpty(Name) == true)
        {
            var errorMessage =
                string.Format(Resources.Messages.RequiredError, Resources.DataDictionary.NameFA);

            result.WithError(errorMessage);
        }

        FixData();

        return result.ConvertToSampleResult();
    }

    private void FixData()
    {
        if (PersonType == PersonType.Individual) //حقیقی
        {
            AboutShop = "";
            VatCertificateImage = null;
            OfficialGazetteImage = null;
            CompanyIdentificationNumber = "";
            RegistrationNumber = null;
            EconomicCode = "";
            Email = null;
            PhoneNumber = null;
            SiteShop = null;
            WorkingDay = null;
        }
        else // حقوقی
        {
            NationalCode = null;
        }
    }
}

public class ShopConfirmationViewModel
{
    public string Id { get; set; }
    public string? ConfirmReason { get; set; }
}