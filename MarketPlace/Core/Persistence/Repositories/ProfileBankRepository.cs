using Domain;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class ProfileBankRepository : Repository<ProfileBank>, IProfileBankRepository
{
	internal ProfileBankRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
}