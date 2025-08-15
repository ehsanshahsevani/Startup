using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

public class GoldValue : BaseEntity
{
    public GoldValue()
    {
    }
    
    // *********************************************
    /// <summary>
    /// میزان طلا به سوت
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.GoldSoot))]

    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    public int GoldSoot { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// تعداد
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Count))]

    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    public int Count { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// لیست درخواست های طلا
    /// </summary>
    public List<GoldRequest> GoldRequests { get; set; }
    
    // *********************************************
    
}