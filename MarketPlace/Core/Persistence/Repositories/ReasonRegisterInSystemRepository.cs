using Domain;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class ReasonRegisterInSystemRepository : Repository<ReasonRegisterInSystem>, IReasonRegisterInSystemRepository
{
	internal ReasonRegisterInSystemRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
}