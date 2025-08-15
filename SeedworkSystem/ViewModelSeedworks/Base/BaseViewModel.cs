using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using InfrastructureSeedworks;

namespace ViewmodelSeedworks.Base;

public class BaseViewModel : ServerSettings
{
	public BaseViewModel()
	{
		IsActive = true;
	}
	
	/// <summary>
	/// شناسه دیتابیس
	/// </summary>
    
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Guid))]
    
	public string? Id { get; set; }

	
	/// <summary>
	/// توضیحات
	/// </summary>
    
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Description))]

	public string? Description { get; set; }
    
	/// <summary>
	/// فعال بودن یا نبودن
	/// </summary>
    
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActive))]

	public bool IsActive { get; set; }
    
	/// <summary>
	/// ترتیب نمایش
	/// </summary>
    
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Order))]
	
	public int? Ordering { get; set; }
	
	public override string ToString()
	{
		var stringBuilder = new StringBuilder();

		// دریافت تمام پراپرتی‌های کلاس با Reflection
		foreach (PropertyInfo property in this.GetType().GetProperties())
		{
			// گرفتن نام و مقدار پراپرتی
			string propertyName = property.Name;
			object? propertyValue = property.GetValue(this);

			// اضافه کردن به استرینگ خروجی با پایپ
			stringBuilder.Append($"{propertyName}: {propertyValue} | ");
		}

		// حذف پایپ اضافی در انتها
		if (stringBuilder.Length > 0)
		{
			stringBuilder.Length -= 3; // حذف " | " آخر
		}

		return stringBuilder.ToString();
	}
}