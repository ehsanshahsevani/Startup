using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface ITalaSootBankAccountRepository : IRepository<TalaSootBankAccount>
{
	/// <summary>
	/// دریافت حساب موجود در سیستم
	/// </summary>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	Task<TalaSootBankAccount?>
		FindFirstRecordAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// گرفتن اولین رکورد با اولین مقدار موجود در دیتابیس
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<decimal> FindFirstAmountAsync
		(CancellationToken cancellationToken = default);
}