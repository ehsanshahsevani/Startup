using Persistence.Abstracts;
using Persistence.Repositories;

namespace Persistence;

public class UnitOfWork : Base.UnitOfWork, IUnitOfWork
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public UnitOfWork(Tools.Options options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	{
	}

	private IAccountCodingRepository _accountCodingRepository;

	public IAccountCodingRepository AccountCodingRepository
	{
		get
		{
			_accountCodingRepository ??= new AccountCodingRepository(DatabaseContext);
			return _accountCodingRepository;
		}
	}

	// **************************************************
	private IAccountCodingSubSystemLocalRepository _accountCodingSubSystemLocalRepository;

	public IAccountCodingSubSystemLocalRepository AccountCodingSubSystemLocalRepository
	{
		get
		{
			_accountCodingSubSystemLocalRepository ??= new AccountCodingSubSystemLocalRepository(DatabaseContext);
			return _accountCodingSubSystemLocalRepository;
		}
	}

	// **************************************************
	private IAttachmentRepository _attachmentRepository;

	public IAttachmentRepository AttachmentRepository
	{
		get
		{
			_attachmentRepository ??= new AttachmentRepository(DatabaseContext);
			return _attachmentRepository;
		}
	}

	// **************************************************
	private IAttachmentSubjectRepository _attachmentSubjectRepository;

	public IAttachmentSubjectRepository AttachmentSubjectRepository
	{
		get
		{
			_attachmentSubjectRepository ??= new AttachmentSubjectRepository(DatabaseContext);
			return _attachmentSubjectRepository;
		}
	}

	// **************************************************
	private IBankRepository _bankRepository;

	public IBankRepository BankRepository
	{
		get
		{
			_bankRepository ??= new BankRepository(DatabaseContext);
			return _bankRepository;
		}
	}

	// **************************************************
	private ITalaSootBankAccountRepository _talaSootBankAccountRepository;

	public ITalaSootBankAccountRepository TalaSootBankAccountRepository
	{
		get
		{
			_talaSootBankAccountRepository ??= new TalaSootBankAccountRepository(DatabaseContext);
			return _talaSootBankAccountRepository;
		}
	}

	// **************************************************
	private IBranchRepository _branchRepository;

	public IBranchRepository BranchRepository
	{
		get
		{
			_branchRepository ??= new BranchRepository(DatabaseContext);
			return _branchRepository;
		}
	}

	// **************************************************
	private ICartItemRepository _cartItemRepository;

	public ICartItemRepository CartItemRepository
	{
		get
		{
			_cartItemRepository ??= new CartItemRepository(DatabaseContext);
			return _cartItemRepository;
		}
	}

	// **************************************************
	private ICategoryRepository _categoryRepository;

	public ICategoryRepository CategoryRepository
	{
		get
		{
			_categoryRepository ??= new CategoryRepository(DatabaseContext);
			return _categoryRepository;
		}
	}

	// **************************************************
	private ICityRepository _cityRepository;

	public ICityRepository CityRepository
	{
		get
		{
			_cityRepository ??= new CityRepository(DatabaseContext);
			return _cityRepository;
		}
	}

	// **************************************************
	private IDocumentRepository _documentRepository;

	public IDocumentRepository DocumentRepository
	{
		get
		{
			_documentRepository ??= new DocumentRepository(DatabaseContext);
			return _documentRepository;
		}
	}

	// **************************************************
	private IDocumentDetailRepository _documentDetailRepository;

	public IDocumentDetailRepository DocumentDetailRepository
	{
		get
		{
			_documentDetailRepository ??= new DocumentDetailRepository(DatabaseContext);
			return _documentDetailRepository;
		}
	}

	// **************************************************
	private IGenderRepository _genderRepository;

	public IGenderRepository GenderRepository
	{
		get
		{
			_genderRepository ??= new GenderRepository(DatabaseContext);
			return _genderRepository;
		}
	}

	// **************************************************
	private IGoldRequestRepository _goldRequestRepository;

	public IGoldRequestRepository GoldRequestRepository
	{
		get
		{
			_goldRequestRepository ??= new GoldRequestRepository(DatabaseContext);
			return _goldRequestRepository;
		}
	}

	// **************************************************
	private IGoldRequestStatusRepository _goldRequestStatusRepository;

	public IGoldRequestStatusRepository GoldRequestStatusRepository
	{
		get
		{
			_goldRequestStatusRepository ??= new GoldRequestStatusRepository(DatabaseContext);
			return _goldRequestStatusRepository;
		}
	}

	// **************************************************
	private IGoldValueRepository _goldValueRepository;

	public IGoldValueRepository GoldValueRepository
	{
		get
		{
			_goldValueRepository ??= new GoldValueRepository(DatabaseContext);
			return _goldValueRepository;
		}
	}

	// **************************************************
	private ITransactionRepository _transactionRepository;

	public ITransactionRepository TransactionRepository
	{
		get
		{
			_transactionRepository ??= new TransactionRepository(DatabaseContext);
			return _transactionRepository;
		}
	}

	// **************************************************
	private IOrderRepository _orderRepository;

	public IOrderRepository OrderRepository
	{
		get
		{
			_orderRepository ??= new OrderRepository(DatabaseContext);
			return _orderRepository;
		}
	}

	// **************************************************
	private IOrderItemRepository _orderItemRepository;

	public IOrderItemRepository OrderItemRepository
	{
		get
		{
			_orderItemRepository ??= new OrderItemRepository(DatabaseContext);
			return _orderItemRepository;
		}
	}

	// **************************************************
	private IOrderStatusRepository _orderStatusRepository;

	public IOrderStatusRepository OrderStatusRepository
	{
		get
		{
			_orderStatusRepository ??= new OrderStatusRepository(DatabaseContext);
			return _orderStatusRepository;
		}
	}

	// **************************************************
	private IPageSettingRepository _pageSettingRepository;

	public IPageSettingRepository PageSettingRepository
	{
		get
		{
			_pageSettingRepository ??= new PageSettingRepository(DatabaseContext);
			return _pageSettingRepository;
		}
	}
	// **************************************************
	
	// **************************************************
	private ITagPageSettingRepository _tagPageSettingRepository;

	public ITagPageSettingRepository TagPageSettingRepository
	{
		get
		{
			_tagPageSettingRepository ??= new TagPageSettingRepository(DatabaseContext);
			return _tagPageSettingRepository;
		}
	}
	// **************************************************
	
	// **************************************************
	private IPageSettingTagPageSettingRepository _pageSettingTagPageSettingRepository;

	public IPageSettingTagPageSettingRepository PageSettingTagPageSettingRepository
	{
		get
		{
			_pageSettingTagPageSettingRepository ??= new PageSettingTagPageSettingRepository(DatabaseContext);
			return _pageSettingTagPageSettingRepository;
		}
	}
	// **************************************************

	// **************************************************
	private IProductRepository _productRepository;

	public IProductRepository ProductRepository
	{
		get
		{
			_productRepository ??= new ProductRepository(DatabaseContext);
			return _productRepository;
		}
	}

	// **************************************************
	private IProductBranchRepository _productBranchRepository;

	public IProductBranchRepository ProductBranchRepository
	{
		get
		{
			_productBranchRepository ??= new ProductBranchRepository(DatabaseContext);
			return _productBranchRepository;
		}
	}

	// **************************************************
	private IProfileRepository _profileRepository;

	public IProfileRepository ProfileRepository
	{
		get
		{
			_profileRepository ??= new ProfileRepository(DatabaseContext);
			return _profileRepository;
		}
	}

	// **************************************************
	private IProfileBankRepository _profileBankRepository;

	public IProfileBankRepository ProfileBankRepository
	{
		get
		{
			_profileBankRepository ??= new ProfileBankRepository(DatabaseContext);
			return _profileBankRepository;
		}
	}

	// **************************************************
	private IProfileHistoryRepository _profileHistoryRepository;

	public IProfileHistoryRepository ProfileHistoryRepository
	{
		get
		{
			_profileHistoryRepository ??= new ProfileHistoryRepository(DatabaseContext);
			return _profileHistoryRepository;
		}
	}

	// **************************************************
	private IProvinceRepository _provinceRepository;

	public IProvinceRepository ProvinceRepository
	{
		get
		{
			_provinceRepository ??= new ProvinceRepository(DatabaseContext);
			return _provinceRepository;
		}
	}

	// **************************************************
	private IReasonRegisterInSystemRepository _reasonRegisterInSystemRepository;

	public IReasonRegisterInSystemRepository ReasonRegisterInSystemRepository
	{
		get
		{
			_reasonRegisterInSystemRepository ??= new ReasonRegisterInSystemRepository(DatabaseContext);
			return _reasonRegisterInSystemRepository;
		}
	}

	// **************************************************
	private IReferralRepository _referralRepository;

	public IReferralRepository ReferralRepository
	{
		get
		{
			_referralRepository ??= new ReferralRepository(DatabaseContext);
			return _referralRepository;
		}
	}

	// **************************************************
	private IRoleGoldRepository _roleGoldRepository;

	public IRoleGoldRepository RoleGoldRepository
	{
		get
		{
			_roleGoldRepository ??= new RoleGoldRepository(DatabaseContext);
			return _roleGoldRepository;
		}
	}

	// **************************************************
	private IRoleMoneyRepository _roleMoneyRepository;

	public IRoleMoneyRepository RoleMoneyRepository
	{
		get
		{
			_roleMoneyRepository ??= new RoleMoneyRepository(DatabaseContext);
			return _roleMoneyRepository;
		}
	}

	// **************************************************
	private IShopRepository _shopRepository;

	public IShopRepository ShopRepository
	{
		get
		{
			_shopRepository ??= new ShopRepository(DatabaseContext);
			return _shopRepository;
		}
	}

	// **************************************************
	private ISubSystemLocalRepository _subSystemLocalRepository;

	public ISubSystemLocalRepository SubSystemLocalRepository
	{
		get
		{
			_subSystemLocalRepository ??= new SubSystemLocalRepository(DatabaseContext);
			return _subSystemLocalRepository;
		}
	}

	// **************************************************
	private ITalaSootSettingsRepository _talaSootSettingsRepository;

	public ITalaSootSettingsRepository TalaSootSettingsRepository
	{
		get
		{
			_talaSootSettingsRepository ??= new TalaSootSettingsRepository(DatabaseContext);
			return _talaSootSettingsRepository;
		}
	}

	// **************************************************
	private ITypeRoleGoldRepository _typeRoleGoldRepository;

	public ITypeRoleGoldRepository TypeRoleGoldRepository
	{
		get
		{
			_typeRoleGoldRepository ??= new TypeRoleGoldRepository(DatabaseContext);
			return _typeRoleGoldRepository;
		}
	}

	// **************************************************
	private ITypeRoleMoneyRepository _typeRoleMoneyRepository;

	public ITypeRoleMoneyRepository TypeRoleMoneyRepository
	{
		get
		{
			_typeRoleMoneyRepository ??= new TypeRoleMoneyRepository(DatabaseContext);
			return _typeRoleMoneyRepository;
		}
	}

	// **************************************************
	private IUserAssetsRepository _userAssetsRepository;

	public IUserAssetsRepository UserAssetsRepository
	{
		get
		{
			_userAssetsRepository ??= new UserAssetsRepository(DatabaseContext);
			return _userAssetsRepository;
		}
	}
	// **************************************************

	// **************************************************

	private IGoldTreasuryOnlineRepository _goldTreasuryOnlineRepository;

	public IGoldTreasuryOnlineRepository GoldTreasuryOnlineRepository
	{
		get
		{
			_goldTreasuryOnlineRepository ??= new GoldTreasuryOnlineRepository(DatabaseContext);
			return _goldTreasuryOnlineRepository;
		}
	}
	// **************************************************

	private IGoldTreasuryReceiveRepository _goldTreasuryReceiveRepository;

	public IGoldTreasuryReceiveRepository GoldTreasuryReceiveRepository
	{
		get
		{
			_goldTreasuryReceiveRepository ??= new GoldTreasuryReceiveRepository(DatabaseContext);
			return _goldTreasuryReceiveRepository;
		}
	}

	// **************************************************
	private IIncomeCommissionFeeRepository _incomeCommissionFeeRepository;

	public IIncomeCommissionFeeRepository IncomeCommissionFeeRepository
	{
		get
		{
			_incomeCommissionFeeRepository ??= new IncomeCommissionFeeRepository(DatabaseContext);
			return _incomeCommissionFeeRepository;
		}
	}
	// **************************************************

	private IIncomeGoldPurchaseFeeRepository _incomeGoldPurchaseFeeRepository;

	public IIncomeGoldPurchaseFeeRepository IncomeGoldPurchaseFeeRepository
	{
		get
		{
			_incomeGoldPurchaseFeeRepository ??= new IncomeGoldPurchaseFeeRepository(DatabaseContext);
			return _incomeGoldPurchaseFeeRepository;
		}
	}
	// **************************************************

	private IIncomeMaintenanceAndInsuranceFeeRepository _incomeMaintenanceAndInsuranceFeeRepository;

	public IIncomeMaintenanceAndInsuranceFeeRepository IncomeMaintenanceAndInsuranceFeeRepository
	{
		get
		{
			_incomeMaintenanceAndInsuranceFeeRepository ??=
				new IncomeMaintenanceAndInsuranceFeeRepository(DatabaseContext);
			return _incomeMaintenanceAndInsuranceFeeRepository;
		}
	}
	// **************************************************

	private IIncomeSaleOfGoldFeeRepository _incomeSaleOfGoldFeeRepository;

	public IIncomeSaleOfGoldFeeRepository IncomeSaleOfGoldFeeRepository
	{
		get
		{
			_incomeSaleOfGoldFeeRepository ??= new IncomeSaleOfGoldFeeRepository(DatabaseContext);
			return _incomeSaleOfGoldFeeRepository;
		}
	}
	// **************************************************

	private IIncomeWalletRechargeFeeRepository _incomeWalletRechargeFeeRepository;

	public IIncomeWalletRechargeFeeRepository IncomeWalletRechargeFeeRepository
	{
		get
		{
			_incomeWalletRechargeFeeRepository ??= new IncomeWalletRechargeFeeRepository(DatabaseContext);
			return _incomeWalletRechargeFeeRepository;
		}
	}
	// **************************************************
}