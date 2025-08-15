using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class TalaSootSettingsRepository : Repository<TalaSootSettings>, ITalaSootSettingsRepository
{
	internal TalaSootSettingsRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}

	/// <summary>
	/// دریافت قانون موجود در سیستم
	/// </summary>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public async Task<TalaSootSettings?>
		FindFirstRecordAsync(CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			.FirstOrDefaultAsync(cancellationToken);
		
		return result;
	}
}