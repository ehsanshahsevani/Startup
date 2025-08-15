using Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities;

namespace Domain;

/// <summary>
/// کدهای حسابداری 
/// </summary>
public class AccountCoding : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public AccountCoding() : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        Children = new List<AccountCoding>();
    }

    #region Constants

    [NotMapped]
    public static readonly string TalaSootBankAccountCode = "011011";

    [NotMapped]
    public static readonly string UserMoneyAssetsCode = "01102";

    [NotMapped]
    public static readonly int UserMoneyAssetsCodeCharCount = 8;

    [NotMapped]
    public static readonly string UserGoldAssetsCode = "01103";
    
    [NotMapped]
    public static readonly int UserGoldAssetsCodeCharCount = 8;

    [NotMapped]
    public static readonly string UserBankAccountCode = "01104";

    [NotMapped]
    public static readonly int UserBankAccountCodeCharCount = 9;

    [NotMapped]
    public static readonly string GoldTreasuryCode = "01105";

    [NotMapped]
    public static readonly string UserRoundCode = "01106";
    
    [NotMapped]
    public static readonly int UserRoundCodeCount = 8;

    [NotMapped]
    public static readonly string GoldPurchaseFee = "01201";

    [NotMapped]
    public static readonly string WalletRechargeFeeIncome = "01202";

    [NotMapped]
    public static readonly string GoldMaintenanceFeeCode = "01203";

    [NotMapped]
    public static readonly string IncomeSaleOfGoldCode = "01204";

    [NotMapped]
    public static readonly string IncomeSellingGoods = "01205";

    [NotMapped]
    public static readonly string IncomeCommissionCode = "01206";
    
    [NotMapped]
    public static readonly string ReferalCode = "01301";

    [NotMapped]
    public static readonly Dictionary<string, string> AccountCodings = InitializeAccountCodings();

    private static Dictionary<string, string> InitializeAccountCodings()
    {
        var accountCodings = new Dictionary<string, string>();

        void AddSafely(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key) == false)
            {
                if (accountCodings.ContainsKey(key) == false)
                {
                    accountCodings.Add(key, value);
                }
                else
                {
                    Console.WriteLine($"Warning: Duplicate key `{key}` was not added to AccountCodings.");
                }
            }
            else
            {
                Console.WriteLine($"Error: Attempted to add an empty or null key to AccountCodings.");
            }
        }
        
        AddSafely("01", "طلاسوت");
        AddSafely("011", "دارایی");
        AddSafely("01101", "موجودی بانک");

        AddSafely(TalaSootBankAccountCode, "موجودی حساب بانکی طلاسوت");
        
        AddSafely(UserMoneyAssetsCode, "کیف پول کاربران");
        AddSafely($"{UserMoneyAssetsCode}00000001", "کیف پول احسان شاهسونی");

        AddSafely(UserGoldAssetsCode, "موجودی طلای کاربران");
        AddSafely($"{UserGoldAssetsCode}00000001", "موجودی طلای احسان شاهسونی");

        AddSafely(UserBankAccountCode, "حساب های بانکی کاربران");
        AddSafely($"{UserBankAccountCode}000000001", "حساب بانک کشاورزی احسان شاهسونی - 6037701642801184");

        AddSafely(GoldTreasuryCode, "خزانه طلا");
        
        AddSafely(UserRoundCode, "گرد کردن موجودی کاربران");
        AddSafely($"{UserRoundCode}000000001", "درآمد رند کردن موجودی احسان شاهسونی");
        
        AddSafely("012", "درآمد");
        AddSafely(GoldPurchaseFee, "کارمزد خرید طلا");
        AddSafely(WalletRechargeFeeIncome, "کارمزد شارژ کیف پول");
        AddSafely(GoldMaintenanceFeeCode, "کارمزد نگهداری طلا");
        AddSafely(IncomeSaleOfGoldCode, "کارمزد فروش طلا");
        AddSafely(IncomeSellingGoods, "درآمد فروش محصولات");
        AddSafely($"{IncomeSellingGoods}00000000001", "درآمد فروش محصول فروشگاه با کد سفارش - 030514030001");
        AddSafely(IncomeCommissionCode, "کمسیون");

        AddSafely("013", "هزینه ها");
        AddSafely(ReferalCode, "هزینه رفرال");

        return accountCodings;
    }

    #endregion /Constants

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
    /// کد 
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Code))]
    
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]

    public string Code { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// شناسه والد
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Guid))]
    
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
    
    public string? ParentId { get; set; }

    public AccountCoding? Parent { get; set; }
    // *********************************************
    
    public List<AccountCoding> Children { get; set; }
    public List<DocumentDetail> DocumentDetails { get; set; }
    
    // *********************************************
    // ناحیه مربوط به ثبت سند در ریپازیتوری ها
    
    /// <summary>
    /// بدهکار
    /// </summary>
    [NotMapped]
    public bool IsDebtor { get; set; }
    
    /// <summary>
    /// بستانکار
    /// </summary>
    [NotMapped]
    public bool IsCreditor { get; set; }

    /// <summary>
    /// استفاده در سند والد و اصلی
    /// </summary>
    [NotMapped]
    public bool UseParentDocument { get; set; }
    
    /// <summary>
    /// قیمت
    /// </summary>
    [NotMapped]
    public decimal Amount { get; private set; }
    
    /// <summary>
    /// سوت طلا - وزن
    /// </summary>
    [NotMapped]
    public decimal GoldSoot { get; private set; }
    
    /// <summary>
    /// قیمت لحظه ای طلا
    /// </summary>
    [NotMapped]
    public decimal PriceInThisTime { get; private set; }

    /// <summary>
    /// ساب سیستم آیدی
    /// </summary>
    [NotMapped]
    public string SubSystemLocalId { get; set; }

    /// <summary>
    /// شناسه رکورد موجود در دیتابیس
    /// </summary>
    [NotMapped]
    public string RelationId { get; set; }

    /// <summary>
    /// قیمت وارد میشود و با توجه به قیمت لحظه ای طلا معادل طلای آن محاسبه میشود
    /// </summary>
    /// <param name="amount">مبلغ این سند</param>
    /// <param name="goldPriceInThisTime">قیمت لحظه ای طلا</param>
    /// <param name="searchLocation">شناسه زیر سیستم مربوطبه و آیدی رکورد موجود در دیتابیس</param>
    public void SetDocumentPropertiesByAmount
        (decimal amount, decimal goldPriceInThisTime, AccountCodingSubSystemLocal searchLocation)
    {
        Amount = amount;
        PriceInThisTime = goldPriceInThisTime;
        GoldSoot = amount.TomanToGold(goldPriceInThisTime);
        
        RelationId = searchLocation.RelationId;
        SubSystemLocalId = searchLocation.SubSystemLocalId;
    }
    // *********************************************
    public AccountCoding Clone()
    {
        var clone = new AccountCoding
        {
            Name = this.Name,
            Code = this.Code,
            
            Parent = Parent,
            ParentId = this.ParentId,
        };

        return clone;
    }
}