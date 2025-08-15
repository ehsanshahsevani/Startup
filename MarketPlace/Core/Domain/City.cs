using Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;
/// <summary>
/// شهر
/// </summary>
public class City : BaseEntity
{
#pragma warning disable CS8618, CS9264
    public City() : base()
#pragma warning restore CS8618, CS9264
    {
    }

    // *********************************************
    /// <summary>
    /// نام
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
    /// استان
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ProvinceLbl))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string ProvinceId { get; set; }
    
    /// <summary>
    /// استان
    /// </summary>
    
    public Province? Province { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// استان
    /// </summary>
    public List<Branch> Branches { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// استان
    /// </summary>
    public List<Profile> Profiles { get; set; }
    // *********************************************
}