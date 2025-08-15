using Domain;
using Persistence.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class AttachmentRepository : Repository<Attachment>, IAttachmentRepository
{
	internal AttachmentRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}

	public async Task<List<Attachment>> GetAllAttachmentsByShopId
		(string id, string subSystemName, CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			.Include(current => current.SubSystemLocal)
			.Include(current => current.AttachmentSubject)
			.Where(current => current.SubSystemLocal.NameEN.Trim().ToLower().Equals(subSystemName.Trim().ToLower()))
			.Where(p => p.RelationId == id)
			.ToListAsync(cancellationToken);

		return result;
	}

	public async Task<List<Attachment>> GetAllAttachmentsByIdsAndSubSystemName
		(List<string> ids, string subSystemName, CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			.Include(current => current.SubSystemLocal)
			.Include(current => current.AttachmentSubject)
			.Where(current => current.IsDeleted == false)
			.Where(current => current.IsActive == true)
			.Where(current => current.SubSystemLocal.NameEN.Trim().ToLower().Equals(subSystemName.Trim().ToLower()))
			.Where(p => ids.Contains(p.RelationId))
			.ToListAsync(cancellationToken);

		return result;
	}

	/// <summary>
	/// find all attachments with subject by sub system local id and relation id for other tables
	/// </summary>
	/// <param name="subSystemId">subSystemLocalId</param>
	/// <param name="relationId">EntityId</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>attachments list</returns>
	public async Task<List<Attachment>> FindBySubSystemNameAndRelationIdAsync(
		string subSystemId,
		 string relationId,
		  CancellationToken cancellationToken = default)
	{
		var result = await DbSet

			.Include(current => current.AttachmentSubject)

			.Where(current => current.IsDeleted == false)
			.Where(current => current.SubSystemLocalId == subSystemId)
			.Where(current => current.RelationId == relationId)

			.ToListAsync(cancellationToken);

		return result;
	}

	// /// <summary>
	// /// Configures products by identifying and processing specific attachment subjects
	// /// associated with product profiles.
	// /// </summary>
	// /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	// /// <returns>A task that represents the asynchronous operation.</returns>
	// /// <exception cref="ArgumentNullException">
	// /// Thrown if required subsystems or attachment subjects are not found.
	// /// </exception>
	// public async Task ConfigureProductsAsync(CancellationToken cancellationToken = default)
	// {
	// 	ISubSystemLocalRepository subSystemRepository =
	// 		new SubSystemLocalRepository(DatabaseContext);
	//
	// 	var subSystem = await subSystemRepository
	// 		.FindByNameAsync(nameof(Product), cancellationToken);
	//
	// 	if (subSystem == null)
	// 	{
	// 		throw new ArgumentNullException(nameof(subSystem));
	// 	}
	//
	// 	IAttachmentSubjectRepository
	// 		attachmentSubjectRepository =
	// 			new AttachmentSubjectRepository(DatabaseContext);
	//
	// 	var subjectProfileProduct =
	// 		await attachmentSubjectRepository
	// 			.FindByAttachmentSubjectEnumAsync(
	// 				AttachmentSubjectEnum.ProductProfile, cancellationToken);
	//
	// 	if (subjectProfileProduct == null)
	// 	{
	// 		throw new ArgumentNullException(nameof(subjectProfileProduct));
	// 	}
	//
	// 	var subjectOtherImages =
	// 		await attachmentSubjectRepository
	// 			.FindByAttachmentSubjectEnumAsync(
	// 				AttachmentSubjectEnum.ProductListImage, cancellationToken);
	//
	// 	if (subjectOtherImages == null)
	// 	{
	// 		throw new ArgumentNullException(nameof(subjectOtherImages));
	// 	}
	//
	// 	var result = await DbSet
	// 		.Include(current => current.AttachmentSubject)
	// 		.Where(p => p.IsDeleted == false)
	// 		.Where(current => current.SubSystemLocalId == subSystem.Id)
	// 		.Where(current => current.AttachmentSubjectId == subjectProfileProduct.Id)
	// 		.GroupBy(current => current.RelationId)
	// 		.ToListAsync(cancellationToken);
	//
	// 	var groupWithCountBigThen1 = result.Where(current => current.Count() > 1).ToList();
	//
	// 	if (groupWithCountBigThen1.Count != 0 == true)
	// 	{
	// 		foreach (var group in groupWithCountBigThen1)
	// 		{
	// 			var isFirst = true;
	//
	// 			foreach (var item in group)
	// 			{
	// 				if (isFirst == true)
	// 				{
	// 					isFirst = false;
	// 					continue;
	// 				}
	//
	// 				item.AttachmentSubjectId = subjectOtherImages.Id;
	// 			}
	// 		}
	//
	// 		await DatabaseContext.SaveChangesAsync(cancellationToken);
	// 	}
	// }
}