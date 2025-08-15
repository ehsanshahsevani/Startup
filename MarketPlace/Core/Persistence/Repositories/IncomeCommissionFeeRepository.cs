using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class IncomeCommissionFeeRepository : Repository<IncomeCommissionFee>, IIncomeCommissionFeeRepository
{
	internal IncomeCommissionFeeRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
	
	/// <summary>
	/// Retrieves the income value from the database.
	/// </summary>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>The income sale of gold fee as a decimal.</returns>
	public async Task<decimal> FindIncomeAmountAsync(CancellationToken cancellationToken = default)
	{
		var fee = await DbSet
			.Select(x => x.Amount)
			.FirstAsync(cancellationToken);

		return fee;
	}
	
	/// <summary>
	/// Retrieves the income entity from the database.
	/// </summary>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>The income sale of gold fee as a decimal.</returns>
	public async Task<IncomeCommissionFee?> FindIncomeAsync(CancellationToken cancellationToken = default)
	{
		var fee = await DbSet
			.FirstOrDefaultAsync(cancellationToken);

		return fee;
	}
}