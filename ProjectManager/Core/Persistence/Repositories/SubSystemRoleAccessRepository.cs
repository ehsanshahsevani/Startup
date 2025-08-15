using Domain;
using Persistence.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class SubSystemRoleAccessRepository : Repository<SubSystemRoleAccess>, ISubSystemRoleAccessRepository
{
	internal SubSystemRoleAccessRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}

	/// <summary>
	/// check repeat and deleted data from actions table and delete data from this table! 
	/// </summary>
	/// <param name="cancellationToken"></param>
	public async Task CheckAllPermissionsAsync(CancellationToken cancellationToken = default)
	{
		IActionRepository actionRepository =
			new ActionRepository(DatabaseContext);

		var allPermisions = await DbSet
			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.ToListAsync(cancellationToken);

		var listActionCodes =
			allPermisions.Where(x => x.ActionCode is not null)
				.Select(current => current.ActionCode)
				.ToList();

		var actions = await actionRepository
			.FindByActionCodesAsync(listActionCodes!, cancellationToken);

		foreach (var systemRoleAccess in allPermisions.Where(x => x.ActionCode is not null))
		{
			var isExist = actions.Any(x => x.ActionCode == systemRoleAccess.ActionCode);

			if (isExist == false)
			{
				await base.RemoveByIdAsync(systemRoleAccess.Id, cancellationToken);
			}
		}

		await DatabaseContext.SaveChangesAsync(cancellationToken);
	}
}