using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface IProductBranchRepository : IRepository<ProductBranch>
{
	/// <summary>
	/// delete all product branches in this product
	/// </summary>
	/// <param name="productId"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task RemoveByProductIdAsync(string productId, CancellationToken cancellationToken = default);
}