using Domain;
using Persistence;
using Infrastructure;
using Persistence.Tests.Helpers;
using Microsoft.Extensions.Configuration;

namespace Base;

public abstract class BaseTestWithDatabaseInMemory : object
{
	protected IUnitOfWork UnitOfWork { get; }

	#region Database_Constructor_InitialData

	protected BaseTestWithDatabaseInMemory() : base()
	{
		UnitOfWork = UnitOfWorkFactory.Create();

		// **************************************************
		// sub system data
		// find all domains in Domain
		List<string> domians =
			DomainSeedworks.BaseEntity.DoaminFinder(nameof(Domain));

		// insert to db if not exist ...
		UnitOfWork.SubSystemLocalRepository.AddByNamesAsync(domians).GetAwaiter().GetResult();

		UnitOfWork.SaveAsync().GetAwaiter().GetResult();
		// **************************************************

		// **************************************************
		var config = new ConfigurationBuilder()
			.AddInMemoryCollection()
			.Build();

		var seeder = new InitialData(config, UnitOfWork);

		seeder.CreateTalaSootSettingsAsync().GetAwaiter().GetResult();

		seeder.CreateTalaSootFeeAsync(
				walletRechargeFee: 0.5m,
				maintenanceAndInsuranceFee: 0.5m,
				goldPurchaseFee: 0.5m,
				incomeSaleOfGoldFee: 0.5m,
				incomeCommissionFee: 0.5m)
			.GetAwaiter().GetResult();

		seeder.CreateTypeRoleGoldAsync().GetAwaiter().GetResult();
		seeder.CreateTypeRoleMoneyAsync().GetAwaiter().GetResult();
		seeder.CreateAccountCodingAsync().GetAwaiter().GetResult();
		seeder.CreateTalaSootBankAccountAsync().GetAwaiter().GetResult();
		seeder.CreateGoldTreasuryAsync().GetAwaiter().GetResult();
		// **************************************************
	}

	#endregion /Database_Constructor_InitialData

	#region Private Functions

	protected Profile CreateUserProfile(
		string firstName,
		string lastName,
		string nationalCode,
		DateTime birthDate,
		string address,
		string genderId,
		string cityName,
		string provinceName,
		bool isActive = true)
	{
		// Create the Province
		var province = new Province
		{
			Name = provinceName,
			IsActive = isActive,
			IsDeleted = false,
			Ordering = 100_000
		};

		// Create the City with the related Province
		var city = new City
		{
			Name = cityName,
			IsActive = isActive,
			IsDeleted = false,
			Ordering = 100_000,
			Province = province
		};

		// Create the Profile
		var profile = new Profile
		{
			IsActive = isActive,
			IsDeleted = false,
			Ordering = 100_000,
			UserId = Guid.NewGuid().ToString(),
			FirstName = firstName,
			LastName = lastName,
			NationalCode = nationalCode,
			BirthDate = birthDate,
			Address = address,
			GenderId = genderId,
			City = city
		};

		return profile;
	}

	protected ProfileBank CreateProfileWithBank(
		string firstName,
		string lastName,
		string nationalCode,
		DateTime birthDate,
		string address,
		string genderId,
		string cityName,
		string provinceName,
		bool profileIsActive,
		string bankName,
		bool bankIsActive,
		string cardNumber,
		string shaba,
		string accountNumber,
		bool profileBankIsActive)
	{
		// Create Profile
		var profile = CreateUserProfile(
			firstName: firstName,
			lastName: lastName,
			nationalCode: nationalCode,
			birthDate: birthDate,
			address: address,
			genderId: genderId,
			cityName: cityName,
			provinceName: provinceName,
			isActive: profileIsActive);

		// Create Bank
		var bank = new Bank
		{
			IsActive = bankIsActive,
			IsDeleted = false,
			Ordering = 100_000,
			Name = bankName,
		};

		// Create ProfileBank
		var profileBank = new ProfileBank
		{
			IsActive = profileBankIsActive,
			IsDeleted = false,
			Ordering = 100_000,
			CardNumber = cardNumber,
			Shaba = shaba,
			AccountNumber = accountNumber,
			ProfileId = profile.Id,
			Profile = profile,
			Bank = bank,
			BankId = bank.Id,
		};

		return profileBank;
	}

	#endregion
}