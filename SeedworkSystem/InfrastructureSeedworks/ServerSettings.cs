namespace InfrastructureSeedworks;
public class ServerSettings : object
{
	[System.Text.Json.Serialization.JsonIgnore]
	public static string DomainApiProjectManager { get; set; } = "";

	[System.Text.Json.Serialization.JsonIgnore]
	public static string DomainApiAttachmentManager { get; set; } = "";
	
	[System.Text.Json.Serialization.JsonIgnore]
	public static string DomainApiMarketPlace { get; set; } = "";
	
	[System.Text.Json.Serialization.JsonIgnore]
	public static string DomainAdmin { get; set; } = "";
	
	[System.Text.Json.Serialization.JsonIgnore]
	public static string DomainWeb { get; set; } = "";

	[System.Text.Json.Serialization.JsonIgnore]
	public static string DomainGoldApi { get; set; } = "";
	
	[System.Text.Json.Serialization.JsonIgnore]
	public static string UploadsFolderName { get; } = "uploads";
	
	[System.Text.Json.Serialization.JsonIgnore]
	public static string DeletedFolderName { get; } = "deleted";
}