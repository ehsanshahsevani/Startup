// using System.ComponentModel.DataAnnotations;
// using ViewmodelSeedworks.Request;
//
// namespace ViewmodelSeedworks.Response;
//
// public abstract class BaseResponseViewModelWithName<T> : BaseResponseViewModel<T> where T : BaseRequestViewModel
// {
// #pragma warning disable CS8618, CS9264
//     public BaseResponseViewModelWithName()
// #pragma warning restore CS8618, CS9264
//     {
//     }
//     
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
// }