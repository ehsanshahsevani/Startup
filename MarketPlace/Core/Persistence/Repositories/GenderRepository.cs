using Domain;
using Persistence.Abstracts;

namespace Persistence.Repositories;

public class GenderRepository : Repository<Gender>, IGenderRepository
{
	internal GenderRepository(DatabaseContext databaseContext) : base(databaseContext)
	{
	}
}