using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface IAccountCodingSubSystemLocalRepository : IRepository<AccountCodingSubSystemLocal>
{
	/// <summary>
	/// Finds an active, non-deleted AccountCodingSubSystemLocal entity based on the provided entity's unique key properties.
	/// </summary>
	/// <param name="entity">The AccountCodingSubSystemLocal entity containing the unique key data for the search.</param>
	/// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
	/// <returns>
	/// A task that represents the asynchronous operation. The task result contains the found AccountCodingSubSystemLocal entity
	/// if a matching entity exists; otherwise, null.
	/// </returns>
	Task<AccountCodingSubSystemLocal?> FindEntityByThisKeyAsync(
		AccountCodingSubSystemLocal entity,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Asynchronously retrieves an AccountCodingSubSystemLocal entity based on the specified relation ID and subsystem name.
	/// </summary>
	/// <param name="relationId">The ID representing the relation to filter the entity.</param>
	/// <param name="subSystemName">The name of the subsystem to filter the entity.</param>
	/// <returns>A task that represents the asynchronous operation.
	/// The task result contains the matched AccountCodingSubSystemLocal entity if found; otherwise, null.</returns>
	Task<AccountCodingSubSystemLocal?> FindByRelationIdAndSubSystemNameAsync(string relationId, string subSystemName);

	/// <summary>
	/// Retrieves an active, non-deleted AccountCodingSubSystemLocal entity based on a relation ID,
	/// subsystem local name, and the code of the parent account coding.
	/// </summary>
	/// <param name="relationId">The unique relation ID used to identify the entity.</param>
	/// <param name="subSystemName">The name of the subsystem local associated with the entity.</param>
	/// <param name="parentAccountCoding">The code of the parent account coding to filter by.</param>
	/// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
	/// <returns>
	/// A task that represents the asynchronous operation. The task result contains the found
	/// AccountCodingSubSystemLocal entity if a match is found; otherwise, null.
	/// </returns>
	Task<AccountCodingSubSystemLocal?>
		FindByRelationIdAndSubSystemLocalNameAndParentAccountCodingAsync(
			string relationId, string subSystemName, string parentAccountCoding, CancellationToken cancellationToken = default);

	/// <summary>
	/// find talasoot bank account account coding subsystem local record
	/// - create if not exist this record
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>AccountCodingSubSystemLocal</returns>
	/// <exception cref="NullReferenceException"></exception>
	Task<AccountCodingSubSystemLocal>
		FindAccountCodingSubSystemLocalBankTalasootAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// find wallet recharge fee the account coding subsystem local record
	/// - create if not the exist this record
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>AccountCodingSubSystemLocal</returns>
	/// <exception cref="NullReferenceException"></exception>
	Task<AccountCodingSubSystemLocal>
		FindAccountCodingSubSystemLocalWalletRechargeFeeAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// find referal the account coding subsystem local record
	/// - create if not the exist this record
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>AccountCodingSubSystemLocal</returns>
	/// <exception cref="NullReferenceException"></exception>
	Task<AccountCodingSubSystemLocal>
		FindAccountCodingSubSystemLocalReferalAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// find Gold Purchase Fee the account coding subsystem local record
	/// - create if not the exist this record
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>AccountCodingSubSystemLocal</returns>
	/// <exception cref="NullReferenceException"></exception>
	Task<AccountCodingSubSystemLocal>
		FindAccountCodingSubSystemLocalGoldPurchaseAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// find Gold Treasury Fee the account coding subsystem local record
	/// - create if not the exist this record
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>AccountCodingSubSystemLocal</returns>
	/// <exception cref="NullReferenceException"></exception>
	Task<AccountCodingSubSystemLocal>
		FindAccountCodingSubSystemLocalGoldTreasuryAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// find Gold Of Fee the account coding subsystem local record
	/// - create if not the exist this record
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>AccountCodingSubSystemLocal</returns>
	/// <exception cref="NullReferenceException"></exception>
	Task<AccountCodingSubSystemLocal> FindAccountCodingSubSystemLocalGoldOfSaleFeeAsync(CancellationToken cancellationToken);
}