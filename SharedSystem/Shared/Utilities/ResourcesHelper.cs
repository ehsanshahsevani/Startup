using System.Resources;
using System.Globalization;

namespace Utilities;

public static class ResourcesHelper
{
	/// <summary>
	/// Get a value from a specific resource file using its type and a key.
	/// </summary>
	/// <param name="resourceType">The auto-generated resource class (e.g., typeof(Messages))</param>
	/// <param name="key">The key to look up</param>
	/// <returns>The localized string value</returns>
	public static string? GetValue(Type? resourceType, string? key)
	{
		if (resourceType == null || string.IsNullOrWhiteSpace(key))
		{
			return null;
		}

		var resourceManagerProperty =
			resourceType.GetProperty("ResourceManager",
				System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic |
				System.Reflection.BindingFlags.Public);

		if (resourceManagerProperty == null)
		{
			throw new ArgumentException("Invalid resource type; missing ResourceManager property");
		}

		var resourceManager = resourceManagerProperty.GetValue(null) as ResourceManager;

		return resourceManager?.GetString(key, CultureInfo.CurrentCulture);
	}
}