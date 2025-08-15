using System.Globalization;
using System.Runtime.CompilerServices;
using Domain;
using Enums.Marketplace;
using FluentAssertions;
using Persistence.Tools;
using HttpServices.Marketplace.Gold;
using Utilities;

namespace Repositories;

public class DocumentRepositoryTest : Base.BaseTestWithDatabaseInMemory
{
	#region Database_Constructor_InitialDat

	public DocumentRepositoryTest() : base()
	{
	}

	#endregion /Database_Constructor_InitialData

	#region CheckUpdateUserAssetsInUserAssetsTable_ShouldReturnCorrectEntity

	/// <summary>
	/// آپدیت دارایی کاربر بعد از تغییر دارایی آن در یک تراکنش
	/// </summary>
	[Fact(DisplayName = "آپدیت دارایی کاربر بعد از تغییر دارایی آن در یک تراکنش")]
	public async Task CheckUpdateUserAssetsInUserAssetsTable_ShouldReturnCorrectEntity()
	{
		// Theory:
		// شارژ کیف پول به مبلغ 30000 تومان ( سی هزار تومان )
		// آپدیت دارایی با دارایی اولیه صفر

		var onlineGoldService = new OnlineGoldService();

		var goldPriceInThisTime =
			await onlineGoldService.GoldPriceInThisTime();

		decimal amount = 30_000;

		// Arrange
		// **************************************************
		var profileBank = await CreateUserDataByUserAssetsAsync(goldPriceInThisTime);

		var accountCodings =
			await UnitOfWork.AccountCodingRepository
				.FindAllAccountCodingDataForDepositAndWithdrawalAsync
					(profileBank, isDeposit: false, amount, goldPriceInThisTime);
		// **************************************************

		// **************************************************
		var codingFee = accountCodings.First(x => x.Code == AccountCoding.WalletRechargeFeeIncome);
		var balance = amount - codingFee.Amount;

		// Act
		var newAssets =
			await UnitOfWork
				.UserAssetsRepository
				.UpdateUserAssetsAsync(accountCodings, profileBank.Profile!);

		// Assets
		newAssets.Should().NotBeNull();

		newAssets.AssetsGold.Should().Be(0);
		newAssets.AssetsWallet.Should().Be(balance);

		newAssets.Amount.Should().Be(newAssets.GoldSoot.GoldToToman(goldPriceInThisTime));

		newAssets.GoldSoot.Should().Be(newAssets.Amount.TomanToGold(goldPriceInThisTime));
		// **************************************************
	}

	#endregion /CheckUpdateUserAssetsInUserAssetsTable_ShouldReturnCorrectEntity

	#region CreateDocumentBoxForDeposit_ShouldReturnCorrectEntity

	/// <summary>
	/// ایجاد داکیومنت باکس مناسب جهت ایجاد متن و تفسیر سند
	/// </summary>
	[Fact(DisplayName = "ایجاد داکیومنت باکس مناسب جهت ایجاد متن و تفسیر سند")]
	public async Task CreateDocumentBoxForDeposit_ShouldReturnCorrectEntity()
	{
		// Theory:
		// شارژ کیف پول به مبلغ 30000 تومان ( سی هزار تومان )
		// آپدیت دارایی با دارایی اولیه صفر
		// ایجاد داکیومنت باکس مناسب جهت ایجاد متن و تفسیر سند

		var onlineGoldService = new OnlineGoldService();

		var goldPriceInThisTime =
			await onlineGoldService.GoldPriceInThisTime();

		decimal amount = 30_000;

		// Arrange
		// **************************************************
		var profileBank = await CreateUserDataByUserAssetsAsync(goldPriceInThisTime);

		var accountCodings =
			await UnitOfWork.AccountCodingRepository
				.FindAllAccountCodingDataForDepositAndWithdrawalAsync
					(profileBank, isDeposit: false, amount, goldPriceInThisTime);

		var newAssets =
			await UnitOfWork
				.UserAssetsRepository
				.UpdateUserAssetsAsync(accountCodings, profileBank.Profile!);

		var codingFee =
			accountCodings
				.First(x => x.Code == AccountCoding.WalletRechargeFeeIncome);

		var balance = amount - codingFee.Amount;
		// **************************************************

		// **************************************************
		// Act
		var documentBox =
			DocumentBox.Create
				(DocumentType.Deposit, accountCodings, newAssets);

		// Assets
		documentBox.Should().NotBeNull();
		documentBox.DocumentFor.Should().NotBeNull();
		documentBox.NotificationText.Should().NotBeNull();

		var stringBalance =
			string.Format("{0:N0}", balance);

		var stringAssetsWallet =
			string.Format("{0:N0}", Math.Round(newAssets.AssetsWallet));

		documentBox.DocumentFor.Should().Contain(stringBalance);
		documentBox.NotificationText.Should().Contain(stringBalance);

		documentBox.NotificationText.Should().Contain($"مانده: {stringAssetsWallet}");
		// **************************************************
	}

	#endregion /CreateDocumentBoxForDeposit_ShouldReturnCorrectEntity

	#region CreateDocumentForDeposit_ShouldReturnCorrectEntity

	/// <summary>
	/// ثبت سندهای شارژ کیف پول در دیتابیس
	/// </summary>
	[Fact(DisplayName = "ثبت سندهای شارژ کیف پول در دیتابیس")]
	public async Task CreateDocumentForDeposit_ShouldReturnCorrectEntity()
	{
		// Theory:
		// شارژ کیف پول به مبلغ 30000 تومان ( سی هزار تومان )
		// آپدیت دارایی با دارایی اولیه صفر
		// ایجاد داکیومنت باکس مناسب جهت ایجاد متن و تفسیر سند
		// ایجاد اسناد مالی اصلی و اساسی سیستم

		var onlineGoldService = new OnlineGoldService();

		var goldPriceInThisTime =
			await onlineGoldService.GoldPriceInThisTime();

		decimal amount = 30_000;
		decimal assetsWallet = 45_600;
		
		// Arrange
		// **************************************************
		var profileBank = await CreateUserDataByUserAssetsAsync(goldPriceInThisTime, assetsWallet);

		var accountCodings =
			await UnitOfWork.AccountCodingRepository
				.FindAllAccountCodingDataForDepositAndWithdrawalAsync
					(profileBank, isDeposit: false, amount, goldPriceInThisTime);

		var newAssets =
			await UnitOfWork
				.UserAssetsRepository
				.UpdateUserAssetsAsync(accountCodings, profileBank.Profile!);
		
		var documentBox =
			DocumentBox.Create
				(DocumentType.Deposit, accountCodings, newAssets);
		// **************************************************

		// **************************************************
		var document = new Domain.Document
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,

			DocumentFor = documentBox.DocumentFor,
			Description = documentBox.NotificationText,
		};

		// Act
		List<Document> documents =
			await UnitOfWork.DocumentRepository
				.AddByAccountCodingsAsync(accountCodings, document);

		await UnitOfWork.SaveAsync();

		// Assets Document
		documents.Should().NotBeNull();
		documents.Count.Should().Be(2);

		// Assets Document 1 => parent doc- index [0]
		// *****
		documents.FirstOrDefault().Should().NotBeNull();
		documents.First().DocumentFor.Should().Be(document.DocumentFor);
		documents.First().Description.Should().Be(document.Description);
		documents.First().DocumentDetails.Should().Contain(x => x.ParentDocumentId == null);
		documents.First().DocumentDetails.Count.Should().Be(3);

		DocumentDetail? feeDetialDocParent =
			documents
				.First().DocumentDetails
				.Where(x => x.IsDebtor == true)
				.Where(x => x.IsCreditor == false)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.WalletRechargeFeeIncome))
				.FirstOrDefault();

		feeDetialDocParent.Should().NotBeNull();

		DocumentDetail? walletUser =
			documents
				.First().DocumentDetails
				.Where(x => x.IsDebtor == true)
				.Where(x => x.IsCreditor == false)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.UserMoneyAssetsCode))
				.FirstOrDefault();

		walletUser.Should().NotBeNull();

		DocumentDetail? talasootBank =
			documents
				.First().DocumentDetails
				.Where(x => x.IsDebtor == false)
				.Where(x => x.IsCreditor == true)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.TalaSootBankAccountCode))
				.FirstOrDefault();

		talasootBank.Should().NotBeNull();

		talasootBank.Amount.Should().Be(feeDetialDocParent.Amount + walletUser.Amount);
		// *****
		newAssets.AssetsWallet.Should().Be(assetsWallet + (amount - feeDetialDocParent.Amount));
		// *****

		// Assets Document 2 => parent doc- index [1] means last index
		// *****
		documents.LastOrDefault().Should().NotBeNull();
		documents.Last().DocumentFor.Should().Be(document.DocumentFor);
		documents.Last().Description.Should().Be(document.Description);
		documents.Last().DocumentDetails.Count.Should().Be(2);

		documents.Last().DocumentDetails.Should().Contain(x => x.ParentDocumentId != null);

		DocumentDetail? bankUser =
			documents
				.Last().DocumentDetails
				.Where(x => x.IsDebtor == false)
				.Where(x => x.IsCreditor == true)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.UserBankAccountCode))
				.FirstOrDefault();

		bankUser.Should().NotBeNull();

		DocumentDetail? talasootBankChild =
			documents
				.Last().DocumentDetails
				.Where(x => x.IsDebtor == true)
				.Where(x => x.IsCreditor == false)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.TalaSootBankAccountCode))
				.FirstOrDefault();

		talasootBankChild.Should().NotBeNull();

		talasootBankChild.Amount.Should().Be(bankUser.Amount);
		// *****
		// **************************************************
	}

	#endregion /CreateDocumentForDeposit_ShouldReturnCorrectEntity

	#region CreateDocumentForWithdrawal_ShouldReturnCorrectEntity

	/// <summary>
	/// ثبت سندهای برداشت از کیف پول در دیتابیس
	/// </summary>
	[Fact(DisplayName = "ثبت سندهای برداشت از کیف پول در دیتابیس")]
	public async Task CreateDocumentForWithdrawal_ShouldReturnCorrectEntity()
	{
		// Theory:
		// آپدیت دارایی با دارایی اولیه صفر
		// ایجاد داکیومنت باکس مناسب جهت ایجاد متن و تفسیر سند
		// ایجاد اسناد مالی اصلی و اساسی سیستم

		var onlineGoldService = new OnlineGoldService();

		var goldPriceInThisTime =
			await onlineGoldService.GoldPriceInThisTime();

		decimal amount = 30_000;
		decimal assetsWallet = 45_600;
		// Arrange
		// **************************************************
		var profileBank = 
			await CreateUserDataByUserAssetsAsync(goldPriceInThisTime, assetsWallet);

		var accountCodings =
			await UnitOfWork.AccountCodingRepository
				.FindAllAccountCodingDataForDepositAndWithdrawalAsync
					(profileBank, isDeposit: true, amount, goldPriceInThisTime);

		var newAssets =
			await UnitOfWork
				.UserAssetsRepository
				.UpdateUserAssetsAsync(accountCodings, profileBank.Profile!);

		newAssets.AssetsWallet.Should().Be(assetsWallet - amount);
		
		var documentBox =
			DocumentBox.Create
				(DocumentType.Withdraw, accountCodings, newAssets);
		// **************************************************

		// **************************************************
		var document = new Domain.Document
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,

			DocumentFor = documentBox.DocumentFor,
			Description = documentBox.NotificationText,
		};

		// Act
		List<Document> documents =
			await UnitOfWork.DocumentRepository
				.AddByAccountCodingsAsync(accountCodings, document);

		await UnitOfWork.SaveAsync();

		// Assets Document
		documents.Should().NotBeNull();
		documents.Count.Should().Be(2);

		// Assets Document 1 => parent doc- index [0]
		// *****
		documents.FirstOrDefault().Should().NotBeNull();
		documents.First().DocumentFor.Should().Be(document.DocumentFor);
		documents.First().Description.Should().Be(document.Description);
		documents.First().DocumentDetails.Should().Contain(x => x.ParentDocumentId == null);
		documents.First().DocumentDetails.Count.Should().Be(2);

		DocumentDetail? walletUser =
			documents
				.First().DocumentDetails
				.Where(x => x.IsDebtor == false)
				.Where(x => x.IsCreditor == true)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.UserMoneyAssetsCode))
				.FirstOrDefault();

		walletUser.Should().NotBeNull();

		DocumentDetail? talasootBank =
			documents
				.First().DocumentDetails
				.Where(x => x.IsDebtor == true)
				.Where(x => x.IsCreditor == false)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.TalaSootBankAccountCode))
				.FirstOrDefault();

		talasootBank.Should().NotBeNull();

		talasootBank.Amount.Should().Be(walletUser.Amount);
		// *****

		// Assets Document 2 => parent doc- index [1] means last index
		// *****
		documents.LastOrDefault().Should().NotBeNull();
		documents.Last().DocumentFor.Should().Be(document.DocumentFor);
		documents.Last().Description.Should().Be(document.Description);
		documents.Last().DocumentDetails.Count.Should().Be(2);

		documents.Last().DocumentDetails.Should().Contain(x => x.ParentDocumentId != null);

		DocumentDetail? bankUser =
			documents
				.Last().DocumentDetails
				.Where(x => x.IsDebtor == true)
				.Where(x => x.IsCreditor == false)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.UserBankAccountCode))
				.FirstOrDefault();

		bankUser.Should().NotBeNull();

		DocumentDetail? talasootBankChild =
			documents
				.Last().DocumentDetails
				.Where(x => x.IsDebtor == false)
				.Where(x => x.IsCreditor == true)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.TalaSootBankAccountCode))
				.FirstOrDefault();

		talasootBankChild.Should().NotBeNull();

		talasootBankChild.Amount.Should().Be(bankUser.Amount);
		// *****
		// **************************************************
	}

	#endregion /CreateDocumentForWithdrawal_ShouldReturnCorrectEntity

	#region CreateDocumentForGoldPurchase_ShouldReturnCorrectEntity

	/// <summary>
	/// ثبت سندهای نهایی مربوط به خرید آنلاین طلای آب شده
	/// </summary>
	[Fact(DisplayName = "ثبت سندهای نهایی مربوط به خرید آنلاین طلای آب شده")]
	public async Task CreateDocumentForGoldPurchase_ShouldReturnCorrectEntity()
	{
		// Theory:
		// آپدیت دارایی با دارایی اولیه صفر
		// ایجاد داکیومنت باکس مناسب جهت ایجاد متن و تفسیر سند
		// ایجاد اسناد مالی اصلی و اساسی سیستم
		// خرید طلای آب شده به میزان 30 سوت
		var onlineGoldService = new OnlineGoldService();

		var goldPriceInThisTime =
			await onlineGoldService.GoldPriceInThisTime();

		decimal amount = 30;
		decimal assetsGold = 10;
		decimal assetsWallet = 1_500_000;
		
		// Arrange
		// **************************************************
		var profileBank = 
			await CreateUserDataByUserAssetsAsync(goldPriceInThisTime, assetsWallet, assetsGold);

		var accountCodings =
			await UnitOfWork.AccountCodingRepository
				.FindAllAccountCodingDataForGoldPurchaseAsync
					(profileBank.Profile!, goldSoot: amount, goldPriceInThisTime);

		UserAssets newAssets =
			await UnitOfWork
				.UserAssetsRepository
				.UpdateUserAssetsAsync(accountCodings, profileBank.Profile!);
		
		var documentBox =
			DocumentBox.Create
				(DocumentType.GoldPurchase, accountCodings, newAssets);
		// **************************************************

		// **************************************************
		var document = new Domain.Document
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,

			DocumentFor = documentBox.DocumentFor,
			Description = documentBox.NotificationText,
		};

		// Act
		List<Document> documents =
			await UnitOfWork.DocumentRepository
				.AddByAccountCodingsAsync(accountCodings, document);

		await UnitOfWork.SaveAsync();

		// Assets Document
		documents.Should().NotBeNull();
		documents.Count.Should().Be(1);

		// Assets Document 1 => parent doc- index [0]
		// *****
		var finalDocument = documents.FirstOrDefault();
		
		finalDocument.Should().NotBeNull();
		finalDocument.DocumentFor.Should().Be(document.DocumentFor);
		finalDocument.Description.Should().Be(document.Description);
		finalDocument.DocumentDetails.Should().Contain(x => x.ParentDocumentId == null);

		// دارایی رند کاربر
		DocumentDetail? roundedUserDoc =
			finalDocument.DocumentDetails
				.Where(x => x.IsDebtor == true)
				.Where(x => x.IsCreditor == false)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.UserRoundCode))
				.FirstOrDefault();

		finalDocument.DocumentDetails.Count.Should().Be(roundedUserDoc is null ? 5 : 6);

		// کیف پول باید بستانکار باشد
		DocumentDetail? walletUser =
			finalDocument.DocumentDetails
				.Where(x => x.IsDebtor == false)
				.Where(x => x.IsCreditor == true)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.UserMoneyAssetsCode))
				.FirstOrDefault();

		walletUser.Should().NotBeNull();

		// حساب بانکی طلاسوت
		DocumentDetail? talasootBank =
			finalDocument.DocumentDetails
				.Where(x => x.IsDebtor == true)
				.Where(x => x.IsCreditor == false)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.TalaSootBankAccountCode))
				.FirstOrDefault();

		talasootBank.Should().NotBeNull();

		// کارمزد خرید طلا
		DocumentDetail? goldPurchaseFee =
			finalDocument.DocumentDetails
				.Where(x => x.IsDebtor == true)
				.Where(x => x.IsCreditor == false)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.GoldPurchaseFee))
				.FirstOrDefault();

		talasootBank.Should().NotBeNull();

		// خزانه طلا بستانکار
		DocumentDetail? goldTreasuryCode =
			finalDocument.DocumentDetails
				.Where(x => x.IsDebtor == false)
				.Where(x => x.IsCreditor == true)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.GoldTreasuryCode))
				.FirstOrDefault();

		goldTreasuryCode.Should().NotBeNull();
		
		// دارایی طلای کاربر بدهکار
		DocumentDetail? goldAssetsUser =
			finalDocument.DocumentDetails
				.Where(x => x.IsDebtor == true)
				.Where(x => x.IsCreditor == false)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.GoldPurchaseFee))
				.FirstOrDefault();

		goldAssetsUser.Should().NotBeNull();

		walletUser.Amount.Should().Be(talasootBank.Amount + goldPurchaseFee!.Amount + roundedUserDoc?.Amount);
		// *****
		// **************************************************
	}

	#endregion /CreateDocumentForGoldPurchase_ShouldReturnCorrectEntity

	#region CreateDocumentForSaleOfGoldCode_ShouldReturnCorrectEntity

	/// <summary>
	/// ثبت سندهای نهایی مربوط به فروش آنلاین طلای آب شده
	/// </summary>
	[Fact(DisplayName = "ثبت سندهای نهایی مربوط به فروش آنلاین طلای آب شده")]
	public async Task CreateDocumentForSaleOfGoldCode_ShouldReturnCorrectEntity()
	{
		// Theory:
		// آپدیت دارایی با دارایی اولیه صفر
		// ایجاد داکیومنت باکس مناسب جهت ایجاد متن و تفسیر سند
		// ایجاد اسناد مالی اصلی و اساسی سیستم
		// فروش طلای آب شده به میزان 30 سوت
		var onlineGoldService = new OnlineGoldService();

		var goldPriceInThisTime =
			await onlineGoldService.GoldPriceInThisTime();

		decimal amount = 30;
		decimal assetsGold = 50;
		decimal assetsWallet = 1_500_000;
		
		// Arrange
		// **************************************************
		var profileBank = 
			await CreateUserDataByUserAssetsAsync(goldPriceInThisTime, assetsWallet, assetsGold);

		var accountCodings =
			await UnitOfWork.AccountCodingRepository
				.FindAllAccountCodingDataForSeleOfGoldAsync
					(profileBank.Profile!, goldSoot: amount, goldPriceInThisTime);

		var newAssets =
			await UnitOfWork
				.UserAssetsRepository
				.UpdateUserAssetsAsync(accountCodings, profileBank.Profile!);
		
		var documentBox =
			DocumentBox.Create
				(DocumentType.SaleOfGoldCode, accountCodings, newAssets);
		// **************************************************

		// **************************************************
		var document = new Domain.Document
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,

			DocumentFor = documentBox.DocumentFor,
			Description = documentBox.NotificationText,
		};

		// Act
		List<Document> documents =
			await UnitOfWork.DocumentRepository
				.AddByAccountCodingsAsync(accountCodings, document);

		await UnitOfWork.SaveAsync();

		// Assets Document
		documents.Should().NotBeNull();
		documents.Count.Should().Be(1);

		// Assets Document 1 => parent doc- index [0]
		// *****
		var finalDocument = documents.FirstOrDefault();
		
		finalDocument.Should().NotBeNull();
		finalDocument.DocumentFor.Should().Be(document.DocumentFor);
		finalDocument.Description.Should().Be(document.Description);
		finalDocument.DocumentDetails.Should().Contain(x => x.ParentDocumentId == null);

		// دارایی رند کاربر
		DocumentDetail? roundedUserDoc =
			finalDocument.DocumentDetails
				.Where(x => x.IsDebtor == true)
				.Where(x => x.IsCreditor == false)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.UserRoundCode))
				.FirstOrDefault();

		finalDocument.DocumentDetails.Count.Should().Be(roundedUserDoc is null ? 5 : 6);

		// کیف پول بدهکار
		DocumentDetail? walletUser =
			finalDocument.DocumentDetails
				.Where(x => x.IsDebtor == true)
				.Where(x => x.IsCreditor == false)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.UserMoneyAssetsCode))
				.FirstOrDefault();

		walletUser.Should().NotBeNull();

		// حساب بانکی طلاسوت
		DocumentDetail? talasootBank =
			finalDocument.DocumentDetails
				.Where(x => x.IsDebtor == false)
				.Where(x => x.IsCreditor == true)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.TalaSootBankAccountCode))
				.FirstOrDefault();

		talasootBank.Should().NotBeNull();

		// کارمزد فروش طلا
		DocumentDetail? saleOfGoldCodeFee =
			finalDocument.DocumentDetails
				.Where(x => x.IsDebtor == true)
				.Where(x => x.IsCreditor == false)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.IncomeSaleOfGoldCode))
				.FirstOrDefault();

		talasootBank.Should().NotBeNull();

		// خزانه طلا بستانکار
		DocumentDetail? goldTreasuryCode =
			finalDocument.DocumentDetails
				.Where(x => x.IsDebtor == true)
				.Where(x => x.IsCreditor == false)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.GoldTreasuryCode))
				.FirstOrDefault();

		goldTreasuryCode.Should().NotBeNull();
		
		// دارایی طلای کاربر بستانکار
		DocumentDetail? goldAssetsUser =
			finalDocument.DocumentDetails
				.Where(x => x.IsDebtor == false)
				.Where(x => x.IsCreditor == true)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.UserGoldAssetsCode))
				.FirstOrDefault();

		goldAssetsUser.Should().NotBeNull();

		talasootBank.Amount.Should().Be(walletUser.Amount + saleOfGoldCodeFee!.Amount + roundedUserDoc?.Amount);
		// *****
		// **************************************************
	}

	#endregion /CreateDocumentForSaleOfGoldCode_ShouldReturnCorrectEntity
	
	#region CreateDocumentForReferalCode_ShouldReturnCorrectEntity

	/// <summary>
	/// ثبت سندهای نهایی مربوط به هدیه رفرال کاربران
	/// </summary>
	[Fact(DisplayName = "ثبت سندهای نهایی مربوط به هدیه رفرال کاربران")]
	public async Task CreateDocumentForReferalCode_ShouldReturnCorrectEntity()
	{
		// Theory:
		// آپدیت دارایی با دارایی اولیه صفر
		// ایجاد داکیومنت باکس مناسب جهت ایجاد متن و تفسیر سند
		// ایجاد اسناد مالی اصلی و اساسی سیستم
		// اعطای هزینه رفرال هدیه به دارایی طلای کاربر
		var onlineGoldService = new OnlineGoldService();

		var goldPriceInThisTime =
			await onlineGoldService.GoldPriceInThisTime();

		decimal assetsGold = 50;
		decimal assetsWallet = 1_500_000;
		
		// Arrange
		// **************************************************
		var profileBank = 
			await CreateUserDataByUserAssetsAsync(goldPriceInThisTime, assetsWallet, assetsGold);

		var accountCodings =
			await UnitOfWork.AccountCodingRepository
				.FindAllAccountCodingDataForReferalAsync
					(profileBank.Profile!, goldPriceInThisTime);

		var newAssets =
			await UnitOfWork
				.UserAssetsRepository
				.UpdateUserAssetsAsync(accountCodings, profileBank.Profile!);
		
		var documentBox =
			DocumentBox.Create
				(DocumentType.Referal, accountCodings, newAssets);
		// **************************************************

		// **************************************************
		var document = new Domain.Document
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,

			DocumentFor = documentBox.DocumentFor,
			Description = documentBox.NotificationText,
		};

		// Act
		List<Document> documents =
			await UnitOfWork.DocumentRepository
				.AddByAccountCodingsAsync(accountCodings, document);

		await UnitOfWork.SaveAsync();

		// Assets Document
		documents.Should().NotBeNull();
		documents.Count.Should().Be(1);

		// Assets Document 1 => parent doc- index [0]
		// *****
		var finalDocument = documents.FirstOrDefault();
		
		finalDocument.Should().NotBeNull();
		finalDocument.DocumentFor.Should().Be(document.DocumentFor);
		finalDocument.Description.Should().Be(document.Description);
		finalDocument.DocumentDetails.Should().Contain(x => x.ParentDocumentId == null);
		
		finalDocument.DocumentDetails.Count.Should().Be(2);
		
		// هزینه رفرال بستانکار
		DocumentDetail? referalDocumentDetaile =
			finalDocument.DocumentDetails
				.Where(x => x.IsDebtor == false)
				.Where(x => x.IsCreditor == true)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.ReferalCode))
				.FirstOrDefault();

		referalDocumentDetaile.Should().NotBeNull();
		
		// دارایی طلای کاربر بدهکار
		DocumentDetail? goldAssetsUser =
			finalDocument.DocumentDetails
				.Where(x => x.IsDebtor == true)
				.Where(x => x.IsCreditor == false)
				.Where(x => x.AccountCoding.Code.StartsWith(AccountCoding.UserGoldAssetsCode))
				.FirstOrDefault();

		goldAssetsUser.Should().NotBeNull();
		
		goldAssetsUser.GoldSoot.Should().Be(referalDocumentDetaile!.GoldSoot);
		// *****
		// **************************************************
	}

	#endregion /CreateDocumentForReferalCode_ShouldReturnCorrectEntity
	
	#region Private_Functions

	private async Task<ProfileBank> CreateUserDataByUserAssetsAsync(
		decimal goldPriceInThisTime, decimal assetsWallet = 0, decimal assetsGold = 0)
	{
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

		//کدینگ دارایی طلای کاربر
		_ =
			await UnitOfWork
				.AccountCodingRepository
				.CreateUserGoldAssetsAsync(profileBank.Profile!);

		// تنظیمات اولیه دریافت رفرال برای کاربران
		var referal = new Referral()
		{
			GoldSoot = 5,
			CountUsed = 1,
			IsActive = true,
			IsDeleted = false,
			MaxUserUse = 100,
			Ordering = 100_000,
			StartDate = DateTime.Now.AddDays(-3),
			EndDate = DateTime.Now.AddDays(3),
			Description = "تنظیمات اولیه دریافت رفرال برای کاربران",
		};
		
		await UnitOfWork.ReferralRepository.AddAsync(referal);

		// دارایی اولیه کاربر
		if (assetsWallet == 0 && assetsGold == 0)
		{
			_ =
				await UnitOfWork
					.UserAssetsRepository
					.CreateIfNotExistFirstAssetsForNewProfile(profileBank.Profile!, goldPriceInThisTime);
		}
		else
		{
			var newAssets = new UserAssets
			{
				IsActive = true,
				IsDeleted = false,
				DocumentId = null,
				Ordering = 100_000,
				AssetsGold = assetsGold,
				AssetsWallet = assetsWallet,
				Profile = profileBank.Profile,
				ProfileId = profileBank.ProfileId,
				GoldPriceInThisTime = goldPriceInThisTime,
				Amount = assetsGold.GoldToToman(goldPriceInThisTime),
				GoldSoot = assetsWallet.TomanToGold(goldPriceInThisTime),
			};

			await UnitOfWork.UserAssetsRepository.AddAsync(newAssets);
		}

		await UnitOfWork.SaveAsync();

		return profileBank;
	}

	#endregion /Private_Functions
}