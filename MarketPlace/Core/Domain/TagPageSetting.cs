using System.ComponentModel.DataAnnotations;

namespace Domain;

public class TagPageSetting : Base.BaseEntity
{
	// **************************************************
	public TagPageSetting(string nameEn, string nameFa) : base()
	{
		OnDelete = true;
		
		NameEn = nameEn;
		NameFa = nameFa;
		
		PageSettingTagPageSettings = new List<PageSettingTagPageSetting>();
	}
	// **************************************************
	
	// **************************************************
	/// <summary>
	/// نام انگلیسی
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.NameEN))]
     
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
     
	[MaxLength(
		Constants.MaxLength.Name,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
     
	public string NameEn { get; set; }
	// **************************************************
	
	// **************************************************
	/// <summary>
	/// نام فارسی
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.NameFA))]
     
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
     
	[MaxLength(
		Constants.MaxLength.Name,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]
     
	public string NameFa { get; set; }
	// **************************************************
	
	// **************************************************
	/// <summary>
	/// قابلیت حذف
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.OnDelete))]

	public bool OnDelete { get; set; }
	// **************************************************
	
	// **************************************************
	public List<PageSettingTagPageSetting> PageSettingTagPageSettings { get; set; }
	// **************************************************
}