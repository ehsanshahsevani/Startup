using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface ITalaSootSettingsRepository : IRepository<TalaSootSettings>
{
	/// <summary>
	/// دریافت قانون موجود در سیستم
	/// </summary>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	Task<TalaSootSettings?> FindFirstRecordAsync(CancellationToken cancellationToken = default);
}