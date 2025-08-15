using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain;

public class GoldRequest : BaseEntity
{
    public GoldRequest()
    {
    }

    // *********************************************
    /// <summary>
    /// شناسه کاربر
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Profile))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string ProfileId { get; set; }

    /// <summary>
    /// پروفایل
    /// </summary>
    public Profile? Profile { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// شناسه شعبه
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Branch))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string BranchId { get; set; }

    /// <summary>
    /// شناسه شعبه
    /// </summary>
    public Branch Branch { get; set; }
    // *********************************************


    // *********************************************
    /// <summary>
    /// شناسه مقادیر طلا
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.GoldValue))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string GoldValueId { get; set; }

    /// <summary>
    /// شناسه مقادیر طلا
    /// </summary>
    public GoldValue GoldValue { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// تاریخ دریافت 
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ReceiveDate))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    public DateTime ReceiveDate { get; set; }
    // *********************************************

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
    /// قیمت لحظه ای طلا
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.GoldPriceInThisTime))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    public int GoldPriceInThisTime { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// معادل تومان
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Amount))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    public int Amount { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// شناسه مقادیر طلا
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.GoldRequestStatus))]
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string GoldRequestStatusId { get; set; }

    /// <summary>
    /// شناسه مقادیر طلا
    /// </summary>
    public GoldRequestStatus GoldRequestStatus { get; set; }
    // *********************************************
}