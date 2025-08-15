using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewmodelSeedworks.Request;

public abstract class BaseRequestViewModelWithImage : BaseRequestViewModel
{
    /// <summary>
    /// توضیحات کاملی درباره تصویر برای گوگل
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Alternative))]
    
    [MaxLength(
        Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string? Alternative { get; set; }

    /// <summary>
    /// توضیحات تصویر برای زمانی که کاربر روی تصویر با موس ایستاد
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ImageTitle))]
    
    [MaxLength(
        Constants.MaxLength.Title,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string? ImageTitle { get; set; }
    
    /// <summary>
    /// هشدار
    /// اکیدا از تغییر نام این پراپرتی به کلمه File بپرهیزید
    /// سیستم اتومپر پراپرتی این آبجکت را اشتباهی با دامین های ما مپ میکند
    /// و مجبور میشویم نهایتا اتربیوت یا تغییر نام را در دامین ها استفاده کنیم
    /// </summary>
    public IFormFile? FileUpload { get; set; }

    
    // /// <summary>
    // /// هشدار
    // /// اکیدا از تغییر نام این پراپرتی به کلمه File بپرهیزید
    // /// سیستم اتومپر پراپرتی این آبجکت را اشتباهی با دامین های ما مپ میکند
    // /// و مجبور میشویم نهایتا اتربیوت یا تغییر نام را در دامین ها استفاده کنیم
    // /// </summary>
    // public IBrowserFile? FileUploadBlazor { get; set; }
}