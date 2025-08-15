namespace LawConfigs;

public class Law : object
{
	public ExtensionType ExtensionType { get; set; }

	/// <summary>
	/// number to mb
	/// </summary>
	/// <param name="extensionType">image, document, min value, max value</param>
	/// <param name="minVolumeMb">mb * 1024 * 1024</param>
	/// <param name="maxVolumeMb">mb * 1024 * 1024</param>
	public Law(ExtensionType extensionType = ExtensionType.Image, float minVolumeMb = 0.0f,
		float maxVolumeMb = 10) : base()
	{
		MinVolumeMb = (long)(minVolumeMb * 1024 * 1024);
		MaxVolumeMb = (long)(maxVolumeMb * 1024 * 1024);

		_extensions = new List<string>();

		ExtensionType = extensionType;

		_extensions = GetExtensions(extensionType);
	}

	private List<string> _extensions;

	public long MinVolumeMb { get; set; }
	public long MaxVolumeMb { get; set; }

	private static List<string> GetExtensions(ExtensionType extensionType)
	{
		return extensionType switch
		{
			ExtensionType.Image => [".png", ".svg", ".jpg", ".jpeg", ".gif"],
			ExtensionType.Document => [".docx", ".pdf"],
			ExtensionType.Video => [".mp4"],
			ExtensionType.Music => [".mp3"],
			ExtensionType.NotSupported => [],
			_ => [".png", ".svg", ".jpg", ".jpeg", ".gif"]
		};
	}
}