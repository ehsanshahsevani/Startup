using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Utilities;

public static class EnumTools
{
	public static string? GetEnumDisplayName(this Enum enumType)
	{
		var result =  enumType
			
			.GetType().GetMember(name: enumType.ToString())
			
			.First()
			
			.GetCustomAttribute<DisplayAttribute>()
			
			?.Name;
        
		return result;
	}
}