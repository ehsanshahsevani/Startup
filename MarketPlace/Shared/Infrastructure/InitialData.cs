using Domain;
using Persistence;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public class InitialData : object
{
	#region Settings

	public InitialData(
		IConfiguration configuration,
		IUnitOfWork unitOfWork) : base()
	{
		UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
	}

	private IConfiguration Configuration { get; }
	private IUnitOfWork UnitOfWork { get; }

	#endregion

	#region TalaSootSettings

	/// <summary>
	/// ثبت قوانین اولیه طلاسوت
	/// </summary>
	/// <returns></returns>
	public async Task CreateTalaSootSettingsAsync()
	{
		var firstRecord = await UnitOfWork
			.TalaSootSettingsRepository.FindFirstRecordAsync();

		if (firstRecord is null)
		{
			firstRecord = new TalaSootSettings()
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				Description =
					"تنظیمات سیستم طلا سوت - این جدول همیشه دارای یک رکورد است و آخرید رکورد مورد بررسی قرار میگیرد",

				BuyFromTheStore = false,
				WithdrawalFromWallet = false,
				SendGoldDeliveryRequest = false,
				WithdrawalAndDepositFromWallet = false,
				ClosingSaleAndPurchaseMoltenGold = false,
			};

			await UnitOfWork
				.TalaSootSettingsRepository.AddAsync(firstRecord);

			await UnitOfWork.SaveAsync();
		}
	}
	
	/// <summary>
	/// اولین حساب بانکی تستی با اطلاعات فیک به صورت حساب غییر فعال
	/// </summary>
	public async Task CreateTalaSootBankAccountAsync()
	{
		var firstRecord = await UnitOfWork
			.TalaSootBankAccountRepository.FindFirstRecordAsync();

		if (firstRecord is null)
		{
			firstRecord = new TalaSootBankAccount
			{
				IsActive = false,
				IsDeleted = false,
				Ordering = 100_000,

				Description =
					"حساب بانکی طلا سوت به عنوان پیشفرض و غییر فعال در سیستم ثبت میشود",
				
				Amount = 0,
				AccountNumber = "0000000000",
				CardNumber = "0000000000000000",
				Shaba = "IR000000000000000000000000",
				BankName = "بانک کشاورزی",
				FullName = "کارت بانکی تستی"
			};

			await UnitOfWork
				.TalaSootBankAccountRepository.AddAsync(firstRecord);

			await UnitOfWork.SaveAsync();
		}
	}
	
	/// <summary>
	/// اولین خزانه تستی
	/// </summary>
	public async Task CreateGoldTreasuryAsync()
	{
		var firstRecordOnline = await UnitOfWork
			.GoldTreasuryOnlineRepository.FindFirstRecordAsync();

		if (firstRecordOnline is null)
		{
			firstRecordOnline =
				new GoldTreasuryOnline(amount: 100000)
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,

				Description =
					"اولین رکورد خزانه طلا آنلاین به صورت سیستمی و اتوماتیک ثبت شده است",
			};

			await UnitOfWork
				.GoldTreasuryOnlineRepository.AddAsync(firstRecordOnline);

			await UnitOfWork.SaveAsync();
		}
		
		var firstRecordReceive = await UnitOfWork
			.GoldTreasuryReceiveRepository.FindFirstRecordAsync();

		if (firstRecordReceive is null)
		{
			firstRecordReceive = new GoldTreasuryReceive(amount: 1000)
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,

				Description =
					"اولین رکورد خزانه طلا تحویل فیزیکی به صورت سیستمی و اتوماتیک ثبت شده است",
			};

			await UnitOfWork
				.GoldTreasuryReceiveRepository.AddAsync(firstRecordReceive);

			await UnitOfWork.SaveAsync();
		}
	}

	/// <summary>
	/// ثبت کارمزدهای اولیه طلاسوت
	/// </summary>
	/// <returns></returns>
	public async Task CreateTalaSootFeeAsync(
		decimal walletRechargeFee = 0,
		decimal maintenanceAndInsuranceFee = 0,
		decimal goldPurchaseFee = 0,
		decimal incomeSaleOfGoldFee = 0,
		decimal incomeCommissionFee = 0)
	{
		// **************************************************
		// walletRechargeFee
		var walletRechargeFeeRecord = await UnitOfWork
			.IncomeWalletRechargeFeeRepository.FindIncomeAsync();

		if (walletRechargeFeeRecord is null)
		{
			walletRechargeFeeRecord = new IncomeWalletRechargeFee(walletRechargeFee)
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				Description =
					"کارمزد شارژ کیف پول - این جدول همیشه دارای یک رکورد است و اولین رکورد مورد بررسی قرار میگیرد",
			};

			await UnitOfWork
				.IncomeWalletRechargeFeeRepository.AddAsync(walletRechargeFeeRecord);

			await UnitOfWork.SaveAsync();
		}
		// **************************************************
		
		// **************************************************
		// maintenanceAndInsuranceFee
		var maintenanceAndInsuranceFeeRecord = await UnitOfWork
			.IncomeMaintenanceAndInsuranceFeeRepository.FindIncomeAsync();

		if (maintenanceAndInsuranceFeeRecord is null)
		{
			maintenanceAndInsuranceFeeRecord = new IncomeMaintenanceAndInsuranceFee(maintenanceAndInsuranceFee)
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				Description =
					"کارمزد نگهداری و بیمه - این جدول همیشه دارای یک رکورد است و اولین رکورد مورد بررسی قرار میگیرد",
			};

			await UnitOfWork
				.IncomeMaintenanceAndInsuranceFeeRepository.AddAsync(maintenanceAndInsuranceFeeRecord);

			await UnitOfWork.SaveAsync();
		}
		// **************************************************
		
		// **************************************************
		// goldPurchaseFee
		var goldPurchaseFeeRecord = await UnitOfWork
			.IncomeGoldPurchaseFeeRepository.FindIncomeAsync();

		if (goldPurchaseFeeRecord is null)
		{
			goldPurchaseFeeRecord = new IncomeGoldPurchaseFee(goldPurchaseFee)
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				Description =
					"کارمزد خرید طلای آنلاین - این جدول همیشه دارای یک رکورد است و اولین رکورد مورد بررسی قرار میگیرد",
			};

			await UnitOfWork
				.IncomeGoldPurchaseFeeRepository.AddAsync(goldPurchaseFeeRecord);

			await UnitOfWork.SaveAsync();
		}
		// **************************************************
		
		// **************************************************
		// incomeSaleOfGoldFee
		var incomeSaleOfGoldFeeRecord = await UnitOfWork
			.IncomeSaleOfGoldFeeRepository.FindIncomeAsync();

		if (incomeSaleOfGoldFeeRecord is null)
		{
			incomeSaleOfGoldFeeRecord = new IncomeSaleOfGoldFee(incomeSaleOfGoldFee)
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				Description =
					"کارمزد فروش طلای آنلاین - این جدول همیشه دارای یک رکورد است و اولین رکورد مورد بررسی قرار میگیرد",
			};

			await UnitOfWork
				.IncomeSaleOfGoldFeeRepository.AddAsync(incomeSaleOfGoldFeeRecord);

			await UnitOfWork.SaveAsync();
		}
		// **************************************************
		
		// **************************************************
		// incomeCommissionFee
		var incomeCommissionFeeRecord = await UnitOfWork
			.IncomeCommissionFeeRepository.FindIncomeAsync();

		if (incomeCommissionFeeRecord is null)
		{
			incomeCommissionFeeRecord = new IncomeCommissionFee(incomeCommissionFee)
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				Description =
					"کارمزد فروش طلای آنلاین - این جدول همیشه دارای یک رکورد است و اولین رکورد مورد بررسی قرار میگیرد",
			};

			await UnitOfWork
				.IncomeCommissionFeeRepository.AddAsync(incomeCommissionFeeRecord);

			await UnitOfWork.SaveAsync();
		}
		// **************************************************
	}
	#endregion /TalaSootSettings

	#region TypeRoleGold - TypeRoleMoney

	public async Task CreateTypeRoleGoldAsync()
	{
		IEnumerable<TypeRoleGold?>? result =
			await UnitOfWork.TypeRoleGoldRepository.GetAllAsync();

		var typeRoleGoldBoth = new TypeRoleGold
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,
			Description = "خرید و فروش",

			Code = nameof(Resources.DataDictionary.TypeRoleBoth),
			Name = Resources.DataDictionary.TypeRoleBoth,
		};

		if (result.Any(p => p?.Code == typeRoleGoldBoth.Code) == false)
		{
			await UnitOfWork.TypeRoleGoldRepository.AddAsync(typeRoleGoldBoth);
			await UnitOfWork.SaveAsync();
		}

		var typeRoleGoldBuy = new TypeRoleGold
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,
			Description = Resources.DataDictionary.TypeRoleGoldBuy,

			Code = nameof(Resources.DataDictionary.TypeRoleGoldBuy),
			Name = Resources.DataDictionary.TypeRoleGoldBuy,
		};

		if (result.Any(p => p?.Code == typeRoleGoldBuy.Code) == false)
		{
			await UnitOfWork.TypeRoleGoldRepository.AddAsync(typeRoleGoldBuy);
			await UnitOfWork.SaveAsync();
		}

		var typeRoleGoldSell = new TypeRoleGold
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,
			Description = Resources.DataDictionary.TypeRoleGoldSell,

			Code = nameof(Resources.DataDictionary.TypeRoleGoldSell),
			Name = Resources.DataDictionary.TypeRoleGoldSell,
		};

		if (result.Any(p => p?.Code == typeRoleGoldSell.Code) == false)
		{
			await UnitOfWork.TypeRoleGoldRepository.AddAsync(typeRoleGoldSell);
			await UnitOfWork.SaveAsync();
		}
	}

	public async Task CreateTypeRoleMoneyAsync()
	{
		IEnumerable<TypeRoleMoney?>? result =
			await UnitOfWork.TypeRoleMoneyRepository.GetAllAsync();

		var typeRoleMoneyBoth = new TypeRoleMoney
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,
			Description = "واریز و برداشت",

			Code = nameof(Resources.DataDictionary.TypeRoleBoth),
			Name = Resources.DataDictionary.TypeRoleBoth,
		};

		if (result.Any(p => p?.Code == typeRoleMoneyBoth.Code) == false)
		{
			await UnitOfWork.TypeRoleMoneyRepository.AddAsync(typeRoleMoneyBoth);
			await UnitOfWork.SaveAsync();
		}

		var typeRoleMoneyDeposit = new TypeRoleMoney
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,
			Description = "واریز",

			Code = nameof(Resources.DataDictionary.TypeRoleMoneyDeposit),
			Name = Resources.DataDictionary.TypeRoleMoneyDeposit,
		};

		if (result.Any(p => p?.Code == typeRoleMoneyDeposit.Code) == false)
		{
			await UnitOfWork.TypeRoleMoneyRepository.AddAsync(typeRoleMoneyDeposit);
			await UnitOfWork.SaveAsync();
		}

		var typeRoleMoneyWithdrawal = new TypeRoleMoney
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,
			Description = "برداشت",

			Code = nameof(Resources.DataDictionary.TypeRoleMoneyWithdrawal),
			Name = Resources.DataDictionary.TypeRoleMoneyWithdrawal,
		};

		if (result.Any(p => p?.Code == typeRoleMoneyWithdrawal.Code) == false)
		{
			await UnitOfWork.TypeRoleMoneyRepository.AddAsync(typeRoleMoneyWithdrawal);
			await UnitOfWork.SaveAsync();
		}
	}

	#endregion /TypeRoleGold - TypeRoleMoney

	#region AccountCoding

	public async Task CreateAccountCodingAsync()
	{
		// **************************************************
		string talasootCode = "01";

		var startCode = await UnitOfWork
			.AccountCodingRepository.FindByCodeAsync(talasootCode);

		if (startCode is null)
		{
			startCode = new AccountCoding
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,

				Code = talasootCode,
				Name = Domain.AccountCoding.AccountCodings[talasootCode],
			};

			await UnitOfWork
				.AccountCodingRepository.AddAsync(startCode);
		}
		// **************************************************

		// **************************************************
		string assetsCode = "011";

		var assetsCodeSearch = await UnitOfWork
			.AccountCodingRepository.FindByCodeAsync(assetsCode);

		if (assetsCodeSearch is null)
		{
			assetsCodeSearch = new AccountCoding
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,

				ParentId = startCode.Id,
				
				Code = assetsCode,
				Name = Domain.AccountCoding.AccountCodings[assetsCode],
			};

			await UnitOfWork
				.AccountCodingRepository.AddAsync(assetsCodeSearch);
		}
		// **************************************************

		// **************************************************
		string assetsCodeAccountBank = "01101";

		var assetsassetsCodeAccountBankTalasoot =
			await UnitOfWork.AccountCodingRepository.FindByCodeAsync(assetsCodeAccountBank);

		if (assetsassetsCodeAccountBankTalasoot is null)
		{
			assetsassetsCodeAccountBankTalasoot = new AccountCoding
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				
				ParentId = assetsCodeSearch.Id,

				Code = assetsCodeAccountBank,
				Name = Domain.AccountCoding.AccountCodings[assetsCodeAccountBank],
			};

			await UnitOfWork
				.AccountCodingRepository.AddAsync(assetsassetsCodeAccountBankTalasoot);
		}
		// **************************************************

		// **************************************************
		string talaSootBankAccountCode =
			Domain.AccountCoding.TalaSootBankAccountCode;

		var searchTalaSootBankAccountCode =
			await UnitOfWork.AccountCodingRepository.FindByCodeAsync(talaSootBankAccountCode);

		if (searchTalaSootBankAccountCode is null)
		{
			searchTalaSootBankAccountCode = new AccountCoding
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				
				ParentId = assetsassetsCodeAccountBankTalasoot.Id,

				Code = talaSootBankAccountCode,
				Name = Domain.AccountCoding.AccountCodings[talaSootBankAccountCode],
			};

			await UnitOfWork
				.AccountCodingRepository.AddAsync(searchTalaSootBankAccountCode);
		}
		// **************************************************
		
		// **************************************************
		string userMoneyAssetsCode =
			Domain.AccountCoding.UserMoneyAssetsCode;

		var searchUserMoneyAssetsCode =
			await UnitOfWork.AccountCodingRepository.FindByCodeAsync(userMoneyAssetsCode);

		if (searchUserMoneyAssetsCode is null)
		{
			searchUserMoneyAssetsCode = new AccountCoding
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				
				ParentId = assetsCodeSearch.Id,

				Code = userMoneyAssetsCode,
				Name = Domain.AccountCoding.AccountCodings[userMoneyAssetsCode],
			};

			await UnitOfWork
				.AccountCodingRepository.AddAsync(searchUserMoneyAssetsCode);
		}
		// **************************************************
		
		// **************************************************
		string userGoldAssetsCode =
			Domain.AccountCoding.UserGoldAssetsCode;

		var searchUserGoldAssetsCode =
			await UnitOfWork.AccountCodingRepository.FindByCodeAsync(userGoldAssetsCode);

		if (searchUserGoldAssetsCode is null)
		{
			searchUserGoldAssetsCode = new AccountCoding
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				
				ParentId = assetsCodeSearch.Id,

				Code = userGoldAssetsCode,
				Name = Domain.AccountCoding.AccountCodings[userGoldAssetsCode],
			};

			await UnitOfWork
				.AccountCodingRepository.AddAsync(searchUserGoldAssetsCode);
		}
		// **************************************************
		
		// **************************************************
		string userBankAccountCode =
			Domain.AccountCoding.UserBankAccountCode;

		var searchUserBankAccountCode =
			await UnitOfWork.AccountCodingRepository.FindByCodeAsync(userBankAccountCode);

		if (searchUserBankAccountCode is null)
		{
			searchUserBankAccountCode = new AccountCoding
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,

				ParentId = assetsCodeSearch.Id,
				
				Code = userBankAccountCode,
				Name = Domain.AccountCoding.AccountCodings[userBankAccountCode],
			};

			await UnitOfWork
				.AccountCodingRepository.AddAsync(searchUserBankAccountCode);
		}
		// **************************************************
		
		// **************************************************
		string goldTreasuryCode =
			Domain.AccountCoding.GoldTreasuryCode;

		var searchGoldTreasuryCode =
			await UnitOfWork.AccountCodingRepository.FindByCodeAsync(goldTreasuryCode);

		if (searchGoldTreasuryCode is null)
		{
			searchGoldTreasuryCode = new AccountCoding
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				
				ParentId = assetsCodeSearch.Id,

				Code = goldTreasuryCode,
				Name = Domain.AccountCoding.AccountCodings[goldTreasuryCode],
			};

			await UnitOfWork
				.AccountCodingRepository.AddAsync(searchGoldTreasuryCode);
		}
		// **************************************************
		
		// **************************************************
		string userRoundCode =
			Domain.AccountCoding.UserRoundCode;

		var searchUserRoundCode =
			await UnitOfWork.AccountCodingRepository.FindByCodeAsync(userRoundCode);

		if (searchUserRoundCode is null)
		{
			searchUserRoundCode = new AccountCoding
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				
				ParentId = assetsCodeSearch.Id,

				Code = userRoundCode,
				Name = Domain.AccountCoding.AccountCodings[userRoundCode],
			};

			await UnitOfWork
				.AccountCodingRepository.AddAsync(searchUserRoundCode);
		}
		// **************************************************
		
		// **************************************************
		string assetsParent = "012";

		var searchAssetsParent =
			await UnitOfWork.AccountCodingRepository.FindByCodeAsync(assetsParent);

		if (searchAssetsParent is null)
		{
			searchAssetsParent = new AccountCoding
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,

				ParentId = startCode.Id,
				
				Code = assetsParent,
				Name = Domain.AccountCoding.AccountCodings[assetsParent],
			};

			await UnitOfWork
				.AccountCodingRepository.AddAsync(searchAssetsParent);
		}
		// **************************************************

		// **************************************************
		// کارمزد خرید طلا
		string assetsFeeBuyAndSellGold = 
			AccountCoding.GoldPurchaseFee;

		var searchAssetsFeeBuyAndSellGold =
			await UnitOfWork.AccountCodingRepository.FindByCodeAsync(assetsFeeBuyAndSellGold);

		if (searchAssetsFeeBuyAndSellGold is null)
		{
			searchAssetsFeeBuyAndSellGold = new AccountCoding
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,

				ParentId = searchAssetsParent.Id,
				
				Code = assetsFeeBuyAndSellGold,
				Name = Domain.AccountCoding.AccountCodings[assetsFeeBuyAndSellGold],
			};

			await UnitOfWork
				.AccountCodingRepository.AddAsync(searchAssetsFeeBuyAndSellGold);
		}
		// **************************************************

		// **************************************************
		// کارمزد شارژ کیف پول
		string assetsFeeAssetsMoney =
			AccountCoding.WalletRechargeFeeIncome;

		var searchAssetsFeeAssetsMoney =
			await UnitOfWork.AccountCodingRepository.FindByCodeAsync(assetsFeeAssetsMoney);

		if (searchAssetsFeeAssetsMoney is null)
		{
			searchAssetsFeeAssetsMoney = new AccountCoding
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,

				ParentId = searchAssetsParent.Id,
				
				Code = assetsFeeAssetsMoney,
				Name = Domain.AccountCoding.AccountCodings[assetsFeeAssetsMoney],
			};

			await UnitOfWork
				.AccountCodingRepository.AddAsync(searchAssetsFeeAssetsMoney);
		}
		// **************************************************
		
		// **************************************************
		// کارمزد نگهداری طلا
		string assetsFeeKeepAssetsGold =
			AccountCoding.GoldMaintenanceFeeCode;

		var searchAssetsFeeKeepAssetsGold =
			await UnitOfWork.AccountCodingRepository.FindByCodeAsync(assetsFeeKeepAssetsGold);

		if (searchAssetsFeeKeepAssetsGold is null)
		{
			searchAssetsFeeKeepAssetsGold = new AccountCoding
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,

				ParentId = searchAssetsParent.Id,
				
				Code = assetsFeeKeepAssetsGold,
				Name = Domain.AccountCoding.AccountCodings[assetsFeeKeepAssetsGold],
			};

			await UnitOfWork
				.AccountCodingRepository.AddAsync(searchAssetsFeeKeepAssetsGold);
		}
		// **************************************************
		
		// **************************************************
		// کارمزد فروش طلا
		string assetsFeeSellingAssetsGold =
			AccountCoding.IncomeSaleOfGoldCode;

		var searchAssetsFeeSellingAssetsGold =
			await UnitOfWork.AccountCodingRepository.FindByCodeAsync(assetsFeeSellingAssetsGold);

		if (searchAssetsFeeSellingAssetsGold is null)
		{
			searchAssetsFeeSellingAssetsGold = new AccountCoding
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,

				ParentId = searchAssetsParent.Id,
				
				Code = assetsFeeSellingAssetsGold,
				Name = Domain.AccountCoding.AccountCodings[assetsFeeSellingAssetsGold],
			};

			await UnitOfWork
				.AccountCodingRepository.AddAsync(searchAssetsFeeSellingAssetsGold);
		}
		// **************************************************
		
		// **************************************************
		// درآمد خرید محصول از فروشگاه
		string incomeSellingGoods =
			AccountCoding.IncomeSellingGoods;

		var searchIncomeSellingGoods =
			await UnitOfWork.AccountCodingRepository.FindByCodeAsync(incomeSellingGoods);

		if (searchIncomeSellingGoods is null)
		{
			searchIncomeSellingGoods = new AccountCoding
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,

				ParentId = searchAssetsParent.Id,
				
				Code = incomeSellingGoods,
				Name = Domain.AccountCoding.AccountCodings[incomeSellingGoods],
			};

			await UnitOfWork
				.AccountCodingRepository.AddAsync(searchIncomeSellingGoods);
		}
		// **************************************************
		
		// **************************************************
		// کمسیون
		string incomeCommissionCode =
			AccountCoding.IncomeCommissionCode;

		var searchIncomeCommissionCode =
			await UnitOfWork.AccountCodingRepository.FindByCodeAsync(incomeCommissionCode);

		if (searchIncomeCommissionCode is null)
		{
			searchIncomeCommissionCode = new AccountCoding
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,

				ParentId = searchAssetsParent.Id,
				
				Code = incomeCommissionCode,
				Name = Domain.AccountCoding.AccountCodings[incomeCommissionCode],
			};

			await UnitOfWork
				.AccountCodingRepository.AddAsync(searchIncomeCommissionCode);
		}
		// **************************************************
		
		// **************************************************
		string cost = "013";

		var searchCost =
			await UnitOfWork.AccountCodingRepository.FindByCodeAsync(cost);

		if (searchCost is null)
		{
			searchCost = new AccountCoding
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,

				ParentId = startCode.Id,
				
				Code = cost,
				Name = Domain.AccountCoding.AccountCodings[cost],
			};

			await UnitOfWork
				.AccountCodingRepository.AddAsync(searchCost);
		}
		// **************************************************
		
		// **************************************************
		var referalCode = Domain.AccountCoding.ReferalCode;

		var searchReferalCode =
			await UnitOfWork.AccountCodingRepository.FindByCodeAsync(referalCode);

		if (searchReferalCode is null)
		{
			searchReferalCode = new AccountCoding
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,

				ParentId = searchCost.Id,
				
				Code = referalCode,
				Name = Domain.AccountCoding.AccountCodings[referalCode],
			};

			await UnitOfWork
				.AccountCodingRepository.AddAsync(searchReferalCode);
		}
		// **************************************************
		
		await UnitOfWork.SaveAsync();
	}
	
	#endregion /AccountCoding
	
	// **************************************************
	public async Task CreateTagPageSettingsAsync()
	{
		var faqTag = await UnitOfWork
			.TagPageSettingRepository.FindByNameAsync(nameof(Resources.DataDictionary.Faq));

		if (faqTag is null)
		{
			faqTag = new TagPageSetting(nameEn: nameof(Resources.DataDictionary.Faq), nameFa: Resources.DataDictionary.Faq)
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				OnDelete = false,
				
				Description = "این رکورد به طور اتوماتیک در سیستم ثبت شده و غیر قابل حذف میباشد",
			};
			
			await UnitOfWork.TagPageSettingRepository.AddAsync(faqTag);
		}
		
		var bannerTag = await UnitOfWork
			.TagPageSettingRepository.FindByNameAsync(nameof(Resources.DataDictionary.Banner));

		if (bannerTag is null)
		{
			bannerTag = new TagPageSetting(nameEn: nameof(Resources.DataDictionary.Banner), nameFa: Resources.DataDictionary.Banner)
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				OnDelete = false,
				
				Description = "این رکورد به طور اتوماتیک در سیستم ثبت شده و غیر قابل حذف میباشد",
			};
			
			await UnitOfWork.TagPageSettingRepository.AddAsync(bannerTag);
		}

		var socialTag = await UnitOfWork
			.TagPageSettingRepository.FindByNameAsync(nameof(Resources.DataDictionary.Social));

		if (socialTag is null)
		{
			socialTag = new TagPageSetting(nameEn: nameof(Resources.DataDictionary.Social), nameFa: Resources.DataDictionary.Social)
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				OnDelete = false,
				
				Description = "این رکورد به طور اتوماتیک در سیستم ثبت شده و غیر قابل حذف میباشد",
			};
			
			await UnitOfWork.TagPageSettingRepository.AddAsync(socialTag);
		}
		
		var textDynamicTag = await UnitOfWork
			.TagPageSettingRepository.FindByNameAsync(nameof(Resources.DataDictionary.TextDynamic));

		if (textDynamicTag is null)
		{
			textDynamicTag = new TagPageSetting(nameEn: nameof(Resources.DataDictionary.TextDynamic), nameFa: Resources.DataDictionary.TextDynamic)
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				OnDelete = false,
				
				Description = "این رکورد به طور اتوماتیک در سیستم ثبت شده و غیر قابل حذف میباشد",
			};
			
			await UnitOfWork.TagPageSettingRepository.AddAsync(textDynamicTag);
		}
		
		var appLogoTag = await UnitOfWork
			.TagPageSettingRepository.FindByNameAsync(nameof(Resources.DataDictionary.AppLogo));

		if (appLogoTag is null)
		{
			appLogoTag = new TagPageSetting(nameEn: nameof(Resources.DataDictionary.AppLogo), nameFa: Resources.DataDictionary.AppLogo)
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				
				OnDelete = false,
				
				Description = "این رکورد به طور اتوماتیک در سیستم ثبت شده و غیر قابل حذف میباشد",
			};
			
			await UnitOfWork.TagPageSettingRepository.AddAsync(appLogoTag);
		}
		
		var phoneNumberTag = await UnitOfWork
			.TagPageSettingRepository.FindByNameAsync(nameof(Resources.DataDictionary.PhoneNumber));

		if (phoneNumberTag is null)
		{
			phoneNumberTag = new TagPageSetting(nameEn: nameof(Resources.DataDictionary.PhoneNumber), nameFa: Resources.DataDictionary.PhoneNumber)
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				
				OnDelete = false,
				
				Description = "این رکورد به طور اتوماتیک در سیستم ثبت شده و غیر قابل حذف میباشد",
			};
			
			await UnitOfWork.TagPageSettingRepository.AddAsync(phoneNumberTag);
		}
		
		var emailTag = await UnitOfWork
			.TagPageSettingRepository.FindByNameAsync(nameof(Resources.DataDictionary.Email));

		if (emailTag is null)
		{
			emailTag = new TagPageSetting(nameEn: nameof(Resources.DataDictionary.Email), nameFa: Resources.DataDictionary.Email)
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				
				OnDelete = false,
				
				Description = "این رکورد به طور اتوماتیک در سیستم ثبت شده و غیر قابل حذف میباشد",
			};
			
			await UnitOfWork.TagPageSettingRepository.AddAsync(emailTag);
		}
		
		var timeWorkTag = await UnitOfWork
			.TagPageSettingRepository.FindByNameAsync(nameof(Resources.DataDictionary.TimeWork));

		if (timeWorkTag is null)
		{
			timeWorkTag =
				new TagPageSetting(
					nameEn: nameof(Resources.DataDictionary.TimeWork),
					nameFa: Resources.DataDictionary.TimeWork)
			{
				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,
				
				OnDelete = false,
				
				Description = "این رکورد به طور اتوماتیک در سیستم ثبت شده و غیر قابل حذف میباشد",
			};
			
			await UnitOfWork.TagPageSettingRepository.AddAsync(timeWorkTag);
		}

		await UnitOfWork.SaveAsync();
	}
	// **************************************************
}