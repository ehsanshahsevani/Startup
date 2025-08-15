using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface IIncomeSaleOfGoldFeeRepository : IRepository<IncomeSaleOfGoldFee>
{
	/// <summary>
	/// Retrieves the income value from the database.
	/// </summary>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>The income sale of gold fee as a decimal.</returns>
	Task<decimal> FindIncomeAmountAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves the income entity from the database.
	/// </summary>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>The income sale of gold fee as a decimal.</returns>
	Task<IncomeSaleOfGoldFee?> FindIncomeAsync(CancellationToken cancellationToken = default);
}