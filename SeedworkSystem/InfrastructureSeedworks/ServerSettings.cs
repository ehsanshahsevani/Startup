namespace InfrastructureSeedworks;
public class ServerSettings : object
{
	[System.Text.Json.Serialization.JsonIgnore]
	public static string DomainApiProjectManager { get; set; } = "https://ToolsA.decoyab.com";

	[System.Text.Json.Serialization.JsonIgnore]
	public static string DomainApiAttachmentManager { get; set; } = "https://ToolsB.decoyab.com";
	
	[System.Text.Json.Serialization.JsonIgnore]
	public static string DomainApiMarketPlace { get; set; } = "https://MarketplaceApi.decoyab.com";
	
	[System.Text.Json.Serialization.JsonIgnore]
	public static string DomainAdmin { get; set; } = "https://admin.decoyab.com";

	[System.Text.Json.Serialization.JsonIgnore]
	public static string DomainWeb { get; set; } = "https://decoyab.com";

	[System.Text.Json.Serialization.JsonIgnore]
	public static string DomainGoldApi { get; set; } = "https://goldapi.decoyab.com";
	
	[System.Text.Json.Serialization.JsonIgnore]
	public static string UploadsFolderName { get; } = "uploads";
	
	[System.Text.Json.Serialization.JsonIgnore]
	public static string DeletedFolderName { get; } = "deleted";
}

// namespace InfrastructureSeedworks;
//
// public class ServerSettings : object
// {
// 	[System.Text.Json.Serialization.JsonIgnore] 
// 	public static string DomainApiProjectManager { get; set; } = "https://localhost:6061";
//
// 	[System.Text.Json.Serialization.JsonIgnore]
// 	public static string DomainApiAttachmentManager { get; set; } = "https://localhost:9091";
// 	
// 	[System.Text.Json.Serialization.JsonIgnore]
// 	public static string DomainApiMarketPlace { get; set; } = "https://localhost:5051";
// 	
// 	[System.Text.Json.Serialization.JsonIgnore]
// 	public static string DomainAdmin { get; set; } = "https://admin.decoyab.com";
//
// 	[System.Text.Json.Serialization.JsonIgnore]
// 	public static string DomainWeb { get; set; } = "https://decoyab.com";
//
// 	[System.Text.Json.Serialization.JsonIgnore]
// 	public static string UploadsFolderName { get; } = "uploads";
// 	
// 	[System.Text.Json.Serialization.JsonIgnore]
// 	public static string DeletedFolderName { get; } = "deleted";
// }