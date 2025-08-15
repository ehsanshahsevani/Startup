using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;
/// <summary>
/// استان
/// </summary>
public class Province: BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Province():base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        Cities = new List<City>();
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
    
    public List<City> Cities { get; set; }
}