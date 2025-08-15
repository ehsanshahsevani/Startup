using SampleResult;
using Enums.SharedService;
using InfrastructureSeedworks;
using Microsoft.AspNetCore.Http;
using ViewModels.AttachmentManager;

namespace HttpServices.AttachmentManager;

/// <summary>
/// مدیریت فایل ها
/// </summary>
public class AttachmentService : HttpServiceSeedworks.HttpService
{
	public AttachmentService() : base(ServerSettings.DomainApiAttachmentManager)
	{
		base.SetBaseApi(nameof(Resources.DataDictionary.Attachment));
	}

	#region POST: "{serverId}/{domainName}"

	/// <summary>
	/// آپلود تصویر روی سرورهای تصویر
	/// </summary>
	/// <param name="file">فایل</param>
	/// <param name="serverId">شناسه سرور</param>
	/// <param name="domainName">نام بخشی که تصویر به آن ربط دارد</param>
	/// <returns></returns>
	public async
		Task<Result<FileUploadOnServerResult>>
		UploadAsync(IFormFile file, string serverId, string domainName)
	{
		string url = $"{serverId}/{domainName}";
		
		var files = new Dictionary<string, IFormFile>
		{
			{ "file", file } // کلید باید با نام پارامتر سمت API یکی باشه
		};
		
		var result =
			await base.PostAsync
				<object, Result<FileUploadOnServerResult>>
				(url, data: new {}, contentType: ContentType.MultipartFormData, fileDict: files);

		return result!;
	}

	#endregion

	#region DELETE: "{serverId}/{fileName}"

	/// <summary>
	/// حذف فایل های مورد نظر
	/// </summary>
	/// <param name="fileName">نام فایل</param>
	/// <param name="serverId">شناسه سرور</param>
	/// <returns></returns>
	public async
		Task<Result<FileUploadOnServerResult>>
		DeleteAsync(string fileName, string serverId)
	{
		string url = $"{serverId}/{fileName}";
		
		var result =
			await base.DeleteAsync
				<Result<FileUploadOnServerResult>>(url);

		return result;
	}

	#endregion
}