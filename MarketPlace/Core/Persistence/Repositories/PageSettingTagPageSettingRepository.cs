using Domain;
using Persistence.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class PageSettingTagPageSettingRepository : Repository<PageSettingTagPageSetting>, IPageSettingTagPageSettingRepository
{
	internal PageSettingTagPageSettingRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}

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
	public async Task<List<PageSetting?>> FindPageSettingListByTagIdAsync(string tagId)
	{
		List<PageSetting?> result = await DbSet
				
			.Include(current => current.PageSetting)
			
			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.Where(current => current.TagPageSettingId == tagId)
			
			.Select(current => current.PageSetting)
			
			.ToListAsync();
		
		return result;
	}
	
    /// <summary>
    /// Updates the tag list for a specific page setting by removing existing tags and adding new ones.
    /// </summary>
    /// <param name="pageSettingId">The unique identifier of the page setting.</param>
    /// <param name="tagIds">List of tag IDs to be associated with the page setting.</param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    public async Task ClearAllTagsForThisPageAndSetNewListAsync(string pageSettingId, List<string> tagIds)
    {
        var existingTags = await DbSet
		    .Where(current => current.IsDeleted == false)
            .Where(current => current.PageSettingId == pageSettingId)
            .ToListAsync();

        await RemoveRangeAsync(existingTags);

        var newTags = new List<PageSettingTagPageSetting>();
        
        foreach (var tagId in tagIds)
        {
	        var pageTagPage =
		        new PageSettingTagPageSetting(pageSettingId, tagId);
	        
	        newTags.Add(pageTagPage);
        }

        await AddRangeAsync(newTags);
    }
    
    /// <summary>
    /// Updates the tag list for a specific page setting by removing existing tags and adding new ones.
    /// </summary>
    /// <param name="pageSettingId">The unique identifier of the page setting.</param>
    /// <param name="tagNames">List of tag Names to be associated with the page setting.</param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    public async Task ClearAllTagsForThisPageAndSetNewListByTagNamesAsync(string pageSettingId, List<string> tagNames)
    {
	    ITagPageSettingRepository 
		    tagPageSettingRepository
			    = new TagPageSettingRepository(DatabaseContext);
	    
	    var existingTags = await DbSet
		    .Where(current => current.IsDeleted == false)
		    .Where(current => current.PageSettingId == pageSettingId)
		    .ToListAsync();

	    await RemoveRangeAsync(existingTags);

	    var newTags = new List<PageSettingTagPageSetting>();
        
	    foreach (var name in tagNames)
	    {
		    var tag =
			    await tagPageSettingRepository.FindByNameAsync(name);

		    if (tag is null)
		    {
			    throw new NullReferenceException(nameof(tag));
		    }
			    
		    var pageTagPage =
			    new PageSettingTagPageSetting(pageSettingId, tag.Id);
	        
		    newTags.Add(pageTagPage);
	    }

	    await AddRangeAsync(newTags);
    }
}