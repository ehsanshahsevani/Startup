using System.Resources;
using System.Globalization;
using Enums.Shared;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Infrastructure;

public static class Utility
{
	public static async Task<IFormFile> ConvertIBrowserFileToFormFileAsync(IBrowserFile browserFile)
	{
		// باز کردن استریم فایل به صورت غیرهمزمان
		var stream = browserFile.OpenReadStream();
		var memoryStream = new MemoryStream();

		// کپی کردن استریم به حافظه به صورت غیرهمزمان
		await stream.CopyToAsync(memoryStream);

		// بازگرداندن فایل به صورت IFormFile
		return new FormFile(memoryStream, 0, browserFile.Size, "FileUpload", browserFile.Name)
		{
			Headers = new HeaderDictionary(),
			ContentType = browserFile.ContentType
		};
	}
	
	public static async Task<byte[]?> IBrowserFileToByteArray(this IBrowserFile? file)
	{
		if (file is null)
		{
			return Array.Empty<byte>();
		}

		byte[] buffer = new byte[file.Size];

		_ = await file
			.OpenReadStream(buffer.Length * 2)
			.ReadAsync(buffer, 0, buffer.Length);

		return buffer; 
	}

	
	public static async Task<string> ConvertToBase64Async(this IFormFile file)
	{
		using var ms = new MemoryStream();
		await file.CopyToAsync(ms);
		var fileBytes = ms.ToArray();
		var base64 = Convert.ToBase64String(fileBytes);
		var contentType = file.ContentType;
		return $"data:{contentType};base64,{base64}";
	}
	
	public static async Task<string?> IBrowserFileToBase64(this IBrowserFile? file)
	{
		if (file is null)
		{
			return null;
		}

		byte[] buffer = new byte[file.Size];

		_ = await file
		   .OpenReadStream(buffer.Length * 2)
		   .ReadAsync(buffer, 0, buffer.Length);

		string base64 = Convert.ToBase64String(buffer);

		return base64;
	}

	public static byte[] Base64ToByteArray(this string? base64)
	{
		if (string.IsNullOrEmpty(base64) == true)
		{
			return new byte[0];
		}

		byte[] result = Convert.FromBase64String(base64);
		return result;
	}

	/// <summary>
	/// https://localhost:1101/Name?X=1&Y=2
	/// </summary>
	/// <param name="location"></param>
	/// <returns>Name</returns>
	public static string GetNamePage(string location)
	{
		var arrayBySlash = location.Split('/');
		var hrefPage = arrayBySlash[3];
		if (hrefPage.Contains('?') == true)
		{
			var result = hrefPage.Split('?').First();
			return result;
		}
		else
		{
			return hrefPage;
		}
	}

	// **************************************************
	public static Dictionary<string, string?>? GetValuesFromResources<TResource>()
	{
		var result = new Dictionary<string, string?>();

		ResourceManager resource = new ResourceManager(typeof(TResource));

		ResourceSet? resourceSet =
			resource.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

		if (resourceSet is null)
		{
			throw new ArgumentNullException(nameof(resourceSet));
		}

		var dictionaryEnumerator = resourceSet.GetEnumerator();

		if (dictionaryEnumerator is null)
		{
			throw new ArgumentNullException(nameof(dictionaryEnumerator));
		}

		while (dictionaryEnumerator.MoveNext() == true)
		{
			var value =
				dictionaryEnumerator.Value is null ? "" : (string)dictionaryEnumerator.Value;

			result.Add((string)dictionaryEnumerator.Key, value);
		}

		return result;
	}
	// **************************************************
	
	// **************************************************
	public static string GetTableColorByBootstrapColor(BootstrapColor color)
	{
		switch (color)
		{
			case BootstrapColor.Success: return "table-success";
			case BootstrapColor.Danger: return "table-danger";
			case BootstrapColor.Primary: return "table-primary";
			case BootstrapColor.Info: return "table-info";
			case BootstrapColor.Light: return "table-light";
			case BootstrapColor.Dark: return "table-dark";
			case BootstrapColor.Warning: return "table-warning";
			case BootstrapColor.Secondary: return "table-secondary";
			default: return string.Empty;
		}
	}
	
	public static string GetBtnColorByBootstrapColor(BootstrapColor color)
	{
		switch (color)
		{
			case BootstrapColor.Success: return "btn-success";
			case BootstrapColor.Danger: return "btn-danger";
			case BootstrapColor.Primary: return "btn-primary";
			case BootstrapColor.Info: return "btn-info";
			case BootstrapColor.Light: return "btn-light";
			case BootstrapColor.Dark: return "btn-dark";
			case BootstrapColor.Warning: return "btn-warning";
			case BootstrapColor.Secondary: return "btn-secondary";
			case BootstrapColor.Link: return "btn-link";
			default: return string.Empty;
		}
	}
	
	public static string GetBtnOutLineColorByBootstrapColor(BootstrapColor color)
	{
		switch (color)
		{
			case BootstrapColor.Success: return "btn-outline-success";
			case BootstrapColor.Danger: return "btn-outline-danger";
			case BootstrapColor.Primary: return "btn-outline-primary";
			case BootstrapColor.Info: return "btn-outline-info";
			case BootstrapColor.Light: return "btn-outline-light";
			case BootstrapColor.Dark: return "btn-outline-dark";
			case BootstrapColor.Warning: return "btn-outline-warning";
			case BootstrapColor.Secondary: return "btn-outline-secondary";
			case BootstrapColor.Link: return "btn-outline-link";
			default: return string.Empty;
		}
	}
	
	public static string GetBackgoundColorByBootstrapColor(BootstrapColor color)
	{
		switch (color)
		{
			case BootstrapColor.Success: return "bg-success";
			case BootstrapColor.Danger: return "bg-danger";
			case BootstrapColor.Primary: return "bg-primary";
			case BootstrapColor.Info: return "bg-info";
			case BootstrapColor.Light: return "bg-light";
			case BootstrapColor.Dark: return "bg-dark";
			case BootstrapColor.Warning: return "bg-warning";
			case BootstrapColor.Secondary: return "bg-secondary";
			case BootstrapColor.Link: return "bg-link";
			default: return string.Empty;
		}
	}
	
	public static string GetForgroundColorByBootstrapColor(BootstrapColor color)
	{
		switch (color)
		{
			case BootstrapColor.Success: return "text-success";
			case BootstrapColor.Danger: return "text-danger";
			case BootstrapColor.Primary: return "text-primary";
			case BootstrapColor.Info: return "text-info";
			case BootstrapColor.Light: return "text-light";
			case BootstrapColor.Dark: return "text-dark";
			case BootstrapColor.Warning: return "text-warning";
			case BootstrapColor.Secondary: return "text-secondary";
			case BootstrapColor.Link: return "text-link";
			default: return string.Empty;
		}
	}
	
	public static string GetBorderColorByBootstrapColor(BootstrapColor color)
	{
		switch (color)
		{
			case BootstrapColor.Success: return "border-success";
			case BootstrapColor.Danger: return "border-danger";
			case BootstrapColor.Primary: return "border-primary";
			case BootstrapColor.Info: return "border-info";
			case BootstrapColor.Light: return "border-light";
			case BootstrapColor.Dark: return "border-dark";
			case BootstrapColor.Warning: return "border-warning";
			case BootstrapColor.Secondary: return "border-secondary";
			case BootstrapColor.Link: return "border-link";
			default: return string.Empty;
		}
	}
	// **************************************************

}
