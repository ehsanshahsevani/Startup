using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface IUserAssetsRepository : IRepository<UserAssets>
{
	/// <summary>
	/// Finds a user asset by the associated profile.
	/// </summary>
	/// <param name="profile">The profile associated with the user asset.</param>
	/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
	/// <returns>A task that represents the asynchronous operation. The task result is the user asset associated with the specified profile, or null if no such asset is found.</returns>
	Task<UserAssets> FindByProfileAsync(
		Profile profile, CancellationToken cancellationToken = default);

	/// <summary>
	/// Creates the initial user assets for a new profile if they do not already exist.
	/// </summary>
	/// <param name="profile">The profile to create the initial user assets for.</param>
	/// <param name="goldPriceInThisTime">The current gold price to initialize the assets with.</param>
	/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
	/// <returns>A task that represents the asynchronous operation. The task result is the user assets associated with the specified profile.</returns>
	Task<UserAssets>
		CreateIfNotExistFirstAssetsForNewProfile(
			Profile profile, decimal goldPriceInThisTime, CancellationToken cancellationToken = default);
	
	/// <summary>
	/// Updates the user assets based on the provided account codings and profile.
	/// </summary>
	/// <param name="codings">A list of account codings used to determine changes in gold and wallet assets.</param>
	/// <param name="profile">The profile associated with the user assets to be updated.</param>
	/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
	/// <returns>A task that represents the asynchronous operation. The task result is the updated user assets.</returns>
	/// <exception cref="ArgumentNullException">Thrown if the provided profile or codings are null.</exception>
	/// <exception cref="NullReferenceException">Thrown if no existing assets are found for the specified profile.</exception>
	Task<UserAssets> UpdateUserAssetsAsync(
		List<AccountCoding> codings, Profile profile,
		CancellationToken cancellationToken = default);
}