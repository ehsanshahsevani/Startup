using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class ReferralRepository : Repository<Referral>, IReferralRepository
{
	internal ReferralRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}

	/// <summary>
	/// پیدا کردن آخرین رکورد تنظیمات مربوط به رفرال و اکانت کدینگ
	/// درصورت نبودن نال برمیگرداند
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>رکورد نهایی / نال</returns>
	public async Task<Referral?> FindLastRecordAsync(CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			
			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			
			.OrderByDescending(x => x.CreateDateTime)
			
			.FirstOrDefaultAsync(cancellationToken);
		
		return result;
	}
}