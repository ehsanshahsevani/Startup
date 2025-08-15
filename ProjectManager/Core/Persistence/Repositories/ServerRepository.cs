using Domain;
using Enums.SharedService;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class ServerRepository : Repository<Server>, IServerRepository
{
	internal ServerRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}

	/// <summary>
	/// بررسی وجود یک سرور با آیدی
	/// یک سرور نمیتواند بیشتر از یک بار تکرار شود
	/// </summary>
	/// <param name="serverId"></param>
	/// <param name="projectType"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public async Task<Server?> GetByServerIdAndProjectTypeAsync(string serverId, ProjectType projectType, CancellationToken cancellationToken = default)
	{
		var result = await DbSet

			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.Where(current => current.Id == serverId)
			.Where(current => current.ServerKey == serverId)
			.Where(current => current.ProjectType == projectType)

			.FirstOrDefaultAsync(cancellationToken);
		
		return result;
	}

	/// <summary>
	/// بررسی یک انتیتی با کلید در فیلد های شناسه اصلی و کلید سرور
	/// </summary>
	/// <param name="id">شناسه سرور</param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public override async Task<Server?> FindAsync(object id, CancellationToken cancellationToken = default)
	{
		var result = await DbSet

			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.Where(current => current.Id == id.ToString())
			.Where(current => current.ServerKey == id.ToString())

			.FirstOrDefaultAsync(cancellationToken);
		
		return result;
	}
}