using Domain;
using PersistenceSeedworks;

namespace Persistence.Abstracts;

public interface IDocumentRepository : IRepository<Document>
{
	Task<List<Document>> AddByAccountCodingsAsync(
		List<AccountCoding> accountCodings,
		Document document,
		CancellationToken cancellationToken = default);
}