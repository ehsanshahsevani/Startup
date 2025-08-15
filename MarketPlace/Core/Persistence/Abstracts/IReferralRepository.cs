using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface IReferralRepository : IRepository<Referral>
{
	/// <summary>
	/// پیدا کردن آخرین رکورد تنظیمات مربوط به رفرال و اکانت کدینگ
	/// درصورت نبودن نال برمیگرداند
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>رکورد نهایی / نال</returns>
	Task<Referral?> FindLastRecordAsync(CancellationToken cancellationToken = default);
}