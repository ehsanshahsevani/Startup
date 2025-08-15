using Domain;
using SampleResult;
using Persistence.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class TagPageSettingRepository : Repository<TagPageSetting>, ITagPageSettingRepository
{
	internal TagPageSettingRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}

	/// <summary>
	/// Retrieves all active and non-deleted TagPageSetting entities from the repository asynchronously.
	/// </summary>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>An enumerable collection of active and non-deleted TagPageSetting entities.</returns>
	public override async Task<IEnumerable<TagPageSetting?>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		var result = await DbSet

			.Where(x => x.IsDeleted == false)
			.Where(x => x.IsActive == true)

			.ToListAsync(cancellationToken);
		
		return result;
	}

	/// <summary>
	/// Retrieves the TagPageSetting entity with the specified name from the repository asynchronously.
	/// </summary>
	/// <param name="name">The name of the TagPageSetting entity to be retrieved.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>The TagPageSetting entity with the specified name, or null if no such entity exists.</returns>
	public async Task<TagPageSetting?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			
			.Where(x => x.IsDeleted == false)
			.Where(x => x.IsActive == true)
			
			.Where(current => current.NameEn == name || current.NameFa == name)
			
			.FirstOrDefaultAsync(cancellationToken);
		
		return result;
	}

	/// <summary>
	/// Adds a new TagPageSetting entity to the repository asynchronously.
	/// </summary>
	/// <param name="entity">The TagPageSetting entity to be added. Cannot be null.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
	/// <exception cref="ArgumentNullException">Thrown when the provided entity is null.</exception>
	public override async Task<Result> AddAsync(TagPageSetting? entity, CancellationToken cancellationToken = default)
	{
		var result = new FluentResults.Result();

		if (entity is null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		var exists = await DbSet
			.Where(current=> current.IsDeleted == false)
			.Where(current => current.NameEn == entity.NameEn || current.NameFa == entity.NameFa)
			.AnyAsync(cancellationToken);

		if (exists == true)
		{
			var fieldName = $"{nameof(entity.NameEn)} یا {nameof(entity.NameEn)}";
			
			var errorMessage = string.Format(Resources.Messages.RepeatError, fieldName);
			
			result.WithError(errorMessage);
			
			return result.ConvertToSampleResult();
		}

		return await base.AddAsync(entity, cancellationToken);
	}

	/// <summary>
	/// find all tags by name and get when the name-en start with name
	/// </summary>
	/// <param name="name">Banner -> BannerHomePageTopLeft, BannerHomePageBottom</param>
	/// <param name="cancellationToken"></param>
	/// <returns>TagPageSetting List</returns>
	public async Task<List<TagPageSetting>> FindByStartWithNamesAsync(
		string name, CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			.Where(x => x.IsDeleted == false)
			.Where(x => x.IsActive == true)
			
			.Where(current => current.NameEn.ToLower().StartsWith(name.ToLower()))
			
			.ToListAsync(cancellationToken);

		return result;
	}
}