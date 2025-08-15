using Domain;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class ProfileRepository : Repository<Profile>, IProfileRepository
{
	internal ProfileRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
}