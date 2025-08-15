using Utilities;
using LawConfigs;
using AutoMapper;
using HttpServices.ProjectManager;
using Persistence;
using InfrastructureSeedworks;
using Microsoft.AspNetCore.Mvc;
using RestFullApi.Infrastructure;
using ViewModels.AttachmentManager;
using PersistenceSeedworks.LogManager;

namespace RestFullApi.Controllers;

/// <summary>
/// مدیریت تصاویر دکویاب
/// </summary>
public class AttachmentController : BaseAttachmentManagerController
{
	#region DI Settings & Constructor

	public AttachmentController(
		IMapper mapper, HttpClient httpClient, IConfiguration configuration,
		IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork,
		ILogDetailManager logDetailManager, ILogServerManager logServerManager,
		IWebHostEnvironment environment)
		: base(mapper, httpClient, configuration,
			httpContextAccessor, unitOfWork, logDetailManager, logServerManager)
	{
		Environment = environment;
	}

	public IWebHostEnvironment Environment { get; }

	#endregion

	#region Upload File On Server For PageSettigs

	/// <summary>
	/// upload attachments
	/// </summary>
	/// <param name="serverId">server id for save file in directory</param>
	/// <param name="file">file with IFormFile Type</param>
	/// <param name="domainName">image for this domain: nameof(Category), nameof(Product), ...</param>
	/// <returns></returns>
	[HttpPost(template: "{serverId}/{domainName}")]
	public async Task<IActionResult> UploadAsync(IFormFile? file, string serverId, string domainName)
	{
		var result = new FluentResults.Result<FileUploadOnServerResult>();

		if (string.IsNullOrEmpty(domainName) == true)
		{
			result.WithError(Resources.Messages.EmptySection);
		}

		if (string.IsNullOrEmpty(serverId) == true)
		{
			var errorMessage =
				string.Format(Resources.Messages.RequiredError, Resources.DataDictionary.ServerId);

			result.WithError(errorMessage);
		}
		
		// check server by server service: HTTPS-REQUEST
		SubSystemService serverService =
			new HttpServices.ProjectManager.SubSystemService();

		var resultCheckServer =
			await serverService.CheckServerAsync(serverId, domainName);

		if (resultCheckServer.IsFailed == true)
		{
			result.WithErrors(resultCheckServer.Errors);
		}

		if (file is null)
		{
			var errorMessage =
				string.Format(Resources.Messages.RequiredError, Resources.DataDictionary.File);

			result.WithError(errorMessage);
		}

		if (result.IsSuccess == true)
		{
			var resultValidate =
				LawsFiles.FileValidate(file!, serverId, domainName);

			if (resultValidate.IsFailed == true)
			{
				result.WithErrors(resultValidate.Errors);
			}
			else
			{
				var path = Path.Combine
					(Environment.WebRootPath, ServerSettings.UploadsFolderName, serverId);

				if (Directory.Exists(path) == false)
				{
					Directory.CreateDirectory(path);
				}

				FluentResults.Result<PackImage>
					saveFileResult = await file!.SaveIFormFileInDirAsync(
						path, domainName, thumbnail: resultValidate.Value == ExtensionType.Image);

				if (saveFileResult.IsFailed == true)
				{
					result.WithErrors(saveFileResult.Errors);
				}

				if (saveFileResult.IsSuccess == true)
				{
					var resultValue = new
						FileUploadOnServerResult(
							file!.FileName,
							saveFileResult.Value.FileName,
							saveFileResult.Value.FileThumbnailName);

					result.WithValue(resultValue);

					var successMessage =
						string.Format(Resources.Messages.CreateSuccessMessage, Resources.DataDictionary.File);
					
					result.WithSuccess(successMessage);
				}
			}
		}

		return FluentResult(result);
	}
	
	/// <summary>
	/// delete file from attachment server by file name and serverId
	/// </summary>
	/// <param name="fileName">نام فایل</param>
	/// <param name="serverId">شناسه سرور</param>
	/// <returns></returns>
	[HttpDelete(template: "{serverId}/{fileName}")]
	public async Task<IActionResult> Delete(string fileName, string serverId)
	{
		var result = new FluentResults.Result<FileUploadOnServerResult>();

		if (string.IsNullOrEmpty(serverId) == true)
		{
			var errorMessage = string.Format
				(Resources.Messages.RequiredError, Resources.DataDictionary.ServerId);
			
			result.WithError(errorMessage);

			return FluentResult(result);
		}
		
		// check server by server service: HTTPS-REQUEST
		var serverService =
			new HttpServices.ProjectManager.ServerService();

		var resultCheckServer = await serverService.CheckServerAsync(serverId);

		if (resultCheckServer.IsFailed == true)
		{
			result.WithErrors(resultCheckServer.Errors);
		}
		
		if (string.IsNullOrEmpty(fileName) == true)
		{
			result.WithError(Resources.Messages.FileEmpty);
			
			return FluentResult(result);
		}

		var ext = Path.GetExtension(fileName).ToLower();

		List<string> listExtensionsThumbnail = [".png", ".jpg", ".jpeg"];

		 string fileNameOrginal = "";
		 string fileNameTumbnail = "";
		 
		if (listExtensionsThumbnail.Contains(ext) == true)
		{
			if (fileName.Contains("-thumbnail") == true)
			{
				fileNameTumbnail = fileName;
				fileNameOrginal = fileName.Replace("-thumbnail", "");
			}
			else
			{
				fileNameOrginal = fileName;
				fileNameTumbnail = fileName.Replace(ext, $"-thumbnail{ext}");
			}

			CreateIfNotExistServerFolder(serverId);
			
			MoveFilesToDeletedFolder(serverId, fileNameTumbnail, fileNameOrginal);
		}
		else
		{
			CreateIfNotExistServerFolder(serverId);

			MoveFileToDeletedFolder(fileName, serverId);
		}

		if (result.IsSuccess == true)
		{
			var successMessage = 
				string.Format(Resources.Messages.DeleteMessageSuccess, Resources.DataDictionary.File);
			
			result.WithSuccess(successMessage);

			var value =
				new FileUploadOnServerResult(string.Empty, fileNameOrginal, fileNameTumbnail);
			
			result.WithValue(value);
		}
		
		return FluentResult(result);
	}

	private void MoveFileToDeletedFolder(string fileName, string serverId)
	{
		var pathFile = Path.Combine(
			Environment.WebRootPath,
			ServerSettings.UploadsFolderName, serverId, fileName);

		var pathDeleted = Path.Combine(
			Environment.WebRootPath,
			ServerSettings.DeletedFolderName, serverId, fileName);

		if (System.IO.File.Exists(pathFile) == true)
		{
			// move file from uploads to deleted folder
			FileTools.MoveFile(pathFile, pathDeleted);
		}
	}

	private void MoveFilesToDeletedFolder(string serverId, string fileNameTumbnail, string fileNameOrginal)
	{
		// move thumbnail file!
		var pathFileThumbnail = Path.Combine(
			Environment.WebRootPath,
			ServerSettings.UploadsFolderName, serverId, fileNameTumbnail);
			
		var pathDeletedThumbnail = Path.Combine(
			Environment.WebRootPath,
			ServerSettings.DeletedFolderName, serverId, fileNameTumbnail);

		if (System.IO.File.Exists(pathFileThumbnail) == true)
		{
			// move file from uploads to deleted folder
			FileTools.MoveFile(pathFileThumbnail, pathDeletedThumbnail);
		}
			
		// move files!
		var pathFileOrginal = Path.Combine(
			Environment.WebRootPath,
			ServerSettings.UploadsFolderName, serverId, fileNameOrginal);

		var pathDeletedOriginal = Path.Combine(
			Environment.WebRootPath,
			ServerSettings.DeletedFolderName, serverId, fileNameOrginal);

		if (System.IO.File.Exists(pathFileOrginal) == true)
		{
			// move file from uploads to deleted folder
			FileTools.MoveFile(pathFileOrginal, pathDeletedOriginal);
		}
	}

	private void CreateIfNotExistServerFolder(string serverId)
	{
		var deleteDirectory = Path.Combine(
			Environment.WebRootPath,
			ServerSettings.DeletedFolderName, serverId);

		if (Directory.Exists(deleteDirectory) == false)
		{
			Directory.CreateDirectory(deleteDirectory);
		}
	}

	#endregion
}