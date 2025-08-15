using BaseProject.Model.ViewModel.Public;
using Domain;
using Enums.Marketplace;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface IAttachmentSubjectRepository : IRepository<AttachmentSubject>
{
	/// <summary>
	/// Find By AttachmentSubjectEnum
	/// </summary>
	/// <param name="attachmentSubjectEnum"></param>
	/// <param name="cancellationToken"></param>
	/// <returns>AttachmentSubject?</returns>
	Task<AttachmentSubject> FindByAttachmentSubjectEnumAsync(
		AttachmentSubjectEnum attachmentSubjectEnum,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// نمایش DropDown های جدول
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>  مدلی از نام فارسی | انگلیسی و شناسه جدول  </returns>
	Task<List<UiSelectModel>> GetSelectValues(CancellationToken cancellationToken = default);

	/// <summary>
	/// Set Display Name Attribute From Enum In the database to CodeDisplay  
	/// </summary>
	/// <param name="cancellationToken"></param>
	Task ConfigureCodeDisplayAsync(CancellationToken cancellationToken = default);
}