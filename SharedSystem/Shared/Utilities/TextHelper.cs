namespace Utilities;

public static class TextHelper
{
	public static string SeparateThousands(this int? number)
	{
		if (number.HasValue == false)
		{
			return string.Empty;	
		}
		
		return number.Value.ToString("N0", System.Globalization.CultureInfo.InvariantCulture);
	}

	public static string SeparateThousands(this long? number)
	{
		if (number.HasValue == false)
		{
			return string.Empty;	
		}

		return number.Value.ToString("N0", System.Globalization.CultureInfo.InvariantCulture);
	}
	
	public static string SeparateThousands(this int number)
	{
		return number.ToString("N0", System.Globalization.CultureInfo.InvariantCulture);
	}

	public static string SeparateThousands(this long number)
	{
		return number.ToString("N0", System.Globalization.CultureInfo.InvariantCulture);
	}
}