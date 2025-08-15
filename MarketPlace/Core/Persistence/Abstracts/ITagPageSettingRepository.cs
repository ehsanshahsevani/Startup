using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface ITagPageSettingRepository : IRepository<TagPageSetting>
{
	/// <summary>
	/// Retrieves the TagPageSetting entity with the specified name from the repository asynchronously.
	/// </summary>
	/// <param name="name">The name of the TagPageSetting entity to be retrieved.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>The TagPageSetting entity with the specified name, or null if no such entity exists.</returns>
	Task<TagPageSetting?> FindByNameAsync(string name, CancellationToken cancellationToken = default);

	/// <summary>
	/// find all tags by name and get when the name-en start with name
	/// </summary>
	/// <param name="name">Banner -> BannerHomePageTopLeft, BannerHomePageBottom</param>
	/// <param name="cancellationToken"></param>
	/// <returns>TagPageSetting List</returns>
	Task<List<TagPageSetting>> FindByStartWithNamesAsync(
		string name, CancellationToken cancellationToken = default);
}