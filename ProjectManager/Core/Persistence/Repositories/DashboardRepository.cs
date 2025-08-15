using Domain;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class DashboardRepository : Repository<Dashboard>, IDashboardRepository
{
	internal DashboardRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
}