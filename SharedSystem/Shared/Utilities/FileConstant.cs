namespace Utilities;

public static class FileConstant
{
	/// <summary>
	/// حداکثر سایز تصاویر
	/// </summary>
	public static int MaxSizeImage { get; } = 10;

	/// <summary>
	/// حداکثر سایز اسناد
	/// </summary>
	public static int MaxSizeDocument { get; } = 20;

	/// <summary>
	/// حداکثر سایز ویدیوهای سایت
	/// </summary>
	public static int MaxSizeVideo { get; } = 200;

	/// <summary>
	/// حداکثر سایز پادکست ها سایت
	/// </summary>
	public static int MaxSizePodcast { get; } = 20;

	/// <summary>
	/// حداکثر سایز فایل اپ ها سایت
	/// </summary>
	public static int MaxSizeApp { get; } = 40;

	/// <summary>
	/// پسوند تصاویر
	/// </summary>
	public static List<string> ExtensionsImages
	{
		get
		{
			return new List<string>() { ".png", ".jpg", ".jpeg", ".gif", ".bmp" };
		}
	}

	/// <summary>
	/// پسوند فایل ها صوتی
	/// </summary>
	public static List<string> ExtensionsVideo
	{
		get
		{
			return new List<string>() { ".mp4", ".mvk" };
		}
	}


	/// <summary>
	/// پسوند ویدیو ها
	/// </summary>
	public static List<string> ExtensionsPodcast
	{
		get
		{
			return new List<string>() { ".mp3" };
		}
	}

	/// <summary>
	/// پسوند اسناد
	/// </summary>
	public static List<string> ExtensionsDocument
	{
		get
		{
			return new List<string> { ".pptx", ".pdf", ".docx" };
		}
	}

	/// <summary>
	/// پسوند اسناد
	/// </summary>
	public static List<string> ExtensionsApp
	{
		get
		{
			return new List<string> { ".apk" };
		}
	}
}
