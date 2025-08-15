using Microsoft.AspNetCore.Http;

namespace LawConfigs;

public static class LawsFiles
{
	static LawsFiles()
	{
		InitialLaws();
	}

	private static Dictionary<string, Dictionary<string, List<Law>>> Laws { get; set; } = new();

	/// <summary>
	/// find in laws list law by server id and domain name from resources!
	/// </summary>
	/// <param name="serverId">guid</param>
	/// <param name="domainName">domin</param>
	/// <returns></returns>
	public static List<Law> FindLawsByServerIdAndDomainName(string serverId, string domainName)
	{
		var server = Laws.GetValueOrDefault(serverId);

		if (server is null)
		{
			return new List<Law>();
		}

		var result = server.GetValueOrDefault(domainName);

		return result ?? new List<Law>();
	}

	public static ExtensionType GetExtensionType(string extension)
	{
		extension = extension.ToLowerInvariant();

		return extension switch
		{
			".png" or ".svg" or ".jpg" or ".jpeg" or ".gif" => ExtensionType.Image,
			".docx" or ".pdf" => ExtensionType.Document,
			".mp4" => ExtensionType.Video,
			".mp3" => ExtensionType.Music,
			_ => ExtensionType.NotSupported,
		};
	}

	public static FluentResults.Result<ExtensionType> FileValidate(IFormFile file, string serverId, string domainName)
	{
		var result = new FluentResults.Result<ExtensionType>();

		var laws = FindLawsByServerIdAndDomainName(serverId, domainName);

		if (laws.Any() == true)
		{
			if (file.Length <= 0)
			{
				result.WithError(Resources.Messages.FileEmpty);
			}

			var ext = Path.GetExtension(file.FileName);

			if (string.IsNullOrEmpty(ext) == true || ext.StartsWith('.') == false)
			{
				result.WithError(Resources.Messages.ExtensionFileNotFound);
			}

			var extentionType = GetExtensionType(ext);

			var law = laws.FirstOrDefault(x => x.ExtensionType == extentionType);

			if (law is null)
			{
				result.WithError(Resources.Messages.ExtensionFileNotSupported);
			}
			else
			{
				result.WithValue(extentionType);

				if (law.MaxVolumeMb < file.Length || law.MinVolumeMb > file.Length)
				{
					var errorMessage = string.Format(
						Resources.Messages.ErrorSizeFile,
						file.FileName, law.MaxVolumeMb.ToMb());

					result.WithError(errorMessage);
				}
			}
		}
		else
		{
			// قوانین آپلود فایل برای این مدل در سرور مورد نظر ثبت نشده است
			result.WithError(Resources.Messages.LawNotFoundFileError);
		}

		return result;
	}

	private static long ToMb(this long value)
	{
		return value * 1024 * 1024;
	}

	private static void InitialLaws()
	{
		Laws = new Dictionary<string, Dictionary<string, List<Law>>>()
		{
			// **************************************************
			{
				// set laws for marketplace project.
				"C620E381-9CDE-4A6F-90E3-ACD03D2128BA",
				new Dictionary<string, List<Law>>()
				{
					{
						nameof(Resources.DataDictionary.Category),
						[
							new Law(maxVolumeMb: 1),
						]
					},
					{
						nameof(Resources.DataDictionary.Product),
						[
							new Law(),
							new Law(ExtensionType.Document),
						]
					},
					{
						nameof(Resources.DataDictionary.PageSetting),
						[
							new Law(maxVolumeMb: 1),
						]
					},
					{
						nameof(Resources.DataDictionary.Shop),
						[
							new Law(maxVolumeMb: 4)
						]
					},
					{
						nameof(Resources.DataDictionary.Branch),
						[
							new Law(maxVolumeMb: 5)
						]
					}
				}
			},
			// **************************************************
		};
	}
}