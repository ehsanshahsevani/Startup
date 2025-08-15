using Utilities;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using ViewmodelSeedworks.Base;
using ViewmodelSeedworks.Request;

namespace ViewmodelSeedworks.Response;

public abstract class BaseResponseViewModel<T> : BaseViewModel, IRequestable<T> where T : BaseRequestViewModel
{
    public abstract T ToRequest();
    
#pragma warning disable CS8618, CS9264
    public BaseResponseViewModel() : base()
#pragma warning restore CS8618, CS9264
    {
        CreateDateTime = DateTime.Now;
        UpdateDateTime = DateTime.Now;
    }
    
    /// <summary>
    /// تاریخ ایجاد
    /// </summary>

    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CreateDate))]
    
    public DateTime CreateDateTime { get; set; }

    /// <summary>
    /// تاریخ بروزرسانی
    /// </summary>
    
    [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.UpdateDate))]

    public DateTime UpdateDateTime { get; set; }
    // **************************************************
    public string CreateTime =>
        $"{CreateDateTime.TimeOfDay.Hours.ToString("00")}:{CreateDateTime.TimeOfDay.Minutes.ToString("00")}";

    public string CreateDateShamsi => CreateDateTime.ToShamsi(0);
    
    public string UpdateTime =>
        $"{UpdateDateTime.TimeOfDay.Hours.ToString("00")}:{UpdateDateTime.TimeOfDay.Minutes.ToString("00")}";

    public string UpdateDateShamsi => UpdateDateTime.ToShamsi(0);
    // **************************************************
    
    public string ServerId { get; set; } = string.Empty;
}
