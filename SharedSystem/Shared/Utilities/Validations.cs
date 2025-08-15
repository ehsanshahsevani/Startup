using Microsoft.AspNetCore.Http;
namespace Utilities;

public static class Validations
{
	// **************************************************
	public static bool IsValidImage(this IFormFile? image, long mb = 2)
	{
		var volume = Math.Pow(1024, 2) * mb;

		List<string> extensions =
			[".jpg", ".img", ".png", ".jpeg", ".gif"];

		if (image is null)
		{
			return false;
		}

		var ext =
			System.IO.Path.GetExtension(image.FileName);

		if (image.Length <= volume && extensions.Contains(ext.ToLower()))
		{
			return true;
		}

		return false;
	}
	// **************************************************

	// **************************************************
	public static bool IsValidFile(this IFormFile? file, long mb = 40)
	{
		var volume = Math.Pow(1024, 2) * mb;

		List<string> extensions =
			[".apk"];

		if (file is null)
		{
			return false;
		}

		var ext =
			System.IO.Path.GetExtension(file.FileName);

		if (file.Length <= volume && extensions.Contains(ext.ToLower()))
		{
			return true;
		}

		return false;
	}
	// **************************************************

	// **************************************************
	public static bool IsValidFileSize(this IFormFile file, long MB = 2)
	{
		var volume = Math.Pow(1024, 2) * MB;
		return file.Length <= volume;
	}
	// **************************************************

	// **************************************************
	public static bool IsValidFileSize(this byte[] fileBytes, int maxSizeInMB)
	{
		if (fileBytes == null || fileBytes.Length == 0)
		{
			return false; // اگر فایل خالی است یا داده‌ای وجود ندارد
		}

		// تبدیل مگابایت به بایت
		long maxSizeInBytes = maxSizeInMB * 1024 * 1024;

		// مقایسه طول فایل با مقدار مجاز
		return fileBytes.Length <= maxSizeInBytes;
	}
	// **************************************************

	// **************************************************
	public static bool IsValidSize(long max, double MB = 2)
	{
		var volume = Math.Pow(1024, 2) * MB;
		return (max <= volume) ? true : false;
	}
	// **************************************************
}
