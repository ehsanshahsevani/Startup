using Domain;
using Persistence.Abstracts;
using Microsoft.EntityFrameworkCore;
using Utilities;

namespace Persistence.Repositories;

public class AccountCodingRepository : Repository<AccountCoding>, IAccountCodingRepository
{
	internal AccountCodingRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}

	/// <summary>
	/// پیدا کردن یک اکانت کد با کد 
	/// </summary>
	/// <param name="code"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public async Task<AccountCoding?> FindByCodeAsync(string code, CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.Where(current => current.Code == code)
			.FirstOrDefaultAsync(cancellationToken);

		return result;
	}

	/// <summary>
	/// پیدا کردن اکانت کد مربوط به هدیه رفرال و دعوت دوستان
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>اکانت کد هدیه و رفرال</returns>
	public async Task<AccountCoding?> FindReferalAccountCodingAsync(CancellationToken cancellationToken = default)
	{
		var result =
			await FindByCodeAsync(AccountCoding.ReferalCode, cancellationToken);

		return result;
	}

	/// <summary>
	/// پیدا کردن اکانت کد مربوط به خزانه طلا
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public async Task<AccountCoding?> FindGoldTreasuryAccountCodingAsync(CancellationToken cancellationToken = default)
	{
		var result =
			await FindByCodeAsync(AccountCoding.GoldTreasuryCode, cancellationToken);

		return result;
	}

	/// <summary>
	/// پیدا کردن اکانت کد مربوط به درآمد کارمزد خرید طلا
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public async Task<AccountCoding?> FindGoldPurchaseFeeAccountCodingAsync(
		CancellationToken cancellationToken = default)
	{
		var result =
			await FindByCodeAsync(AccountCoding.GoldPurchaseFee, cancellationToken);

		return result;
	}

	/// <summary>
	/// پیدا کردن اکانت کد مربوط به درآمد نگهداری طلا
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public async Task<AccountCoding?> FindGoldMaintenanceFeeAccountCodingAsync(
		CancellationToken cancellationToken = default)
	{
		var result =
			await FindByCodeAsync(AccountCoding.GoldMaintenanceFeeCode, cancellationToken);

		return result;
	}

	/// <summary>
	/// پیدا کردن اکانت کد مربوط به فروش محصول در فروشگاه
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public async Task<AccountCoding?> FindIncomeSellingGoodsCodeAccountCodingAsync(
		CancellationToken cancellationToken = default)
	{
		var result =
			await FindByCodeAsync(AccountCoding.IncomeSellingGoods, cancellationToken);

		return result;
	}

	/// <summary>
	/// پیدا کردن کد مربوط به حساب طلاسوت
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public async Task<AccountCoding?> FindTalasootBankAccountCodeAccountCodingAsync(
		CancellationToken cancellationToken = default)
	{
		var result =
			await FindByCodeAsync(AccountCoding.TalaSootBankAccountCode, cancellationToken);

		return result;
	}

	/// <summary>
	/// پیدا کردن اکانت کد مربوط به کمسیون
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public async Task<AccountCoding?> FindIncomeCommissionCodeCodeAccountCodingAsync(
		CancellationToken cancellationToken = default)
	{
		var result =
			await FindByCodeAsync(AccountCoding.IncomeCommissionCode, cancellationToken);

		return result;
	}

	/// <summary>
	/// پیدا کردن اکانت کد مربوط به درآمد فروش طلا
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public async Task<AccountCoding?> FindIncomeSaleOfGoldCodeAccountCodingAsync(
		CancellationToken cancellationToken = default)
	{
		var result =
			await FindByCodeAsync(AccountCoding.IncomeSaleOfGoldCode, cancellationToken);

		return result;
	}

	/// <summary>
	/// پیدا کردن اکانت کد مربوط به شارژ کیف پول
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public async Task<AccountCoding?> FindWalletRechargeFeeIncomeAccountCodingAsync(
		CancellationToken cancellationToken = default)
	{
		var result =
			await FindByCodeAsync(AccountCoding.WalletRechargeFeeIncome, cancellationToken);

		return result;
	}

	/// <summary>
	/// اتصال موجودی بانکی ادمین به درخت حسابداری 
	/// code: 011011
	/// name: موجودی حساب بانکی طلاسوت
	/// </summary>
	/// <exception cref="ArgumentNullException"></exception>
	public async Task<AccountCoding> ConnectBankAccountAmountToTalaSootBankAccountAsync(
		TalaSootBankAccount bankAccount, CancellationToken cancellationToken = default)
	{
		var resultCoding = await CheckAndCreateSubSystemAccountCodingAsync(
			bankAccount.Id, nameof(TalaSootBankAccount),
			AccountCoding.TalaSootBankAccountCode,
			charCount: 0,
			isCreate: false, cancellationToken: cancellationToken);

		if (resultCoding is null)
		{
			throw new NullReferenceException(nameof(resultCoding));
		}

		return resultCoding;
	}

	/// <summary>
	/// code: 01102
	/// name: کیف پول کاربران
	/// ایجاد و اتصال حساب بانکی کاربر به درخت حسابداری
	/// این بخش به عنوان کیف پول کاربران در اسناد استفاده میشود
	/// تمامی کاربران همزمان با ایجاد یک پروفایل باید یه کد حسابداری دریافت کنند
	/// </summary>
	/// <param name="profile"></param>
	/// <param name="cancellationToken"></param>
	/// <exception cref="ArgumentNullException"></exception>
	/// <exception cref="NullReferenceException"></exception>
	public async Task<AccountCoding> CreateUserMoneyAssetsAsync(
		Profile profile, CancellationToken cancellationToken = default)
	{
		var newAccountCodingName =
			string.Format(Resources.Messages.CreateUserMoneyAssets, profile.FullName);

		var resultCoding =
			await CheckAndCreateSubSystemAccountCodingAsync(profile.Id, nameof(Profile),
				AccountCoding.UserMoneyAssetsCode,
				AccountCoding.UserMoneyAssetsCodeCharCount,
				isCreate: true, newAccountCodingName, cancellationToken: cancellationToken);

		if (resultCoding is null)
		{
			throw new NullReferenceException(nameof(resultCoding));
		}

		return resultCoding;
	}

	/// <summary>
	/// code: 01103
	/// name: کد حسابداری دارایی طلای کاربران
	/// ایجاد و اتصال حساب طلای کاربر به درخت حسابداری
	/// این بخش به عنوان دارایی طلای کاربران در اسناد استفاده میشود
	/// تمامی کاربران همزمان با ایجاد یک پروفایل باید یک کد حسابداری دریافت کنند
	/// </summary>
	/// <param name="profile"></param>
	/// <param name="cancellationToken"></param>
	/// <exception cref="ArgumentNullException"></exception>
	/// <exception cref="NullReferenceException"></exception>
	public async Task<AccountCoding> CreateUserGoldAssetsAsync(Profile profile,
		CancellationToken cancellationToken = default)
	{
		var newAccountCodingName =
			string.Format(Resources.Messages.CreateUserGoldAssets, profile.FullName);

		var resultCoding =
			await CheckAndCreateSubSystemAccountCodingAsync(profile.Id, nameof(Profile),
				AccountCoding.UserGoldAssetsCode,
				AccountCoding.UserGoldAssetsCodeCharCount,
				isCreate: true, newAccountCodingName, cancellationToken: cancellationToken);

		if (resultCoding is null)
		{
			throw new NullReferenceException(nameof(resultCoding));
		}

		return resultCoding;
	}

	/// <summary>
	/// code: 01104
	/// name: کد حساب بانکی کاربران
	/// ایجاد و اتصال حساب بانکی کاربر به درخت حسابداری
	/// این بخش به عنوان کد حساب بانکی در اسناد استفاده میشود
	/// تمامی کاربران همزمان با ایجاد یک پروفایل باید یک کد حسابداری دریافت کنند
	/// </summary>
	/// <param name="profileBank"></param>
	/// <param name="cancellationToken"></param>
	public async Task<AccountCoding> CreateUserBankAccountAsync(ProfileBank profileBank,
		CancellationToken cancellationToken = default)
	{
		if (profileBank is null)
		{
			throw new ArgumentNullException(nameof(profileBank));
		}

		if (profileBank.Profile is null)
		{
			throw new ArgumentNullException(nameof(profileBank.Profile));
		}

		if (profileBank.Bank is null)
		{
			throw new ArgumentNullException(nameof(profileBank.Bank));
		}

		var newAccountCodingName =
			string.Format(
				Resources.Messages.CreateUserBankAccount,
				profileBank.Bank.Name,
				profileBank.Profile.FullName,
				profileBank.CardNumber);

		var resultCoding =
			await CheckAndCreateSubSystemAccountCodingAsync(
				profileBank.Id,
				nameof(ProfileBank),
				AccountCoding.UserBankAccountCode,
				AccountCoding.UserBankAccountCodeCharCount,
				isCreate: true, newAccountCodingName, cancellationToken: cancellationToken);

		if (resultCoding is null)
		{
			throw new NullReferenceException(nameof(resultCoding));
		}

		return resultCoding;
	}

	/// <summary>
	/// code: 01106
	/// name: درآمد رند کردن موجودی احسان شاهسونی
	/// ایجاد و اتصال پروفایل کاربر به درخت حسابداری
	/// این بخش به عنوان مبلغ رند در اسناد استفاده میشود
	/// تمامی کاربران همزمان با ایجاد یک پروفایل باید یه کد حسابداری دریافت کنند
	/// </summary>
	/// <param name="profile"></param>
	/// <param name="cancellationToken"></param>
	/// <exception cref="ArgumentNullException"></exception>
	/// <exception cref="NullReferenceException"></exception>
	public async Task<AccountCoding> CreateUserRoundCodeAsync(
		Profile profile, CancellationToken cancellationToken = default)
	{
		var newAccountCodingName =
			string.Format(Resources.Messages.UserRoundCode, profile.FullName);

		var resultCoding =
			await CheckAndCreateSubSystemAccountCodingAsync(profile.Id, nameof(Profile),
				AccountCoding.UserRoundCode,
				AccountCoding.UserRoundCodeCount,
				isCreate: true, newAccountCodingName, cancellationToken: cancellationToken);

		if (resultCoding is null)
		{
			throw new NullReferenceException(nameof(resultCoding));
		}

		return resultCoding;
	}

	/// <summary>
	/// دریافت لیست کدینگ مربوط به ثبت سند حسابداری واریز یا برداشت از کیف پول
	/// </summary>
	/// <param name="profileBank">حساب بانکی مورد نظر شخص</param>
	/// <param name="isDeposit">برداشت انجام میشود یا خیر در صورت برداشت نبودن سیستم واریز درنظر میگیرد</param>
	/// <param name="amount">مبلغ مورد نظر برای واریز یا برداشت</param>
	/// <param name="goldPriceInThisTime">قیمت لحظه ای طلا</param>
	/// <param name="cancellationToken"></param>
	/// <returns>لیست کدینگ هایی مناسب ثبت سند برای بخش حسابداری</returns>
	public async Task<List<AccountCoding>> FindAllAccountCodingDataForDepositAndWithdrawalAsync(
		ProfileBank profileBank, bool isDeposit, decimal amount, decimal goldPriceInThisTime,
		CancellationToken cancellationToken = default)
	{
		var result = new List<AccountCoding>();

		IAccountCodingSubSystemLocalRepository
			accountCodingSubSystemLocalRepository =
				new AccountCodingSubSystemLocalRepository(DatabaseContext);

		var codingBankTalaSoot = AccountCoding.TalaSootBankAccountCode;

		var codingUserMoneyAssets = AccountCoding.UserMoneyAssetsCode;

		// var codingUserGoldAssets = AccountCoding.UserGoldAssetsCode;

		var codingUserBankAccount = AccountCoding.UserBankAccountCode;

		// **************************************************
		var accountCodingBankTalasootAccountCodingSubSystemLocal =
			await accountCodingSubSystemLocalRepository
				.FindAccountCodingSubSystemLocalBankTalasootAsync(cancellationToken);

		accountCodingBankTalasootAccountCodingSubSystemLocal
			.AccountCoding.SetDocumentPropertiesByAmount(
				amount, goldPriceInThisTime, accountCodingBankTalasootAccountCodingSubSystemLocal);

		accountCodingBankTalasootAccountCodingSubSystemLocal.AccountCoding.IsDebtor = isDeposit;
		accountCodingBankTalasootAccountCodingSubSystemLocal.AccountCoding.IsCreditor = !isDeposit;
		accountCodingBankTalasootAccountCodingSubSystemLocal.AccountCoding.UseParentDocument = true;

		result.Add(accountCodingBankTalasootAccountCodingSubSystemLocal.AccountCoding);

		// *****
		var accountCodingSubSystemLocalUserMoneyAssets =
			await accountCodingSubSystemLocalRepository
				.FindByRelationIdAndSubSystemLocalNameAndParentAccountCodingAsync(
					profileBank.ProfileId, nameof(Profile), codingUserMoneyAssets, cancellationToken);

		if (accountCodingSubSystemLocalUserMoneyAssets is null)
		{
			throw new NullReferenceException(nameof(accountCodingSubSystemLocalUserMoneyAssets));
		}
		else
		{
			accountCodingSubSystemLocalUserMoneyAssets
				.AccountCoding.SetDocumentPropertiesByAmount(
					amount,
					goldPriceInThisTime,
					accountCodingSubSystemLocalUserMoneyAssets);

			accountCodingSubSystemLocalUserMoneyAssets.AccountCoding.IsDebtor = !isDeposit;
			accountCodingSubSystemLocalUserMoneyAssets.AccountCoding.IsCreditor = isDeposit;
			accountCodingSubSystemLocalUserMoneyAssets.AccountCoding.UseParentDocument = true;

			// فقط واریز
			if (isDeposit == false)
			{
				IIncomeWalletRechargeFeeRepository incomeRepository =
					new IncomeWalletRechargeFeeRepository(DatabaseContext);

				var walletRechargeFee =
					await incomeRepository.FindIncomeAmountAsync(cancellationToken);

				// تبدیل عدد وارد شده به درصد (یعنی 5% از مقدار کل)
				decimal percentage =
					walletRechargeFee.ConvertToPercentage(); // 5 => 0.05 (برای درصد)

				// محاسبه مقدار کارمزد
				decimal walletRechargeFeeAmount =
					amount.CalculatePercentageOfAmount(percentage); // مبلغ کارمزد روی قیمت اصلی

				// محاسبه موجودی نهایی پس از کسر کارمزد
				decimal finalWalletAmount = amount - walletRechargeFeeAmount; // موجودی نهایی پس از کسر کارمزد

				// بررسی اینکه موجودی نهایی اعشاری است
				decimal roundingDifference = finalWalletAmount.GetFractionalDifference(); // اختلاف اعشاری

				// مبلغ نهایی برای واریز به کیف پول کاربر
				decimal roundedFinalAmount = finalWalletAmount - roundingDifference;

				accountCodingSubSystemLocalUserMoneyAssets
					.AccountCoding.SetDocumentPropertiesByAmount(
						roundedFinalAmount,
						goldPriceInThisTime,
						accountCodingSubSystemLocalUserMoneyAssets);
				// *****
				var accountCodingSubSystemLocalWalletChargeFee =
					await accountCodingSubSystemLocalRepository
						.FindAccountCodingSubSystemLocalWalletRechargeFeeAsync(cancellationToken);

				// var accountCodingWalletRechargeFee =
				// 	await FindWalletRechargeFeeIncomeAccountCodingAsync(cancellationToken);

				accountCodingSubSystemLocalWalletChargeFee.AccountCoding
					.SetDocumentPropertiesByAmount
						(walletRechargeFeeAmount, goldPriceInThisTime, accountCodingSubSystemLocalWalletChargeFee);

				accountCodingSubSystemLocalWalletChargeFee.AccountCoding.IsDebtor = true;
				accountCodingSubSystemLocalWalletChargeFee.AccountCoding.IsCreditor = false;
				accountCodingSubSystemLocalWalletChargeFee.AccountCoding.UseParentDocument = true;

				result.Add(accountCodingSubSystemLocalWalletChargeFee.AccountCoding);

				if (roundingDifference != 0)
				{
					var accountCodingSubSystemLocalProfileRoundCode =
						await accountCodingSubSystemLocalRepository
							.FindByRelationIdAndSubSystemLocalNameAndParentAccountCodingAsync(
								profileBank.ProfileId, nameof(ProfileBank.Profile), AccountCoding.UserRoundCode,
								cancellationToken);

					if (accountCodingSubSystemLocalProfileRoundCode is null)
					{
						throw new NullReferenceException(nameof(accountCodingSubSystemLocalProfileRoundCode));
					}
					else
					{
						accountCodingSubSystemLocalProfileRoundCode
							.AccountCoding.SetDocumentPropertiesByAmount(
								roundingDifference,
								goldPriceInThisTime,
								accountCodingSubSystemLocalProfileRoundCode);

						accountCodingSubSystemLocalProfileRoundCode.AccountCoding.IsDebtor = true;
						accountCodingSubSystemLocalProfileRoundCode.AccountCoding.IsCreditor = false;
						accountCodingSubSystemLocalProfileRoundCode.AccountCoding.UseParentDocument = true;

						result.Add(accountCodingSubSystemLocalProfileRoundCode.AccountCoding);
					}
				}
			}

			result.Add(accountCodingSubSystemLocalUserMoneyAssets.AccountCoding);
		}
		// **************************************************

		// **************************************************
		var accountCodingSubSystemLocalProfileBankUser =
			await accountCodingSubSystemLocalRepository
				.FindByRelationIdAndSubSystemLocalNameAndParentAccountCodingAsync(
					profileBank.Id, nameof(ProfileBank), codingUserBankAccount, cancellationToken);

		if (accountCodingSubSystemLocalProfileBankUser is null)
		{
			throw new NullReferenceException(nameof(accountCodingSubSystemLocalProfileBankUser));
		}
		else
		{
			accountCodingSubSystemLocalProfileBankUser
				.AccountCoding.SetDocumentPropertiesByAmount(
					amount,
					goldPriceInThisTime,
					accountCodingSubSystemLocalProfileBankUser);

			accountCodingSubSystemLocalProfileBankUser.AccountCoding.IsDebtor = isDeposit;
			accountCodingSubSystemLocalProfileBankUser.AccountCoding.IsCreditor = !isDeposit;
			accountCodingSubSystemLocalProfileBankUser.AccountCoding.UseParentDocument = false;

			result.Add(accountCodingSubSystemLocalProfileBankUser.AccountCoding);
		}

		// *****
		var accountCodingUserBankTalasootDocument2 =
			accountCodingBankTalasootAccountCodingSubSystemLocal.AccountCoding.Clone();

		accountCodingUserBankTalasootDocument2
			.SetDocumentPropertiesByAmount(amount, goldPriceInThisTime,
				accountCodingBankTalasootAccountCodingSubSystemLocal);

		accountCodingUserBankTalasootDocument2.IsDebtor = !isDeposit;
		accountCodingUserBankTalasootDocument2.IsCreditor = isDeposit;
		accountCodingUserBankTalasootDocument2.UseParentDocument = false;

		result.Add(accountCodingUserBankTalasootDocument2);
		// **************************************************

		return result;
	}

	/// <summary>
	/// دریافت لیست کدینگ مربوط به ثبت سند حسابداری هدیه رفرال و دعوت دوستان
	/// </summary>
	/// <param name="profile">کاربری که قرار است هدیه را دریافت کند</param>
	/// <param name="goldPriceInThisTime"></param>
	/// <param name="cancellationToken"></param>
	/// <returns>لیست کدینگ هایی مناسب ثبت سند برای بخش حسابداری</returns>
	public async Task<List<AccountCoding>> FindAllAccountCodingDataForReferalAsync(
		Profile profile, decimal goldPriceInThisTime, CancellationToken cancellationToken = default)
	{
		IReferralRepository referralRepository =
			new ReferralRepository(DatabaseContext);

		var referalRecord =
			await referralRepository.FindLastRecordAsync(cancellationToken);

		if (referalRecord is null || referalRecord.CanUse() == false)
		{
			string errorMessage = string.Format(
				Resources.Messages.NotFoundError,
				Resources.DataDictionary.ReferalCode);

			throw new Exception(errorMessage);
		}

		var result = new List<AccountCoding>();

		IAccountCodingSubSystemLocalRepository
			accountCodingSubSystemLocalRepository =
				new AccountCodingSubSystemLocalRepository(DatabaseContext);

		var codingUserGoldAssets = AccountCoding.UserGoldAssetsCode;
		// **************************************************
		var accountCodingSubSystemLocalReferal =
			await accountCodingSubSystemLocalRepository
				.FindAccountCodingSubSystemLocalReferalAsync(cancellationToken);


		accountCodingSubSystemLocalReferal.AccountCoding
			.SetDocumentPropertiesByAmount(amount: referalRecord.GoldSoot
				.GoldToToman(goldPriceInThisTime), goldPriceInThisTime, accountCodingSubSystemLocalReferal);

		accountCodingSubSystemLocalReferal.AccountCoding.IsDebtor = false;
		accountCodingSubSystemLocalReferal.AccountCoding.IsCreditor = true;
		accountCodingSubSystemLocalReferal.AccountCoding.UseParentDocument = true;

		result.Add(accountCodingSubSystemLocalReferal.AccountCoding);


		// *****
		var accountCodingSubSystemLocalUserGoldAssets =
			await accountCodingSubSystemLocalRepository
				.FindByRelationIdAndSubSystemLocalNameAndParentAccountCodingAsync(
					profile.Id, nameof(Profile), codingUserGoldAssets, cancellationToken);

		if (accountCodingSubSystemLocalUserGoldAssets is null)
		{
			throw new NullReferenceException(nameof(accountCodingSubSystemLocalUserGoldAssets));
		}
		else
		{
			accountCodingSubSystemLocalUserGoldAssets
				.AccountCoding.SetDocumentPropertiesByAmount
				(referalRecord.GoldSoot.GoldToToman(goldPriceInThisTime),
					goldPriceInThisTime, accountCodingSubSystemLocalUserGoldAssets);

			accountCodingSubSystemLocalUserGoldAssets.AccountCoding.IsDebtor = true;
			accountCodingSubSystemLocalUserGoldAssets.AccountCoding.IsCreditor = false;
			accountCodingSubSystemLocalUserGoldAssets.AccountCoding.UseParentDocument = true;

			result.Add(accountCodingSubSystemLocalUserGoldAssets.AccountCoding);
		}
		// **************************************************

		return result;
	}

	/// <summary>
	/// دریافت لیست کدینگ مربوط به ثبت سند حسابداری خرید طلای آب شده با موجودی کیف پول
	/// </summary>
	/// <param name="profile">پروفایل</param>
	/// <param name="goldSoot">وزن به سوت برای خرید</param>
	/// <param name="goldPriceInThisTime">قیمت لحظه ای طلا</param>
	/// <param name="cancellationToken"></param>
	/// <returns>لیست کدینگ هایی مناسب ثبت سند برای بخش حسابداری</returns>
	public async Task<List<AccountCoding>>
		FindAllAccountCodingDataForGoldPurchaseAsync(
			Profile profile,
			decimal goldSoot,
			decimal goldPriceInThisTime,
			CancellationToken cancellationToken = default)
	{
		var result = new List<AccountCoding>();

		IAccountCodingSubSystemLocalRepository
			accountCodingSubSystemLocalRepository =
				new AccountCodingSubSystemLocalRepository(DatabaseContext);

		IIncomeGoldPurchaseFeeRepository incomeRepository =
			new IncomeGoldPurchaseFeeRepository(DatabaseContext);

		IUserAssetsRepository userAssetsRepository =
			new UserAssetsRepository(DatabaseContext);

		// کارمزد خرید
		var goldPurchaseFee =
			await incomeRepository.FindIncomeAmountAsync(cancellationToken);

		var amount =
			goldSoot.GoldToToman(goldPriceInThisTime);

		// // تبدیل عدد وارد شده به درصد (یعنی 5% از مقدار کل)
		decimal percentage =
			goldPurchaseFee.ConvertToPercentage(); // 5 => 0.05 (برای درصد)

		// محاسبه مقدار کارمزد
		decimal goldPurchaseFeeAmount =
			amount.CalculatePercentageOfAmount(percentage); // مبلغ کارمزد روی قیمت اصلی

		// محاسبه موجودی با کارمزد خرید طلا برای برداشت از کیف پول
		decimal finalWalletAmount = amount + goldPurchaseFeeAmount;

		// رند کردن موجودی به عدد بزرگتر
		decimal roundedFinalAmount = Math.Ceiling(finalWalletAmount);

		// بررسی اینکه موجودی نهایی اعشاری است
		decimal roundingDifference =
			roundedFinalAmount - finalWalletAmount; // اختلاف اعشاری
		// **************************************************

		// **********
		// ثبت مقدار برداشت شده از کیف پول
		var accountCodingSubSystemLocalUserMoneyAssets =
			await accountCodingSubSystemLocalRepository
				.FindByRelationIdAndSubSystemLocalNameAndParentAccountCodingAsync(
					profile.Id, nameof(Profile), AccountCoding.UserMoneyAssetsCode, cancellationToken);

		if (accountCodingSubSystemLocalUserMoneyAssets is null)
		{
			throw new NullReferenceException(nameof(accountCodingSubSystemLocalUserMoneyAssets));
		}
		else
		{
			accountCodingSubSystemLocalUserMoneyAssets.AccountCoding.IsDebtor = false;
			accountCodingSubSystemLocalUserMoneyAssets.AccountCoding.IsCreditor = true;
			accountCodingSubSystemLocalUserMoneyAssets.AccountCoding.UseParentDocument = true;

			accountCodingSubSystemLocalUserMoneyAssets.AccountCoding
				.SetDocumentPropertiesByAmount(
					roundedFinalAmount,
					goldPriceInThisTime,
					accountCodingSubSystemLocalUserMoneyAssets);

			result.Add(accountCodingSubSystemLocalUserMoneyAssets.AccountCoding);
		}
		// **********

		// **********
		// واریز به حساب بانکی طلا سوت
		// اکانت کدینگ ساب سیستم بانک طلاسوت
		var accountCodingSubSystemLocalBankTalasoot =
			await accountCodingSubSystemLocalRepository
				.FindAccountCodingSubSystemLocalBankTalasootAsync(cancellationToken);

		accountCodingSubSystemLocalBankTalasoot.AccountCoding
			.SetDocumentPropertiesByAmount
			(roundedFinalAmount - goldPurchaseFeeAmount - roundingDifference,
				goldPriceInThisTime, accountCodingSubSystemLocalBankTalasoot);

		accountCodingSubSystemLocalBankTalasoot.AccountCoding.IsDebtor = true;
		accountCodingSubSystemLocalBankTalasoot.AccountCoding.IsCreditor = false;
		accountCodingSubSystemLocalBankTalasoot.AccountCoding.UseParentDocument = true;

		result.Add(accountCodingSubSystemLocalBankTalasoot.AccountCoding);

		// **********

		// **********
		// ثبت کارمزد خرید در سند
		var accountCodingSubSystemLocalPurchaseFee =
			await accountCodingSubSystemLocalRepository
				.FindAccountCodingSubSystemLocalGoldPurchaseAsync(cancellationToken);

		accountCodingSubSystemLocalPurchaseFee.AccountCoding.SetDocumentPropertiesByAmount(
			goldPurchaseFeeAmount, goldPriceInThisTime, accountCodingSubSystemLocalPurchaseFee);

		accountCodingSubSystemLocalPurchaseFee.AccountCoding.IsDebtor = true;
		accountCodingSubSystemLocalPurchaseFee.AccountCoding.IsCreditor = false;
		accountCodingSubSystemLocalPurchaseFee.AccountCoding.UseParentDocument = true;

		result.Add(accountCodingSubSystemLocalPurchaseFee.AccountCoding);
		// **********

		// **********
		// اگر مقدار اعشاری داشت اینجا برای کاربر مقدار رند را ذخیره میکنیم
		if (roundingDifference != 0)
		{
			var accountCodingSubSystemLocalProfileRoundCode =
				await accountCodingSubSystemLocalRepository
					.FindByRelationIdAndSubSystemLocalNameAndParentAccountCodingAsync(
						profile.Id, nameof(ProfileBank.Profile), AccountCoding.UserRoundCode,
						cancellationToken);

			if (accountCodingSubSystemLocalProfileRoundCode is null)
			{
				throw new NullReferenceException(nameof(accountCodingSubSystemLocalProfileRoundCode));
			}
			else
			{
				accountCodingSubSystemLocalProfileRoundCode.AccountCoding
					.SetDocumentPropertiesByAmount(
						roundingDifference,
						goldPriceInThisTime,
						accountCodingSubSystemLocalProfileRoundCode);

				accountCodingSubSystemLocalProfileRoundCode.AccountCoding.IsDebtor = true;
				accountCodingSubSystemLocalProfileRoundCode.AccountCoding.IsCreditor = false;
				accountCodingSubSystemLocalProfileRoundCode.AccountCoding.UseParentDocument = true;

				result.Add(accountCodingSubSystemLocalProfileRoundCode.AccountCoding);
			}
		}
		// **********

		// **********
		// خزانه طلای طلاسوت
		var accountCodingSubSystemLocalGoldTreasury =
			await accountCodingSubSystemLocalRepository
				.FindAccountCodingSubSystemLocalGoldTreasuryAsync(cancellationToken);

		accountCodingSubSystemLocalGoldTreasury.AccountCoding.SetDocumentPropertiesByAmount
			(goldSoot.GoldToToman(goldPriceInThisTime), goldPriceInThisTime, accountCodingSubSystemLocalGoldTreasury);

		accountCodingSubSystemLocalGoldTreasury.AccountCoding.IsDebtor = false;
		accountCodingSubSystemLocalGoldTreasury.AccountCoding.IsCreditor = true;
		accountCodingSubSystemLocalGoldTreasury.AccountCoding.UseParentDocument = true;

		result.Add(accountCodingSubSystemLocalGoldTreasury.AccountCoding);
		// **********

		// **********
		// دارایی طلای کاربر
		var accountCodingSubSystemLocalAssetsGoldCode =
			await accountCodingSubSystemLocalRepository
				.FindByRelationIdAndSubSystemLocalNameAndParentAccountCodingAsync(
					profile.Id, nameof(ProfileBank.Profile), AccountCoding.UserGoldAssetsCode,
					cancellationToken);

		if (accountCodingSubSystemLocalAssetsGoldCode is null)
		{
			throw new NullReferenceException(nameof(accountCodingSubSystemLocalAssetsGoldCode));
		}
		else
		{
			accountCodingSubSystemLocalAssetsGoldCode
				.AccountCoding.SetDocumentPropertiesByAmount(
					goldSoot.GoldToToman(goldPriceInThisTime),
					goldPriceInThisTime, accountCodingSubSystemLocalAssetsGoldCode);

			accountCodingSubSystemLocalAssetsGoldCode.AccountCoding.IsDebtor = true;
			accountCodingSubSystemLocalAssetsGoldCode.AccountCoding.IsCreditor = false;
			accountCodingSubSystemLocalAssetsGoldCode.AccountCoding.UseParentDocument = true;

			result.Add(accountCodingSubSystemLocalAssetsGoldCode.AccountCoding);
		}
		// **********
		// **************************************************

		return result;
	}

	/// <summary>
	/// دریافت لیست کدینگ مربوط به ثبت سند حسابداری فروش طلای آب شده با موجودی دارای طلا
	/// </summary>
	/// <param name="profile">پروفایل</param>
	/// <param name="goldSoot">وزن به سوت برای فروش</param>
	/// <param name="goldPriceInThisTime">قیمت لحظه ای طلا</param>
	/// <param name="cancellationToken"></param>
	/// <returns>لیست کدینگ هایی مناسب ثبت سند برای بخش حسابداری</returns>
	public async Task<List<AccountCoding>>
		FindAllAccountCodingDataForSeleOfGoldAsync(
			Profile profile,
			decimal goldSoot,
			decimal goldPriceInThisTime,
			CancellationToken cancellationToken = default)
	{
		var result = new List<AccountCoding>();

		IAccountCodingSubSystemLocalRepository
			accountCodingSubSystemLocalRepository =
				new AccountCodingSubSystemLocalRepository(DatabaseContext);

		IIncomeSaleOfGoldFeeRepository incomeRepository =
			new IncomeSaleOfGoldFeeRepository(DatabaseContext);

		IUserAssetsRepository userAssetsRepository =
			new UserAssetsRepository(DatabaseContext);

		// کارمزد فروش
		var goldOfSaleFeeAmount =
			await incomeRepository.FindIncomeAmountAsync(cancellationToken);

		var amount =
			goldSoot.GoldToToman(goldPriceInThisTime);

		// // تبدیل عدد وارد شده به درصد (یعنی 5% از مقدار کل)
		decimal percentage =
			goldOfSaleFeeAmount.ConvertToPercentage(); // 5 => 0.05 (برای درصد)

		// محاسبه مقدار کارمزد
		decimal goldPurchaseFeeAmount =
			amount.CalculatePercentageOfAmount(percentage); // مبلغ کارمزد روی قیمت اصلی

		// محاسبه موجودی با کسر کارمزد فروش طلا برای واریز به کیف پول
		decimal finalWalletAmount = amount - goldPurchaseFeeAmount;

		// رند کردن موجودی به عدد کوچکتر
		decimal roundedFinalAmount =
			finalWalletAmount - finalWalletAmount.GetFractionalDifference();

		// بررسی اینکه موجودی نهایی اعشاری است
		decimal roundingDifference =
			finalWalletAmount.GetFractionalDifference(); // اختلاف اعشاری
		// **************************************************

		// **********
		// دارایی طلای کاربر
		var accountCodingSubSystemLocalAssetsGoldCode =
			await accountCodingSubSystemLocalRepository
				.FindByRelationIdAndSubSystemLocalNameAndParentAccountCodingAsync(
					profile.Id, nameof(ProfileBank.Profile), AccountCoding.UserGoldAssetsCode,
					cancellationToken);

		if (accountCodingSubSystemLocalAssetsGoldCode is null)
		{
			throw new NullReferenceException(nameof(accountCodingSubSystemLocalAssetsGoldCode));
		}
		else
		{
			accountCodingSubSystemLocalAssetsGoldCode
				.AccountCoding.SetDocumentPropertiesByAmount(
					goldSoot.GoldToToman(goldPriceInThisTime),
					goldPriceInThisTime,
					accountCodingSubSystemLocalAssetsGoldCode);

			accountCodingSubSystemLocalAssetsGoldCode
					.AccountCoding
					.SubSystemLocalId =
				accountCodingSubSystemLocalAssetsGoldCode.SubSystemLocalId;

			accountCodingSubSystemLocalAssetsGoldCode
					.AccountCoding
					.RelationId =
				accountCodingSubSystemLocalAssetsGoldCode.RelationId;

			accountCodingSubSystemLocalAssetsGoldCode.AccountCoding.IsDebtor = false;
			accountCodingSubSystemLocalAssetsGoldCode.AccountCoding.IsCreditor = true;
			accountCodingSubSystemLocalAssetsGoldCode.AccountCoding.UseParentDocument = true;

			result.Add(accountCodingSubSystemLocalAssetsGoldCode.AccountCoding);
		}
		// **********

		// **********
		// خزانه طلای طلاسوت
		var accountCodingSubSystemLocalGoldTreasury =
			await accountCodingSubSystemLocalRepository
				.FindAccountCodingSubSystemLocalGoldTreasuryAsync(cancellationToken);

		accountCodingSubSystemLocalGoldTreasury.AccountCoding.SetDocumentPropertiesByAmount
			(goldSoot.GoldToToman(goldPriceInThisTime), goldPriceInThisTime, accountCodingSubSystemLocalGoldTreasury);

		accountCodingSubSystemLocalGoldTreasury.AccountCoding.IsDebtor = true;
		accountCodingSubSystemLocalGoldTreasury.AccountCoding.IsCreditor = false;
		accountCodingSubSystemLocalGoldTreasury.AccountCoding.UseParentDocument = true;

		result.Add(accountCodingSubSystemLocalGoldTreasury.AccountCoding);
		// **********

		// **********
		// برداشت از حساب بانکی طلا سوت
		var accountCodingSubSystemLocalBankTalaSoot =
			await accountCodingSubSystemLocalRepository
				.FindAccountCodingSubSystemLocalBankTalasootAsync(cancellationToken);

		accountCodingSubSystemLocalBankTalaSoot.AccountCoding.SetDocumentPropertiesByAmount
			(goldSoot.GoldToToman(goldPriceInThisTime), goldPriceInThisTime, accountCodingSubSystemLocalBankTalaSoot);

		accountCodingSubSystemLocalBankTalaSoot.AccountCoding.IsDebtor = false;
		accountCodingSubSystemLocalBankTalaSoot.AccountCoding.IsCreditor = true;
		accountCodingSubSystemLocalBankTalaSoot.AccountCoding.UseParentDocument = true;

		result.Add(accountCodingSubSystemLocalBankTalaSoot.AccountCoding);
		// **********

		// **********
		// ثبت مقدار واریز شده به کیف پول
		var accountCodingSubSystemLocalUserMoneyAssets =
			await accountCodingSubSystemLocalRepository
				.FindByRelationIdAndSubSystemLocalNameAndParentAccountCodingAsync(
					profile.Id, nameof(Profile), AccountCoding.UserMoneyAssetsCode, cancellationToken);

		if (accountCodingSubSystemLocalUserMoneyAssets is null)
		{
			throw new NullReferenceException(nameof(accountCodingSubSystemLocalUserMoneyAssets));
		}
		else
		{
			accountCodingSubSystemLocalUserMoneyAssets.AccountCoding.IsDebtor = true;
			accountCodingSubSystemLocalUserMoneyAssets.AccountCoding.IsCreditor = false;
			accountCodingSubSystemLocalUserMoneyAssets.AccountCoding.UseParentDocument = true;

			accountCodingSubSystemLocalUserMoneyAssets
				.AccountCoding
				.SetDocumentPropertiesByAmount(
					roundedFinalAmount,
					goldPriceInThisTime,
					accountCodingSubSystemLocalUserMoneyAssets);

			result.Add(accountCodingSubSystemLocalUserMoneyAssets.AccountCoding);
		}
		// **********
		
		// **********
		// ثبت کارمزد فروش در سند
		var accountCodingSubSystemLocalGoldOfSaleFeeAmount =
			await accountCodingSubSystemLocalRepository
				.FindAccountCodingSubSystemLocalGoldOfSaleFeeAsync(cancellationToken);

		accountCodingSubSystemLocalGoldOfSaleFeeAmount.AccountCoding.SetDocumentPropertiesByAmount(
			goldPurchaseFeeAmount, goldPriceInThisTime, accountCodingSubSystemLocalGoldOfSaleFeeAmount);

		accountCodingSubSystemLocalGoldOfSaleFeeAmount.AccountCoding.IsDebtor = true;
		accountCodingSubSystemLocalGoldOfSaleFeeAmount.AccountCoding.IsCreditor = false;
		accountCodingSubSystemLocalGoldOfSaleFeeAmount.AccountCoding.UseParentDocument = true;

		result.Add(accountCodingSubSystemLocalGoldOfSaleFeeAmount.AccountCoding);
		// **********

		// **********
		// اگر مقدار اعشاری داشت اینجا برای کاربر مقدار رند را ذخیره میکنیم
		if (roundingDifference != 0)
		{
			var accountCodingSubSystemLocalProfileRoundCode =
				await accountCodingSubSystemLocalRepository
					.FindByRelationIdAndSubSystemLocalNameAndParentAccountCodingAsync(
						profile.Id, nameof(ProfileBank.Profile), AccountCoding.UserRoundCode,
						cancellationToken);

			if (accountCodingSubSystemLocalProfileRoundCode is null)
			{
				throw new NullReferenceException(nameof(accountCodingSubSystemLocalProfileRoundCode));
			}
			else
			{
				accountCodingSubSystemLocalProfileRoundCode.AccountCoding
					.SetDocumentPropertiesByAmount(
						roundingDifference,
						goldPriceInThisTime,
						accountCodingSubSystemLocalProfileRoundCode);

				accountCodingSubSystemLocalProfileRoundCode.AccountCoding.IsDebtor = true;
				accountCodingSubSystemLocalProfileRoundCode.AccountCoding.IsCreditor = false;
				accountCodingSubSystemLocalProfileRoundCode.AccountCoding.UseParentDocument = true;

				result.Add(accountCodingSubSystemLocalProfileRoundCode.AccountCoding);
			}
		}
		// **********
		// **************************************************

		return result;
	}


	/// <summary>
	/// Checks and creates a subsystem account coding entry as required.
	/// </summary>
	/// <param name="relationId">The identifier for the related entity, which links this subsystem account coding.</param>
	/// <param name="domainName">The domain name associated with the subsystem.</param>
	/// <param name="code">The account coding identifier to be checked or created.</param>
	/// <param name="charCount">The character count for the account coding, used if a new coding is created.</param>
	/// <param name="isCreate">Specifies whether to create a new entry if one does not exist.</param>
	/// <param name="newAccountCodingName">new name for when isCreate equal true</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	/// <exception cref="NullReferenceException">Thrown when the specified account coding does not exist.</exception>
	/// <exception cref="ArgumentNullException">Thrown when the specified subsystem local cannot be found.</exception>
	private async Task<AccountCoding?> CheckAndCreateSubSystemAccountCodingAsync(
		string relationId, string domainName, string code, int charCount, bool isCreate = true,
		string? newAccountCodingName = null,
		CancellationToken cancellationToken = default)
	{
		IAccountCodingSubSystemLocalRepository
			accountCodingSubSystemLocalRepository =
				new AccountCodingSubSystemLocalRepository(DatabaseContext);

		ISubSystemLocalRepository
			subSystemLocalRepository =
				new SubSystemLocalRepository(DatabaseContext);

		var findAccountCoding = await FindByCodeAsync(code, cancellationToken);

		if (findAccountCoding is null)
		{
			throw new NullReferenceException(nameof(findAccountCoding));
		}

		if (isCreate == true && string.IsNullOrEmpty(newAccountCodingName) == true)
		{
			throw new NullReferenceException(nameof(newAccountCodingName));
		}

		var findSubSystemLocal =
			await subSystemLocalRepository
				.FindByNameAsync(domainName, cancellationToken);

		if (findSubSystemLocal == null)
		{
			throw new ArgumentNullException(nameof(findSubSystemLocal));
		}

		if (isCreate == true)
		{
			var newCoding =
				await GenerateAndAddNextCodeAsync(code, newAccountCodingName!, charCount, cancellationToken);

			var accountCodingSubSystemLocal =
				new AccountCodingSubSystemLocal
				{
					IsActive = true,
					IsDeleted = false,
					Ordering = 100_000,

					RelationId = relationId,
					AccountCodingId = newCoding.Id,
					SubSystemLocalId = findSubSystemLocal.Id,
				};

			var result = await accountCodingSubSystemLocalRepository
				.AddAsync(accountCodingSubSystemLocal, cancellationToken);

			if (result.IsSuccess == true)
			{
				return newCoding;
			}

			return null;
		}
		else
		{
			var accountCodingSubSystemLocal =
				new AccountCodingSubSystemLocal
				{
					IsActive = true,
					IsDeleted = false,
					Ordering = 100_000,

					RelationId = relationId,
					AccountCodingId = findAccountCoding.Id,
					SubSystemLocalId = findSubSystemLocal.Id,
				};

			var result = await accountCodingSubSystemLocalRepository
				.AddAsync(accountCodingSubSystemLocal, cancellationToken);

			if (result.IsSuccess == true)
			{
				return findAccountCoding;
			}

			return null;
		}
	}

	/// <summary>
	/// تولید و افزودن کد بعدی برای یک حساب.
	/// </summary>
	/// <param name="parentCode">کد والد که بر اساس آن کد جدید تولید می‌شود.</param>
	/// <param name="name">نام حساب برای کد جدید.</param>
	/// <param name="countChars">تعداد کاراکترهای کد جدید.</param>
	/// <param name="cancellationToken">توکن لغو عملیات.</param>
	/// <returns>یک شیء AccountCoding که نماینده کد جدید ایجادشده است.</returns>
	/// <exception cref="ArgumentNullException">در صورتی که والد با کد مشخص‌شده یافت نشود.</exception>
	private async Task<AccountCoding> GenerateAndAddNextCodeAsync(
		string parentCode, string name, int countChars, CancellationToken cancellationToken = default)
	{
		// دریافت بزرگ‌ترین کد زیرمجموعه
		var lastAccountCoding =
			await FindLastAccountCodeByParentCodeAsync(parentCode, cancellationToken);

		var parent =
			await FindByCodeAsync(parentCode, cancellationToken);

		if (parent == null)
		{
			throw new ArgumentNullException(nameof(parent));
		}

		string newCode;

		if (lastAccountCoding is null)
		{
			// اگر زیرمجموعه‌ای وجود ندارد، کد اولیه تولید شود
			newCode = parentCode + 1.ToString().PadLeft(countChars, paddingChar: '0');
		}
		else
		{
			// اگر زیرمجموعه‌ای وجود دارد، کد جدید بر اساس عدد آخر تولید شود
			// حذف کد والد از کد آخر
			var numericPart = lastAccountCoding.Code.Substring(parentCode.Length);

			var nextNumber =
				(long.Parse(numericPart) + 1).ToString()
				.PadLeft(numericPart.Length, '0');

			newCode = parentCode + nextNumber;
		}

		// ایجاد رکورد جدید
		var newAccountCoding = new AccountCoding
		{
			IsActive = true,
			IsDeleted = false,

			Name = name,
			Code = newCode,
			ParentId = parent.Id,
		};

		await AddAsync(newAccountCoding, cancellationToken);

		return newAccountCoding;
	}

	/// <summary>
	/// پیدا کردن آخرین کد اکانت که با کد والد شروع می‌شود
	/// </summary>
	/// <param name="parentCode">کد والد که کدهای زیرمجموعه با آن شروع می‌شوند</param>
	/// <param name="cancellationToken">توکن لغو عملیات</param>
	/// <returns>آخرین کد اکانت مطابق با معیارهای مشخص‌شده، یا مقدار null اگر هیچ کدی یافت نشود</returns>
	public async Task<AccountCoding?> FindLastAccountCodeByParentCodeAsync(
		string parentCode, CancellationToken cancellationToken = default)
	{
		// پیدا کردن بزرگترین کدی که با کد والد شروع می‌شود
		var result = await DbSet
			.Where(current => current.IsActive == true)
			.Where(current => current.IsDeleted == false)
			.Where(current => current.Code.StartsWith(parentCode))
			.Where(current => current.Code.Length > parentCode.Length)
			.OrderByDescending(current => current.Code)
			.FirstOrDefaultAsync(cancellationToken);

		return result;
	}
}