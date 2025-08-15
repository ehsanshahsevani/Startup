using FluentResults;
using SixLabors.ImageSharp;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats.Jpeg;
using Microsoft.AspNetCore.Http.Internal;

namespace Utilities;

public static class FileTools
{
	static FileTools()
	{
	}

	// **************************************************
	// V 0.1
	public static byte[] Base64ToByteArray(this string? base64)
	{
		if (string.IsNullOrEmpty(base64) == true)
		{
			return Array.Empty<byte>();
		}

		byte[] result = Convert.FromBase64String(base64);

		return result;
	}

	// V 0.2
	//public static byte[] Base64ToByteArray(this string? base64)
	//{
	//	byte[] result = Convert.FromBase64String(base64 ?? string.Empty);

	//	return result;
	//}

	// V 0.3
	// Not Recommanded !
	//public static byte[] Base64ToByteArray(this string? base64)
	//	=> string.IsNullOrEmpty(base64) ? Array.Empty<byte>() : Convert.FromBase64String(base64);

	// V 0.4
	// Not Recommanded !
	//public static byte[] Base64ToByteArray(this string? base64)
	//	=> Convert.FromBase64String(base64 ?? string.Empty);
	// **************************************************

	// **************************************************
	public static void SaveByteArrayToFile(this byte[] data, string filePath)
	{
		using var stream = File.Create(filePath);
		stream.Write(data, 0, data.Length);
	}
	// **************************************************

	// **************************************************
	public static byte[] IFormFileToByte(this IFormFile file)
	{
		var buffer = new byte[file.Length];

		file.OpenReadStream().Read(buffer: buffer, 0, buffer.Length);

		return buffer;
	}
	// **************************************************

	// **************************************************
	public static byte[] FileToByte(string path)
	{
		byte[] arrayByte = File.ReadAllBytes(path);

		return arrayByte;
	}
	// **************************************************

	// **************************************************
	/// <summary>
	/// مناسب برای تمام سیستم عامل ها
	/// </summary>
	/// <param name="data"></param>
	/// <returns></returns>
	public static Image<Rgba32> ByteArrayToImage(this byte[] data)
	{
		using var ms = new MemoryStream(data);

		// با این خط، فرمت تصویر به‌صورت خودکار شناسایی می‌شود (jpeg, png, webp و ...)
		var image = Image.Load<Rgba32>(ms);

		return image;
	}
	// **************************************************

	// **************************************************
	public static string SaveImageInDirectory(
		this Image<Rgba32> image,
		string directoryPath,
		string? fileName = null)
	{
		// اگر نام فایل داده نشده بود، یک نام با Guid بساز
		var name = string.IsNullOrWhiteSpace(fileName)
			? $"{Guid.NewGuid()}.jpeg"
			: fileName;

		var path = Path.Combine(directoryPath, name);

		// اگر پوشه وجود نداشت، بسازش
		if (!Directory.Exists(directoryPath))
		{
			Directory.CreateDirectory(directoryPath);
		}

		// ذخیره تصویر با فرمت JPEG
		image.Save(path, new JpegEncoder());

		return name;
	}
	// **************************************************

	// **************************************************
	/// <summary>
	/// تبدیل تصویر به فرمت JPEG به همراه ساخت thumbnail
	/// </summary>
	/// <param name="data">داده خام تصویر (byte array)</param>
	/// <returns>تاپل شامل تصویر تبدیل شده و thumbnail آن</returns>
	public static (byte[] byteImage, byte[] byteThumbnail)
		ChangeFormatImageToJpegWithThumbnail(this byte[] data)
	{
		// بارگذاری تصویر از بایت‌ها
		using var image = SixLabors.ImageSharp.Image.Load(data);

		// تبدیل تصویر اصلی به JPEG
		using var saveImageStream = new MemoryStream();
		image.Save(saveImageStream, new JpegEncoder());

		// ساخت thumbnail با اندازه 1/3 تصویر
		int widthThumbnail = image.Width / 3;
		int heightThumbnail = image.Height / 3;

		using var thumbnail =
			image.Clone(ctx => ctx.Resize(widthThumbnail, heightThumbnail));
		
		using var saveThumbnailStream = new MemoryStream();
		
		thumbnail.Save(saveThumbnailStream, new JpegEncoder());

		// تبدیل به byte[]
		byte[] resultImage = saveImageStream.ToArray();
		byte[] resultThumbnail = saveThumbnailStream.ToArray();

		return (resultImage, resultThumbnail);
	}
	// **************************************************

	// **************************************************
	public static void MoveFile(string sourcePath, string destinationPath)
	{
		// بررسی وجود فایل در مسیر مبدا
		if (File.Exists(sourcePath))
		{
			// انتقال فایل به مسیر مقصد
			File.Move(sourcePath, destinationPath);
		}
	}
	// **************************************************

	// **************************************************
	public static async Task<Result<PackImage>> SaveIFormFileInDirAsync(
		this IFormFile file, string path,
		string domainName, bool thumbnail = false)
	{
		var result = new Result<PackImage>();
		
		string ext = Path.GetExtension(file.FileName).ToLower();

		List<string> listExtensionsThumnail = [".png", ".jpg", ".jpeg"];
		
		if (listExtensionsThumnail.Contains(ext) == false || thumbnail == false)
		{
			string fileName = domainName + "-" + Guid.NewGuid().ToString() + ext;

			using (FileStream fileStream =
			       new(Path.Combine(path, fileName), FileMode.Create))
			{
				await file.CopyToAsync(fileStream);

				result.WithValue(new PackImage(fileName, string.Empty));
			}
		}
		else // create image with thumbnail
		{
			byte[] imageBytes = file.IFormFileToByte();

			(byte[] byteImage, byte[] byteThumbnail) =
				imageBytes.ChangeFormatImageToJpegWithThumbnail();

			var guidNameFile = Guid.NewGuid().ToString();
			
			string fileName = domainName + "-" + guidNameFile + ext;
			string fileNameThumbnail = domainName + "-" + guidNameFile + "-thumbnail" + ext;
			
			Image<Rgba32> img = byteImage.ByteArrayToImage();
			string nameImg = img.SaveImageInDirectory(path, fileName: fileName);

			Image<Rgba32> imgTh = byteThumbnail.ByteArrayToImage();
			string nameImgTh = imgTh.SaveImageInDirectory(path, fileName: fileNameThumbnail);

			result.WithValue(new PackImage(nameImg, nameImgTh));
		}

		return result;
	}
	// **************************************************

	// **************************************************
	public static async Task<Result<string>> SaveBase64InDirAsync(
		string base64Data, string fileName, string path)
	{
		if (string.IsNullOrWhiteSpace(base64Data))
		{
			throw new ArgumentNullException(nameof(base64Data));
		}

		if (string.IsNullOrWhiteSpace(fileName))
		{
			throw new ArgumentNullException(nameof(fileName));
		}

		if (string.IsNullOrWhiteSpace(path))
		{
			throw new ArgumentNullException(nameof(path));
		}

		var result = new Result<string>();

		byte[] fileBytes;

		try
		{
			// تبدیل رشته Base64 به آرایه بایت
			fileBytes = Convert.FromBase64String(base64Data);
		}
		catch (FormatException)
		{
			return result.WithError("Invalid Base64 string.");
		}

		if (fileBytes.Length <= 0)
		{
			return result.WithError(Resources.Messages.ErroZiroByteFile);
		}

		string? ext = Path.GetExtension(fileName)?.ToLower();

		// اعتبارسنجی نوع فایل
		if (FileConstant.ExtensionsImages.Contains(ext))
		{
			if (fileBytes.Length > FileConstant.MaxSizeImage * 1024 * 1024)
			{
				return result.WithError(
					string.Format(Resources.Messages.ErrorSizeFile,
						Resources.DataDictionary.Image, FileConstant.MaxSizeImage));
			}
		}
		else if (FileConstant.ExtensionsPodcast.Contains(ext))
		{
			if (fileBytes.Length > FileConstant.MaxSizePodcast * 1024 * 1024)
			{
				return result.WithError(
					string.Format(Resources.Messages.ErrorSizeFile,
						Resources.DataDictionary.Podcast, FileConstant.MaxSizePodcast));
			}
		}
		else if (FileConstant.ExtensionsVideo.Contains(ext))
		{
			if (fileBytes.Length > FileConstant.MaxSizeVideo * 1024 * 1024)
			{
				return result.WithError(
					string.Format(Resources.Messages.ErrorSizeFile,
						Resources.DataDictionary.Video, FileConstant.MaxSizeVideo));
			}
		}
		else if (FileConstant.ExtensionsDocument.Contains(ext))
		{
			if (fileBytes.Length > FileConstant.MaxSizeDocument * 1024 * 1024)
			{
				return result.WithError(
					string.Format(Resources.Messages.ErrorSizeFile,
						Resources.DataDictionary.Document, FileConstant.MaxSizeDocument));
			}
		}
		else
		{
			return result.WithError("Invalid Base64 string.");
		}

		string newFileName = Guid.NewGuid().ToString() + ext;

		try
		{
			string fullPath = Path.Combine(path, newFileName);

			// ذخیره فایل در مسیر مشخص شده
			await File.WriteAllBytesAsync(fullPath, fileBytes);

			result = result.WithValue(newFileName);
		}
		catch (Exception ex)
		{
			return result.WithError($"Error saving file: {ex.Message}");
		}

		return result;
	}
	// **************************************************

	// **************************************************
	public static (string NameImage, string NameImageThumbnail)
		IFormFileImageToDirectoryWithThumbnail(this IFormFile file, string directoryPath)
	{
		byte[] imageBytes = file.IFormFileToByte();

		(byte[] byteImage, byte[] byteThumbnail) =
			imageBytes.ChangeFormatImageToJpegWithThumbnail();

		Image<Rgba32> img = byteImage.ByteArrayToImage();
		string nameImg = img.SaveImageInDirectory(directoryPath);

		Image<Rgba32> imgTh = byteThumbnail.ByteArrayToImage();
		string nameImgTh = imgTh.SaveImageInDirectory(directoryPath);

		return (nameImg, nameImgTh);
	}
	// **************************************************

	// **************************************************
	public static IFormFile ConvertByteArrayToFormFile(
		this byte[] fileBytes,
		string fileName,
		string contentType = "application/octet-stream")
	{
		var stream = new MemoryStream(fileBytes);

		// توجه داشته باشید که کلاس FormFile همچنان در فضای نام Microsoft.AspNetCore.Http وجود دارد
		var result = new FormFile(stream, 0, fileBytes.Length, "file", fileName)
		{
			Headers = new HeaderDictionary(),
			ContentType = contentType
		};

		return result;
	}
	// **************************************************
}

public record PackImage(string FileName, string FileThumbnailName);