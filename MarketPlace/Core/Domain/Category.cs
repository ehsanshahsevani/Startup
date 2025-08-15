using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain;

/// <summary>
/// دسته بندی
/// </summary>
public class Category : BaseEntity
{
#pragma warning disable CS8618, CS9264
    public Category() : base()
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
    ///  محصولات
    /// </summary>
    public List<Product> Products { get; set; } // رابطه یک به چند
    // *********************************************
}