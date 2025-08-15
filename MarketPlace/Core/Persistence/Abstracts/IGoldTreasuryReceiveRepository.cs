using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface IGoldTreasuryReceiveRepository : IRepository<GoldTreasuryReceive>
{
	/// <summary>
	/// دریافت رکورد موجود در سیستم
	/// </summary>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	Task<GoldTreasuryReceive?>
		FindFirstRecordAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// گرفتن اولین رکورد با اولین مقدار موجود در دیتابیس
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<decimal> FindFirstAmountAsync
		(CancellationToken cancellationToken = default);
}