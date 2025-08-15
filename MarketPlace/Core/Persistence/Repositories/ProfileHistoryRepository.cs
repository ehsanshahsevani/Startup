using Domain;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class ProfileHistoryRepository : Repository<ProfileHistory>, IProfileHistoryRepository
{
	internal ProfileHistoryRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
}