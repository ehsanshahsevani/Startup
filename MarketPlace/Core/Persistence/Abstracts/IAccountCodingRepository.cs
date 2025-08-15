using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface IAccountCodingRepository : IRepository<AccountCoding>
{
	/// <summary>
	/// پیدا کردن یک اکانت کد با 
	/// </summary>
	/// <param name="code"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<AccountCoding?> FindByCodeAsync(string code, CancellationToken cancellationToken = default);

	/// <summary>
	/// پیدا کردن اکانت کد مربوط به هدیه رفرال و دعوت دوستان
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>اکانت کد هدیه و رفرال</returns>
	Task<AccountCoding?> FindReferalAccountCodingAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// پیدا کردن اکانت کد مربوط به خزانه طلا
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<AccountCoding?> FindGoldTreasuryAccountCodingAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// پیدا کردن اکانت کد مربوط به درآمد کارمزد خرید طلا
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<AccountCoding?> FindGoldPurchaseFeeAccountCodingAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// پیدا کردن اکانت کد مربوط به درآمد نگهداری طلا
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<AccountCoding?> FindGoldMaintenanceFeeAccountCodingAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// پیدا کردن اکانت کد مربوط به درآمد فروش طلا
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<AccountCoding?> FindIncomeSaleOfGoldCodeAccountCodingAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// پیدا کردن اکانت کد مربوط به شارژ کیف پول
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<AccountCoding?> FindWalletRechargeFeeIncomeAccountCodingAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// اتصال موجودی بانکی ادمین به درخت حسابداری 
	/// code: 011011
	/// name: موجودی حساب بانکی طلاسوت
	/// </summary>
	/// <exception cref="ArgumentNullException"></exception>
	Task<AccountCoding> ConnectBankAccountAmountToTalaSootBankAccountAsync(TalaSootBankAccount bankAccount,
		CancellationToken cancellationToken = default);

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
	Task<AccountCoding> CreateUserMoneyAssetsAsync(Profile profile, CancellationToken cancellationToken = default);

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
	Task<AccountCoding> CreateUserGoldAssetsAsync(Profile profile, CancellationToken cancellationToken = default);

	/// <summary>
	/// code: 01104
	/// name: کد حساب بانکی کاربران
	/// ایجاد و اتصال حساب بانکی کاربر به درخت حسابداری
	/// این بخش به عنوان کد حساب بانکی در اسناد استفاده میشود
	/// تمامی کاربران همزمان با ایجاد یک پروفایل باید یک کد حسابداری دریافت کنند
	/// </summary>
	/// <param name="profileBank"></param>
	/// <param name="cancellationToken"></param>
	Task<AccountCoding> CreateUserBankAccountAsync(
		ProfileBank profileBank, CancellationToken cancellationToken = default);

	/// <summary>
	/// دریافت لیست کدینگ مربوط به ثبت سند حسابداری واریز یا برداشت از کیف پول
	/// </summary>
	/// <param name="profileBank">حساب بانکی مورد نظر شخص</param>
	/// <param name="isDeposit">برداشت انجام میشود یا خیر در صورت برداشت نبودن سیستم واریز درنظر میگیرد</param>
	/// <param name="amount">مبلغ مورد نظر برای واریز یا برداشت</param>
	/// <param name="goldPriceInThisTime"></param>
	/// <param name="cancellationToken"></param>
	/// <returns>لیست کدینگ هایی مناسب ثبت سند برای بخش حسابداری</returns>
	Task<List<AccountCoding>> FindAllAccountCodingDataForDepositAndWithdrawalAsync(ProfileBank profileBank,
		bool isDeposit, decimal amount, decimal goldPriceInThisTime, CancellationToken cancellationToken = default);

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
	Task<AccountCoding> CreateUserRoundCodeAsync(
		Domain.Profile profile, CancellationToken cancellationToken = default);

	/// <summary>
	/// دریافت لیست کدینگ مربوط به ثبت سند حسابداری هدیه رفرال و دعوت دوستان
	/// </summary>
	/// <param name="profile">کاربری که قرار است هدیه را دریافت کند</param>
	/// <param name="goldPriceInThisTime"></param>
	/// <param name="cancellationToken"></param>
	/// <returns>لیست کدینگ هایی مناسب ثبت سند برای بخش حسابداری</returns>
	Task<List<AccountCoding>> FindAllAccountCodingDataForReferalAsync(
		Profile profile, decimal goldPriceInThisTime, CancellationToken cancellationToken = default);

	/// <summary>
	/// پیدا کردن اکانت کد مربوط به فروش محصول در فروشگاه
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<AccountCoding?> FindIncomeSellingGoodsCodeAccountCodingAsync(
		CancellationToken cancellationToken = default);

	/// <summary>
	/// پیدا کردن اکانت کد مربوط به کمسیون
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<AccountCoding?> FindIncomeCommissionCodeCodeAccountCodingAsync(
		CancellationToken cancellationToken = default);

	/// <summary>
	/// دریافت لیست کدینگ مربوط به ثبت سند حسابداری خرید طلای آب شده با موجودی کیف پول
	/// </summary>
	/// <param name="profile">پروفایل</param>
	/// <param name="goldSoot">وزن به سوت برای خرید</param>
	/// <param name="goldPriceInThisTime">قیمت لحظه ای طلا</param>
	/// <param name="cancellationToken"></param>
	/// <returns>لیست کدینگ هایی مناسب ثبت سند برای بخش حسابداری</returns>
	Task<List<AccountCoding>>
		FindAllAccountCodingDataForGoldPurchaseAsync(
			Profile profile,
			decimal goldSoot,
			decimal goldPriceInThisTime,
			CancellationToken cancellationToken = default);

	/// <summary>
	/// دریافت لیست کدینگ مربوط به ثبت سند حسابداری فروش طلای آب شده با موجودی دارای طلا
	/// </summary>
	/// <param name="profile">پروفایل</param>
	/// <param name="goldSoot">وزن به سوت برای فروش</param>
	/// <param name="goldPriceInThisTime">قیمت لحظه ای طلا</param>
	/// <param name="cancellationToken"></param>
	/// <returns>لیست کدینگ هایی مناسب ثبت سند برای بخش حسابداری</returns>
	Task<List<AccountCoding>>
		FindAllAccountCodingDataForSeleOfGoldAsync(
			Profile profile,
			decimal goldSoot,
			decimal goldPriceInThisTime,
			CancellationToken cancellationToken = default);

	/// <summary>
	/// پیدا کردن کد مربوط به حساب طلاسوت
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<AccountCoding?> FindTalasootBankAccountCodeAccountCodingAsync(
		CancellationToken cancellationToken = default);
}