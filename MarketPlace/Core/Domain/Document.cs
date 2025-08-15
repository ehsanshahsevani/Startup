using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

public class Document : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Document() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        DocumentDetails =  new List<DocumentDetail>();
        RelationalDocumentDetails = new List<DocumentDetail>();
    }

    // *********************************************
    /// <summary>
    /// شماره سند
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.DocumentNumber))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.MaxLength.Title,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string DocumentNumber { get; set; }
    // *********************************************


    // *********************************************
    /// <summary>
    /// شرح سند
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.DocumentFor))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string DocumentFor { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// جزئیات سند
    /// </summary>
    
    public List<DocumentDetail> DocumentDetails { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
     /// جزئیات سندهای وابسته
    /// </summary>
    
    public List<DocumentDetail> RelationalDocumentDetails { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// لیست دارایی هایی که برای این سند ثبت شده
    /// </summary>
    
    public List<UserAssets> UserAssetsList { get; set; }
    // *********************************************
}