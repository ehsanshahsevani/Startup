using InfrastructureSeedworks;
using SampleResult;

namespace HttpServices.ProjectManager;

/// <summary>
/// سرویسی جهت اتصال و مدیریت درخواست های بخش سرورها
/// </summary>
public class ServerService : HttpServiceSeedworks.HttpService
{
	public ServerService() : base(ServerSettings.DomainApiProjectManager)
	{
		SetBaseApi(nameof(Resources.DataDictionary.Server));
	}

	#region GET: /check-server

	/// <summary>
	/// بررسی وجود یک سرور
	/// </summary>
	/// <param name="serverKey">شناسه سرور</param>
	/// <returns></returns>
	public async Task<Result> CheckServerAsync(string serverKey)
	{
		var url = $"check-server?{nameof(serverKey)}={serverKey}";

		var result = await base.GetAsync<Result>(url);
		
		return result;
	}

	#endregion
}