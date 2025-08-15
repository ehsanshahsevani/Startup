using Domain;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class GoldRequestStatusRepository : Repository<GoldRequestStatus>, IGoldRequestStatusRepository
{
	internal GoldRequestStatusRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
}