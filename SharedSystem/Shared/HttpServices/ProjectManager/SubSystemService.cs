using Enums.SharedService;
using InfrastructureSeedworks;
using SampleResult;
using ViewModels.ProjectManager;

namespace HttpServices.ProjectManager;

public class SubSystemService : HttpServiceSeedworks.HttpService
{
	public SubSystemService() : base(ServerSettings.DomainApiProjectManager)
	{
		SetBaseApi(nameof(Resources.DataDictionary.SubSystem));
	}

	#region POST: /add-range/serverid?projecttype=projectType

	/// <summary>
	/// ذخیره و ثبت لیستی از دامین های هر پروژه به صورت اتوماتیک
	/// توسط این سرویس اجرا و اعمال میشود
	/// بعد از تایید و ثبت نهایی این سرویس پروژه اجازه ران شدن دارند
	/// </summary>
	/// <param name="model">لیست زیر سیستم های مربوط به سرور</param>
	/// <param name="projectType">پروژه مورد نظر</param>
	/// <param name="serverId">شناسه سرور - کلید</param>
	/// <returns></returns>
	public async Task<Result?> AddAsync(List<SubSystemResponseViewModel> model, ProjectType projectType, string serverId)
	{
		var url = $"add-range/{serverId}?projectType={projectType}";

		var result =
			await base.PostAsync<List<SubSystemResponseViewModel>, Result>(url, data: model);

		return result;
	}

	#endregion

	#region GET: /check-server

	/// <summary>
	/// بررسی یک سرور درحال استفاده روی سروری دیگر
	/// سرور میزبان باید با استفاده از این سرویس بررسی کند که آیا سرور مهمان
	/// وجود خارجی دارد و معتبر است یا خیر
	/// </summary>
	/// <param name="serverId">شناسه سرور</param>
	/// <param name="domainName">دامین درحال اجرا</param>
	/// <returns></returns>
	public async Task<Result> CheckServerAsync(string serverId, string domainName)
	{
		var url = $"check-server?{nameof(serverId)}={serverId}&{nameof(domainName)}={domainName}";
		
		var result = await base.GetAsync<Result>(route: url);
		
		return result;
	}

	#endregion
}