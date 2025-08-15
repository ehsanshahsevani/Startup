using System.ComponentModel.DataAnnotations;

namespace Domain;

public class PageSettingTagPageSetting : Base.BaseEntity
{
	public PageSettingTagPageSetting(string pageSettingId, string tagPageSettingId) : base()
	{
		PageSettingId = pageSettingId;
		TagPageSettingId = tagPageSettingId;
	}
	
	// **************************************************
	/// <summary>
	/// شناسه تنظمات صفحه
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.PageSetting))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string PageSettingId { get; set; }

	/// <summary>
	/// تنظیمات صفحه
	/// </summary>
	public PageSetting? PageSetting { get; set; }
	// **************************************************
	
	// **************************************************
	/// <summary>
	/// شناسه تگ تنظمات صفحه
	/// </summary>

	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.TagPageSetting))]
	
	[Required(
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.RequiredError))]
	
	[MaxLength(
		length: Constants.FixedLength.Guid,
		ErrorMessageResourceType = typeof(Resources.Messages),
		ErrorMessageResourceName = nameof(Resources.Messages.MaxLengthError))]

	public string TagPageSettingId { get; set; }

	/// <summary>
	/// تگ تنظیمات صفحه
	/// </summary>
	public TagPageSetting? TagPageSetting { get; set; }
	// **************************************************
}