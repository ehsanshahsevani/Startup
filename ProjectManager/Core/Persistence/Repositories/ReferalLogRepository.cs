using Domain;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class ReferalLogRepository : Repository<ReferalLog>, IReferalLogRepository
{
	internal ReferalLogRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
}