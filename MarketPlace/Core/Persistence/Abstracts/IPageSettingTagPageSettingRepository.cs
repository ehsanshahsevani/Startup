using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface IPageSettingTagPageSettingRepository : IRepository<PageSettingTagPageSetting>
{
	/// <summary>
	/// Retrieves a list of active, non-deleted PageSetting entities associated with the specified tag identifier.
	/// </summary>
	/// <param name="tagId">
	/// The unique identifier of the tag to filter PageSetting entities.
	/// </param>
	/// <returns>
	/// A task representing the asynchronous operation, containing a list of PageSetting entities
	/// that match the specified tag identifier and meet the criteria of being active and non-deleted.
	/// </returns>
	Task<List<PageSetting?>> FindPageSettingListByTagIdAsync(string tagId);

	/// <summary>
	/// Updates the tag list for a specific page setting by removing existing tags and adding new ones.
	/// </summary>
	/// <param name="pageSettingId">The unique identifier of the page setting.</param>
	/// <param name="tagIds">List of tag IDs to be associated with the page setting.</param>
	/// <returns>
	/// A task representing the asynchronous operation.
	/// </returns>
	Task ClearAllTagsForThisPageAndSetNewListAsync(string pageSettingId, List<string> tagIds);

	/// <summary>
	/// Updates the tag list for a specific page setting by removing existing tags and adding new ones.
	/// </summary>
	/// <param name="pageSettingId">The unique identifier of the page setting.</param>
	/// <param name="tagNames">List of tag Names to be associated with the page setting.</param>
	/// <returns>
	/// A task representing the asynchronous operation.
	/// </returns>
	Task ClearAllTagsForThisPageAndSetNewListByTagNamesAsync(string pageSettingId, List<string> tagNames);
}