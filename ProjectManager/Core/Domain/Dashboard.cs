using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

/// <summary>
/// داشبورد های کلاینت های هر سرور
/// </summary>
public class Dashboard : BaseEntity
{
    public Dashboard() : base()
    {
    }

    // **************************************************
    /// <summary>
    /// آیدی مشخص از سروری که دامین فعلی درونش قرار دارد
    /// اصولا این آیدی در BaseEntity سرورهای مقصد وجود دارد
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Server))]

    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public new string ServerId { get; set; }
    public Server? Server { get; set; }
    // **************************************************

    // **************************************************
    /// <summary>
    /// نام صفحه
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Name))]

    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    [MaxLength(
        length: Constants.MaxLength.Name,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string Name { get; set; }
    // **************************************************

    // **************************************************
    /// <summary>
    /// لیست دسترسی های مربوط به این داشبورد
    /// </summary>
    public List<DashboardPageRole> DashboardPageRoles { get; set; }
    // **************************************************
}