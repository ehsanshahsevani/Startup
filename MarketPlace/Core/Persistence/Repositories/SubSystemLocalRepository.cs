using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class SubSystemLocalRepository : Repository<SubSystemLocal>, ISubSystemLocalRepository
{
	internal SubSystemLocalRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}

	public async Task AddByNamesAsync(List<string> domains, CancellationToken cancellationToken = default)
	{
		List<string> listExisted = await DbSet
			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.Where(current => domains.Contains(current.NameEN) == true)
			.Select(current => current.NameEN)
			.ToListAsync(cancellationToken: cancellationToken);

		var domainsToAdd =
			domains.Where(d => listExisted.Contains(d) == false).ToList();

		List<SubSystemLocal> list = new();

		foreach (var subSystem in domainsToAdd)
		{
			var subSystemLocal = new SubSystemLocal()
			{
				IsActive = true,
				NameEN = subSystem,
				NameFA = subSystem,
			};

			list.Add(subSystemLocal);
		}

		await AddRangeAsync(list, cancellationToken);
	}

	public async Task<SubSystemLocal?> FindByNameAsync(string domain, CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.FirstOrDefaultAsync(p => p.NameEN == domain, cancellationToken: cancellationToken);
		return result;
	}

	public override async Task<IEnumerable<SubSystemLocal?>>
		GetAllAsync(CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.ToListAsync(cancellationToken);

		return result;
	}

	public async Task<string?> FindDescriptionBySubSystemNameAsync(string subSystemName,
		CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.Where(current => current.NameEN == subSystemName)
			.Select(current => current.Description)
			.FirstOrDefaultAsync(cancellationToken);

		return result;
	}

	public async Task<SubSystemLocal?> UpdateDescriptionByIdAsync(string id, string description,
		CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.Where(current => current.Id == id)
			.FirstOrDefaultAsync(cancellationToken);

		if (result is not null)
		{
			result.Description = description;
			result.UpdateDateTime = DateTime.Now;
		}

		return result;
	}
}