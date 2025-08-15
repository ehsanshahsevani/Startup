using BaseProject.Model.ViewModel.Public;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class TypeRoleMoneyRepository : Repository<TypeRoleMoney>, ITypeRoleMoneyRepository
{
	internal TypeRoleMoneyRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
	
	public async Task<List<UiSelectModel>> GetSelectValues(CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			.Where(p => p.IsDeleted == false)
			.Select(p => new UiSelectModel(p.Name, p.Id))
			.ToListAsync(cancellationToken);

		return result;
	}
}