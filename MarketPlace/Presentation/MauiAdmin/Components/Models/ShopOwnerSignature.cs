using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SampleResult;

namespace MauiAdmin.Components.Models;

public class ShopOwnerSignature {
    public string Id { get; set; }
    
    // *********************************************
    /// <summary>
    /// نام صاحب امضاء
    /// </summary>
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.UserName))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.MaxLength.Username,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    // *********************************************
    public string? FullName { get; set; }
    // *********************************************
    
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
    /// اعتبارسنجی اطلاعات
    /// </summary>
    public bool? IsValid { get; set; }
    // *********************************************
    
    public Result Validate()
    {
        var result = new FluentResults.Result();
        
        var resultModel = Utilities.ValidationHelper.GetValidationResults(this);
        
        result.WithErrors(resultModel.Select(x => x.ErrorMessage));

        if (NationalCardFront is null)
        {
            var errorMessage =
                string.Format(Resources.Messages.RequiredError,  Resources.DataDictionary.NationalCardFront);
            
            result.WithError(errorMessage);
        }
        
        if (NationalCardBack is null)
        {
            var errorMessage =
                string.Format(Resources.Messages.RequiredError,  Resources.DataDictionary.NationalCardBack);
            
            result.WithError(errorMessage);
        }
        
        return result.ConvertToSampleResult();
    }

    public ShopOwnerSignature Clone()
    {
        return (ShopOwnerSignature)this.MemberwiseClone();
    }
}