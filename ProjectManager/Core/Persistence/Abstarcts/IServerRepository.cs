using Domain;
using Enums.SharedService;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface IServerRepository : IRepository<Server>
{
	/// <summary>
	/// بررسی وجود یک سرور با آیدی
	/// یک سرور نمیتواند بیشتر از یک بار تکرار شود
	/// </summary>
	/// <param name="serverId"></param>
	/// <param name="projectType"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<Server?> GetByServerIdAndProjectTypeAsync(string serverId, ProjectType projectType,
		CancellationToken cancellationToken = default);
}