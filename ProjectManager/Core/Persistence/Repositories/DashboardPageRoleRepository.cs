using Domain;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class DashboardPageRoleRepository : Repository<DashboardPageRole>, IDashboardPageRoleRepository
{
	internal DashboardPageRoleRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
}