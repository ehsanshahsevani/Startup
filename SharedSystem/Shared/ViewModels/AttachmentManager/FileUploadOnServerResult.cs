namespace ViewModels.AttachmentManager;

public class FileUploadOnServerResult : object
{
	public FileUploadOnServerResult(string fileOriginalName, string fileName, string fileThumbnailName) : base()
	{
		FileOriginalName = fileOriginalName;
		
		FileName = fileName;
		FileThumbnailName = fileThumbnailName;
	}

	public string? FileOriginalName { get; set; }

	public string? FileName { get; set; }
	public string? FileThumbnailName { get; set; }
}