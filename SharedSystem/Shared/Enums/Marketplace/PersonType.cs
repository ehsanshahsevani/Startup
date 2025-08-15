using System.ComponentModel.DataAnnotations;

namespace Enums.Marketplace;

/// <summary>
/// نوع شخص
/// </summary>
public enum PersonType : int
{
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Individual))]
	
	Individual = 0, // حقیقی
	
	
	[Display(
		ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Company))]
	
	Company = 1 // حقوقی
}