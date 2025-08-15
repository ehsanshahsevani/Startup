// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
//
// namespace DomainSeedworks;
//
// public class BaseEntityWithName : BaseEntity
// {
//     /// <summary>
//     /// نام فارسی
//     /// </summary>
//
//     [Display(
//         ResourceType = typeof(Resources.DataDictionary),
//         Name = nameof(Resources.DataDictionary.NameFA))]
//     
//     [Required(
//         ErrorMessageResourceType = typeof(Resources.Messages),
//         ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
//     
//     [MaxLength(
//         Constants.MaxLength.Name,
//         ErrorMessageResourceType = typeof(Resources.Messages),
//         ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
//     
//     public string NameFA { get; set; }
//     
//     
//     /// <summary>
//     /// نام انگلیسی
//     /// </summary>
//
//     [Display(
//         ResourceType = typeof(Resources.DataDictionary),
//         Name = nameof(Resources.DataDictionary.NameEN))]
//     
//     [Required(
//         ErrorMessageResourceType = typeof(Resources.Messages),
//         ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
//     
//     [MaxLength(
//         Constants.MaxLength.Name,
//         ErrorMessageResourceType = typeof(Resources.Messages),
//         ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
//     
//     public string NameEN { get; set; }
//     
//     [NotMapped]
//     public virtual string DisplayName => (NameFA + " | " + NameEN).Trim();
// }