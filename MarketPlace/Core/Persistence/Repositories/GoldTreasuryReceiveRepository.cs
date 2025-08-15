using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class GoldTreasuryReceiveRepository : Repository<GoldTreasuryReceive>, IGoldTreasuryReceiveRepository
{
	internal GoldTreasuryReceiveRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
	
	/// <summary>
	/// دریافت رکورد موجود در سیستم
	/// </summary>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public async Task<GoldTreasuryReceive?>
		FindFirstRecordAsync(CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			.FirstOrDefaultAsync(cancellationToken);
		
		return result;
	}
	
	/// <summary>
	/// گرفتن اولین رکورد با اولین مقدار موجود در دیتابیس
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public async Task<decimal> FindFirstAmountAsync
		(CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			.OrderByDescending(current => current.CreateDateTime)
			.Select(current => current.Amount)
			.FirstAsync(cancellationToken);
		
		return result;
	}
}