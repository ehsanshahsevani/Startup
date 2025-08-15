using Domain;
using Utilities;
using FluentAssertions;
using HttpServices.Marketplace.Gold;

namespace Repositories;

public class AccountCodingRepositoryTest : Base.BaseTestWithDatabaseInMemory
{
	#region Database_Constructor_InitialData
	
	public AccountCodingRepositoryTest() : base()
	{
	}

	#endregion /Database_Constructor_InitialData

	#region FindByCodeAsync_ShouldReturnCorrectEntity

	/// <summary>
	/// بررسی اینکه متد FindByCodeAsync کد حساب را به درستی پیدا می‌کند
	/// </summary>
	[Fact(DisplayName = "بررسی اینکه متد FindByCodeAsync کد حساب را به درستی پیدا می‌کند")]
	public async Task FindByCodeAsync_ShouldReturnCorrectEntity()
	{
		// Arrange
		var code = AccountCoding.ReferalCode;

		// Act
		var result =
			await UnitOfWork.AccountCodingRepository.FindByCodeAsync(code);

		// Assert
		// Assert.NotNull(result);
		result.Should().NotBeNull();

		// Assert.Equal(code, result!.Code);
		result!.Code.Should().Be(code);
	}

	#endregion /FindByCodeAsync_ShouldReturnCorrectEntity

	#region FindReferalAccountCodingAsync_ShouldReturnReferalCode

	/// <summary>
	/// بررسی یافتن کد هدیه و دعوت دوستان
	/// </summary>
	[Fact(DisplayName = "بررسی یافتن کد هدیه و دعوت دوستان")]
	public async Task FindReferalAccountCodingAsync_ShouldReturnReferalCode()
	{
		var result =
			await UnitOfWork
				.AccountCodingRepository
				.FindReferalAccountCodingAsync();

		// Assert.NotNull(result);
		result.Should().NotBeNull();

		// Assert.Equal(AccountCoding.ReferalCode, result!.Code);
		result!.Code.Should().Be(AccountCoding.ReferalCode);
	}

	#endregion /FindReferalAccountCodingAsync_ShouldReturnReferalCode

	#region FindGoldTreasuryAccountCodingAsync_ShouldReturnCorrectCode

	/// <summary>
	/// بررسی یافتن کد خزانه طلا
	/// </summary>
	[Fact(DisplayName = "بررسی یافتن کد خزانه طلا")]
	public async Task FindGoldTreasuryAccountCodingAsync_ShouldReturnCorrectCode()
	{
		var result =
			await UnitOfWork
				.AccountCodingRepository
				.FindGoldTreasuryAccountCodingAsync();

		// Assert.NotNull(result);
		result.Should().NotBeNull();

		// Assert.Equal(AccountCoding.GoldTreasuryCode, result!.Code);
		result!.Code.Should().Be(AccountCoding.GoldTreasuryCode);
	}

	#endregion /FindGoldTreasuryAccountCodingAsync_ShouldReturnCorrectCode

	#region FindGoldTradingIncomeAccountCodingAsync_ShouldReturnCorrectCode

	/// <summary>
	/// بررسی یافتن کد کارمزد معاملات طلا
	/// </summary>
	[Fact(DisplayName = "بررسی یافتن کد کارمزد معاملات طلا")]
	public async Task FindGoldTradingIncomeAccountCodingAsync_ShouldReturnCorrectCode()
	{
		var result =
			await UnitOfWork
				.AccountCodingRepository
				.FindGoldPurchaseFeeAccountCodingAsync();

		// Assert.NotNull(result);
		result.Should().NotBeNull();

		// Assert.Equal(AccountCoding.GoldTradingIncomeCode, result!.Code);
		result!.Code.Should().Be(AccountCoding.GoldPurchaseFee);
	}

	#endregion /FindGoldTradingIncomeAccountCodingAsync_ShouldReturnCorrectCode

	#region GoldMaintenanceFeeAccountCodingAsync_ShouldReturnCorrectCode

	/// <summary>
	/// بررسی یافتن کد هزینه نگهداری طلا
	/// </summary>
	[Fact(DisplayName = "بررسی یافتن کد هزینه نگهداری طلا")]
	public async Task GoldMaintenanceFeeAccountCodingAsync_ShouldReturnCorrectCode()
	{
		var result =
			await UnitOfWork
				.AccountCodingRepository
				.FindGoldMaintenanceFeeAccountCodingAsync();

		// Assert.NotNull(result);
		result.Should().NotBeNull();

		// Assert.Equal(AccountCoding.GoldMaintenanceFeeCode, result!.Code);
		result!.Code.Should().Be(AccountCoding.GoldMaintenanceFeeCode);
	}

	#endregion /GoldMaintenanceFeeAccountCodingAsync_ShouldReturnCorrectCode

	#region IncomeSaleOfGoldCodeAccountCodingAsync_ShouldReturnCorrectCode

	/// <summary>
	/// بررسی یافتن کد درآمد فروش طلا
	/// </summary>
	[Fact(DisplayName = "بررسی یافتن کد درآمد فروش طلا")]
	public async Task IncomeSaleOfGoldCodeAccountCodingAsync_ShouldReturnCorrectCode()
	{
		var result = await UnitOfWork.AccountCodingRepository.FindIncomeSaleOfGoldCodeAccountCodingAsync();

		// Assert.NotNull(result);
		result.Should().NotBeNull();

		// Assert.Equal(AccountCoding.IncomeSaleOfGoldCode, result!.Code);
		result!.Code.Should().Be(AccountCoding.IncomeSaleOfGoldCode);
	}

	#endregion /IncomeSaleOfGoldCodeAccountCodingAsync_ShouldReturnCorrectCode

	#region IncomeSellingGoodsAccountCodingAsync_ShouldReturnCorrectCode

	/// <summary>
	/// بررسی یافتن فروش محصول در فروشگاه
	/// </summary>
	[Fact(DisplayName = "بررسی یافتن فروش محصول در فروشگاه")]
	public async Task IncomeSellingGoodsAccountCodingAsync_ShouldReturnCorrectCode()
	{
		var result =
			await UnitOfWork
				.AccountCodingRepository
				.FindIncomeSellingGoodsCodeAccountCodingAsync();

		// Assert.NotNull(result);
		result.Should().NotBeNull();

		// Assert.Equal(AccountCoding.IncomeSaleOfGoldCode, result!.Code);
		result!.Code.Should().Be(AccountCoding.IncomeSellingGoods);
	}

	#endregion /IncomeSellingGoodsAccountCodingAsync_ShouldReturnCorrectCode

	#region IncomeCommissionCodeAccountCodingAsync_ShouldReturnCorrectCode

	/// <summary>
	/// بررسی یافتن کمیسیون
	/// </summary>
	[Fact(DisplayName = "بررسی یافتن کمیسیون")]
	public async Task IncomeCommissionCodeAccountCodingAsync_ShouldReturnCorrectCode()
	{
		var result =
			await UnitOfWork
				.AccountCodingRepository
				.FindIncomeCommissionCodeCodeAccountCodingAsync();

		// Assert.NotNull(result);
		result.Should().NotBeNull();

		// Assert.Equal(AccountCoding.IncomeSaleOfGoldCode, result!.Code);
		result!.Code.Should().Be(AccountCoding.IncomeCommissionCode);
	}

	#endregion /IncomeCommissionCodeAccountCodingAsync_ShouldReturnCorrectCode

	#region WalletRechargeFeeIncomeAccountCodingAsync_ShouldReturnCorrectCode

	/// <summary>
	/// بررسی یافتن کد درآمد شارژ کیف پول
	/// </summary>
	[Fact(DisplayName = "بررسی یافتن کد درآمد شارژ کیف پول")]
	public async Task WalletRechargeFeeIncomeAccountCodingAsync_ShouldReturnCorrectCode()
	{
		var result = await UnitOfWork.AccountCodingRepository.FindWalletRechargeFeeIncomeAccountCodingAsync();

		// Assert.NotNull(result);
		result.Should().NotBeNull();

		// Assert.Equal(AccountCoding.WalletRechargeFeeIncome, result!.Code);
		result!.Code.Should().Be(AccountCoding.WalletRechargeFeeIncome);
	}

	#endregion /WalletRechargeFeeIncomeAccountCodingAsync_ShouldReturnCorrectCode

	#region FindGoldTreasuryAccountCodingAsync_ShouldReturnCorrectAccount

	/// <summary>
	/// بررسی موفقیت‌آمیز بودن بازیابی کد خزانه طلا
	/// </summary>
	[Fact(DisplayName = "بررسی موفقیت آمیز بودن بازیابی خزانه طلا")]
	public async Task FindGoldTreasuryAccountCodingAsync_ShouldReturnCorrectAccount()
	{
		// Arrange
		var expectedCode = AccountCoding.GoldTreasuryCode;
		var expectedName = AccountCoding.AccountCodings[expectedCode];

		// Act
		var result =
			await UnitOfWork
				.AccountCodingRepository
				.FindGoldTreasuryAccountCodingAsync();

		// Assert
		result.Should().NotBeNull();
		result!.Code.Should().Be(expectedCode);
		result.Name.Should().Be(expectedName);
	}

	#endregion /FindGoldTreasuryAccountCodingAsync_ShouldReturnCorrectAccount

	#region FindReferalAccountCodingAsync_ShouldReturnCorrectAccount

	/// <summary>
	/// بررسی بازیابی کد هدیه و دعوت دوستان
	/// </summary>
	[Fact(DisplayName = "بررسی بازیابی کد هدیه و دعوت دوستان")]
	public async Task FindReferalAccountCodingAsync_ShouldReturnCorrectAccount()
	{
		// Arrange
		var expectedCode = AccountCoding.ReferalCode;
		var expectedName = AccountCoding.AccountCodings[expectedCode];

		// Act
		var result =
			await UnitOfWork
				.AccountCodingRepository
				.FindReferalAccountCodingAsync();

		// Assert
		result.Should().NotBeNull();
		result!.Code.Should().Be(expectedCode);
		result.Name.Should().Be(expectedName);
	}

	#endregion /FindReferalAccountCodingAsync_ShouldReturnCorrectAccount

	#region FindGoldTradingIncomeAccountCodingAsync_ShouldReturnCorrectAccount

	/// <summary>
	/// بررسی بازیابی کد درآمد کارمزد خرید طلا
	/// </summary>
	[Fact(DisplayName = "بررسی بازیابی کد درآمد کارمزد خرید طلا")]
	public async Task FindGoldTradingIncomeAccountCodingAsync_ShouldReturnCorrectAccount()
	{
		// Arrange
		var expectedCode = AccountCoding.GoldPurchaseFee;
		var expectedName = AccountCoding.AccountCodings[expectedCode];

		// Act
		var result =
			await UnitOfWork
				.AccountCodingRepository
				.FindGoldPurchaseFeeAccountCodingAsync();

		// Assert
		result.Should().NotBeNull();
		result!.Code.Should().Be(expectedCode);
		result.Name.Should().Be(expectedName);
	}

	#endregion /FindGoldTradingIncomeAccountCodingAsync_ShouldReturnCorrectAccount

	#region GoldMaintenanceFeeAccountCodingAsync_ShouldReturnCorrectAccount

	/// <summary>
	/// بررسی بازیابی کد درآمد نگهداری طلا
	/// </summary>
	[Fact(DisplayName = "بررسی بازیابی کد درآمد نگهداری طلا")]
	public async Task GoldMaintenanceFeeAccountCodingAsync_ShouldReturnCorrectAccount()
	{
		// Arrange
		var expectedCode = AccountCoding.GoldMaintenanceFeeCode;
		var expectedName = AccountCoding.AccountCodings[expectedCode];

		await UnitOfWork.AccountCodingRepository.AddAsync(new AccountCoding
		{
			IsActive = true,
			IsDeleted = false,
			Code = expectedCode,
			Name = expectedName
		});

		// Act
		var result = await UnitOfWork.AccountCodingRepository.FindGoldMaintenanceFeeAccountCodingAsync();

		// Assert
		result.Should().NotBeNull();
		result!.Code.Should().Be(expectedCode);
		result.Name.Should().Be(expectedName);
	}

	#endregion /GoldMaintenanceFeeAccountCodingAsync_ShouldReturnCorrectAccount

	#region IncomeSaleOfGoldCodeAccountCodingAsync_ShouldReturnCorrectAccount

	/// <summary>
	/// بررسی بازیابی کد درآمد حاصل از فروش طلا
	/// </summary>
	[Fact(DisplayName = "بررسی بازیابی کد درآمد حاصل از فروش طلا")]
	public async Task IncomeSaleOfGoldCodeAccountCodingAsync_ShouldReturnCorrectAccount()
	{
		// Arrange
		var expectedCode = AccountCoding.IncomeSaleOfGoldCode;
		var expectedName = AccountCoding.AccountCodings[expectedCode];

		// Act
		var result =
			await UnitOfWork
				.AccountCodingRepository
				.FindIncomeSaleOfGoldCodeAccountCodingAsync();

		// Assert
		result.Should().NotBeNull();
		result!.Code.Should().Be(expectedCode);
		result.Name.Should().Be(expectedName);
	}

	#endregion /IncomeSaleOfGoldCodeAccountCodingAsync_ShouldReturnCorrectAccount

	#region WalletRechargeFeeIncomeAccountCodingAsync_ShouldReturnCorrectAccount

	/// <summary>
	/// بررسی بازیابی کد درآمد کارمزد شارژ کیف پول
	/// </summary>
	[Fact(DisplayName = "بررسی بازیابی کد درآمد کارمزد شارژ کیف پول")]
	public async Task WalletRechargeFeeIncomeAccountCodingAsync_ShouldReturnCorrectAccount()
	{
		// Arrange
		var expectedCode = AccountCoding.WalletRechargeFeeIncome;
		var expectedName = AccountCoding.AccountCodings[expectedCode];

		// Act
		var result =
			await UnitOfWork
				.AccountCodingRepository
				.FindWalletRechargeFeeIncomeAccountCodingAsync();

		// Assert
		result.Should().NotBeNull();
		result!.Code.Should().Be(expectedCode);
		result.Name.Should().Be(expectedName);
	}

	#endregion /WalletRechargeFeeIncomeAccountCodingAsync_ShouldReturnCorrectAccount

	#region ConnectBankAccountAmountToTalaSootBankAccountAsync_Should_Create_SubSystemAccountCoding_Throw_When_AccountCoding_NotExists_Throw_When_SubSystem_NotExists

	/// <summary>
	/// اتصال موجودی بانکی به حساب بانکی طلاسوت - موفق
	/// </summary>
	[Fact(DisplayName = "اتصال موجودی بانکی به حساب بانکی طلاسوت - موفق")]
	public async Task
		ConnectBankAccountAmountToTalaSootBankAccountAsync_Should_Create_SubSystemAccountCoding_Throw_When_AccountCoding_NotExists_Throw_When_SubSystem_NotExists()
	{
		string code = AccountCoding.TalaSootBankAccountCode;
		string name = AccountCoding.AccountCodings[code];

		// Arrange
		var bankAccount = new TalaSootBankAccount
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,

			BankName = "ملت",
			FullName = "احسان شاهسونی",
			AccountNumber = "1234567890",
			Shaba = "IR123456789012345678901234",
			CardNumber = "6037701234567890",
			Amount = 1500000
		};

		await UnitOfWork.TalaSootBankAccountRepository.AddAsync(bankAccount);

		// Act
		var accountCoding =
			await UnitOfWork
				.AccountCodingRepository
				.ConnectBankAccountAmountToTalaSootBankAccountAsync(bankAccount);

		await UnitOfWork.SaveAsync();

		// Assert
		var accountCodingSubSystemLocalWithRelationIdAndDomainName =
			await UnitOfWork
				.AccountCodingSubSystemLocalRepository
				.FindByRelationIdAndSubSystemNameAsync(bankAccount.Id, nameof(TalaSootBankAccount));

		var accountCodingSubSystemLocalWithAccountCodingId =
			await UnitOfWork
				.AccountCodingSubSystemLocalRepository
				.FindByRelationIdAndSubSystemNameAsync(bankAccount.Id, nameof(TalaSootBankAccount));

		var subSystem =
			await UnitOfWork.SubSystemLocalRepository
				.FindByNameAsync(domain: nameof(TalaSootBankAccount));

		subSystem.Should().NotBeNull();

		accountCodingSubSystemLocalWithAccountCodingId.Should().NotBeNull();
		accountCodingSubSystemLocalWithRelationIdAndDomainName.Should().NotBeNull();

		accountCoding.Should().NotBeNull();

		accountCodingSubSystemLocalWithRelationIdAndDomainName.AccountCodingId.Should().Be(accountCoding.Id);

		accountCodingSubSystemLocalWithRelationIdAndDomainName.SubSystemLocalId.Should().Be(subSystem.Id);

		accountCoding.IsActive.Should().BeTrue();
		accountCoding.IsDeleted.Should().BeFalse();

		accountCoding.IsActive.Should().BeTrue();
		accountCoding.IsDeleted.Should().BeFalse();
	}

	#endregion /ConnectBankAccountAmountToTalaSootBankAccountAsync_Should_Create_SubSystemAccountCoding_Throw_When_AccountCoding_NotExists_Throw_When_SubSystem_NotExists

	#region CreateUserMoneyAssetsAsync_Should_Create_SubSystemAccountCoding_Throw_When_AccountCoding_NotExists_Throw_When_SubSystem_NotExists

	/// <summary>
	/// ایجاد و اتصال کد حسابداری برای کیف پول کاربران
	/// </summary>
	[Fact(DisplayName = "ایجاد و اتصال کد حسابداری برای کیف پول کاربران")]
	public async Task
		CreateUserMoneyAssetsAsync_Should_Create_SubSystemAccountCoding_Throw_When_AccountCoding_NotExists_Throw_When_SubSystem_NotExists()
	{
		string code = AccountCoding.UserMoneyAssetsCode;
		string name = AccountCoding.AccountCodings[code];
		int lengthCode = AccountCoding.UserMoneyAssetsCodeCharCount;

		// Arrange
		// **************************************************
		// profile user 1
		var user1 = new Profile
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,
			UserId = Guid.NewGuid().ToString(),
			FirstName = "نام کاربر 1",
			LastName = "نام خانوادگی کاربر 1",
			NationalCode = "1234567891",
			BirthDate = DateTime.Now.AddYears(-30),
			Address = "شیراز - استان فارس",
			GenderId = Guid.NewGuid().ToString(),

			City = new City
			{
				Name = "شیراز",

				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,

				Province = new Province
				{
					Name = "فارس",

					IsActive = true,
					IsDeleted = false,
					Ordering = 100_000,
				}
			},
		};
		// **************************************************

		// **************************************************
		// profile user 2
		var user2 = new Profile
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,
			UserId = Guid.NewGuid().ToString(),
			FirstName = "نام کاربر 2",
			LastName = "نام خانوادگی کاربر 2",
			NationalCode = "1234567890",
			BirthDate = DateTime.Now.AddYears(-30),
			Address = "تهران - سعادت‌آباد",
			GenderId = Guid.NewGuid().ToString(),

			City = new City
			{
				Name = "تهران",

				IsActive = true,
				IsDeleted = false,
				Ordering = 100_000,

				Province = new Province
				{
					Name = "تهران",

					IsActive = true,
					IsDeleted = false,
					Ordering = 100_000,
				}
			},
		};
		// **************************************************

		await UnitOfWork.ProfileRepository.AddAsync(user1);

		// Act
		var accountCodingUser1 =
			await UnitOfWork
				.AccountCodingRepository
				.CreateUserMoneyAssetsAsync(user1);

		await UnitOfWork.SaveAsync();

		await UnitOfWork.ProfileRepository.AddAsync(user2);

		// Act
		var accountCodingUser2 =
			await UnitOfWork
				.AccountCodingRepository
				.CreateUserMoneyAssetsAsync(user2);

		await UnitOfWork.SaveAsync();

		// Assert
		var accountCodingSubSystemLocalWithRelationIdAndDomainName =
			await UnitOfWork
				.AccountCodingSubSystemLocalRepository
				.FindByRelationIdAndSubSystemNameAsync(user1.Id, nameof(Profile));

		var accountCodingSubSystemLocalWithAccountCodingId =
			await UnitOfWork
				.AccountCodingSubSystemLocalRepository
				.FindByRelationIdAndSubSystemNameAsync(user1.Id, nameof(Profile));

		var subSystem =
			await UnitOfWork.SubSystemLocalRepository
				.FindByNameAsync(domain: nameof(Profile));

		subSystem.Should().NotBeNull();

		accountCodingSubSystemLocalWithAccountCodingId.Should().NotBeNull();
		accountCodingSubSystemLocalWithRelationIdAndDomainName.Should().NotBeNull();

		accountCodingUser1.Should().NotBeNull();
		accountCodingUser1.Code.Should().HaveLength(lengthCode + code.Length);

		accountCodingSubSystemLocalWithRelationIdAndDomainName.AccountCodingId.Should().Be(accountCodingUser1.Id);
		accountCodingSubSystemLocalWithRelationIdAndDomainName.SubSystemLocalId.Should().Be(subSystem.Id);

		accountCodingUser1.IsActive.Should().BeTrue();
		accountCodingUser1.IsDeleted.Should().BeFalse();

		accountCodingUser2.Should().NotBeNull();
		accountCodingUser2.Code.Should().HaveLength(lengthCode + code.Length);
		accountCodingUser2.Code.Should().NotBe(accountCodingUser1.Code);

		int codeingUser1 =
			Convert.ToInt32(accountCodingUser1.Code.Replace(code, ""));

		int codeingUser2 =
			Convert.ToInt32(accountCodingUser2.Code.Replace(code, ""));

		(codeingUser1 + 1).Should().Be(codeingUser2);
	}

	#endregion /CreateUserMoneyAssetsAsync_Should_Create_SubSystemAccountCoding_Throw_When_AccountCoding_NotExists_Throw_When_SubSystem_NotExists

	#region CreateUserGoldAssetsAsync_Should_Create_SubSystemAccountCoding_Throw_When_AccountCoding_NotExists_Throw_When_SubSystem_NotExists

	/// <summary>
	/// ایجاد و اتصال کد حسابداری برای دارایی طلای کاربران
	/// </summary>
	[Fact(DisplayName = "ایجاد و اتصال کد حسابداری برای دارایی طلای کاربران")]
	public async Task
		CreateUserGoldAssetsAsync_Should_Create_SubSystemAccountCoding_Throw_When_AccountCoding_NotExists_Throw_When_SubSystem_NotExists()
	{
		string code = AccountCoding.UserGoldAssetsCode;
		string name = AccountCoding.AccountCodings[code];
		int lengthCode = AccountCoding.UserGoldAssetsCodeCharCount;

		// Arrange
		// **************************************************
		// profile user 1
		var user1 = CreateUserProfile(
			firstName: "نام کاربر 1",
			lastName: "نام خانوادگی کاربر 1",
			nationalCode: "2234567891",
			birthDate: DateTime.Now.AddYears(-30),
			address: "اهواز - کیانپارس",
			genderId: Guid.NewGuid().ToString(),
			cityName: "اهواز",
			provinceName: "خوزستان"
		);
		// **************************************************

		// **************************************************
		// profile user 2
		var user2 = CreateUserProfile(
			firstName: "نام کاربر 2",
			lastName: "نام خانوادگی کاربر 2",
			nationalCode: "2234567890",
			birthDate: DateTime.Now.AddYears(-30),
			address: "مشهد - الهیه",
			genderId: Guid.NewGuid().ToString(),
			cityName: "مشهد",
			provinceName: "خراسان رضوی"
		);
		// **************************************************

		await UnitOfWork.ProfileRepository.AddAsync(user1);

		// Act
		var accountCodingUser1 =
			await UnitOfWork
				.AccountCodingRepository
				.CreateUserGoldAssetsAsync(user1);

		await UnitOfWork.SaveAsync();

		await UnitOfWork.ProfileRepository.AddAsync(user2);

		// Act
		var accountCodingUser2 =
			await UnitOfWork
				.AccountCodingRepository
				.CreateUserGoldAssetsAsync(user2);

		await UnitOfWork.SaveAsync();

		// Assert
		var accountCodingSubSystemLocalWithRelationIdAndDomainName =
			await UnitOfWork
				.AccountCodingSubSystemLocalRepository
				.FindByRelationIdAndSubSystemNameAsync(user1.Id, nameof(Profile));

		var accountCodingSubSystemLocalWithAccountCodingId =
			await UnitOfWork
				.AccountCodingSubSystemLocalRepository
				.FindByRelationIdAndSubSystemNameAsync(user1.Id, nameof(Profile));

		var subSystem =
			await UnitOfWork.SubSystemLocalRepository
				.FindByNameAsync(domain: nameof(Profile));

		subSystem.Should().NotBeNull();

		accountCodingSubSystemLocalWithAccountCodingId.Should().NotBeNull();
		accountCodingSubSystemLocalWithRelationIdAndDomainName.Should().NotBeNull();

		accountCodingUser1.Should().NotBeNull();
		accountCodingUser1.Code.Should().HaveLength(lengthCode + code.Length);

		var newAccountCodingNameUser1 =
			string.Format(Resources.Messages.CreateUserGoldAssets, user1.FullName);

		accountCodingUser1.Name.Should().Be(newAccountCodingNameUser1);

		accountCodingSubSystemLocalWithRelationIdAndDomainName.AccountCodingId.Should().Be(accountCodingUser1.Id);
		accountCodingSubSystemLocalWithRelationIdAndDomainName.SubSystemLocalId.Should().Be(subSystem.Id);

		accountCodingUser1.IsActive.Should().BeTrue();
		accountCodingUser1.IsDeleted.Should().BeFalse();

		accountCodingUser2.Should().NotBeNull();
		accountCodingUser2.Code.Should().HaveLength(lengthCode + code.Length);

		var newAccountCodingNameUser2 =
			string.Format(Resources.Messages.CreateUserGoldAssets, user2.FullName);

		accountCodingUser2.Name.Should().Be(newAccountCodingNameUser2);

		accountCodingUser2.Code.Should().NotBe(accountCodingUser1.Code);

		int codeingUser1 =
			Convert.ToInt32(accountCodingUser1.Code.Replace(code, ""));

		int codeingUser2 =
			Convert.ToInt32(accountCodingUser2.Code.Replace(code, ""));

		(codeingUser1 + 1).Should().Be(codeingUser2);
	}

	#endregion /CreateUserGoldAssetsAsync_Should_Create_SubSystemAccountCoding_Throw_When_AccountCoding_NotExists_Throw_When_SubSystem_NotExists

	#region UserRoundCodeAsync_Should_Create_SubSystemAccountCoding_Throw_When_AccountCoding_NotExists_Throw_When_SubSystem_NotExists

	/// <summary>
	/// ایجاد و اتصال کد حسابداری مبالغ رند به درخت حسابداری
	/// </summary>
	[Fact(DisplayName = "ایجاد و اتصال کد حسابداری مبالغ رند به درخت حسابداری")]
	public async Task
		UserRoundCodeAsync_Should_Create_SubSystemAccountCoding_Throw_When_AccountCoding_NotExists_Throw_When_SubSystem_NotExists()
	{
		string code = AccountCoding.UserRoundCode;
		string name = AccountCoding.AccountCodings[code];
		int lengthCode = AccountCoding.UserRoundCodeCount;

		// Arrange
		// **************************************************
		// profile user 1
		// Arrange
		// **************************************************
		// profile user 1
		var user1 = CreateUserProfile(
			firstName: "نام کاربر 1",
			lastName: "نام خانوادگی کاربر 1",
			nationalCode: "2234567891",
			birthDate: DateTime.Now.AddYears(-30),
			address: "اهواز - کیانپارس",
			genderId: Guid.NewGuid().ToString(),
			cityName: "اهواز",
			provinceName: "خوزستان"
		);
		// **************************************************

		// **************************************************
		// profile user 2
		var user2 = CreateUserProfile(
			firstName: "نام کاربر 2",
			lastName: "نام خانوادگی کاربر 2",
			nationalCode: "2234567890",
			birthDate: DateTime.Now.AddYears(-30),
			address: "مشهد - الهیه",
			genderId: Guid.NewGuid().ToString(),
			cityName: "مشهد",
			provinceName: "خراسان رضوی"
		);
		// **************************************************

		await UnitOfWork.ProfileRepository.AddAsync(user1);

		// Act
		var accountCodingUser1 =
			await UnitOfWork
				.AccountCodingRepository
				.CreateUserRoundCodeAsync(user1);

		await UnitOfWork.SaveAsync();

		await UnitOfWork.ProfileRepository.AddAsync(user2);

		// Act
		var accountCodingUser2 =
			await UnitOfWork
				.AccountCodingRepository
				.CreateUserRoundCodeAsync(user2);

		await UnitOfWork.SaveAsync();

		// Assert
		var accountCodingSubSystemLocalWithRelationIdAndDomainName =
			await UnitOfWork
				.AccountCodingSubSystemLocalRepository
				.FindByRelationIdAndSubSystemNameAsync(user1.Id, nameof(Profile));

		var accountCodingSubSystemLocalWithAccountCodingId =
			await UnitOfWork
				.AccountCodingSubSystemLocalRepository
				.FindByRelationIdAndSubSystemNameAsync(user1.Id, nameof(Profile));

		var subSystem =
			await UnitOfWork.SubSystemLocalRepository
				.FindByNameAsync(domain: nameof(Profile));

		subSystem.Should().NotBeNull();

		accountCodingSubSystemLocalWithAccountCodingId.Should().NotBeNull();
		accountCodingSubSystemLocalWithRelationIdAndDomainName.Should().NotBeNull();

		accountCodingUser1.Should().NotBeNull();
		accountCodingUser1.Code.Should().HaveLength(lengthCode + code.Length);

		accountCodingSubSystemLocalWithRelationIdAndDomainName.AccountCodingId.Should().Be(accountCodingUser1.Id);
		accountCodingSubSystemLocalWithRelationIdAndDomainName.SubSystemLocalId.Should().Be(subSystem.Id);

		accountCodingUser1.IsActive.Should().BeTrue();
		accountCodingUser1.IsDeleted.Should().BeFalse();

		accountCodingUser2.Should().NotBeNull();
		accountCodingUser2.Code.Should().HaveLength(lengthCode + code.Length);
		accountCodingUser2.Code.Should().NotBe(accountCodingUser1.Code);

		int codeingUser1 =
			Convert.ToInt32(accountCodingUser1.Code.Replace(code, ""));

		int codeingUser2 =
			Convert.ToInt32(accountCodingUser2.Code.Replace(code, ""));

		(codeingUser1 + 1).Should().Be(codeingUser2);
	}

	#endregion /UserRoundCodeAsync_Should_Create_SubSystemAccountCoding_Throw_When_AccountCoding_NotExists_Throw_When_SubSystem_NotExists

	#region CreateUserBankAccountAsync_Should_Create_SubSystemAccountCoding_Throw_When_AccountCoding_NotExists_Throw_When_SubSystem_NotExists

	/// <summary>
	/// ایجاد و اتصال کد حسابداری برای حساب بانکی کاربران
	/// </summary>
	[Fact(DisplayName = "ایجاد و اتصال کد حسابداری برای حساب بانکی کاربران")]
	public async Task
		CreateUserBankAccountAsync_Should_Create_SubSystemAccountCoding_Throw_When_AccountCoding_NotExists_Throw_When_SubSystem_NotExists()
	{
		// Arrange
		string code = AccountCoding.UserBankAccountCode;
		string name = AccountCoding.AccountCodings[code];

		int lengthCode = AccountCoding.UserBankAccountCodeCharCount;

		// profile
		var profileBank = CreateProfileWithBank(
			firstName: "نام کاربر 1",
			lastName: "نام خانوادگی کاربر 1",
			nationalCode: "1234567891",
			birthDate: DateTime.Now.AddYears(-30),
			address: "شیراز - استان فارس",
			genderId: Guid.NewGuid().ToString(),
			cityName: "شیراز",
			provinceName: "فارس",
			profileIsActive: true,
			bankName: "سپه",
			bankIsActive: true,
			cardNumber: "6037701642801184",
			shaba: "IR330150000003120110553978",
			accountNumber: "1234567890",
			profileBankIsActive: true
		);

		await UnitOfWork.ProfileBankRepository.AddAsync(profileBank);

		// Act
		var accountCoding =
			await UnitOfWork
				.AccountCodingRepository
				.CreateUserBankAccountAsync(profileBank);

		await UnitOfWork.SaveAsync();

		// Assert
		var accountCodingSubSystemLocalWithRelationIdAndDomainName =
			await UnitOfWork
				.AccountCodingSubSystemLocalRepository
				.FindByRelationIdAndSubSystemNameAsync(profileBank.Id, nameof(ProfileBank));

		var subSystem =
			await UnitOfWork.SubSystemLocalRepository
				.FindByNameAsync(domain: nameof(ProfileBank));

		string expectedName =
			string.Format(Resources.Messages.CreateUserBankAccount,
				profileBank.Bank!.Name,
				profileBank.Profile!.FullName,
				profileBank.CardNumber);

		subSystem.Should().NotBeNull();

		accountCodingSubSystemLocalWithRelationIdAndDomainName.Should().NotBeNull();
		accountCoding.Should().NotBeNull();

		accountCoding.Name.Should().Be(expectedName);
		accountCoding.Code.Should().HaveLength(lengthCode + code.Length);

		accountCodingSubSystemLocalWithRelationIdAndDomainName.AccountCodingId.Should().Be(accountCoding.Id);
		accountCodingSubSystemLocalWithRelationIdAndDomainName.SubSystemLocalId.Should().Be(subSystem.Id);

		accountCoding.IsActive.Should().BeTrue();
		accountCoding.IsDeleted.Should().BeFalse();
	}

	#endregion /CreateUserBankAccountAsync_Should_Create_SubSystemAccountCoding_Throw_When_AccountCoding_NotExists_Throw_When_SubSystem_NotExists

	#region FindAllAccountCodingData_For_Deposit_And_Withdraw_Should_Return_AccountCodings_With_Name_And_Code

	[Fact(DisplayName = "در آوردن لیست تمام اکانت کدینگ های مربوط به واریز یا برداشت از کیف پول")]
	public async Task
		FindAllAccountCodingData_For_Deposit_And_Withdraw_Should_Return_AccountCodings_With_Name_And_Code()
	{
		//🧾 سند شماره WD-14030302-0001
		// شرح: برداشت از کیف پول و انتقال به حساب سیستم  
		// - بدهکار: حساب بانکی سیستم (011011) → 30,000
		// - بستانکار: کیف پول کاربر (01102000000001) → 30,000

		// 🧾 سند شماره WD-14030302-0002
		// شرح: انتقال وجه به حساب بانکی علی حسینی  
		// - بدهکار: حساب بانکی علی حسینی (011040000000001) → 30,000
		// - بستانکار: حساب بانکی سیستم (011011) → 30,000

		// Arrange
		// **************************************************
		var profileBank = CreateProfileWithBank(
			firstName: "نام کاربر 1",
			lastName: "نام خانوادگی کاربر 1",
			nationalCode: "1234567891",
			birthDate: DateTime.Now.AddYears(-30),
			address: "شیراز - استان فارس",
			genderId: Guid.NewGuid().ToString(),
			cityName: "شیراز",
			provinceName: "فارس",
			profileIsActive: true,
			bankName: "سپه",
			bankIsActive: true,
			cardNumber: "6037701642801184",
			shaba: "IR330150000003120110553978",
			accountNumber: "1234567890",
			profileBankIsActive: true
		);

		await UnitOfWork.ProfileBankRepository.AddAsync(profileBank);
		// **************************************************
		// Act
		// کیف پول برای کاربر
		_ =
			await UnitOfWork
				.AccountCodingRepository
				.CreateUserMoneyAssetsAsync(profileBank.Profile!);

		// مبلغ رند کاربر
		_ =
			await UnitOfWork
				.AccountCodingRepository
				.CreateUserRoundCodeAsync(profileBank.Profile!);

		//کدینگ حساب بانکی
		_ =
			await UnitOfWork
				.AccountCodingRepository
				.CreateUserBankAccountAsync(profileBank);

		await UnitOfWork.SaveAsync();
		
		var onlineGoldService = new OnlineGoldService();
		
		var goldPriceInThisTime =
			await onlineGoldService.GoldPriceInThisTime();
		// **************************************************
		// **************************************************
		//📅 تاریخ: ۱۴۰۳/۰۳/۰۲
		// 💰 مبلغ برداشت: ۳۰٬۰۰۰ تومان
		// 🏦 مقصد: حساب بانک کشاورزی ۶۰۳۷•••۱۱۸۴
		// مبدا: کیف پول کاربر
		// ✅ وضعیت: موفق
		// **************************************************
		// Theory: یافتن و تست کدهای بخش برداشت از کیف پول
		var resultTheory1 =
			await UnitOfWork.AccountCodingRepository
				.FindAllAccountCodingDataForDepositAndWithdrawalAsync
					(profileBank, isDeposit: true, amount: 30_000, goldPriceInThisTime);

		resultTheory1.Should().NotBeNull();
		resultTheory1.Count.Should().Be(4);

		resultTheory1.Select(x => x.Amount).Should().Contain(30_000);

		resultTheory1
			.Where(current => current.UseParentDocument == true)
			.Count()
			.Should()
			.Be(2);

		resultTheory1
			.Where(current => current.UseParentDocument == false)
			.Count()
			.Should()
			.Be(2);

		resultTheory1
			.Where(current => current.Code == AccountCoding.TalaSootBankAccountCode)
			.Count()
			.Should()
			.Be(2);

		resultTheory1
			.Where(current => current.Code == AccountCoding.TalaSootBankAccountCode)
			.Where(current => current.IsDebtor == true)
			.Where(current => current.IsCreditor == false)
			.Where(current => current.UseParentDocument == true)
			.Count()
			.Should()
			.Be(1);

		resultTheory1
			.Where(current => current.Code == AccountCoding.TalaSootBankAccountCode)
			.Where(current => current.IsDebtor == false)
			.Where(current => current.IsCreditor == true)
			.Where(current => current.UseParentDocument == false)
			.Count()
			.Should()
			.Be(1);

		resultTheory1
			.Where(current => current.Code.StartsWith(AccountCoding.UserBankAccountCode))
			.Where(current => current.IsDebtor == true)
			.Where(current => current.IsCreditor == false)
			.Where(current => current.UseParentDocument == false)
			.Count()
			.Should()
			.Be(1);

		resultTheory1
			.Where(current => current.Code.StartsWith(AccountCoding.UserMoneyAssetsCode))
			.Where(current => current.IsDebtor == false)
			.Where(current => current.IsCreditor == true)
			.Where(current => current.UseParentDocument == true)
			.Count()
			.Should()
			.Be(1);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		//📅 تاریخ: ۱۴۰۳/۰۳/۰۲
		// 💰 مبلغ واریز: ۳۰٬۰۰۰ تومان
		// 🏦 مبدا: حساب بانک کشاورزی ۶۰۳۷•••۱۱۸۴
		// مقصد کیف پول کاربر
		// ✅ وضعیت: موفق
		// **************************************************
		// Theory: یافتن و تست کدهای بخش واریز
		var resultTheory2 =
			await UnitOfWork.AccountCodingRepository
				.FindAllAccountCodingDataForDepositAndWithdrawalAsync
					(profileBank, isDeposit: false, amount: 30_000, goldPriceInThisTime);

		resultTheory2.Should().NotBeNull();
		resultTheory2.Count.Should().Be(5);

		resultTheory2
			.Where(current => current.UseParentDocument == true)
			.Count()
			.Should()
			.Be(3);

		resultTheory2
			.Where(current => current.UseParentDocument == false)
			.Count()
			.Should()
			.Be(2);

		resultTheory2
			.Where(current => current.Code == AccountCoding.TalaSootBankAccountCode)
			.Count()
			.Should()
			.Be(2);

		resultTheory2
			.Where(current => current.Code == AccountCoding.TalaSootBankAccountCode)
			.Where(current => current.IsDebtor == true)
			.Where(current => current.IsCreditor == false)
			.Where(current => current.UseParentDocument == false)
			.Count()
			.Should()
			.Be(1);

		resultTheory2
			.Where(current => current.Code == AccountCoding.TalaSootBankAccountCode)
			.Where(current => current.IsDebtor == true)
			.Where(current => current.IsCreditor == false)
			.Where(current => current.UseParentDocument == false)
			.Select(x => x.Amount)
			.Should()
			.Contain(30_000);

		resultTheory2
			.Where(current => current.Code.StartsWith(AccountCoding.UserBankAccountCode))
			.Where(current => current.IsDebtor == false)
			.Where(current => current.IsCreditor == true)
			.Where(current => current.UseParentDocument == false)
			.Count()
			.Should()
			.Be(1);

		resultTheory2
			.Where(current => current.Code.StartsWith(AccountCoding.UserBankAccountCode))
			.Where(current => current.IsDebtor == false)
			.Where(current => current.IsCreditor == true)
			.Where(current => current.UseParentDocument == false)
			.Select(x => x.Amount)
			.Should()
			.Contain(30_000);

		resultTheory2
			.Where(current => current.Code == AccountCoding.TalaSootBankAccountCode)
			.Where(current => current.IsDebtor == false)
			.Where(current => current.IsCreditor == true)
			.Where(current => current.UseParentDocument == true)
			.Count()
			.Should()
			.Be(1);

		resultTheory2
			.Where(current => current.Code == AccountCoding.TalaSootBankAccountCode)
			.Where(current => current.IsDebtor == false)
			.Where(current => current.IsCreditor == true)
			.Where(current => current.UseParentDocument == true)
			.Select(x => x.Amount)
			.Should()
			.Contain(30_000);

		resultTheory2
			.Where(current => current.Code.StartsWith(AccountCoding.UserMoneyAssetsCode))
			.Where(current => current.IsDebtor == true)
			.Where(current => current.IsCreditor == false)
			.Where(current => current.UseParentDocument == true)
			.Count()
			.Should()
			.Be(1);

		resultTheory2
			.Where(current => current.Code == AccountCoding.WalletRechargeFeeIncome)
			.Where(current => current.IsDebtor == true)
			.Where(current => current.IsCreditor == false)
			.Where(current => current.UseParentDocument == true)
			.Count()
			.Should()
			.Be(1);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// 📅 تاریخ: ۱۴۰۳/۰۳/۰۲
		// 💰 مبلغ برداشت: ۳۱٬۹۹۹٬۹۹۹ تومان
		// 🧾 مبلغ کارمزد (۰.۵٪): ۱۵۹٬۹۹۹.۹۹ تومان
		// 🔻 رندینگ اعشار: ۰.۰۰۵ تومان
		// 🎯 مبلغ نهایی واریز به کیف پول: ۳۱٬۸۳۹٬۹۹۹ تومان
		// 🏦 مقصد: کیف پول کاربر ۱
		// مبدا: حساب بانک سپه ۶۰۳۷•••۱۱۸۴
		// ✅ وضعیت: موفق
		// **************************************************
		// Theory: یافتن و تست کدهای بخش واریز
		var resultTheory3 =
			await UnitOfWork.AccountCodingRepository
				.FindAllAccountCodingDataForDepositAndWithdrawalAsync
					(profileBank, isDeposit: false, amount: 31_999_999, goldPriceInThisTime);

		resultTheory3.Should().NotBeNull();
		resultTheory3.Count.Should().BeGreaterThan(4);

		resultTheory3
			.Where(current => current.UseParentDocument == true)
			.Count()
			.Should()
			.BeGreaterThan(2);

		resultTheory3
			.Where(current => current.UseParentDocument == false)
			.Count()
			.Should()
			.Be(2);

		resultTheory3
			.Where(current => current.Code == AccountCoding.TalaSootBankAccountCode)
			.Count()
			.Should()
			.Be(2);

		resultTheory3
			.Where(current => current.Code == AccountCoding.TalaSootBankAccountCode)
			.Where(current => current.IsDebtor == true)
			.Where(current => current.IsCreditor == false)
			.Where(current => current.UseParentDocument == false)
			.Count()
			.Should()
			.Be(1);

		resultTheory3
			.Where(current => current.Code.StartsWith(AccountCoding.UserBankAccountCode))
			.Where(current => current.IsDebtor == false)
			.Where(current => current.IsCreditor == true)
			.Where(current => current.UseParentDocument == false)
			.Count()
			.Should()
			.Be(1);

		resultTheory3
			.Where(current => current.Code == AccountCoding.TalaSootBankAccountCode)
			.Where(current => current.IsDebtor == false)
			.Where(current => current.IsCreditor == true)
			.Where(current => current.UseParentDocument == true)
			.Count()
			.Should()
			.Be(1);

		resultTheory3
			.Where(current => current.Code.StartsWith(AccountCoding.UserMoneyAssetsCode))
			.Where(current => current.IsDebtor == true)
			.Where(current => current.IsCreditor == false)
			.Where(current => current.UseParentDocument == true)
			.Count()
			.Should()
			.Be(1);

		resultTheory3
			.Where(current => current.Code.StartsWith(AccountCoding.UserRoundCode))
			.Where(current => current.IsDebtor == true)
			.Where(current => current.IsCreditor == false)
			.Where(current => current.UseParentDocument == true)
			.Count()
			.Should()
			.BeInRange(0, 2);

		resultTheory3
			.Where(current => current.Code == AccountCoding.WalletRechargeFeeIncome)
			.Where(current => current.IsDebtor == true)
			.Where(current => current.IsCreditor == false)
			.Where(current => current.UseParentDocument == true)
			.Count()
			.Should()
			.Be(1);
	}

	#endregion /FindAllAccountCodingData_For_Deposit_And_Withdraw_Should_Return_AccountCodings_With_Name_And_Code

	#region FindAllReferalAccountCodingUser_CheckCount_CheckAmount_CheckCoding

	/// <summary>
	/// دریافت لیست همه کدینگ های مربوط به هدیه رفرال کاربر به دارایی طلای آن
	/// </summary>
	[Fact(DisplayName = "دریافت لیست همه کدینگ های مربوط به هدیه رفرال کاربر به دارایی طلای آن")]
	public async Task
		FindAllReferalAccountCodingUser_CheckCount_CheckAmount_CheckCoding()
	{
		// سناریو:
		// کاربر بعد از اینکه به کاربر عضو و احراز شده تبدیل میشود میتواند در صورت داشتن کد دعوت از دیگران
		// هدیه خود را مطابق میزان هدیه ی ثبتی در دیتابیس دریافت کند
		// این هدیه به دارایی طلای کاربر اضاف میشود
		// جزئیات کدینگ به شکل زیر باید باشد
		// دارای دو رکورد کدینگ هستیم
		// 1. هزینه رفرال با مقدار n سوت / معادل n تومان بستانکار میشود
		// 2. دارایی طلای کاربر به همان مقدار سوت معادل همان مقدار طلا به تومان بدهکار میشود
		// پس خروجی باید دارای 2 رکورد باشد
		// در آن کاربر بدهکار و هزینه رفرال بستانکار شود
		// فقط یک سند اصلی وجود دارد

		// Arrange
		var profile = CreateUserProfile(
			firstName: "احسان",
			lastName: "شاهسونی",
			nationalCode: "1234567890",
			birthDate: DateTime.Now.AddYears(-26),
			address: "شیراز - میانرود",
			genderId: Guid.NewGuid().ToString(),
			cityName: "شیراز",
			provinceName: "فارس"
		);

		await UnitOfWork.ProfileRepository.AddAsync(profile);

		// ایجاد کدینگ دارایی طلا
		_ = await UnitOfWork
			.AccountCodingRepository
			.CreateUserGoldAssetsAsync(profile);

		var referral = new Referral
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,

			GoldSoot = 5,
			CountUsed = 5,
			MaxUserUse = 10,

			EndDate = DateTime.Now.AddDays(10),
			StartDate = DateTime.Now.AddDays(-10)
		};

		await UnitOfWork.ReferralRepository.AddAsync(referral);

		await UnitOfWork.SaveAsync();

		referral.CanUse().Should().BeTrue();

		var onlineGoldService = new OnlineGoldService();
		
		var goldPriceInThisTime =
			await onlineGoldService.GoldPriceInThisTime();

		var allAccountCodingDataForReferal = await UnitOfWork.AccountCodingRepository
			.FindAllAccountCodingDataForReferalAsync(profile, goldPriceInThisTime);

		allAccountCodingDataForReferal.Should().NotBeNull();
		allAccountCodingDataForReferal.Count.Should().Be(2);

		allAccountCodingDataForReferal.Should().Contain(x =>
			x.Code == AccountCoding.ReferalCode
			&& x.IsCreditor == true
			&& x.IsDebtor == false
			&& x.UseParentDocument == true
			&& x.Amount == referral.GoldSoot.GoldToToman(goldPriceInThisTime));

		allAccountCodingDataForReferal.Should().Contain(x =>
			x.Code.StartsWith(AccountCoding.UserGoldAssetsCode)
			&& x.IsCreditor == false
			&& x.IsDebtor == true
			&& x.UseParentDocument == true
			&& x.Amount == referral.GoldSoot.GoldToToman(goldPriceInThisTime));
	}

	#endregion /FindAllReferalAccountCodingUser_CheckCount_CheckAmount_CheckCoding

	#region FindAllGoldPurchaseAccountCodingUser_CheckCount_CheckAmount_CheckCoding

	/// <summary>
	/// دریافت کدینگ های مربوط به خرید آنلاین طلای آب شده
	/// </summary>
	[Fact(DisplayName = "دریافت کدینگ های مربوط به خرید آنلاین طلای آب شده")]
	public async Task
		FindAllGoldPurchaseAccountCodingUser_CheckCount_CheckAmount_CheckCoding()
	{
		// سناریو:
		// خرید طلای آب شده آنلاین با قیمت لحظه ای و مشخص طلا
		// کاربر از طریق موجودی کیف پول تلاش به خرید n سوت طلا میکند
		
		// Arrange
		var profile = CreateUserProfile(
			firstName: "احسان",
			lastName: "شاهسونی",
			nationalCode: "1234567890",
			birthDate: DateTime.Now.AddYears(-26),
			address: "شیراز - میانرود",
			genderId: Guid.NewGuid().ToString(),
			cityName: "شیراز",
			provinceName: "فارس"
		);

		await UnitOfWork.ProfileRepository.AddAsync(profile);

		// ایجاد کدینگ دارایی طلا
		_ = await UnitOfWork
			.AccountCodingRepository
			.CreateUserGoldAssetsAsync(profile);

		// ایجاد کدینگ کیف پول
		_ = await UnitOfWork
			.AccountCodingRepository
			.CreateUserMoneyAssetsAsync(profile);

		// مبلغ رند کاربر
		_ = await UnitOfWork
			.AccountCodingRepository
			.CreateUserRoundCodeAsync(profile);

		await UnitOfWork.SaveAsync();

		var onlineGoldService = new OnlineGoldService();
		
		var goldPriceInThisTime =
			await onlineGoldService.GoldPriceInThisTime();
		
		var goldSoot = 1001m;
		
		// Act
		var allAccountCodingData = await UnitOfWork.AccountCodingRepository
			.FindAllAccountCodingDataForGoldPurchaseAsync(
				profile,
				goldSoot: goldSoot,
				goldPriceInThisTime: goldPriceInThisTime);

		// Assets
		allAccountCodingData.Should().NotBeNull();
		allAccountCodingData.Count.Should().Be(6);

		var feeDoc = allAccountCodingData
			.Where(x => x.Code == AccountCoding.GoldPurchaseFee)
			.FirstOrDefault();

		feeDoc.Should().NotBeNull();
		
		var roundDoc = allAccountCodingData
			.Where(x => x.Code.StartsWith(AccountCoding.UserRoundCode))
			.FirstOrDefault();
		
		roundDoc.Should().NotBeNull();
		
		var moneyAssetsDoc = allAccountCodingData
			.Where(x => x.Code.StartsWith(AccountCoding.UserMoneyAssetsCode))
			.FirstOrDefault();

		moneyAssetsDoc.Should().NotBeNull();
		
		var treasuryDoc = allAccountCodingData
			.Where(x => x.Code == AccountCoding.GoldTreasuryCode)
			.FirstOrDefault();
		
		treasuryDoc.Should().NotBeNull();
		
		var goldAssetsDoc = allAccountCodingData
			.Where(x => x.Code.StartsWith(AccountCoding.UserGoldAssetsCode))
			.FirstOrDefault();
		
		goldAssetsDoc.Should().NotBeNull();
		
		var bankTalaSootDoc = allAccountCodingData
			.Where(x => x.Code == AccountCoding.TalaSootBankAccountCode)
			.FirstOrDefault();
		
		bankTalaSootDoc.Should().NotBeNull();
		
		allAccountCodingData.Should().Contain(
			x => x.Code.StartsWith(AccountCoding.UserMoneyAssetsCode)
			&& x.IsCreditor == true && x.IsDebtor == false && x.UseParentDocument == true
			&& x.Amount ==
				goldSoot.GoldToToman(goldPriceInThisTime) + feeDoc.Amount +  roundDoc.Amount
		);

		allAccountCodingData.Should().Contain(
			x => x.Code.StartsWith(AccountCoding.UserRoundCode)
			&& x.IsCreditor == false && x.IsDebtor == true && x.UseParentDocument == true
			&& x.Amount ==
			(moneyAssetsDoc.Amount - bankTalaSootDoc.Amount - feeDoc.Amount)
		);
		
		allAccountCodingData.Should().Contain(
			x => x.Code == AccountCoding.GoldPurchaseFee
			&& x.IsCreditor == false && x.IsDebtor == true && x.UseParentDocument == true
			&& x.Amount ==
				moneyAssetsDoc.Amount - bankTalaSootDoc.Amount - roundDoc.Amount
		);

		allAccountCodingData.Should().Contain(
			x => x.Code == AccountCoding.TalaSootBankAccountCode
			&& x.IsCreditor == false && x.IsDebtor == true && x.UseParentDocument == true
			&& x.Amount ==
				moneyAssetsDoc.Amount - roundDoc.Amount - feeDoc.Amount
		);

		allAccountCodingData.Should().Contain(
			x => x.Code == AccountCoding.GoldTreasuryCode
			&& x.IsCreditor == true && x.IsDebtor == false &&  x.UseParentDocument == true
			&& x.Amount == goldSoot.GoldToToman(goldPriceInThisTime)
		);

		allAccountCodingData.Should().Contain(
			x => x.Code.StartsWith(AccountCoding.UserGoldAssetsCode)
			&& x.IsCreditor == false && x.IsDebtor == true &&  x.UseParentDocument == true
			&& x.Amount == goldSoot.GoldToToman(goldPriceInThisTime)
		);
		
		treasuryDoc.Amount.Should().Be(goldAssetsDoc.Amount);
		treasuryDoc.GoldSoot.Should().Be(goldAssetsDoc.GoldSoot);
	}

	#endregion /FindAllGoldPurchaseAccountCodingUser_CheckCount_CheckAmount_CheckCoding

	#region FindAllSaleOfGoldAccountCodingUser_CheckCount_CheckAmount_CheckCoding
	
	/// <summary>
	/// دریافت کدینگ های مربوط به فروش آنلاین طلای آب شده
	/// </summary>
	[Fact(DisplayName = "دریافت کدینگ های مربوط به فروش آنلاین طلای آب شده")]
	public async Task
		FindAllSaleOfGoldAccountCodingUser_CheckCount_CheckAmount_CheckCoding()
	{
		// سناریو:
		// فروش طلای آب شده آنلاین با قیمت لحظه ای و مشخص طلا
		// کاربر از طریق دارایی طلای خود تلاش به فروش n سوت طلا میکند
		
		// Arrange
		var profile = CreateUserProfile(
			firstName: "احسان",
			lastName: "شاهسونی",
			nationalCode: "1234567890",
			birthDate: DateTime.Now.AddYears(-26),
			address: "شیراز - میانرود",
			genderId: Guid.NewGuid().ToString(),
			cityName: "شیراز",
			provinceName: "فارس"
		);

		await UnitOfWork.ProfileRepository.AddAsync(profile);

		// ایجاد کدینگ دارایی طلا
		_ = await UnitOfWork
			.AccountCodingRepository
			.CreateUserGoldAssetsAsync(profile);

		// ایجاد کدینگ کیف پول
		_ = await UnitOfWork
			.AccountCodingRepository
			.CreateUserMoneyAssetsAsync(profile);

		// مبلغ رند کاربر
		_ = await UnitOfWork
			.AccountCodingRepository
			.CreateUserRoundCodeAsync(profile);

		await UnitOfWork.SaveAsync();

		var onlineGoldService = new OnlineGoldService();
		
		var goldPriceInThisTime =
			await onlineGoldService.GoldPriceInThisTime();
		
		var goldSoot = 10m;
		
		// Act
		var allAccountCodingData = await UnitOfWork.AccountCodingRepository
			.FindAllAccountCodingDataForSeleOfGoldAsync(
				profile,
				goldSoot: goldSoot,
				goldPriceInThisTime: goldPriceInThisTime);

		// Assets
		allAccountCodingData.Should().NotBeNull();
		allAccountCodingData.Count.Should().Be(6);

		var feeDoc = allAccountCodingData
			.Where(x => x.Code == AccountCoding.IncomeSaleOfGoldCode)
			.FirstOrDefault();

		feeDoc.Should().NotBeNull();
		
		var roundDoc = allAccountCodingData
			.Where(x => x.Code.StartsWith(AccountCoding.UserRoundCode))
			.FirstOrDefault();
		
		roundDoc.Should().NotBeNull();
		
		var moneyAssetsDoc = allAccountCodingData
			.Where(x => x.Code.StartsWith(AccountCoding.UserMoneyAssetsCode))
			.FirstOrDefault();

		moneyAssetsDoc.Should().NotBeNull();
		
		var treasuryDoc = allAccountCodingData
			.Where(x => x.Code == AccountCoding.GoldTreasuryCode)
			.FirstOrDefault();
		
		treasuryDoc.Should().NotBeNull();
		
		var goldAssetsDoc = allAccountCodingData
			.Where(x => x.Code.StartsWith(AccountCoding.UserGoldAssetsCode))
			.FirstOrDefault();
		
		goldAssetsDoc.Should().NotBeNull();
		
		var bankTalaSootDoc = allAccountCodingData
			.Where(x => x.Code == AccountCoding.TalaSootBankAccountCode)
			.FirstOrDefault();
		
		bankTalaSootDoc.Should().NotBeNull();
		
		allAccountCodingData.Should().Contain(
			x => x.Code.StartsWith(AccountCoding.UserGoldAssetsCode)
			     && x.IsCreditor == true && x.IsDebtor == false &&  x.UseParentDocument == true
			     && x.Amount == goldSoot.GoldToToman(goldPriceInThisTime)
		);
		
		allAccountCodingData.Should().Contain(
			x => x.Code == AccountCoding.GoldTreasuryCode
			     && x.IsCreditor == false && x.IsDebtor == true &&  x.UseParentDocument == true
			     && x.Amount == goldSoot.GoldToToman(goldPriceInThisTime)
		);
		
		treasuryDoc.Amount.Should().Be(goldAssetsDoc.Amount);
		treasuryDoc.GoldSoot.Should().Be(goldAssetsDoc.GoldSoot);
		
		allAccountCodingData.Should().Contain(
			x => x.Code == AccountCoding.TalaSootBankAccountCode
			     && x.IsCreditor == true && x.IsDebtor == false && x.UseParentDocument == true
			     && x.Amount ==
			     goldSoot.GoldToToman(goldPriceInThisTime)
		);
	}

	#endregion /FindAllSaleOfGoldAccountCodingUser_CheckCount_CheckAmount_CheckCoding
}