using Domain;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class GoldRequestRepository : Repository<GoldRequest>, IGoldRequestRepository
{
	internal GoldRequestRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
}