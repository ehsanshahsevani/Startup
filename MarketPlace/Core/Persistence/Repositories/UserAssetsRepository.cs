using Domain;
using Persistence.Abstracts;
using Microsoft.EntityFrameworkCore;
using Utilities;

namespace Persistence.Repositories;

public class UserAssetsRepository : Repository<UserAssets>, IUserAssetsRepository
{
	internal UserAssetsRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}

	/// <summary>
	/// Finds a user asset by the associated profile.
	/// </summary>
	/// <param name="profile">The profile associated with the user asset.</param>
	/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
	/// <returns>A task that represents the asynchronous operation. The task result is the user asset associated with the specified profile, or null if no such asset is found.</returns>
	public async Task<UserAssets> FindByProfileAsync(
		Profile profile, CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			
			.Where(current => current.ProfileId == profile.Id)

			.OrderByDescending(current => current.CreateDateTime)
			
			.FirstAsync(cancellationToken);
		
		return result;
	}

	/// <summary>
	/// Creates the initial user assets for a new profile if they do not already exist.
	/// </summary>
	/// <param name="profile">The profile to create the initial user assets for.</param>
	/// <param name="goldPriceInThisTime">The current gold price to initialize the assets with.</param>
	/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
	/// <returns>A task that represents the asynchronous operation. The task result is the user assets associated with the specified profile.</returns>
	public async Task<UserAssets>
		CreateIfNotExistFirstAssetsForNewProfile(
			Profile profile, decimal goldPriceInThisTime, CancellationToken cancellationToken = default)
	{
		var userAssets = await DbSet.
			
			Where(current => current.ProfileId == profile.Id)
			
			.OrderByDescending(x => x.CreateDateTime)
			
			.FirstOrDefaultAsync(cancellationToken);

		if (userAssets is not null)
		{
			return userAssets;
		}

		userAssets = new UserAssets
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,
			UpdateDateTime = DateTime.Now,
			
			ProfileId = profile.Id,
			
			Amount = 0,
			GoldSoot = 0,
			AssetsGold = 0,
			AssetsWallet = 0,
			GoldPriceInThisTime = goldPriceInThisTime,
			
			DocumentId = null,
			
			Description = 
				string.Format(
					Resources.Messages.FirstUserAssetsDescription,
					profile.FirstName, profile.LastName),
		};
		
		await AddAsync(userAssets, cancellationToken);
		
		return userAssets;
	}

	/// <summary>
	/// Updates the user assets based on the provided account codings and profile.
	/// </summary>
	/// <param name="codings">A list of account codings used to determine changes in gold and wallet assets.</param>
	/// <param name="profile">The profile associated with the user assets to be updated.</param>
	/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
	/// <returns>A task that represents the asynchronous operation. The task result is the updated user assets.</returns>
	/// <exception cref="ArgumentNullException">Thrown if the provided profile or codings are null.</exception>
	/// <exception cref="NullReferenceException">Thrown if no existing assets are found for the specified profile.</exception>
	public async Task<UserAssets> UpdateUserAssetsAsync(
		List<AccountCoding> codings, Profile profile,
		CancellationToken cancellationToken = default)
	{
		if (profile is null)
		{
			throw new ArgumentNullException(nameof(profile));
		}

		if (codings is null || codings.Count == 0)
		{
			throw new NullReferenceException(nameof(codings));
		}

		var first = codings.First();
		
		if (first.PriceInThisTime <= 0)
		{
			throw new NullReferenceException(nameof(first.PriceInThisTime));
		}

		decimal goldPrice = first.PriceInThisTime;

		// 1. دریافت دارایی فعلی از دیتابیس
		var previousAssets = await FindByProfileAsync(profile, cancellationToken);

		if (previousAssets is null)
		{
			throw new NullReferenceException(nameof(previousAssets));
		}

		// 2. محاسبه تغییرات طلا و کیف پول از لیست کدینگ‌ها
		decimal goldDelta = codings
			.Where(x => x.Code.StartsWith(AccountCoding.UserGoldAssetsCode))
			.Sum(x => x.IsCreditor ? -x.GoldSoot : x.GoldSoot);

		decimal walletDelta = codings
			.Where(x => x.Code.StartsWith(AccountCoding.UserMoneyAssetsCode))
			.Sum(x => x.IsCreditor ? -x.Amount : x.Amount);

		// 3. ساخت دارایی جدید با اعمال تغییرات
		var updatedAssets = new UserAssets
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,
			UpdateDateTime = DateTime.Now,
			
			DocumentId = null,
			
			ProfileId = profile.Id,
			
			Profile = profile,
			
			GoldPriceInThisTime = goldPrice,

			GoldSoot = (int)codings
				.Where(x => x.IsCreditor && x.Code.StartsWith(AccountCoding.UserGoldAssetsCode))
				.Sum(x => x.GoldSoot),
			
			AssetsGold = previousAssets.AssetsGold + goldDelta,
			AssetsWallet = previousAssets.AssetsWallet + walletDelta,
			Amount = (previousAssets.AssetsGold + goldDelta).GoldToToman(goldPrice)
		};

		await AddAsync(updatedAssets, cancellationToken);
		
		return updatedAssets;
	}
}