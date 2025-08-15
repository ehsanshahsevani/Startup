using Enums.SharedService;
using InfrastructureSeedworks;
using SampleResult;
using ViewModels.ProjectManager;
using ViewModels.Shared;

namespace HttpServices.ProjectManager;

public class ActionService : HttpServiceSeedworks.HttpService
{
	public ActionService() : base(ServerSettings.DomainApiProjectManager)
	{
		SetBaseApi(nameof(Resources.DataDictionary.Action));
	}

	#region POST: /add-range/serverid?projecttype=projectType

	/// <summary>
	/// ذخیره و ثبت لیستی از اکشن های هر پروژه به صورت اتوماتیک
	/// توسط این سرویس اجرا و اعمال میشود
	/// بعد از تایید و ثبت نهایی این سرویس پروژه ها اجازه ران شدن دارند
	/// </summary>
	/// <param name="model">لیست اکشن های مربوط به سرور</param>
	/// <param name="projectType">پروژه مورد نظر</param>
	/// <param name="serverId">شناسه سرور - کلید</param>
	/// <returns></returns>
	public async Task<Result?> AddAsync(List<ActionViewModel> model, ProjectType projectType, string serverId)
	{
		var url = $"add-range/{serverId}?projectType={projectType}";

		var result =
			await base.PostAsync<List<ActionViewModel>, Result>(url, data: model);

		return result;
	}

	#endregion
}