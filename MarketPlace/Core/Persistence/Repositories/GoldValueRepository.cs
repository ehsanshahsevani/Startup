using Domain;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class GoldValueRepository : Repository<GoldValue>, IGoldValueRepository
{
	internal GoldValueRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
}