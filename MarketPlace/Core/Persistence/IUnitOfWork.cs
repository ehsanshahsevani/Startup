using Persistence.Abstracts;

namespace Persistence;

public interface IUnitOfWork : PersistenceSeedworks.IUnitOfWork
{
    IAccountCodingRepository AccountCodingRepository { get; }
    IAccountCodingSubSystemLocalRepository AccountCodingSubSystemLocalRepository { get; }
    IAttachmentRepository AttachmentRepository { get; }
    IAttachmentSubjectRepository AttachmentSubjectRepository { get; }
    IBankRepository BankRepository { get; }
    ITalaSootBankAccountRepository TalaSootBankAccountRepository { get; }
    IBranchRepository BranchRepository { get; }
    ICartItemRepository CartItemRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    ICityRepository CityRepository { get; }
    IDocumentRepository DocumentRepository { get; }
    IDocumentDetailRepository DocumentDetailRepository { get; }
    IGenderRepository GenderRepository { get; }
    IGoldRequestRepository GoldRequestRepository { get; }
    IGoldRequestStatusRepository GoldRequestStatusRepository { get; }
    IGoldValueRepository GoldValueRepository { get; }
    ITransactionRepository TransactionRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrderItemRepository OrderItemRepository { get; }
    IOrderStatusRepository OrderStatusRepository { get; }
    ITagPageSettingRepository TagPageSettingRepository { get; }
    IPageSettingRepository PageSettingRepository { get; }
    IPageSettingTagPageSettingRepository PageSettingTagPageSettingRepository { get; }
    IProductRepository ProductRepository { get; }
    IProductBranchRepository ProductBranchRepository { get; }
    IProfileRepository ProfileRepository { get; }
    IProfileBankRepository ProfileBankRepository { get; }
    IProfileHistoryRepository ProfileHistoryRepository { get; }
    IProvinceRepository ProvinceRepository { get; }
    IReasonRegisterInSystemRepository ReasonRegisterInSystemRepository { get; }
    IReferralRepository ReferralRepository { get; }
    IRoleGoldRepository RoleGoldRepository { get; }
    IRoleMoneyRepository RoleMoneyRepository { get; }
    IShopRepository ShopRepository { get; }
    ISubSystemLocalRepository SubSystemLocalRepository { get; }
    ITalaSootSettingsRepository TalaSootSettingsRepository { get; }
    ITypeRoleGoldRepository TypeRoleGoldRepository { get; }
    ITypeRoleMoneyRepository TypeRoleMoneyRepository { get; }
    IUserAssetsRepository UserAssetsRepository { get; }
    IGoldTreasuryOnlineRepository GoldTreasuryOnlineRepository { get; }
    IGoldTreasuryReceiveRepository GoldTreasuryReceiveRepository { get; }
    IIncomeCommissionFeeRepository IncomeCommissionFeeRepository { get; }
    IIncomeGoldPurchaseFeeRepository IncomeGoldPurchaseFeeRepository { get; }
    IIncomeMaintenanceAndInsuranceFeeRepository IncomeMaintenanceAndInsuranceFeeRepository { get; }
    IIncomeSaleOfGoldFeeRepository IncomeSaleOfGoldFeeRepository { get; }
    IIncomeWalletRechargeFeeRepository IncomeWalletRechargeFeeRepository { get; }
}