using Domain;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class DocumentDetailRepository : Repository<DocumentDetail>, IDocumentDetailRepository
{
	internal DocumentDetailRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
}