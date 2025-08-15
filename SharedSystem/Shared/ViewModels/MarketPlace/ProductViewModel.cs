using System.ComponentModel.DataAnnotations;
using Constants;
using Resources;
using SampleResult;
using Utilities;
using ViewmodelSeedworks.Request;
using ViewmodelSeedworks.Response;

namespace ViewModels.Marketplace;

public class ProductResponseViewModel : BaseResponseViewModelWithImage<ProductRequestViewModel>
{
    public ProductResponseViewModel()
    {
        ProductBranchResponseViewModels =
            new List<ProductBranchResponseViewModel>();
    }
    
    // *********************************************
    /// <summary>
    ///     شناسه دسته بندی
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Category))]
    
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]

    [MaxLength(
        FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string CategoryId { get; set; }

    public string CategoryDisplayName { get; set; }

    // *********************************************

    // *********************************************
    /// <summary>
    ///     سوت طلا
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.GoldSoot))]
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]

    public int GoldSoot { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     نام
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Name))]
    
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    
    [MaxLength(
        MaxLength.Name,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Name { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     فی  - قیمت واحد
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Fee))]
    
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]

    public int Fee { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     تعداد
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Count))]
    
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]

    public int Count { get; set; }
    // *********************************************
    
    // *********************************************
    public List<ProductBranchResponseViewModel> ProductBranchResponseViewModels { get; set; }
    // *********************************************

    // *********************************************
    public override ProductRequestViewModel ToRequest()
    {
        var result = new ProductRequestViewModel
        {
            Id = Id,
            Name = Name,
            Fee = Fee,
            Count = Count,
            GoldSoot = GoldSoot,
            CategoryId = CategoryId,
            Ordering = Ordering,
            Description = Description,
            IsActive = IsActive,
            
            BranchIds = ProductBranchResponseViewModels.Select(x => x.BranchId).ToList(),
        };
        return result;
    }
    // *********************************************
}

/// <summary>
///     دیتای مورد نیاز برای ایجاد یک محصول (Request)
/// </summary>
public class ProductRequestViewModel : BaseRequestViewModelWithImage
{
    public ProductRequestViewModel()
    {
        BranchIds = new List<string>();
    }
    
    // *********************************************
    /// <summary>
    /// شناسه دسته بندی
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Category))]
    
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    
    [MaxLength(
        FixedLength.Guid,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string CategoryId { get; set; }

    public string CategoryDisplayName { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     سوت طلا
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.GoldSoot))]
    
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]

    public int? GoldSoot { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     نام
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Name))]
    
    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.RequiredError))]
    
    [MaxLength(
        MaxLength.Name,
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.MaxLengthError))]

    public string Name { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     فی  - قیمت واحد
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Fee))]
    
    // [Required(
    //     ErrorMessageResourceType = typeof(Messages),
    //     ErrorMessageResourceName = nameof(Messages.RequiredError))]

    public int? Fee { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    ///     تعداد
    /// </summary>

    [Display(
        ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Count))]

    // [Required(
    //     ErrorMessageResourceType = typeof(Messages),
    //     ErrorMessageResourceName = nameof(Messages.RequiredError))]

    public int? Count { get; set; }
    // *********************************************
    
    // *********************************************
    /// <summary>
    /// لیست برنچ های این محصول
    /// </summary>
    public List<string?> BranchIds { get; set; }
    // *********************************************

    public override Result Validate()
    {
        var result = new FluentResults.Result();

        var checkValidationResult =
            ValidationHelper.GetValidationResults(this);

        if (checkValidationResult.Any())
        {
            result.WithErrors(checkValidationResult.Select(x => x.ErrorMessage));
        }

        if (Fee.HasValue == false || Fee <= 0)
        {
            var errorMessage =
                string.Format(Messages.FieldMinValueError,
                    DataDictionary.Fee, 0);

            result.WithError(errorMessage);
        }

        if (Count.HasValue == false)
        {
            var errorMessage =
                string.Format(Messages.FieldMinValueError,
                    DataDictionary.Count, 0);

            result.WithError(errorMessage);
        }

        if (BranchIds.Any() == false || BranchIds.Count != 1)
        {
            result.WithError(Messages.RequiredError);
        }

        return result.ConvertToSampleResult();
    }
}

public class ProductBranchResponseViewModel : BaseResponseViewModel<ProductBranchRequestViewModel>
{
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

    public string? BranchId { get; set; }

    /// <summary>
    /// شعبه
    /// </summary>

    public string? BranchDisplayName { get; set; }
    // *********************************************

    // *********************************************
    /// <summary>
    /// شناسه محصول
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Product))]
	
    [Required(
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
    [MaxLength(
        length: Constants.FixedLength.Guid,
        ErrorMessageResourceType = typeof(Resources.Messages),
        ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

    public string? ProductId { get; set; }

    /// <summary>
    /// محصول
    /// </summary>
    public string? ProductDisplayName { get; set; }
    // *********************************************
    
    public override ProductBranchRequestViewModel ToRequest()
    {
        throw new NotImplementedException();
    }
}

public class ProductBranchRequestViewModel : BaseRequestViewModel
{
    public override Result Validate()
    {
        throw new NotImplementedException();
    }
}