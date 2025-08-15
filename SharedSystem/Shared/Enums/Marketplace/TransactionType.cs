using System.ComponentModel.DataAnnotations;

namespace Enums.Marketplace;


/// <summary>
/// جهت استفاده در جدول moneyTransaction جهت انتخاب نوع تراکنش
/// </summary>
public enum TransactionType : byte
{
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Automatic))]
    Automatic = 0,
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Manual))]
    Manual = 1,
}