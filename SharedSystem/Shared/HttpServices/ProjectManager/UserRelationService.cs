using SampleResult;
using InfrastructureSeedworks;
using ViewModels.ProjectManager;

namespace HttpServices.ProjectManager;

public class UserRelationService : HttpServiceSeedworks.HttpService
{
	public UserRelationService() : base(ServerSettings.DomainApiProjectManager)
	{
		base.SetBaseApi(nameof(Resources.DataDictionary.UserRelation));
	}

	#region POST: /add

	/// <summary>
	/// ثبت یک ارتباط موقف برای کاربری که در پروژه های دیگر برایشان یک ریلیشن ایجاد شده است
	/// </summary>
	/// <param name="model.serverId">شناسه سرور</param>
	/// <param name="model.subSystemId">شناسه زیر سیستم</param>
	/// <param name="model.userId">شناسه کاربر</param>
	/// <param name="model.relationId">شناسه مربوط به زیر سیستم اعلام شده در سرور اعلام شده</param>
	/// <param name="model"></param>
	/// <returns></returns>
	public async Task<Result> AddAsync(UserRelationResponseViewModel model)
	{
		var url = $"add";

		var result =
			await base.PostAsync<UserRelationResponseViewModel, Result>(url, data: model);

		return result!;
	}

	#endregion

	#region POST: /save-changes

	/// <summary>
	/// نهایی کردن ارتباطات مربوط به کاربران
	/// جدول تمپ ارتباطات خالی میشود و به ارتباطات اصلی کاربران انتقال پیدا میکنند
	/// این روابط کمک میکند تا مدیریت کاربران بسیار راحت و دقیق شود
	/// </summary>
	/// <param name="serverId">شناسه سرور مورد نظر</param>
	/// <returns></returns>
	public async Task<Result?> SaveChangesAsync(string serverId)
	{
		var url = $"save-changes/{serverId}";

		var result =
			await base.PostAsync
				<object?, Result>(url, data: null);

		return result;
	}

	#endregion
}