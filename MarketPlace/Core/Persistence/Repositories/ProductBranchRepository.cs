using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class ProductBranchRepository : Repository<ProductBranch>, IProductBranchRepository
{
	internal ProductBranchRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}

	public async Task RemoveByProductIdAsync(string productid, CancellationToken cancellationToken = default)
	{
		var result =
			await DbSet
				.Where(current => current.IsDeleted == false)
				.Where(current => current.IsActive == true)
				.Where(x => x.ProductId == productid)
				.ToListAsync(cancellationToken);
		
		await RemoveRangeAsync(result, cancellationToken);
	}
}