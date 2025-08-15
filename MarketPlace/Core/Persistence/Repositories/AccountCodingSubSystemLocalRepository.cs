using Domain;
using SampleResult;
using Persistence.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class AccountCodingSubSystemLocalRepository : Repository<AccountCodingSubSystemLocal>, IAccountCodingSubSystemLocalRepository
{
	internal AccountCodingSubSystemLocalRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}

	public override async Task<Result> AddAsync(
		AccountCodingSubSystemLocal? entity,
		CancellationToken cancellationToken = default)
	{
		if (entity is null)
		{
			throw new ArgumentNullException(nameof(entity));
		}
		
		var result = new FluentResults.Result();
		
		var accountCodingSubSystemLocal =
			await FindEntityByThisKeyAsync(entity, cancellationToken);

		if (accountCodingSubSystemLocal is null)
		{
			await base.AddAsync(entity, cancellationToken);
			
			return result.ConvertToSampleResult();
		}

		var errorMessage = 
			string.Format(Resources.Messages.RepeatError, nameof(AccountCodingSubSystemLocal));
			
		result.WithError(errorMessage);
			
		return result.ConvertToSampleResult();
	}

	/// <summary>
	/// Finds an active, non-deleted AccountCodingSubSystemLocal entity based on the provided entity's unique key properties.
	/// </summary>
	/// <param name="entity">The AccountCodingSubSystemLocal entity containing the unique key data for the search.</param>
	/// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
	/// <returns>
	/// A task that represents the asynchronous operation. The task result contains the found AccountCodingSubSystemLocal entity
	/// if a matching entity exists; otherwise, null.
	/// </returns>
	public async Task<AccountCodingSubSystemLocal?> FindEntityByThisKeyAsync(
		AccountCodingSubSystemLocal entity,
		CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			
			.Where(current => current.IsActive == true)
			.Where(current => current.IsDeleted == false)
				
			.Where(current => current.RelationId == entity.RelationId)
			.Where(current => current.AccountCodingId == entity.AccountCodingId)
			.Where(current => current.SubSystemLocalId == entity.SubSystemLocalId)
			
			.FirstOrDefaultAsync(cancellationToken);

		return result;
	}


	/// <summary>
	/// Finds an active, non-deleted AccountCodingSubSystemLocal entity based on the provided relation ID and subsystem name.
	/// </summary>
	/// <param name="relationId">The unique identifier for the relation associated with the subsystem.</param>
	/// <param name="subSystemName">The name of the subsystem to be searched.</param>
	/// <returns>
	/// A task that represents the asynchronous operation. The task result contains the found AccountCodingSubSystemLocal entity
	/// if a matching entity exists; otherwise, null.
	/// </returns>
	public async Task<AccountCodingSubSystemLocal?>
		FindByRelationIdAndSubSystemNameAsync(string relationId, string subSystemName)
	{
		var result = await DbSet
			.Include(current => current.AccountCoding)
			.Include(current => current.SubSystemLocal)
			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.Where(current => current.RelationId == relationId)
			.Where(current => current.SubSystemLocal.NameEN == subSystemName)
			.FirstOrDefaultAsync();

		return result;
	}

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
	public async Task<AccountCodingSubSystemLocal?>
		FindByRelationIdAndSubSystemLocalNameAndParentAccountCodingAsync(
			string relationId, string subSystemName, string parentAccountCoding, CancellationToken cancellationToken = default)
	{
		var result = await DbSet
				
			.Include(current => current.AccountCoding)
			.Include(current => current.SubSystemLocal)
			
			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.Where(current => current.AccountCoding.Code.StartsWith(parentAccountCoding))
			.Where(current => current.AccountCoding.Code.Length > parentAccountCoding.Length)
			.Where(current => current.RelationId == relationId)
			.Where(current => current.SubSystemLocal.NameEN == subSystemName)
			
			.FirstOrDefaultAsync(cancellationToken);

		return result;
	}


	/// <summary>
	/// find talasoot bank the account coding subsystem local record
	/// - create if not the exist this record
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>AccountCodingSubSystemLocal</returns>
	/// <exception cref="NullReferenceException"></exception>
	public async Task<AccountCodingSubSystemLocal>
		FindAccountCodingSubSystemLocalBankTalasootAsync(CancellationToken cancellationToken = default)
	{
		ITalaSootBankAccountRepository
			talaSootBankAccountRepository =
			new TalaSootBankAccountRepository(DatabaseContext);
		
		TalaSootBankAccount? bank =
			await talaSootBankAccountRepository
				.FindFirstRecordAsync(cancellationToken);

		if (bank is null)
		{
			throw new NullReferenceException(nameof(TalaSootBankAccount));
		}
		
		var result = await DbSet
			.Include(current => current.AccountCoding)
			.Include(current => current.SubSystemLocal)
			
			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.Where(current => current.RelationId == bank.Id)
			.Where(current => current.SubSystemLocal.NameEN == nameof(TalaSootBankAccount))
			
			.Where(current => current.AccountCoding.Code.Equals(AccountCoding.TalaSootBankAccountCode))
			
			.FirstOrDefaultAsync(cancellationToken);

		if (result is null)
		{
			IAccountCodingRepository
				accountCodingRepository =
					new AccountCodingRepository(DatabaseContext);

			ISubSystemLocalRepository
				subSystemLocalRepository =
					new SubSystemLocalRepository(DatabaseContext);
			
			var accountCodingBankTalaSoot =
				await accountCodingRepository
					.FindTalasootBankAccountCodeAccountCodingAsync(cancellationToken);

			if (accountCodingBankTalaSoot is null)
			{
				throw new NullReferenceException(nameof(accountCodingBankTalaSoot));
			}
			
			var subSystemLocalBankTalasoot =
				await subSystemLocalRepository
					.FindByNameAsync(domain: nameof(Domain.TalaSootBankAccount), cancellationToken);

			if (subSystemLocalBankTalasoot is null)
			{
				throw new NullReferenceException(nameof(subSystemLocalBankTalasoot));
			}
			
			result = new AccountCodingSubSystemLocal
			{
				IsActive = true,
				IsDeleted = false,
				
				AccountCoding = accountCodingBankTalaSoot,
				SubSystemLocal = subSystemLocalBankTalasoot,
				
				Ordering = 100_000,
				RelationId = bank.Id,
			};

			await AddAsync(result, cancellationToken);
			await DatabaseContext.SaveChangesAsync(cancellationToken);
		}

		return result;
	}
	
	/// <summary>
	/// find wallet recharge fee the account coding subsystem local record
	/// - create if not the exist this record
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>AccountCodingSubSystemLocal</returns>
	/// <exception cref="NullReferenceException"></exception>
	public async Task<AccountCodingSubSystemLocal>
		FindAccountCodingSubSystemLocalWalletRechargeFeeAsync(CancellationToken cancellationToken = default)
	{
		Abstracts.IIncomeWalletRechargeFeeRepository incomeRepository =
			new IncomeWalletRechargeFeeRepository(DatabaseContext);
		
		IncomeWalletRechargeFee? income =
			await incomeRepository
				.FindIncomeAsync(cancellationToken);

		if (income is null)
		{
			throw new NullReferenceException(nameof(Domain.TalaSootBankAccount));
		}
		
		var result = await DbSet
			.Include(current => current.AccountCoding)
			.Include(current => current.SubSystemLocal)
			
			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.Where(current => current.RelationId == income.Id)
			.Where(current => current.SubSystemLocal.NameEN == nameof(Domain.IncomeWalletRechargeFee))
			
			.Where(current => current.AccountCoding.Code.Equals(AccountCoding.WalletRechargeFeeIncome))
			
			.FirstOrDefaultAsync(cancellationToken);

		if (result is null)
		{
			IAccountCodingRepository
				accountCodingRepository =
					new AccountCodingRepository(DatabaseContext);

			ISubSystemLocalRepository
				subSystemLocalRepository =
					new SubSystemLocalRepository(DatabaseContext);
			
			var accountCodingBankTalaSoot =
				await accountCodingRepository
					.FindWalletRechargeFeeIncomeAccountCodingAsync(cancellationToken);

			if (accountCodingBankTalaSoot is null)
			{
				throw new NullReferenceException(nameof(accountCodingBankTalaSoot));
			}
			
			var subSystemLocalBankTalasoot =
				await subSystemLocalRepository
					.FindByNameAsync(domain: nameof(Domain.IncomeWalletRechargeFee), cancellationToken);

			if (subSystemLocalBankTalasoot is null)
			{
				throw new NullReferenceException(nameof(subSystemLocalBankTalasoot));
			}
			
			result = new AccountCodingSubSystemLocal
			{
				IsActive = true,
				IsDeleted = false,
				
				AccountCoding = accountCodingBankTalaSoot,
				SubSystemLocal = subSystemLocalBankTalasoot,
				
				Ordering = 100_000,
				RelationId = income.Id,
			};

			await AddAsync(result, cancellationToken);
			await DatabaseContext.SaveChangesAsync(cancellationToken);
		}

		return result;
	}
	
	/// <summary>
	/// find referal the account coding subsystem local record
	/// - create if not the exist this record
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>AccountCodingSubSystemLocal</returns>
	/// <exception cref="NullReferenceException"></exception>
	public async Task<AccountCodingSubSystemLocal>
		FindAccountCodingSubSystemLocalReferalAsync(CancellationToken cancellationToken = default)
	{
		Abstracts.IReferralRepository
			referralRepository = new ReferralRepository(DatabaseContext);
		
		Referral? referral =
			await referralRepository
				.FindLastRecordAsync(cancellationToken);

		if (referral is null || referral.IsActive == false)
		{
			throw new NullReferenceException(nameof(Domain.Referral));
		}
		
		var result = await DbSet
			.Include(current => current.AccountCoding)
			.Include(current => current.SubSystemLocal)
			
			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.Where(current => current.RelationId == referral.Id)
			.Where(current => current.SubSystemLocal.NameEN == nameof(Domain.Referral))
			
			.Where(current => current.AccountCoding.Code.Equals(AccountCoding.ReferalCode))
			
			.FirstOrDefaultAsync(cancellationToken);

		if (result is null)
		{
			IAccountCodingRepository
				accountCodingRepository =
					new AccountCodingRepository(DatabaseContext);

			ISubSystemLocalRepository
				subSystemLocalRepository =
					new SubSystemLocalRepository(DatabaseContext);
			
			var coding =
				await accountCodingRepository
					.FindReferalAccountCodingAsync(cancellationToken);

			if (coding is null)
			{
				throw new NullReferenceException(nameof(coding));
			}
			
			var subSystemLocal =
				await subSystemLocalRepository
					.FindByNameAsync(domain: nameof(Domain.Referral), cancellationToken);

			if (subSystemLocal is null)
			{
				throw new NullReferenceException(nameof(subSystemLocal));
			}
			
			result = new AccountCodingSubSystemLocal
			{
				IsActive = true,
				IsDeleted = false,
				
				AccountCoding = coding,
				SubSystemLocal = subSystemLocal,
				
				Ordering = 100_000,
				RelationId = referral.Id,
			};

			await AddAsync(result, cancellationToken);
			await DatabaseContext.SaveChangesAsync(cancellationToken);
		}

		return result;
	}
	
	/// <summary>
	/// find Gold Purchase Fee the account coding subsystem local record
	/// - create if not the exist this record
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>AccountCodingSubSystemLocal</returns>
	/// <exception cref="NullReferenceException"></exception>
	public async Task<AccountCodingSubSystemLocal>
		FindAccountCodingSubSystemLocalGoldPurchaseAsync(CancellationToken cancellationToken = default)
	{
		Abstracts.IIncomeGoldPurchaseFeeRepository
			incomeRepository = new IncomeGoldPurchaseFeeRepository(DatabaseContext);
		
		IncomeGoldPurchaseFee? income =
			await incomeRepository
				.FindIncomeAsync(cancellationToken);

		if (income is null || income.IsActive == false)
		{
			throw new NullReferenceException(nameof(Domain.IncomeGoldPurchaseFee));
		}
		
		var result = await DbSet
			.Include(current => current.AccountCoding)
			.Include(current => current.SubSystemLocal)
			
			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.Where(current => current.RelationId == income.Id)
			.Where(current => current.SubSystemLocal.NameEN == nameof(Domain.IncomeGoldPurchaseFee))
			
			.Where(current => current.AccountCoding.Code.Equals(AccountCoding.GoldPurchaseFee))
			
			.FirstOrDefaultAsync(cancellationToken);

		if (result is null)
		{
			IAccountCodingRepository
				accountCodingRepository =
					new AccountCodingRepository(DatabaseContext);

			ISubSystemLocalRepository
				subSystemLocalRepository =
					new SubSystemLocalRepository(DatabaseContext);
			
			var coding =
				await accountCodingRepository
					.FindGoldPurchaseFeeAccountCodingAsync(cancellationToken);

			if (coding is null)
			{
				throw new NullReferenceException(nameof(coding));
			}
			
			var subSystemLocal =
				await subSystemLocalRepository
					.FindByNameAsync(domain: nameof(Domain.IncomeGoldPurchaseFee), cancellationToken);

			if (subSystemLocal is null)
			{
				throw new NullReferenceException(nameof(subSystemLocal));
			}
			
			result = new AccountCodingSubSystemLocal
			{
				IsActive = true,
				IsDeleted = false,
				
				AccountCoding = coding,
				SubSystemLocal = subSystemLocal,
				
				Ordering = 100_000,
				RelationId = income.Id,
			};

			await AddAsync(result, cancellationToken);
			await DatabaseContext.SaveChangesAsync(cancellationToken);
		}

		return result;
	}
	
	/// <summary>
	/// find Gold Treasury Fee the account coding subsystem local record
	/// - create if not the exist this record
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>AccountCodingSubSystemLocal</returns>
	/// <exception cref="NullReferenceException"></exception>
	public async Task<AccountCodingSubSystemLocal>
		FindAccountCodingSubSystemLocalGoldTreasuryAsync(CancellationToken cancellationToken = default)
	{
		Abstracts.IGoldTreasuryOnlineRepository
			goldTreasuryRepository = new GoldTreasuryOnlineRepository(DatabaseContext);
		
		GoldTreasuryOnline? goldTreasury = 
			await goldTreasuryRepository
				.FindFirstRecordAsync(cancellationToken);

		if (goldTreasury is null || goldTreasury.IsActive == false)
		{
			throw new NullReferenceException(nameof(Domain.GoldTreasuryOnline));
		}
		
		var result = await DbSet
			.Include(current => current.AccountCoding)
			.Include(current => current.SubSystemLocal)
			
			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.Where(current => current.RelationId == goldTreasury.Id)
			.Where(current => current.SubSystemLocal.NameEN == nameof(Domain.GoldTreasuryOnline))
			
			.Where(current => current.AccountCoding.Code.Equals(AccountCoding.GoldPurchaseFee))
			
			.FirstOrDefaultAsync(cancellationToken);

		if (result is null)
		{
			IAccountCodingRepository
				accountCodingRepository =
					new AccountCodingRepository(DatabaseContext);

			ISubSystemLocalRepository
				subSystemLocalRepository =
					new SubSystemLocalRepository(DatabaseContext);
			
			var coding =
				await accountCodingRepository
					.FindGoldTreasuryAccountCodingAsync(cancellationToken);

			if (coding is null)
			{
				throw new NullReferenceException(nameof(coding));
			}
			
			var subSystemLocal =
				await subSystemLocalRepository
					.FindByNameAsync(domain: nameof(Domain.GoldTreasuryOnline), cancellationToken);

			if (subSystemLocal is null)
			{
				throw new NullReferenceException(nameof(subSystemLocal));
			}
			
			result = new AccountCodingSubSystemLocal
			{
				IsActive = true,
				IsDeleted = false,
				
				AccountCoding = coding,
				SubSystemLocal = subSystemLocal,
				
				Ordering = 100_000,
				RelationId = goldTreasury.Id,
			};

			await AddAsync(result, cancellationToken);
			await DatabaseContext.SaveChangesAsync(cancellationToken);
		}

		return result;
	}

	/// <summary>
	/// find Gold Of sale Fee the account coding subsystem local record
	/// - create if not the exist this record
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>AccountCodingSubSystemLocal</returns>
	/// <exception cref="NullReferenceException"></exception>
	public async Task<AccountCodingSubSystemLocal>
		FindAccountCodingSubSystemLocalGoldOfSaleFeeAsync(CancellationToken cancellationToken = default)
	{
		Abstracts.IIncomeSaleOfGoldFeeRepository
			incomeRepository = new IncomeSaleOfGoldFeeRepository(DatabaseContext);
		
		IncomeSaleOfGoldFee? income =
			await incomeRepository
				.FindIncomeAsync(cancellationToken);

		if (income is null || income.IsActive == false)
		{
			throw new NullReferenceException(nameof(Domain.IncomeSaleOfGoldFee));
		}
		
		var result = await DbSet
			.Include(current => current.AccountCoding)
			.Include(current => current.SubSystemLocal)
			
			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.Where(current => current.RelationId == income.Id)
			.Where(current => current.SubSystemLocal.NameEN == nameof(Domain.IncomeSaleOfGoldFee))
			
			.Where(current => current.AccountCoding.Code.Equals(AccountCoding.IncomeSaleOfGoldCode))
			
			.FirstOrDefaultAsync(cancellationToken);

		if (result is null)
		{
			IAccountCodingRepository
				accountCodingRepository =
					new AccountCodingRepository(DatabaseContext);

			ISubSystemLocalRepository
				subSystemLocalRepository =
					new SubSystemLocalRepository(DatabaseContext);
			
			var coding =
				await accountCodingRepository
					.FindIncomeSaleOfGoldCodeAccountCodingAsync(cancellationToken);

			if (coding is null)
			{
				throw new NullReferenceException(nameof(coding));
			}
			
			var subSystemLocal =
				await subSystemLocalRepository
					.FindByNameAsync(domain: nameof(Domain.IncomeSaleOfGoldFee), cancellationToken);

			if (subSystemLocal is null)
			{
				throw new NullReferenceException(nameof(subSystemLocal));
			}
			
			result = new AccountCodingSubSystemLocal
			{
				IsActive = true,
				IsDeleted = false,
				
				AccountCoding = coding,
				SubSystemLocal = subSystemLocal,
				
				Ordering = 100_000,
				RelationId = income.Id,
			};

			await AddAsync(result, cancellationToken);
			await DatabaseContext.SaveChangesAsync(cancellationToken);
		}

		return result;
	}
}