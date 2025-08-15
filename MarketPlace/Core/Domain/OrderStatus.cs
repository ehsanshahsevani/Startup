using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

public class OrderStatus : BaseEntity
{
    public OrderStatus()
    {
    }
    
    // *********************************************
    /// <summary>
    /// نام وضعیت
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Name))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    
    [MaxLength(
        length: Constants.MaxLength.Title,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string Name { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// تاریخ بروز رسانی وضعیت سفارش 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.UpdateDate))]
    
    public DateTime? UpdateStatusDate { get; set; }
    // *********************************************
    
    // *********************************************
    public List<Order> Orders { get; set; }
    // *********************************************
}