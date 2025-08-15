using Domain;
using Utilities;
using Enums.Marketplace;
using Persistence.Abstracts;
using Microsoft.EntityFrameworkCore;
using BaseProject.Model.ViewModel.Public;

namespace Persistence.Repositories;

public class AttachmentSubjectRepository : Repository<AttachmentSubject>, IAttachmentSubjectRepository
{
	internal AttachmentSubjectRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}

	/// <summary>
	/// نمایش DropDown های جدول
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>  مدلی از نام فارسی | انگلیسی و شناسه جدول  </returns>
	public async Task<List<UiSelectModel>> GetSelectValues(CancellationToken cancellationToken = default)
	{
		var result = await DbSet
			.Where(p => p.IsActive == true)
			.Where(p => p.IsDeleted == false)
			.Select(p => new UiSelectModel(p.DisplayName, p.Id))
			.ToListAsync(cancellationToken);

		return result;
	}

	/// <summary>
	/// Find By AttachmentSubjectEnum
	/// </summary>
	/// <param name="attachmentSubjectEnum"></param>
	/// <param name="cancellationToken"></param>
	/// <returns>AttachmentSubject?</returns>
	public async Task<AttachmentSubject> FindByAttachmentSubjectEnumAsync(
		AttachmentSubjectEnum attachmentSubjectEnum,
		CancellationToken cancellationToken = default)
	{
		var codeDisplay =
			attachmentSubjectEnum.GetEnumDisplayName();

		var result = await DatabaseContext.AttachmentSubjects
			.Where(current => current.IsDeleted == false)
			.Where(current => current.Code == attachmentSubjectEnum.ToString())
			.FirstOrDefaultAsync(cancellationToken);

		if (result is not null)
		{
			if (codeDisplay == result.CodeDisplay)
			{
				return result;
			}

			result.CodeDisplay = codeDisplay;
			await DatabaseContext.SaveChangesAsync(cancellationToken);

			return result;
		}

		result = new AttachmentSubject
		{
			IsActive = true,
			IsDeleted = false,
			Ordering = 100_000,
			CodeDisplay = codeDisplay,
			Code = attachmentSubjectEnum.ToString(),
		};

		await AddAsync(result, cancellationToken);

		await DatabaseContext.SaveChangesAsync(cancellationToken);

		return result;
	}

	/// <summary>
	/// Set Display Name Attribute From Enum In the database to CodeDisplay  
	/// </summary>
	/// <param name="cancellationToken"></param>
	public async Task ConfigureCodeDisplayAsync(CancellationToken cancellationToken = default)
	{
		var list = await GetAllAsync(cancellationToken);

		foreach (var item in list)
		{
			if (item is null)
			{
				continue;
			}

			string? codeDisplay =
				Utilities.ResourcesHelper.GetValue(typeof(Resources.DataDictionary), item.Code);

			if (codeDisplay is null)
			{
				continue;
			}
			
			if (codeDisplay == item.CodeDisplay)
			{
				continue;
			}

			item.CodeDisplay = codeDisplay;
			await DatabaseContext.SaveChangesAsync(cancellationToken);
		}
	}
}